using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BaseItem))]
public class ItemInspector : Editor
{
    private BaseItem item;

    private void OnEnable()
    {
        item = (BaseItem)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField(item.itemName, CustomStyles.titles);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField(item.itemDescription, CustomStyles.subtitles);
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Price: " + item.price + " $", CustomStyles.bold);
        if (item.useOnEnemies)
            EditorGUILayout.LabelField("Use on enemies", CustomStyles.bold);
        else
            EditorGUILayout.LabelField("Use on allies", CustomStyles.bold);
        EditorGUILayout.Space();
        if (item.areaEffect)
            EditorGUILayout.LabelField("Affects all targets", CustomStyles.bold);
        else
            EditorGUILayout.LabelField("Affects a single target", CustomStyles.bold);
        EditorGUILayout.Space();

        if (item.useOnEnemies && item.damage >0)
            EditorGUILayout.LabelField("Damage: " + item.damage, CustomStyles.bold);
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Statuses: ", CustomStyles.bold);
        foreach (var actualStat in item.statuses)
        {
            int index = item.statuses.IndexOf(actualStat);
            EditorGUILayout.LabelField(item.statuses[index] + " " + item.percentages[index] + " %", CustomStyles.bold);
        }
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        if (GUILayout.Button("Edit"))
        {
            LoadItemWindow.OpenWindow(item);
        }
    }
}
