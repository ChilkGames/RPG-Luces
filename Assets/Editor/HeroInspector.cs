using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BaseHero))]
public class HeroInspector : Editor
{
    private BaseHero hero;
    private List<int> meleeIndex = new List<int>();
    private List<int> magicIndex = new List<int>();


    private void OnEnable()
    {
        hero = (BaseHero)target;
        hero.gameObject.name = hero.stats.myName;
        for (int i = 0; i < hero.stats.meleeAttacks.Count; i++)
        {
            meleeIndex.Add(i);
        }
        for (int i = 0; i < hero.stats.magicAttacks.Count; i++)
        {
            magicIndex.Add(i);
        }
    }

    public override void OnInspectorGUI()
    {
        hero.stats.myName = EditorGUILayout.TextField("Name", hero.stats.myName);
        EditorGUILayout.Space();

        hero.stats.actualJob = (JobsEnum.Jobs)EditorGUILayout.EnumPopup("Job", hero.stats.actualJob);
        EditorGUILayout.Space();

        hero.stats.level = EditorGUILayout.IntField("Level", hero.stats.level);
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        hero.stats.actualHealth = EditorGUILayout.FloatField("HP:", hero.stats.actualHealth);
        hero.stats.maxHealth = EditorGUILayout.FloatField("of", hero.stats.maxHealth);
        EditorGUILayout.EndHorizontal();
        if (hero.stats.maxHealth < 0)
        {
            hero.stats.maxHealth = 0;
        }
        if (hero.stats.maxHealth < hero.stats.actualHealth)
        {
            hero.stats.maxHealth = hero.stats.actualHealth;
        }

        EditorGUILayout.BeginHorizontal();
        hero.stats.actualMana = EditorGUILayout.FloatField("MP:", hero.stats.actualMana);
        hero.stats.maxMana = EditorGUILayout.FloatField("of", hero.stats.maxMana);
        EditorGUILayout.EndHorizontal();
        if (hero.stats.actualMana < 0)
        {
            hero.stats.actualMana = 0;
        }
        if (hero.stats.actualMana > hero.stats.maxMana)
        {
            hero.stats.actualMana = hero.stats.maxMana;
        }

        EditorGUILayout.BeginHorizontal();
        hero.stats.actualAttack = EditorGUILayout.FloatField("Attack:", hero.stats.actualAttack);
        hero.stats.maxAttack = EditorGUILayout.FloatField("of", hero.stats.maxAttack);
        EditorGUILayout.EndHorizontal();
        if (hero.stats.actualAttack < 0)
        {
            hero.stats.actualAttack = 0;
        }
        if (hero.stats.actualAttack > hero.stats.maxAttack)
        {
            hero.stats.actualAttack = hero.stats.maxAttack;
        }

        EditorGUILayout.BeginHorizontal();
        hero.stats.actualMagic = EditorGUILayout.FloatField("Magic:", hero.stats.actualMagic);
        hero.stats.maxMagic = EditorGUILayout.FloatField("of", hero.stats.maxMagic);
        EditorGUILayout.EndHorizontal();
        if (hero.stats.actualMagic < 0)
        {
            hero.stats.actualMagic = 0;
        }
        if (hero.stats.actualMagic > hero.stats.maxMagic)
        {
            hero.stats.actualMagic = hero.stats.maxMagic;
        }

        EditorGUILayout.BeginHorizontal();
        hero.stats.actualDefense = EditorGUILayout.FloatField("Defense:", hero.stats.actualDefense);
        hero.stats.maxDefense = EditorGUILayout.FloatField("of", hero.stats.maxDefense);
        EditorGUILayout.EndHorizontal();
        if (hero.stats.actualDefense < 0)
        {
            hero.stats.actualDefense = 0;
        }
        if (hero.stats.actualDefense > hero.stats.maxDefense)
        {
            hero.stats.actualDefense = hero.stats.maxDefense;
        }

        EditorGUILayout.BeginHorizontal();
        hero.stats.actualSpeed = EditorGUILayout.FloatField("Speed:", hero.stats.actualSpeed);
        hero.stats.maxSpeed = EditorGUILayout.FloatField("of", hero.stats.maxSpeed);
        EditorGUILayout.EndHorizontal();
        if (hero.stats.actualSpeed < 0)
        {
            hero.stats.actualSpeed = 0;
        }
        if (hero.stats.actualSpeed > hero.stats.maxSpeed)
        {
            hero.stats.actualSpeed = hero.stats.maxSpeed;
        }
        EditorGUILayout.Space();

        hero.stats.isStrategist = EditorGUILayout.Toggle("Is the strategist?", hero.stats.isStrategist);
        EditorGUILayout.Space();

        hero.stats.actualColor = (ColorsEnum.Colors)EditorGUILayout.EnumPopup("Actual color", hero.stats.actualColor);
        EditorGUILayout.Space();

        hero.stats.experience = EditorGUILayout.IntField("Actual Experience", hero.stats.experience);
        EditorGUILayout.Space();
        if (hero.stats.experience < 0)
        {
            hero.stats.experience = 0;
        }

        hero.orderOfAttack = EditorGUILayout.IntField("Order of Attack", hero.orderOfAttack);
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        if (hero.orderOfAttack < 0)
        {
            hero.orderOfAttack = 0;
        }

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Melee Attack"))
        {
            meleeIndex.Add(hero.stats.meleeAttacks.Count);
            hero.stats.meleeAttacks.Add(null);
        }
        if (meleeIndex.Count > 0)
        {
            if (GUILayout.Button("Remove Melee Attack"))
            {
                hero.stats.meleeAttacks.RemoveAt(meleeIndex.Count - 1);
                meleeIndex.RemoveAt(meleeIndex.Count - 1);
            }
        }
        EditorGUILayout.EndHorizontal();

        foreach (var actualMelee in meleeIndex)
        {
            int index = meleeIndex.IndexOf(actualMelee);
            EditorGUILayout.BeginHorizontal();
            if (hero.stats.meleeAttacks[index]!= null)
            {
                hero.stats.meleeAttacks[index] = (BaseAttack)EditorGUILayout.ObjectField(hero.stats.meleeAttacks[index].attackName + ": ", hero.stats.meleeAttacks[index], typeof(BaseAttack), false);
                if (GUILayout.Button("Test"))
                {
                    if (!TestAttacks.testingAction)
                        TestAttacks.TestAttack(hero.gameObject, hero.stats.meleeAttacks[index]);
                }
            }
            else
                hero.stats.meleeAttacks[index] = (BaseAttack)EditorGUILayout.ObjectField("New Melee Attack: ", hero.stats.meleeAttacks[index], typeof(BaseAttack), false);
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Magic Attack"))
        {
            magicIndex.Add(hero.stats.magicAttacks.Count);
            hero.stats.magicAttacks.Add(null);
        }
        if (magicIndex.Count > 0)
        {
            if (GUILayout.Button("Remove Magic Attack"))
            {
                hero.stats.magicAttacks.RemoveAt(magicIndex.Count - 1);
                magicIndex.RemoveAt(magicIndex.Count - 1);
            }
        }
        EditorGUILayout.EndHorizontal();

        foreach (var actualMagic in magicIndex)
        {
            int index = magicIndex.IndexOf(actualMagic);
            EditorGUILayout.BeginHorizontal();
            if (hero.stats.magicAttacks[index] != null)
            {
                hero.stats.magicAttacks[index] = (BaseAttack)EditorGUILayout.ObjectField(hero.stats.magicAttacks[index].attackName + ": ", hero.stats.magicAttacks[index], typeof(BaseAttack), false);
                if (GUILayout.Button("Test"))
                {
                    if (!TestAttacks.testingAction)
                        TestAttacks.TestAttack(hero.gameObject, hero.stats.magicAttacks[index]);
                }
            }
            else
                hero.stats.magicAttacks[index] = (BaseAttack)EditorGUILayout.ObjectField("New Magic Attack: ", hero.stats.magicAttacks[index], typeof(BaseAttack), false);
            EditorGUILayout.EndHorizontal();
        }
    }
}
