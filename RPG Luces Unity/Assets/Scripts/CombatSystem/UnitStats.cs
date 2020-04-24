using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitStats
{
    public string myName;

    public int level;

    public float maxHealth;
    public float actualHealth;

    public float maxMana;
    public float actualMana;

    public float maxAttack;
    public float actualAttack;

    public float maxDefense;
    public float actualDefense;

    public float maxMagic;
    public float actualMagic;

    public float maxSpeed;
    public float actualSpeed;

    public bool isStrategist;

    public List<BaseAttack> meleeAttacks;

    public List<BaseAttack> magicAttacks;

    public ColorsEnum.Colors actualColor;

    public JobsEnum.Jobs actualJob;

    public int experience;
}
