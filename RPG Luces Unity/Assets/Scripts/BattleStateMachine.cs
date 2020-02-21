using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleStateMachine : MonoBehaviour
{
    public enum PerformAction
    {
        WAIT,
        TAKE_ACTION,
        PERFORM_ACTION
    }

    public PerformAction battleState;

    public enum HeroGUI
    {
        ACTIVATE,
        WAITING,
        SINGLE_ATTACK,
        MULTIPLE_ATTACK,
        DONE
    }

    public HeroGUI heroInput;

    private TurnHandler heroChoice;

    public List<GameObject> heroesToManage = new List<GameObject>(); // List of heroes to manage

    public List<TurnHandler> actionsInTurn = new List<TurnHandler>(); //List of actions in a turn

    public List<GameObject> heroesInBattle = new List<GameObject>(); //List of heroes in battle
    public List<GameObject> enemiesInBattle = new List<GameObject>(); //List of enemies in battle

    public List<GameObject> orderList = new List<GameObject>(); // List in wich we have the order of the battle.

    public GameObject centerOfTheBattleground;

    public GameObject enemyButton;

    public Transform spacer;

    public GameObject actionsPanel;
    public GameObject enemySelectPanel;
    public GameObject allEnemiesPanel;

    private void Start()
    {
        actionsPanel.SetActive(false);
        enemySelectPanel.SetActive(false);
        allEnemiesPanel.SetActive(false);

        battleState = PerformAction.WAIT;
        heroInput = HeroGUI.ACTIVATE;

        enemiesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        heroesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Hero"));

        //Assign the center of the battleground to all enemies and players
        foreach(GameObject enemy in enemiesInBattle)
        {
            EnemyStateMachine ESM = enemy.GetComponent<EnemyStateMachine>();
            ESM.centerOfTheBattleground = centerOfTheBattleground;
        }
        foreach (GameObject hero in heroesInBattle)
        {
            PlayerStateMachine PSM = hero.GetComponent<PlayerStateMachine>();
            PSM.centerOfTheBattleground = centerOfTheBattleground;
        }

        EnemyButtons();
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
                GameObject performer = actionsInTurn[0].attackerGameObject;
                if (actionsInTurn[0].type == "Enemy")
                {
                    EnemyStateMachine ESM = performer.GetComponent<EnemyStateMachine>();

                    if (actionsInTurn[0].attack.isAreaAttack)
                    {
                        for (int i = 0; i<heroesInBattle.Count; i++)
                        {
                            if (actionsInTurn[0].targets[0] == heroesInBattle[i])
                            {
                                ESM.heroesToAttack[0] = actionsInTurn[0].targets[0];
                                break;
                            }
                            else
                            {
                                actionsInTurn[0].targets[0] = heroesInBattle[Random.Range(0, heroesInBattle.Count)];
                                ESM.heroesToAttack[0] = actionsInTurn[0].targets[0];
                            }
                        }
                    }     
                    
                    ESM.actualState = EnemyStateMachine.TurnState.ACTION;
                }

                if (actionsInTurn[0].type == "Hero")
                {
                    PlayerStateMachine PSM = performer.GetComponent<PlayerStateMachine>();
                    PSM.actualState = PlayerStateMachine.TurnState.ACTION;
                }
                battleState = PerformAction.PERFORM_ACTION;

                break;

            case PerformAction.PERFORM_ACTION:

                break;
        }

        switch (heroInput)
        {
            case HeroGUI.ACTIVATE:
                if (heroesToManage.Count>0)
                {
                    heroesToManage[0].transform.Find("Selector").gameObject.SetActive(true);
                    heroChoice = new TurnHandler();

                    actionsPanel.SetActive(true);
                    heroInput = HeroGUI.WAITING;
                }
                break;

            case HeroGUI.WAITING:
                break;

            case HeroGUI.SINGLE_ATTACK:
                break;

            case HeroGUI.MULTIPLE_ATTACK:
                break;

            case HeroGUI.DONE:
                HeroInputDone();
                break;
        }
    }

    public void CollectActions(TurnHandler action)
    {
        actionsInTurn.Add(action);
    }

    /// <summary>
    /// Creates a button for every enemy in battle.
    /// </summary>
    public void EnemyButtons()
    {
        foreach(GameObject enemy in enemiesInBattle)
        {
            GameObject newButton = Instantiate(enemyButton) as GameObject;
            EnemySelectButton button = newButton.GetComponent<EnemySelectButton>();

            EnemyStateMachine curr_ESM = enemy.GetComponent<EnemyStateMachine>();
            Text buttonText = newButton.GetComponentInChildren<Text>();
            buttonText.text = enemy.GetComponent<BaseEnemy>().stats.myName;

            button.enemyPrefab = enemy;

            newButton.transform.SetParent(spacer);
        }
    }

    /// <summary>
    /// Input from the panel
    /// </summary>
    public void ActionInput()
    {
        heroChoice.attacker = heroesToManage[0].GetComponent<BaseHero>().stats.myName;
        heroChoice.attackerGameObject = heroesToManage[0];
        heroChoice.type = "Hero";

        actionsPanel.SetActive(false);
        enemySelectPanel.SetActive(true);
    }

    /// <summary>
    /// Selects a simple enemy
    /// </summary>
    /// <param name="enemyToAttack">enemy to attack</param>
    public void UniqueEnemySelectionInput(GameObject enemyToAttack)
    {
        heroChoice.targets = new List<GameObject>
        {
            enemyToAttack
        };
        heroesToManage[0].GetComponent<PlayerStateMachine>().enemyToAttack = enemyToAttack;
        heroesToManage[0].GetComponent<PlayerStateMachine>().isAreaAttack = false;
        heroInput = HeroGUI.DONE;
    }

    /// <summary>
    /// Selects all enemies
    /// </summary>
    public void AllEnemiesSelectionInput()
    {
        heroChoice.targets = enemiesInBattle;
        heroesToManage[0].GetComponent<PlayerStateMachine>().enemiesToAttack = enemiesInBattle;
        heroesToManage[0].GetComponent<PlayerStateMachine>().isAreaAttack = true;
        heroInput = HeroGUI.DONE;
    }


    void HeroInputDone()
    {
        actionsInTurn.Add(heroChoice);
        enemySelectPanel.SetActive(false);
        heroesToManage[0].transform.Find("Selector").gameObject.SetActive(false);
        heroesToManage.RemoveAt(0);
        heroInput = HeroGUI.ACTIVATE;
    }

    private void OrderTurns()
    {

    }
}
