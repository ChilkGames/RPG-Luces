using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    private BattleStateMachine BSM;

    private BaseEnemy enemy;

    public enum TurnState
    {
        WAITING,
        CHOOSEACTION,
        ACTION,
        DEAD
    }
    public TurnState actualState;

    private Vector3 startPosition;
    private float timer;

    void Start()
    {
        enemy = GetComponent<BaseEnemy>();
        BSM = FindObjectOfType<BattleStateMachine>();
        startPosition = transform.position;

        actualState = TurnState.WAITING;
        timer = 5;
    }

    void Update()
    {
        switch (actualState)
        {
            case TurnState.WAITING:
                if (timer<=0)
                {
                    actualState = TurnState.CHOOSEACTION;
                    timer = 5;
                }
                else
                {
                    timer -= Time.deltaTime;
                }
                break;

            case TurnState.CHOOSEACTION:
                ChooseAction();
                actualState = TurnState.WAITING;
                break;

            case TurnState.ACTION:

                break;

            case TurnState.DEAD:

                break;
        }
    }

    void ChooseAction()
    {
        TurnHandler myAttack = new TurnHandler()
        {
            attacker = enemy.enemyName,
            attackerGameObject = gameObject,
            targets = BSM.heroesInBattle
        };
        BSM.CollectActions(myAttack);
    }
}
