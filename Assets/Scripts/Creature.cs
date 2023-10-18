using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Creature : MonoBehaviour
{
    public CreatureData data;

    public Power curPower;

    public int level;
    public int MoneyPerLevel;
    public int levelXP;

    public float maxhealth;
    public float damage;
    public float crate;
    public float cdamage;
    public float defence;
    public float attackSpeed;

    public float health;
    public float curdamage;
    public float curcrate;
    public float curcdamage;
    public float curdefence;
    public float curattackSpeed;

    public Rigidbody2D rb;
    public GameObject sphere;
    public SpriteRenderer healthBar;
    public SpriteRenderer healthBar2;
    public TextMeshPro damageText;


    private float oldSpeed;
    private bool burning = false;
    private float electroGrassBonusTake = 1;
    private bool haveShield = false;
    private float shieldBonus = 0;


    private void Start()
    {
        level = 1;
        MoneyPerLevel = 100;
        levelXP = 1;

        gameObject.GetComponent<SpriteRenderer>().sprite = data.image;
        maxhealth = data.health;
        damage = data.damage;
        crate = data.crate;
        cdamage = data.cdamage;
        defence = data.defence;
        attackSpeed = data.attackSpeed;
        UpdateStats();
    }

    void UpdateStats()
    {
        health = maxhealth;
        curdamage = damage;
        curcrate = crate;
        curcdamage = cdamage;
        curdefence = defence;
        curattackSpeed = attackSpeed;
    }

    private void Update()
    {
        if (data.type == Type.enemy && health <= 0)
            Destroy(gameObject);
        UpdateHealthBar();
    }

    public void Move(float direction, bool jump)
    {
        direction = Mathf.Clamp(direction, -data.speed, data.speed);
        direction = Mathf.Approximately(direction, -data.speed) ? -data.speed : Mathf.Approximately(direction, data.speed) ? data.speed : 0;
        transform.position += new Vector3(direction * Time.deltaTime * GameManager.gameSpeed, 0, 0);

        if (jump)
            rb.AddForce(Vector2.up * data.jumpHeight * GameManager.gameSpeed);
    }

    public void Hit(string tag, Power hitPower, Creature character)
    {
        if (GameManager.gameSpeed == 0)
            return;

        GameObject hit = Instantiate(sphere, gameObject.transform.position, Quaternion.identity);
        hit.gameObject.GetComponent<SpriteRenderer>().color = powerCol(hitPower);

        Color newCol = hit.gameObject.GetComponent<SpriteRenderer>().color;
        newCol.a = 0.2f;
        hit.gameObject.GetComponent<SpriteRenderer>().color = newCol;

        hit.transform.localScale = Vector3.one * data.attackRadius;
        hit.GetComponent<HitRegScript>().damage = SenderDamage();
        hit.GetComponent<HitRegScript>().power = hitPower;
        hit.GetComponent<HitRegScript>().character = character;
        hit.tag = tag;
    }

    public void TakeDamage(float hitDamage, Power hitPower, Creature initiator)
    {
        if (hitPower != curPower)
            hitDamage = Reaction(curPower, hitPower, hitDamage, initiator);

        Debug.Log($"{hitDamage} DMG raw");

        health -= RecieverDamage(hitDamage, initiator);

        Debug.Log($"{hitDamage} DMG calculated");

        curPower = hitPower;

        TextMeshPro tmp = Instantiate(damageText, new Vector2(transform.position.x, transform.position.y + 2), Quaternion.identity);
        tmp.text = ((int)hitDamage).ToString();
        tmp.color = powerCol(hitPower);
    }

    public void LevelUP()
    {
        level++;
        MoneyPerLevel += 200;
        levelXP++;


        maxhealth += data.healthUP;
        damage += data.damageUP;
        defence += data.defenceUP;
        crate += data.crateUP;
        cdamage += data.cdamageUP;
        UpdateStats();
    }

    public float SenderDamage()
    {
        float fullDamage = curdamage;

        if (UnityEngine.Random.Range(0f, 100f) < curcrate)
            fullDamage *= 1f + (curcdamage / 100f);

        return fullDamage;
    }

    public float RecieverDamage(float hitDamage, Creature initiator)
    {
        hitDamage *= electroGrassBonusTake;
        hitDamage -= curdefence - initiator.shieldBonus;
        return hitDamage < 0 ? 0 : hitDamage;
    }

    public float Reaction(Power p1, Power p2, float hitDamage, Creature initiator)
    {
        if (BothSidesPower(p1, Power.fire, p2, Power.water))
        {
            return hitDamage * 2.5f;
        }
        if (BothSidesPower(p1, Power.fire, p2, Power.ice))
        {
            return hitDamage * 3f;
        }
        if (BothSidesPower(p1, Power.fire, p2, Power.electro))
        {
            return hitDamage * 1.5f;
        }
        if (BothSidesPower(p1, Power.fire, p2, Power.grass))
        {
            if (!burning)
                StartCoroutine(Burning(hitDamage * 2f, 5, initiator));
            return hitDamage * 2f;
        }
        if (BothSidesPower(p1, Power.electro, p2, Power.ice))
        {
            return hitDamage * 2.5f;
        }
        if (BothSidesPower(p1, Power.electro, p2, Power.water))
        {
            return hitDamage * 2.5f;
        }
        if (BothSidesPower(p1, Power.electro, p2, Power.grass))
        {
            electroGrassBonusTake = 2.8f;
            Invoke("EndElectroGrassBonus", 5);
            return hitDamage;
        } //shield not working below
        if (p1 == Power.earth || p2 == Power.earth && !BothSidesPower(p1, Power.earth, p2, Power.grass))
        {
            SpawnShield(p1 == Power.earth ? p2 : p1, initiator);
            return hitDamage;
        }

        if (BothSidesPower(p1, Power.water, p2, Power.ice))
        {
            Freeze(3);
            return hitDamage;
        }

        return hitDamage;
    }

    #region  reactions

    IEnumerator Burning(float hitDamage, int ammount, Creature initiator)
    {
        burning = true;
        for (int i = 0; i < ammount * 2; i++)
        {
            yield return new WaitForSeconds(0.5f);
            TakeDamage(hitDamage / 2f, Power.fire, initiator);
        }
        burning = false;
    }

    void Freeze(int time)
    {
        oldSpeed = data.speed;
        data.speed = 0;
        gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
        Invoke("StopFreeze", time);
    }
    void StopFreeze()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        data.speed = oldSpeed;
    }

    void EndElectroGrassBonus()
        => electroGrassBonusTake = 1;

    void SpawnShield(Power power, Creature initiator)
    {
        float time = 5f;

        haveShield = true;
        shieldBonus = initiator.defence * 2;

        var s = Instantiate(sphere, initiator.gameObject.transform);

        s.GetComponent<HitRegScript>().disapearTime = time;
        s.transform.localScale = Vector3.one * 4;
        Color newCol = powerCol(power);
        newCol.a = 0.3f;
        s.GetComponent<SpriteRenderer>().color = newCol;

        Invoke("BreakShield", time);
    }
    void BreakShield()
    {
        shieldBonus = 0;
        haveShield = false;
    }

    #endregion reactions



    public bool BothSidesPower(Power a, Power a2, Power b, Power b2)
        => a == a2 && b == b2 || a == b2 && b == a2;

    private void UpdateHealthBar()
    {
        float offset = health / maxhealth;
        healthBar.transform.localPosition = new Vector2((-1 + offset) / 2, healthBar.transform.localPosition.y);
        healthBar.transform.localScale = new Vector2(offset, healthBar.transform.localScale.y);

        healthBar2.transform.localPosition = new Vector2(offset / 2, healthBar2.transform.localPosition.y);
        healthBar2.transform.localScale = new Vector2(1 - offset, healthBar2.transform.localScale.y);
    }

    private Color powerCol(Power power) =>
        power switch
        {
            Power.fire => Color.red,
            Power.water => Color.blue,
            Power.air => Color.white,
            Power.ice => Color.cyan,
            Power.electro => Color.magenta,
            Power.earth => Color.gray,
            Power.grass => Color.green,
            Power.none => Color.black,
            _ => Color.white
        };
}
