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

    /// <summary>
    /// Level requirement to unlock this attack
    /// </summary>
    public int levelRequirement;

    /// <summary>
    /// List of jobs that can use this attack.
    /// </summary>
    public JobsEnum jobsRequirement;

    /// <summary>
    /// List of colors used
    /// </summary>
    public Dictionary<string, ColorsEnum.Colors> listOfColors;

    /// <summary>
    /// Percentage of the color´s damage.
    /// </summary>
    public Dictionary<string, float> percentageOfColor;

    protected void AddColor(string name, ColorsEnum.Colors colors, float percentage)
    {
        listOfColors.Add(name, colors);
        percentageOfColor.Add(name, percentage);
    }
}


