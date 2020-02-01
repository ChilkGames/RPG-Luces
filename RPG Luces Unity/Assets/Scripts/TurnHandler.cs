using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class TurnHandler
{
    public string attacker; //Name of the attacker
    public GameObject attackerGameObject;
    public List<GameObject> targets; //List of targets of the attack
}
