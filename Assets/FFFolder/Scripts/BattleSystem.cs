using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURNM, WON, LOST }

public class BattleSystem : MonoBehaviour
{

    public GameObject playerPrefab1;
    public GameObject enemyPrefab;

    public Transform playerBattleStation1;
    public Transform enemyBattleStation;

    Unit playerUnit1;
    Unit enemyUnit;

    public Text dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public BattleState state;



    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO1 = Instantiate(playerPrefab1, playerBattleStation1);
        playerUnit1 = playerGO1.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.text = "A Wild " + enemyUnit.unitName + " appears";

        playerHUD.SetHUD(playerUnit1);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit1.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "The attack connects!";

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            //EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURNM;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        // Whatever enemy logic we want goes here
        dialogueText.text = enemyUnit.unitName + " attacks!";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit1.TakeDamage(enemyUnit.damage);
        playerHUD.SetHP(playerUnit1.currentHP);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            Endbattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    void Endbattle()
    {
        if(state == BattleState.WON)
        {
            dialogueText.text = "You won the battle!";
        }else if (state == BattleState.LOST)
        {
            dialogueText.text = "you were defeated";
        }
    }

    void PlayerTurn()
    {
        dialogueText.text = "Choose an action:";
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }
}
