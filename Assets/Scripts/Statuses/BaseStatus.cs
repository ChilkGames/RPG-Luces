using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStatus : ScriptableObject
{
    public string StatusName { get; set; }
    public string StatusDescription { get; set; }
    public bool PercentualStat { get; set; }
    public int TurnDuration { get; set; }
    public int PointsToAffect { get; set; }
    public Parameters ParameterToAffect { get; set; }
    public bool IsBuff { get; set; }
}

public enum Parameters
{
    HP,
    MP,
    SPEED,
    DEFENSE,
    MAGIC,
    ATTACK,
    TURN,
    RED_RES,
    BLUE_RES,
    GREEN_RES,
    YELLOW_RES,
    PURPLE_RES,
    LIGHT_BLUE_RES,
    WHITE_RES,
    BLACK_RES
}