using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelectButton : MonoBehaviour
{
    public GameObject enemyPrefab;

    /// <summary>
    /// This function calls to the Unique Enemy Selection Input function from the Battle State Machine
    /// </summary>
    public void SelectEnemy()
    {
        FindObjectOfType<BattleStateMachine>().GetComponent<BattleStateMachine>().UniqueEnemySelectionInput(enemyPrefab);
    }

    public void ShowEnemySelector()
    {
        enemyPrefab.transform.Find("Selector").gameObject.SetActive(true);
    }

    public void HideEnemySelector()
    {
        enemyPrefab.transform.Find("Selector").gameObject.SetActive(false);
    }
}
