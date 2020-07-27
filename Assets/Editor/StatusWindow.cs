using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class StatusWindow : EditorWindow
{
    private string statusName;
    private string statusDescription;
    private bool percentualStat;
    private int turnDuration;
    private int pointsToAffect;
    private Parameters parameterToAfftect;
    private bool isBuff;

    private Vector2 scrollPosition;

    [MenuItem("RPG Tools/Status Creator")]
    public static void OpenWindow()
    {
        var myWindow = GetWindow<StatusWindow>();
        myWindow.wantsMouseMove = true;
        myWindow.title = "Create Status";
        myWindow.Show();
    }

    public void OnGUI()
    {
        minSize = new Vector2(350, 250);
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true, GUIStyle.none, GUI.skin.verticalScrollbar);
        EditorGUILayout.LabelField("New Status", CustomStyles.titles);
        EditorGUILayout.Space();

        statusName = EditorGUILayout.TextField("Status Name", statusName);
        EditorGUILayout.Space();

        statusDescription = EditorGUILayout.TextField("Status Description", statusDescription);
        EditorGUILayout.Space();

        parameterToAfftect = (Parameters)EditorGUILayout.EnumPopup("Parameter to affect", parameterToAfftect);
        EditorGUILayout.Space();

        percentualStat = EditorGUILayout.Toggle("Affects percentually?", percentualStat);
        EditorGUILayout.Space();

        if (percentualStat)
            pointsToAffect = EditorGUILayout.IntField("Percentage to affect",pointsToAffect);
        else
            pointsToAffect = EditorGUILayout.IntField("Points to affect", pointsToAffect);
        if (pointsToAffect < 0)
            pointsToAffect = 0;
        EditorGUILayout.Space();

        isBuff = EditorGUILayout.Toggle("Is Buff?", isBuff);
        EditorGUILayout.Space();

        turnDuration = EditorGUILayout.IntField("Turns to affect", turnDuration);
        if (turnDuration < 1)
            turnDuration = 1;
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Create"))
        {
            if (statusName != null)
                CreateStatus();
            else
                ShowError("Status must have a name");
        }
        if (GUILayout.Button("Cancel"))
        {
            Close();
        }
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(15);

        GUILayout.EndScrollView();

    }

    private void CreateStatus()
    {
        var scriptableStatus = CreateInstance<BaseStatus>();
        var path = AssetDatabase.GenerateUniqueAssetPath("Assets/Scripts/Statuses/" + statusName + ".asset");

        scriptableStatus.StatusName = statusName;
        scriptableStatus.StatusDescription = statusDescription;
        scriptableStatus.PercentualStat = percentualStat;
        scriptableStatus.ParameterToAffect = parameterToAfftect;
        scriptableStatus.IsBuff = isBuff;
        scriptableStatus.TurnDuration = turnDuration;
        scriptableStatus.PointsToAffect = pointsToAffect;

        AssetDatabase.CreateAsset(scriptableStatus, path);
        EditorUtility.SetDirty(scriptableStatus);
        Save();
    }

    private void Save()
    {
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Close();
    }

    private void ShowError(string error)
    {
        ShowNotification(new GUIContent(error));
    }
}
