using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateMachine : MonoBehaviour
{
    public enum PerformAction
    {
        WAIT,
        TAKE_ACTION,
        PERFORM_ACTION
    }

    public PerformAction battleState;

    public List<TurnHandler> actionsInTurn = new List<TurnHandler>(); //List of actions in a turn

    public List<GameObject> heroesInBattle = new List<GameObject>(); //List of heroes in battle
    public List<GameObject> enemiesInBattle = new List<GameObject>(); //List of enemies in battle

    private void Start()
    {
        battleState = PerformAction.WAIT;
        enemiesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        heroesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Hero"));
    }

    void Update()
    {
        switch(battleState)
        {
            case PerformAction.WAIT:
                if (actionsInTurn.Count>0)
                {
                    battleState = PerformAction.TAKE_ACTION;
                }
                break;

            case PerformAction.TAKE_ACTION:

                break;

            case PerformAction.PERFORM_ACTION:

                break;
        }
    }

    public void CollectActions(TurnHandler action)
    {
        actionsInTurn.Add(action);
    }
}
