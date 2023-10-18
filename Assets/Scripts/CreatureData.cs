using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Creature", menuName = "Creatures/MyCreature")]
public class CreatureData : ScriptableObject
{
    public string creatureName;
    public Sprite image;
    public Power power;
    public Star star;

    public Type type;

    public float speed;
    public float jumpHeight;
    public float attackRadius;

    public float health;  
    public float damage;
    public float crate;
    public float cdamage;
    public float defence;
    public float attackSpeed;

    public float healthUP;  
    public float damageUP;
    public float crateUP;
    public float cdamageUP;
    public float defenceUP;
}

public enum Weapon
{
    bow,
    sword,
    bigSword,
    polearm,
    catalyst,
}

public enum Star
{
    five,
    four,
    none
}

public enum Type
{
    character,
    enemy,
}

public enum Power
{
    none,
    air,
    water,
    earth,
    fire,
    electro,
    ice,
    grass,
    atomic,
}