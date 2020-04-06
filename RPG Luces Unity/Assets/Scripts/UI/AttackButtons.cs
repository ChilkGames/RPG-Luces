using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButtons : MonoBehaviour
{
    public BaseAttack attackToPerform;

    public void PerformAttack()
    {
        GameObject.FindObjectOfType<BattleStateMachine>().AttackInput(attackToPerform);
    }
}