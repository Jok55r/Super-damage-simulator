using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitRegScript : MonoBehaviour
{
    public Creature character;
    public float damage;
    public Power power;
    public float disapearTime;

    private void Start()
    {
        Invoke("Destroy", disapearTime * GameManager.gameSpeed);
    }

    private void Destroy()
        => Destroy(gameObject);
}