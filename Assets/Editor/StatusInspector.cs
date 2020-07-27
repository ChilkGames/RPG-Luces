using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BaseStatus))]
public class StatusInspector : Editor
{
    private BaseStatus status;

    private void OnEnable()
    {
        status = (BaseStatus)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField(status.StatusName, CustomStyles.titles);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField(status.StatusDescription, CustomStyles.subtitles);
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Parameter to Affect: " + status.ParameterToAffect, CustomStyles.bold);
        EditorGUILayout.Space();
        if (status.PercentualStat)
            EditorGUILayout.LabelField("Affects " + status.PointsToAffect + " %", CustomStyles.bold);
        else
            EditorGUILayout.LabelField("Affects " + status.PointsToAffect + " points", CustomStyles.bold);
        EditorGUILayout.Space();
        if (status.IsBuff)
            EditorGUILayout.LabelField("Is Buff", CustomStyles.bold);
        else
            EditorGUILayout.LabelField("Is DeBuff", CustomStyles.bold);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Lasts " + status.TurnDuration + " turns", CustomStyles.bold);

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        if (GUILayout.Button("Edit"))
        {
            //LoadItemWindow.OpenWindow(item);
        }
    }
}
