using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitStats
{
    public enum Color
    {
        BLUE,
        RED,
        GREEN,
        YELLOW,
        PURPLE,
        LIGHT_BLUE,
        WHITE,
        BLACK
    }

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

    public Color actualColor;

    public bool isStrategist;
    
}
