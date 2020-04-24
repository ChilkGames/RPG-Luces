using System.Collections;
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
        EditorGUILayout.LabelField("Level Required: " + attack.levelRequirement, CustomStyles.bold);
        EditorGUILayout.LabelField("Job Required: " + attack.jobsRequirement, CustomStyles.bold);
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Colors: ", CustomStyles.bold);
        foreach (var actualColor in attack.listOfColors)
        {
            int index = attack.listOfColors.IndexOf(actualColor);
            EditorGUILayout.LabelField(actualColor + " " + attack.percentageOfColor[index] + " %", CustomStyles.bold);
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
