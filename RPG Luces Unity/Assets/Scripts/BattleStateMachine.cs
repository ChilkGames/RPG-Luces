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

    public GameObject centerOfTheBattleground;

    private void Start()
    {
        battleState = PerformAction.WAIT;
        enemiesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        heroesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Hero"));

        //Assign the center of the battleground to all enemies
        foreach(GameObject enemy in enemiesInBattle)
        {
            EnemyStateMachine ESM = enemy.GetComponent<EnemyStateMachine>();
            ESM.centerOfTheBattleground = centerOfTheBattleground;
        }
    }

    void Update()
    {
        switch(battleState)
        {
            case PerformAction.WAIT:
                if (actionsInTurn.Count>0)
                {
                    battleState = PerformAction.PERFORM_ACTION;
                }
                break;

            case PerformAction.TAKE_ACTION:
                GameObject performer = actionsInTurn[0].attackerGameObject;
                if (actionsInTurn[0].type == "Enemy")
                {
                    EnemyStateMachine ESM = performer.GetComponent<EnemyStateMachine>();
                    ESM.actualState = EnemyStateMachine.TurnState.ACTION;
                }

                if (actionsInTurn[0].type == "Hero")
                {

                }
                battleState = PerformAction.PERFORM_ACTION;

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
