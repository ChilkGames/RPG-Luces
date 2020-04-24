using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TestAttacks : MonoBehaviour
{
    public static bool testingAction;
    private static bool isAreaAttack;
    private static Vector3 targetPosition = new Vector3();
    private static Vector3 centerPosition = new Vector3();
    private static Vector3 startPosition = new Vector3();
    private static GameObject attacker;

    private void Update()
    {
        if (testingAction)
        {
            StartCoroutine(PerformAction());
            testingAction = false;
        }     

    }

    private IEnumerator PerformAction()
    {
        
        if (isAreaAttack)
        {
            while (MoveToCenter()) { yield return null; }
        }
        else
        {
            while (MoveTowardsTarget()) { yield return null; }
        }

        yield return new WaitForSeconds(0.1f);

        while (MoveToStart()) { yield return null; }
    }

    public static void TestAttack(GameObject performer, BaseAttack attackToTest)
    {
        
        if (!Application.isEditor)
            return;

        startPosition = performer.transform.position;

        GameObject battlegroundCenter = GameObject.Find("Center Of The Battleground");
        GameObject [] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject enemyToAttack = enemies[Random.Range(0, enemies.Length)];
        GameObject [] heroes = GameObject.FindGameObjectsWithTag("Hero");
        GameObject heroToAttack = heroes[Random.Range(0, heroes.Length)];

        isAreaAttack = attackToTest.isAreaAttack;

        centerPosition = new Vector3(battlegroundCenter.transform.position.x, startPosition.y, battlegroundCenter.transform.position.z);

        attacker = performer;

        if (attacker.tag == "Enemy")
            targetPosition = new Vector3(heroToAttack.transform.position.x, startPosition.y, heroToAttack.transform.position.z);
        else if (attacker.tag == "Hero")
            targetPosition = new Vector3(enemyToAttack.transform.position.x, startPosition.y, enemyToAttack.transform.position.z);

        testingAction = true;
    }

    private static bool MoveTowardsTarget()
    {
        return targetPosition != (attacker.transform.position = Vector3.MoveTowards(attacker.transform.position, targetPosition, 10*Time.deltaTime));
    }

    private static bool MoveToStart()
    {
        return startPosition != (attacker.transform.position = Vector3.MoveTowards(attacker.transform.position, startPosition, 10*Time.deltaTime));
    }

    private static bool MoveToCenter()
    {
        return centerPosition != (attacker.transform.position = Vector3.MoveTowards(attacker.transform.position, centerPosition, 10*Time.deltaTime));
    }
}
