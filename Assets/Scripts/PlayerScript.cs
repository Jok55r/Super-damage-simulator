using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using TMPro;
using UnityEditor;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public List<Creature> character = new List<Creature>();

    public int current;

    public Rigidbody2D rb;
    public GameObject sphere;
    public SpriteRenderer healthBar;
    public SpriteRenderer healthBar2;
    public TextMeshPro damageText;

    public void Start()
    {
        foreach (var creature in character)
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
        if (character[current].health <= 0)
        {
            character[current].data.aviable = false;
            for (int i = 0; i < character.Count; i++)
            {
                if (character[i].data.aviable)
                {
                    SwitchCharacter(i);
                    break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
            character[current].Hit("player hit", character[current].data.power, character[current]);
        Move();

        if (Input.GetKeyDown(KeyCode.Alpha1) && character[0].data.aviable)
            SwitchCharacter(0);
        if (Input.GetKeyDown(KeyCode.Alpha2) && character[1].data.aviable)
            SwitchCharacter(1);
        if (Input.GetKeyDown(KeyCode.Alpha3) && character[2].data.aviable)
            SwitchCharacter(2);
        if (Input.GetKeyDown(KeyCode.Alpha4) && character[3].data.aviable)
            SwitchCharacter(3);
        if (Input.GetKeyDown(KeyCode.Alpha5) && character[4].data.aviable)
            SwitchCharacter(4);
    }

    private void SwitchCharacter(int index)
    {
        current = index;
        gameObject.GetComponent<SpriteRenderer>().sprite = character[current].data.image;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy hit")
        {
            character[current].TakeDamage(collision.gameObject.GetComponent<HitRegScript>().damage, collision.gameObject.GetComponent<HitRegScript>().power, collision.gameObject.GetComponent<HitRegScript>().character);
        }
    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            character[current].Move(Input.GetAxisRaw("Horizontal") * character[current].data.speed, true);
        else
            character[current].Move(Input.GetAxisRaw("Horizontal") * character[current].data.speed, false);
    }
}