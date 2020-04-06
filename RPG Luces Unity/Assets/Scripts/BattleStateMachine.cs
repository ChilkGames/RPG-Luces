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
    public GameObject actionButton;
    public GameObject attackButton;

    public GameObject actionsPanel;
    public GameObject attackPanel;
    public GameObject magicPanel;
    private GameObject activePanel;
    public GameObject enemySelectPanel;

    public Transform enemySpacer;
    public Transform attackSpacer;
    public Transform magicSpacer;
    public Transform actionSpacer;

    public List<GameObject> atkButtons = new List<GameObject>();

    private void Start()
    {
        actionsPanel.SetActive(false);
        enemySelectPanel.SetActive(false);
        activePanel = actionsPanel;

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

                    ChangePanels(actionsPanel);
                    CreateButtons();

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

            newButton.transform.SetParent(enemySpacer);
        }
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
        activePanel.SetActive(false);
        DestroyButtons();
        heroesToManage[0].transform.Find("Selector").gameObject.SetActive(false);
        heroesToManage.RemoveAt(0);
        heroInput = HeroGUI.ACTIVATE;
    }

    private void OrderTurns()
    {

    }

    private void CreateButtons()
    {
        GameObject attackButton = Instantiate(actionButton) as GameObject;
        attackButton.name = "AttackButton";
        Text attackButtonText = attackButton.transform.Find("Text").gameObject.GetComponent<Text>();
        attackButtonText.text = "Attack";
        attackButton.GetComponent<Button>().onClick.AddListener(() => OpenAttacksMenu());
        attackButton.transform.SetParent(actionSpacer,false);
        atkButtons.Add(attackButton);

        GameObject magicButton = Instantiate(actionButton) as GameObject;
        magicButton.name = "MagicButton";
        Text magicButtonText = magicButton.transform.Find("Text").gameObject.GetComponent<Text>();
        magicButtonText.text = "Magic";
        magicButton.GetComponent<Button>().onClick.AddListener(() => OpenMagicMenu());
        magicButton.transform.SetParent(actionSpacer, false);
        atkButtons.Add(magicButton);

        GameObject defendButton = Instantiate(actionButton) as GameObject;
        defendButton.name = "DefendButton";
        Text defendButtonText = defendButton.transform.Find("Text").gameObject.GetComponent<Text>();
        defendButtonText.text = "Defend";
        defendButton.GetComponent<Button>().onClick.AddListener(() => Defend());
        defendButton.transform.SetParent(actionSpacer, false);
        atkButtons.Add(defendButton);

        GameObject itemButton = Instantiate(actionButton) as GameObject;
        itemButton.name = "ItemButton";
        Text itemButtonText = itemButton.transform.Find("Text").gameObject.GetComponent<Text>();
        itemButtonText.text = "Item";
        itemButton.GetComponent<Button>().onClick.AddListener(() => OpenItemsMenu());
        itemButton.transform.SetParent(actionSpacer, false);
        atkButtons.Add(itemButton);

        GameObject runButton = Instantiate(actionButton) as GameObject;
        runButton.name = "RunButton";
        Text runButtonText = runButton.transform.Find("Text").gameObject.GetComponent<Text>();
        runButtonText.text = "Run";
        runButton.GetComponent<Button>().onClick.AddListener(() => Run());
        runButton.transform.SetParent(actionSpacer, false);
        atkButtons.Add(runButton);


        if (heroesToManage[0].GetComponent<PlayerStateMachine>().hero.stats.meleeAttacks.Count > 0)
        {
            foreach (BaseAttack actualMelee in heroesToManage[0].GetComponent<PlayerStateMachine>().hero.stats.meleeAttacks)
            {
                GameObject meleeButton = Instantiate(this.attackButton) as GameObject;
                Text meleeButtonText = meleeButton.transform.Find("Text").gameObject.GetComponent<Text>();
                meleeButtonText.text = actualMelee.attackName;
                AttackButtons ATB = meleeButton.GetComponent<AttackButtons>();
                ATB.attackToPerform = actualMelee;
                meleeButton.transform.SetParent(attackSpacer, false);
                atkButtons.Add(meleeButton);
            }
            GameObject returnButtonFromMelee = Instantiate(actionButton) as GameObject;
            returnButtonFromMelee.name = "Return";
            Text returnButtonFromMeleeText = returnButtonFromMelee.transform.Find("Text").gameObject.GetComponent<Text>();
            returnButtonFromMeleeText.text = "Return";
            returnButtonFromMelee.GetComponent<Button>().onClick.AddListener(() => ChangePanels(actionsPanel));
            returnButtonFromMelee.transform.SetParent(attackSpacer, false);
            atkButtons.Add(returnButtonFromMelee);
        }
        else
        {
            attackButton.GetComponent<Button>().interactable = false;
        }

        if (heroesToManage[0].GetComponent<PlayerStateMachine>().hero.stats.magicAttacks.Count>0)
        {
            foreach(BaseAttack actualMagic in heroesToManage[0].GetComponent<PlayerStateMachine>().hero.stats.magicAttacks)
            {
                GameObject spellButton = Instantiate(this.attackButton) as GameObject;
                Text spellButtonText = spellButton.transform.Find("Text").gameObject.GetComponent<Text>();
                spellButtonText.text = actualMagic.attackName;
                AttackButtons ATB = spellButton.GetComponent<AttackButtons>();
                ATB.attackToPerform = actualMagic;
                spellButton.transform.SetParent(magicSpacer, false);
                atkButtons.Add(spellButton);
                if (heroesToManage[0].GetComponent<PlayerStateMachine>().hero.stats.actualMana < actualMagic.manaCost)
                    spellButton.GetComponent<Button>().interactable = false;
            }
            GameObject returnButtonFromMagic = Instantiate(actionButton) as GameObject;
            returnButtonFromMagic.name = "Return";
            Text returnButtonFromMagicText = returnButtonFromMagic.transform.Find("Text").gameObject.GetComponent<Text>();
            returnButtonFromMagicText.text = "Return";
            returnButtonFromMagic.GetComponent<Button>().onClick.AddListener(() => ChangePanels(actionsPanel));
            returnButtonFromMagic.transform.SetParent(magicSpacer, false);
            atkButtons.Add(returnButtonFromMagic);
        }
        else
        {
            magicButton.GetComponent<Button>().interactable = false;
        }
    }

    private void ChangePanels(GameObject panelToActivate)
    {
        activePanel.SetActive(false);
        panelToActivate.SetActive(true);
        activePanel = panelToActivate;
    }

    private void OpenAttacksMenu()
    {
        ChangePanels(attackPanel);
    }

    private void OpenMagicMenu()
    {
        ChangePanels(magicPanel);
    }

    private void OpenItemsMenu()
    {
        Debug.Log("Open Item Menu");
    }

    private void Run()
    {
        Debug.Log("Run From Battle");
    }

    private void Defend()
    {
        Debug.Log("Defend From an attack");
    }

    /// <summary>
    /// Input from the panel
    /// </summary>
    public void AttackInput(BaseAttack choosenAttack)
    {
        heroChoice.attacker = heroesToManage[0].GetComponent<BaseHero>().stats.myName;
        heroChoice.attackerGameObject = heroesToManage[0];
        heroChoice.type = "Hero";
        heroChoice.attack = choosenAttack;
        if (!choosenAttack.isAreaAttack)
        {
            ChangePanels(enemySelectPanel);
        }
        else
        {
            AllEnemiesSelectionInput();
        }
    }

    public void DestroyButtons()
    {
        foreach (GameObject actualButton in atkButtons)
        {
            Destroy(actualButton);
        }
        atkButtons.Clear();
    }
}
