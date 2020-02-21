using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseAttack : MonoBehaviour
{
    public string attackName;

    public string attackDescription;

    /// <summary>
    /// Base damage of the attack. A formula for the attack may be totalDamage = baseDamage + attack + level
    /// </summary>
    public float baseDamage;

    public float manaCost;

    /// <summary>
    /// Defines if the attack is an Area Attack
    /// </summary>
    public bool isAreaAttack;
}
