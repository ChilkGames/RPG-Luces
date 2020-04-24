using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    private BattleStateMachine BSM;

    private BaseEnemy enemy;

    public GameObject selector;

    public enum TurnState
    {
        PROCESSING,
        CHOOSE_ACTION,
        WAITING,
        ACTION,
        DEAD
    }
    public TurnState actualState;

    public List<GameObject> heroesToAttack;
    private GameObject heroToAttack;

    private bool actionStarted;

    private Vector3 startPosition;

    public BaseAttack choosenAttack;

    public GameObject centerOfTheBattleground;
    private bool isAreaAttack;

    private float timer;

    private bool isAlive = true;

    [Range(0.0f, 10.0f)]
    public float animSpeed;

    void Start()
    {
        enemy = GetComponent<BaseEnemy>();
        BSM = FindObjectOfType<BattleStateMachine>();
        startPosition = transform.position;

        actualState = TurnState.PROCESSING;

        timer = 5;
        selector = transform.Find("Selector").gameObject;
        selector.SetActive(false);
    }

    void Update()
    {
        switch (actualState)
        {
            case TurnState.PROCESSING:
                if (timer<=0)
                {
                    actualState = TurnState.CHOOSE_ACTION;
                }
                else
                {
                    timer -= Time.deltaTime;
                }
                break;

            case TurnState.CHOOSE_ACTION:
                ChooseAction();
                actualState = TurnState.WAITING;
                break;

            case TurnState.WAITING:
                break;

            case TurnState.ACTION:
                StartCoroutine(PerformAction());
                break;

            case TurnState.DEAD:
                if (!isAlive)
                {
                    return;
                }
                else
                {
                    Die();
                }
                break;
        }
    }

    /// <summary>
    /// Chose an action. This communicates to the specific IA of the enemy to decide the attack according to the battle.
    /// </summary>
    void ChooseAction()
    {
        //Test function to decide if the attack is gonna be an area attack or not. This must be replaced with a call to the enemy IA, returning a list with the enemies to attack.


        TurnHandler myAttack = new TurnHandler()
        {
            attacker = enemy.stats.myName,
            type = "Enemy",
            attackerGameObject = gameObject,
            attack = IA(),
            targets = heroesToAttack
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

        DoDamage();

        Vector3 initialPosition = startPosition;

        while (MoveToStart(startPosition)) { yield return null; }

        BSM.actionsInTurn.RemoveAt(0);

        BSM.battleState = BattleStateMachine.PerformAction.WAIT;

        actionStarted = false;
        timer = 5; //reset timer
        actualState = TurnState.PROCESSING;
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

    private void DoDamage()
    {
        foreach (GameObject actualHero in heroesToAttack)
        {
            float damage = CalculateDamage();
            actualHero.GetComponent<PlayerStateMachine>().TakeDamage(damage);
        }
    }

    private float CalculateDamage()
    {
        return (enemy.stats.actualAttack + BSM.actionsInTurn[0].attack.baseDamage);
    }

    public void TakeDamage(float amount)
    {
        enemy.stats.actualHealth -= amount;
        if (enemy.stats.actualHealth<=0)
        {
            enemy.stats.actualHealth = 0;
            actualState = TurnState.DEAD;
        }
    }

    public void Die()
    {
        gameObject.tag = "DeadEnemy";
        BSM.enemiesInBattle.Remove(gameObject);
        selector.SetActive(false);

        if (BSM.enemiesInBattle.Count<0)
        {
            for (int i = 0; i < BSM.actionsInTurn.Count; i++)
            {
                if (BSM.actionsInTurn[i].attackerGameObject == this)
                {
                    BSM.actionsInTurn.Remove(BSM.actionsInTurn[i]);
                }

                foreach (GameObject target in BSM.actionsInTurn[i].targets)
                {
                    if (target == gameObject)
                    {
                        BSM.actionsInTurn[i].targets.Remove(gameObject);
                        if (BSM.actionsInTurn[i].targets.Count <= 0)
                        {
                            BSM.actionsInTurn[i].targets = new List<GameObject>
                        {
                            BSM.enemiesInBattle[Random.Range(0,BSM.enemiesInBattle.Count)]
                        };
                        }
                    }
                }
            }
        }
        
        gameObject.GetComponent<MeshRenderer>().material.color = Color.black;
        isAlive = false;
        BSM.EnemyButtons();
        BSM.battleState = BattleStateMachine.PerformAction.CHECK_ALIVE;
    }

    public BaseAttack IA()
    {
        float testIA = Random.Range(0, 2);
        if (testIA % 2 == 0)
        {
            heroesToAttack = BSM.heroesInBattle;
            return enemy.stats.meleeAttacks.Find(x => x.attackName.Equals("Circular Slash"));
        }
        else
        {
            heroToAttack = BSM.heroesInBattle[Random.Range(0, BSM.heroesInBattle.Count)];
            heroesToAttack = new List<GameObject>
            {
                heroToAttack
            };
            return enemy.stats.meleeAttacks.Find(x => x.attackName.Equals("Slash"));
        }

    }
}
