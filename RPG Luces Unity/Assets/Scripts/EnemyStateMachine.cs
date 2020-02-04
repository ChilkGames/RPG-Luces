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

    public List<GameObject> playersToAttack;

    private bool actionStarted;

    private Vector3 startPosition;

    private GameObject heroToAttack;
    public GameObject centerOfTheBattleground;
    private bool isAreaAttack;

    private float timer;
    private float animSpeed;

    void Start()
    {
        enemy = GetComponent<BaseEnemy>();
        BSM = FindObjectOfType<BattleStateMachine>();
        startPosition = transform.position;

        actualState = TurnState.WAITING;

        timer = 5;
        animSpeed = 5;
    }

    void Update()
    {
        switch (actualState)
        {
            case TurnState.WAITING:
                if (timer<=0)
                {
                    actualState = TurnState.CHOOSEACTION;
                }
                else
                {
                    timer -= Time.deltaTime;
                }
                break;

            case TurnState.CHOOSEACTION:
                ChooseAction();
                actualState = TurnState.ACTION;
                break;

            case TurnState.ACTION:
                StartCoroutine(PerformAction());
                break;

            case TurnState.DEAD:

                break;
        }
    }

    /// <summary>
    /// Chose an action. This communicates to the specific IA of the enemy to decide the attack according to the battle.
    /// </summary>
    void ChooseAction()
    {
        //Test function to decide if the attack is gonna be an area attack or not. This must be replaced with a call to the enemy IA, returning a list with the enemies to attack.
        float testIA = Random.Range(0, 2);
        if (testIA%2==0)
        {
            playersToAttack = BSM.heroesInBattle;
        }
        else
        {
            heroToAttack = BSM.heroesInBattle[Random.Range(0, BSM.heroesInBattle.Count)];
            playersToAttack = new List<GameObject>
            {
                heroToAttack
            };
        }

        TurnHandler myAttack = new TurnHandler()
        {
            attacker = enemy.enemyName,
            type = "Enemy",
            attackerGameObject = gameObject,
            targets = playersToAttack
        };

        isAreaAttack = myAttack.targets.Count > 1 ? true : false;

        BSM.CollectActions(myAttack);
    }

    /// <summary>
    /// Executes the selected action.
    /// </summary>
    private IEnumerator PerformAction ()
    {
        if (actionStarted)
        {
            yield break;
        }

        actionStarted = true;
        Vector3 targetPosition = new Vector3();
        Vector3 centerPosition = new Vector3();

        if (!isAreaAttack)
            targetPosition = new Vector3 (heroToAttack.transform.position.x, startPosition.y, heroToAttack.transform.position.z);
        else
            centerPosition = new Vector3 (centerOfTheBattleground.transform.position.x,startPosition.y, centerOfTheBattleground.transform.position.z);

        if (isAreaAttack)
        {
            while (MoveToCenter(centerPosition)) {yield return null;}
        }
        else
        {
            while (MoveTowardsTarget(targetPosition)) {yield return null;}
        }

        //attack animation
        yield return new WaitForSeconds(1);

        //do damage

        Vector3 initialPosition = startPosition;

        while (MoveToStart(startPosition)) { yield return null; }

        BSM.actionsInTurn.RemoveAt(0);

        BSM.battleState = BattleStateMachine.PerformAction.WAIT;

        actionStarted = false;
        timer = 5; //reset timer
        actualState = TurnState.WAITING;
    }

    /// <summary>
    /// Moves the enemy to a target
    /// </summary>
    /// <param name="target">Target we want to move to</param>
    /// <returns>Returns true if the enemy reaches the target</returns>
    private bool MoveTowardsTarget(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position,target,animSpeed * Time.deltaTime));
    }

    /// <summary>
    /// Moves the enemy to his initial position
    /// </summary>
    /// <param name="start">Initial position of the enemy</param>
    /// <returns>Returns true if the enemy reaches his original position</returns>
    private bool MoveToStart(Vector3 start)
    {
        return start != (transform.position = Vector3.MoveTowards(transform.position, start, animSpeed * Time.deltaTime));
    }

    /// <summary>
    /// Moves the enemy to the center of the battleground
    /// </summary>
    /// <param name="center">Center of the battleground</param>
    /// <returns>Returns true if the enemy reaches the center of the battleground</returns>
    private bool MoveToCenter(Vector3 center)
    {
        return center != (transform.position = Vector3.MoveTowards(transform.position, center, animSpeed * Time.deltaTime));
    }
}
