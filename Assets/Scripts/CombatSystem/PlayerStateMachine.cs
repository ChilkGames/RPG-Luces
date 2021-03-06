﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private BattleStateMachine BSM;

    public BaseHero hero;

    public GameObject selector;

    public enum TurnState
    {
        PROCESSING,
        ADD_TO_LIST,
        WAITING,
        ACTION,
        DEAD
    }

    public TurnState actualState;

    public List<GameObject> enemiesToAttack;
    public GameObject enemyToAttack;

    public GameObject centerOfTheBattleground;

    private Vector3 startPosition;

    private bool actionStarted;
    public bool isAreaAttack;

    [Range(0.0f, 10.0f)]
    public float animSpeed;

    private float timer;

    private bool isAlive = true;

    private HeroPanel heroPanelStats;
    public GameObject heroPanel;
    private Transform heroPanelSpacer;

    private const float damageDivisor = 100;

    // Start is called before the first frame update
    void Start()
    {
        hero = GetComponent<BaseHero>();
        heroPanelSpacer = GameObject.Find("LeftPanel").transform.Find("Spacer").GetComponent<Transform>();
        CreateHeroPanel();
        BSM = FindObjectOfType<BattleStateMachine>();
        actualState = TurnState.PROCESSING;
        startPosition = transform.position;
        selector = transform.Find("Selector").gameObject;
        selector.SetActive(false);
        timer = Random.Range(3, 5);
    }

    // Update is called once per frame
    void Update()
    {
        switch (actualState)
        {
            case TurnState.PROCESSING:
                CheckTurn();
                break;

            case TurnState.ADD_TO_LIST:
                BSM.heroesToManage.Add(this.gameObject);
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
    /// This function should receive the list with turns from the BSM at the beginning of each turn. If the player is the one to select an action, change of state
    /// </summary>
    void CheckTurn()
    {
        if (timer <=0)
        {
            timer = 5;
            actualState = TurnState.ADD_TO_LIST;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    /// <summary>
    /// Executes the selected action.
    /// </summary>
    private IEnumerator PerformAction()
    {
        if (actionStarted)
        {
            yield break;
        }

        actionStarted = true;
        Vector3 targetPosition = new Vector3();
        Vector3 centerPosition = new Vector3();

        if (!isAreaAttack)
            targetPosition = new Vector3(enemyToAttack.transform.position.x, startPosition.y, enemyToAttack.transform.position.z);
        else
            centerPosition = new Vector3(centerOfTheBattleground.transform.position.x, startPosition.y, centerOfTheBattleground.transform.position.z);

        if (isAreaAttack)
        {
            while (MoveToCenter(centerPosition)) { yield return null; }
        }
        else
        {
            while (MoveTowardsTarget(targetPosition)) { yield return null; }
        }

        //attack animation
        yield return new WaitForSeconds(1);

        //do damage
        DoDamage();

        Vector3 initialPosition = startPosition;

        while (MoveToStart(startPosition)) { yield return null; }

        BSM.actionsInTurn.RemoveAt(0);

        if (BSM.battleState != BattleStateMachine.PerformAction.WIN && BSM.battleState != BattleStateMachine.PerformAction.LOSE)
        {
            BSM.battleState = BattleStateMachine.PerformAction.WAIT;
            actualState = TurnState.PROCESSING;
        }
        else
        {
            actualState = TurnState.WAITING;
        }

        

        actionStarted = false;

    }

    /// <summary>
    /// Moves the enemy to a target
    /// </summary>
    /// <param name="target">Target we want to move to</param>
    /// <returns>Returns true if the enemy reaches the target</returns>
    private bool MoveTowardsTarget(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
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

    /// <summary>
    /// The character receives an ammount of damage
    /// </summary>
    /// <param name="damageAmmount">Ammount of damage received</param>
    public void TakeDamage(float damageAmmount)
    {
        hero.stats.actualHealth -= damageAmmount;        
        if (hero.stats.actualHealth<=0)
        {
            hero.stats.actualHealth = 0;
            actualState = TurnState.DEAD;
        }
        UpdateHeroPanel();
    }


    public void DoDamage()
    {
        float damage;
        if (BSM.actionsInTurn[0].attack.isMeleeAttack)
        {
            damage = (BSM.actionsInTurn[0].attack.baseDamage / damageDivisor /*+ weaponDamage*/) * hero.stats.actualAttack;
            Debug.Log("melee attack damage = " + damage);
        }
        else
        {
            damage = (BSM.actionsInTurn[0].attack.baseDamage / damageDivisor /*+ weaponDamage*/) * hero.stats.actualMagic;
            Debug.Log("magic attack damage = " + damage);
        }
        
        foreach (GameObject actualEnemy in enemiesToAttack)
        {
            actualEnemy.GetComponent<EnemyStateMachine>().TakeDamage(damage);
        }
    }

    private void CreateHeroPanel()
    {
        heroPanel = Instantiate(heroPanel) as GameObject;
        heroPanelStats = heroPanel.GetComponent<HeroPanel>();
        heroPanelStats.heroName.text = hero.stats.myName;
        heroPanelStats.heroHp.text = "HP: " + hero.stats.actualHealth + "/" + hero.stats.maxHealth;
        heroPanelStats.heroMp.text = "MP: " + hero.stats.actualMana + "/" + hero.stats.maxMana;

        heroPanel.transform.SetParent(heroPanelSpacer, false);
    }

    private void UpdateHeroPanel()
    {
        heroPanelStats.heroHp.text = "HP: " + hero.stats.actualHealth + "/" + hero.stats.maxHealth;
        heroPanelStats.heroMp.text = "MP: " + hero.stats.actualMana + "/" + hero.stats.maxMana; 
    }

    private void Die()
    {
        gameObject.tag = "DeadHero";
        BSM.DestroyButtons();
        BSM.heroesInBattle.Remove(gameObject);
        BSM.heroesToManage.Remove(gameObject);
        selector.SetActive(false);
        BSM.actionsPanel.SetActive(false);
        BSM.enemySelectPanel.SetActive(false);

        if (BSM.heroesInBattle.Count>0)
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
                            BSM.heroesInBattle[Random.Range(0,BSM.heroesInBattle.Count)]
                        };
                        }
                    }
                }
            }
        }
        gameObject.GetComponent<MeshRenderer>().material.color = Color.black;
        isAlive = false;
        BSM.battleState = BattleStateMachine.PerformAction.CHECK_ALIVE;
    }
}
