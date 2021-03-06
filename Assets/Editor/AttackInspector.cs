﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BaseAttack))]
public class AttackInspector : Editor
{
    private BaseAttack attack;

    private void OnEnable()
    {
        attack = (BaseAttack)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField(attack.attackName, CustomStyles.titles);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField(attack.attackDescription, CustomStyles.subtitles);
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        if (attack.isMeleeAttack)
            EditorGUILayout.LabelField("Melee attack", CustomStyles.bold);
        else
        {
            if (attack.isBuff)
                EditorGUILayout.LabelField("Buff", CustomStyles.bold);
            else
                EditorGUILayout.LabelField("Magic attack", CustomStyles.bold);
        }
        EditorGUILayout.Space();
        if (attack.isAreaAttack)
            EditorGUILayout.LabelField("Area Attack", CustomStyles.bold);
        else
            EditorGUILayout.LabelField("Single Attack", CustomStyles.bold);
        EditorGUILayout.Space();
        if (!attack.isBuff)
            EditorGUILayout.LabelField("Base Damage: " + attack.baseDamage, CustomStyles.bold);
        if (!attack.isMeleeAttack)
            EditorGUILayout.LabelField("Mana Cost: " + attack.manaCost, CustomStyles.bold);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Number of attacks: " + attack.attackQty, CustomStyles.bold);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Level Required: " + attack.levelRequirement, CustomStyles.bold);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Job Required: " + attack.jobsRequirement, CustomStyles.bold);
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Colors: ", CustomStyles.bold);
        if (attack.listOfColors.Count <= 0)
            EditorGUILayout.LabelField("None", CustomStyles.bold);
        else
        {
            foreach (var actualColor in attack.listOfColors)
            {
                int index = attack.listOfColors.IndexOf(actualColor);
                EditorGUILayout.LabelField("" + actualColor, CustomStyles.bold);
            }
        }
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Tags: ", CustomStyles.bold);
        if (attack.listOfTags.Count<=0)
            EditorGUILayout.LabelField("None", CustomStyles.bold);
        else
        {
            foreach (var actualTag in attack.listOfTags)
            {
                int index = attack.listOfTags.IndexOf(actualTag);
                EditorGUILayout.LabelField("" + actualTag, CustomStyles.bold);
            }
        }  
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        if (GUILayout.Button("Edit"))
        {
            if (attack.isMeleeAttack)
                LoadMeleeWindow.OpenWindow(attack);
            else
                LoadMagicWindow.OpenWindow(attack);
        }
    }
}
