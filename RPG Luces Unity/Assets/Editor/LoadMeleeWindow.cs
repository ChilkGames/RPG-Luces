using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LoadMeleeWindow : EditorWindow
{
    private BaseAttack baseAttack;

    private List<int> colorIndex = new List<int>();

    private Vector2 scrollPosition;

    public static void OpenWindow(BaseAttack attack)
    {
        var myWindow = GetWindow<LoadMeleeWindow>();
        myWindow.wantsMouseMove = true;
        myWindow.title = "Edit Melee Attack";
        myWindow.baseAttack = attack;
        for (int i =0; i<attack.percentageOfColor.Count; i++)
        {
            myWindow.colorIndex.Add(i);
        }
        myWindow.Show();
    }

    private void OnGUI()
    {
        minSize = new Vector2(350, 350);
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true, GUIStyle.none, GUI.skin.verticalScrollbar);
        EditorGUILayout.LabelField("Edit Melee Attack", CustomStyles.titles);
        EditorGUILayout.Space();
        if (baseAttack != null)
        {
            EditorGUILayout.LabelField(baseAttack.attackName, CustomStyles.subtitles);
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            baseAttack.attackDescription = EditorGUILayout.TextField("Attack Description", baseAttack.attackDescription);
            EditorGUILayout.Space();
            baseAttack.baseDamage = EditorGUILayout.FloatField("Base Damage", baseAttack.baseDamage);
            if (baseAttack.baseDamage < 0)
            {
                baseAttack.baseDamage = 0;
            }
            EditorGUILayout.Space();
            baseAttack.isAreaAttack = EditorGUILayout.Toggle("Is Area Attack?", baseAttack.isAreaAttack);
            EditorGUILayout.Space();
            baseAttack.levelRequirement = EditorGUILayout.IntField("Level Requirement", baseAttack.levelRequirement);
            if (baseAttack.levelRequirement < 0)
            {
                baseAttack.levelRequirement = 0;
            }
            EditorGUILayout.Space();
            baseAttack.jobsRequirement = (JobsEnum.Jobs)EditorGUILayout.EnumPopup("Job Requirement", baseAttack.jobsRequirement);
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Add Color"))
            {
                baseAttack.listOfColors.Add(ColorsEnum.Colors.BLUE);
                baseAttack.percentageOfColor.Add(0);
                colorIndex.Add(baseAttack.percentageOfColor.Count);
            }
            if (colorIndex.Count > 0)
            {
                if (GUILayout.Button("Remove Color"))
                {
                    baseAttack.listOfColors.RemoveAt(colorIndex.Count - 1);
                    baseAttack.percentageOfColor.RemoveAt(colorIndex.Count - 1);
                    colorIndex.RemoveAt(colorIndex.Count - 1);
                }
            }
            EditorGUILayout.EndHorizontal();

            foreach (int actualColor in colorIndex)
            {
                int index = colorIndex.IndexOf(actualColor);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("color" + actualColor + ":");
                baseAttack.listOfColors[index] = (ColorsEnum.Colors)EditorGUILayout.EnumPopup(baseAttack.listOfColors[index]);
                baseAttack.percentageOfColor[index] = EditorGUILayout.FloatField(baseAttack.percentageOfColor[index]);
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Save Changes"))
            {
                Save();
            }
            if (GUILayout.Button("Cancel"))
            {
                AttackWindow.OpenWindow();
                Close();
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndScrollView();
    }

    private void Save()
    {
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
