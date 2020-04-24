using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseAttack : ScriptableObject
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

    /// <summary>
    /// Level requirement to unlock this attack
    /// </summary>
    public int levelRequirement;

    /// <summary>
    /// List of jobs that can use this attack.
    /// </summary>
    public JobsEnum.Jobs jobsRequirement;

    /// <summary>
    /// List of colors used
    /// </summary>
    public List<ColorsEnum.Colors> listOfColors = new List<ColorsEnum.Colors>();

    /// <summary>
    /// If true, the attack is melee attack. If not, it´s magic attack.
    /// </summary>
    public bool isMeleeAttack;

    /// <summary>
    /// Determines if the attack is a buff attack
    /// </summary>
    public bool isBuff;

    public bool isCombination;

    /// <summary>
    /// Percentage of the color´s damage.
    /// </summary>
    public List<float> percentageOfColor = new List<float>();

    public void AddColor(ColorsEnum.Colors colors, float percentage)
    {
        listOfColors.Add(colors);
        percentageOfColor.Add(percentage);
    }
}


