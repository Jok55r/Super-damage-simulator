using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Creature character;
    public GameObject target;

    private void Update()
    {
        if (target == null)
            return;

        character.Move(target.transform.position.x - gameObject.transform.position.x * character.data.speed, false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "player hit")
        {
            character.TakeDamage(collision.gameObject.GetComponent<HitRegScript>().damage, collision.gameObject.GetComponent<HitRegScript>().power, collision.gameObject.GetComponent<HitRegScript>().character);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            character.Hit("enemy hit", character.data.power, character);
        }
    }
}