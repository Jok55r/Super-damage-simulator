using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using TMPro;
using UnityEditor;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public List<CreatureData> creatures = new List<CreatureData>();
    public List<Creature> characters = new List<Creature>();

    public int current;

    public Rigidbody2D rb;
    public GameObject sphere;
    public SpriteRenderer healthBar;
    public SpriteRenderer healthBar2;
    public TextMeshPro damageText;

    public void Start()
    {
        foreach (var creature in creatures)
        {
            Creature cr = gameObject.AddComponent<Creature>();
            cr.data = creature;
            characters.Add(cr);
        }
        foreach (var creature in characters)
        {
            creature.rb = rb;
            creature.sphere = sphere;
            creature.healthBar = healthBar;
            creature.healthBar2 = healthBar2;
            creature.damageText = damageText;
        }
    }

    public void Update()
    {
        if (characters[current].health <= 0)
        {
            characters[current].aviable = false;
            for (int i = 0; i < characters.Count; i++)
            {
                if (characters[i].aviable)
                {
                    SwitchCharacter(i);
                    break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
            characters[current].Hit("player hit", characters[current].data.power, characters[current]);
        Move();

        if (Input.GetKeyDown(KeyCode.Alpha1) && characters[0].aviable)
            SwitchCharacter(0);
        if (Input.GetKeyDown(KeyCode.Alpha2) && characters[1].aviable)
            SwitchCharacter(1);
        if (Input.GetKeyDown(KeyCode.Alpha3) && characters[2].aviable)
            SwitchCharacter(2);
        if (Input.GetKeyDown(KeyCode.Alpha4) && characters[3].aviable)
            SwitchCharacter(3);
        if (Input.GetKeyDown(KeyCode.Alpha5) && characters[4].aviable)
            SwitchCharacter(4);
    }

    private void SwitchCharacter(int index)
    {
        current = index;
        gameObject.GetComponent<SpriteRenderer>().sprite = characters[current].data.image;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy hit")
        {
            characters[current].TakeDamage(collision.gameObject.GetComponent<HitRegScript>().damage, collision.gameObject.GetComponent<HitRegScript>().power, collision.gameObject.GetComponent<HitRegScript>().character);
        }
    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            characters[current].Move(Input.GetAxisRaw("Horizontal") * characters[current].data.speed, true);
        else
            characters[current].Move(Input.GetAxisRaw("Horizontal") * characters[current].data.speed, false);
    }
}