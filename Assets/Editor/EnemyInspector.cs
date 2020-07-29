using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BaseEnemy))]
public class EnemyInspector : Editor
{
    private BaseEnemy enemy;
    private List<int> meleeIndex = new List<int>();
    private List<int> magicIndex = new List<int>();
    private List<int> itemIndex = new List<int>();

    private void OnEnable()
    {
        enemy = (BaseEnemy)target;
        enemy.gameObject.name = enemy.stats.myName;
        for (int i = 0; i < enemy.stats.meleeAttacks.Count; i++)
        {
            meleeIndex.Add(i);
        }
        for (int i = 0; i < enemy.stats.magicAttacks.Count; i++)
        {
            magicIndex.Add(i);
        }
        for (int i = 0; i < enemy.itemsToDrop.Count; i++)
        {
            itemIndex.Add(i);
        }
    }

    public override void OnInspectorGUI()
    {
        enemy.stats.myName = EditorGUILayout.TextField("Name", enemy.stats.myName);
        EditorGUILayout.Space();

        enemy.stats.actualJob = (JobsEnum.Jobs)EditorGUILayout.EnumPopup("Job", enemy.stats.actualJob);
        EditorGUILayout.Space();

        enemy.stats.level = EditorGUILayout.IntField("Level", enemy.stats.level);
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        enemy.stats.actualHealth = EditorGUILayout.FloatField("HP:", enemy.stats.actualHealth);
        enemy.stats.maxHealth = EditorGUILayout.FloatField("of", enemy.stats.maxHealth);
        EditorGUILayout.EndHorizontal();
        if (enemy.stats.maxHealth < 0)
        {
            enemy.stats.maxHealth = 0;
        }
        if (enemy.stats.maxHealth < enemy.stats.actualHealth)
        {
            enemy.stats.maxHealth = enemy.stats.actualHealth;
        }

        EditorGUILayout.BeginHorizontal();
        enemy.stats.actualMana = EditorGUILayout.FloatField("MP:", enemy.stats.actualMana);
        enemy.stats.maxMana = EditorGUILayout.FloatField("of", enemy.stats.maxMana);
        EditorGUILayout.EndHorizontal();
        if (enemy.stats.actualMana < 0)
        {
            enemy.stats.actualMana = 0;
        }
        if (enemy.stats.actualMana > enemy.stats.maxMana)
        {
            enemy.stats.actualMana = enemy.stats.maxMana;
        }

        EditorGUILayout.BeginHorizontal();
        enemy.stats.actualAttack = EditorGUILayout.FloatField("Attack:", enemy.stats.actualAttack);
        enemy.stats.maxAttack = EditorGUILayout.FloatField("of", enemy.stats.maxAttack);
        EditorGUILayout.EndHorizontal();
        if (enemy.stats.actualAttack < 0)
        {
            enemy.stats.actualAttack = 0;
        }
        if (enemy.stats.actualAttack > enemy.stats.maxAttack)
        {
            enemy.stats.actualAttack = enemy.stats.maxAttack;
        }

        EditorGUILayout.BeginHorizontal();
        enemy.stats.actualMagic = EditorGUILayout.FloatField("Magic:", enemy.stats.actualMagic);
        enemy.stats.maxMagic = EditorGUILayout.FloatField("of", enemy.stats.maxMagic);
        EditorGUILayout.EndHorizontal();
        if (enemy.stats.actualMagic < 0)
        {
            enemy.stats.actualMagic = 0;
        }
        if (enemy.stats.actualMagic > enemy.stats.maxMagic)
        {
            enemy.stats.actualMagic = enemy.stats.maxMagic;
        }

        EditorGUILayout.BeginHorizontal();
        enemy.stats.actualDefense = EditorGUILayout.FloatField("Defense:", enemy.stats.actualDefense);
        enemy.stats.maxDefense = EditorGUILayout.FloatField("of", enemy.stats.maxDefense);
        EditorGUILayout.EndHorizontal();
        if (enemy.stats.actualDefense < 0)
        {
            enemy.stats.actualDefense = 0;
        }
        if (enemy.stats.actualDefense > enemy.stats.maxDefense)
        {
            enemy.stats.actualDefense = enemy.stats.maxDefense;
        }

        EditorGUILayout.BeginHorizontal();
        enemy.stats.actualSpeed = EditorGUILayout.FloatField("Speed:", enemy.stats.actualSpeed);
        enemy.stats.maxSpeed = EditorGUILayout.FloatField("of", enemy.stats.maxSpeed);
        EditorGUILayout.EndHorizontal();
        if (enemy.stats.actualSpeed < 0)
        {
            enemy.stats.actualSpeed = 0;
        }
        if (enemy.stats.actualSpeed > enemy.stats.maxSpeed)
        {
            enemy.stats.actualSpeed = enemy.stats.maxSpeed;
        }
        EditorGUILayout.Space();

        enemy.stats.actualColor = (ColorsEnum.Colors)EditorGUILayout.EnumPopup("Actual color", enemy.stats.actualColor);

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Melee Attack"))
        {
            meleeIndex.Add(enemy.stats.meleeAttacks.Count);
            enemy.stats.meleeAttacks.Add(null);
        }
        if (meleeIndex.Count > 0)
        {
            if (GUILayout.Button("Remove Melee Attack"))
            {
                enemy.stats.meleeAttacks.RemoveAt(meleeIndex.Count - 1);
                meleeIndex.RemoveAt(meleeIndex.Count - 1);
            }
        }
        EditorGUILayout.EndHorizontal();

        foreach (var actualMelee in meleeIndex)
        {
            int index = meleeIndex.IndexOf(actualMelee);
            EditorGUILayout.BeginHorizontal();
            if (enemy.stats.meleeAttacks[index] != null)
            {
                enemy.stats.meleeAttacks[index] = (BaseAttack)EditorGUILayout.ObjectField(enemy.stats.meleeAttacks[index].attackName + ": ", enemy.stats.meleeAttacks[index], typeof(BaseAttack), false);
                if (GUILayout.Button("Test"))
                {
                    if (!TestAttacks.testingAction)
                        TestAttacks.TestAttack(enemy.gameObject, enemy.stats.meleeAttacks[index]);
                }
            }
            else
                enemy.stats.meleeAttacks[index] = (BaseAttack)EditorGUILayout.ObjectField("New Melee Attack: ", enemy.stats.meleeAttacks[index], typeof(BaseAttack), false);
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Magic Attack"))
        {
            magicIndex.Add(enemy.stats.magicAttacks.Count);
            enemy.stats.magicAttacks.Add(null);
        }
        if (magicIndex.Count > 0)
        {
            if (GUILayout.Button("Remove Magic Attack"))
            {
                enemy.stats.magicAttacks.RemoveAt(magicIndex.Count - 1);
                magicIndex.RemoveAt(magicIndex.Count - 1);
            }
        }
        EditorGUILayout.EndHorizontal();

        foreach (var actualMagic in magicIndex)
        {
            int index = magicIndex.IndexOf(actualMagic);
            EditorGUILayout.BeginHorizontal();
            if (enemy.stats.magicAttacks[index] != null)
            {
                enemy.stats.magicAttacks[index] = (BaseAttack)EditorGUILayout.ObjectField(enemy.stats.magicAttacks[index].attackName + ": ", enemy.stats.magicAttacks[index], typeof(BaseAttack), false);
                if (GUILayout.Button("Test"))
                {
                    if (!TestAttacks.testingAction)
                        TestAttacks.TestAttack(enemy.gameObject, enemy.stats.magicAttacks[index]);
                }
            }
            else
                enemy.stats.magicAttacks[index] = (BaseAttack)EditorGUILayout.ObjectField("New Magic Attack: ", enemy.stats.magicAttacks[index], typeof(BaseAttack), false);
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        enemy.experienceToDrop = EditorGUILayout.IntField("Experience to Drop", enemy.experienceToDrop);
        if (enemy.experienceToDrop < 0)
        {
            enemy.experienceToDrop = 0;
        }

        enemy.goldToDrop = EditorGUILayout.IntField("Gold to Drop", enemy.goldToDrop);
        if (enemy.goldToDrop < 0)
        {
            enemy.goldToDrop = 0;
        }
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Item to Drop"))
        {
            itemIndex.Add(enemy.itemsToDrop.Count);
            enemy.itemsToDrop.Add(null);
            enemy.percentageOfDrop.Add(0);
        }
        if (itemIndex.Count > 0)
        {
            if (GUILayout.Button("Remove Item to Drop"))
            {
                enemy.itemsToDrop.RemoveAt(itemIndex.Count - 1);
                enemy.percentageOfDrop.RemoveAt(itemIndex.Count - 1);
                itemIndex.RemoveAt(itemIndex.Count - 1);
            }
        }
        EditorGUILayout.EndHorizontal();

        foreach (var actualTiem in itemIndex)
        {
            int index = itemIndex.IndexOf(actualTiem);
            EditorGUILayout.BeginHorizontal();
            enemy.itemsToDrop[index] = (BaseItem)EditorGUILayout.ObjectField(enemy.itemsToDrop[index], typeof(BaseItem), false);
            enemy.percentageOfDrop[index] = EditorGUILayout.IntField("Drop Percentage: ", enemy.percentageOfDrop[index]);
            if (enemy.percentageOfDrop[index]<0)
            {
                enemy.percentageOfDrop[index] = 0;
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
