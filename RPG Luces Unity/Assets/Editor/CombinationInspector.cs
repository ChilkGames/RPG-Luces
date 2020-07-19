using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CombinationAttack))]
public class CombinationInspector : Editor
{
    private CombinationAttack attack;

    private void OnEnable()
    {
        attack = (CombinationAttack)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField(attack.attackName, CustomStyles.titles);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField(attack.attackDescription, CustomStyles.subtitles);
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Combination attack", CustomStyles.bold);
        EditorGUILayout.Space();
        if (attack.isBuff)
        {
            EditorGUILayout.LabelField("Buff", CustomStyles.bold);
            EditorGUILayout.Space();
        }
        if (attack.isAreaAttack)
            EditorGUILayout.LabelField("Area Attack", CustomStyles.bold);
        else
            EditorGUILayout.LabelField("Single Attack", CustomStyles.bold);
        EditorGUILayout.Space();
        if (!attack.isBuff)
            EditorGUILayout.LabelField("Base Damage: " + attack.baseDamage, CustomStyles.bold);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Number of attacks: " + attack.attackQty, CustomStyles.bold);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Level Required: " + attack.levelRequirement, CustomStyles.bold);
        EditorGUILayout.LabelField("Job Required: " + attack.jobsRequirement, CustomStyles.bold);
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Attacks required: ", CustomStyles.bold);
        foreach (var actualAttack in attack.combinationOfAttacks)
        {
            int index = attack.combinationOfAttacks.IndexOf(actualAttack);
            EditorGUILayout.LabelField(actualAttack.attackName, CustomStyles.bold);
        }
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Colors: ", CustomStyles.bold);
        foreach (var actualColor in attack.listOfColors)
        {
            int index = attack.listOfColors.IndexOf(actualColor);
            EditorGUILayout.LabelField(actualColor + "", CustomStyles.bold);
        }
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        if (GUILayout.Button("Edit"))
        {
            LoadCombinationAttack.OpenWindow(attack);
        }
    }
}
