using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public List<Creature> character = new List<Creature>();

    public int current;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            character[current].Hit("player hit", character[current].data.power, character[current]);
        Move();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            current = 0;
            gameObject.GetComponent<SpriteRenderer>().sprite = character[current].data.image;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            current = 1;
            gameObject.GetComponent<SpriteRenderer>().sprite = character[current].data.image;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            current = 2;
            gameObject.GetComponent<SpriteRenderer>().sprite = character[current].data.image;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            current = 3;
            gameObject.GetComponent<SpriteRenderer>().sprite = character[current].data.image;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            current = 4;
            gameObject.GetComponent<SpriteRenderer>().sprite = character[current].data.image;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            current = 5;
            gameObject.GetComponent<SpriteRenderer>().sprite = character[current].data.image;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            current = 6;
            gameObject.GetComponent<SpriteRenderer>().sprite = character[current].data.image;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            current = 7;
            gameObject.GetComponent<SpriteRenderer>().sprite = character[current].data.image;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            current = 8;
            gameObject.GetComponent<SpriteRenderer>().sprite = character[current].data.image;
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            current = 9;
            gameObject.GetComponent<SpriteRenderer>().sprite = character[current].data.image;
        }
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