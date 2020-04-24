using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MagicAttackWindow : EditorWindow
{
    private string attackName;
    private string attackDescription;

    private float baseDamage;
    private float manaCost;

    private bool isAreaAttack;
    private bool isBuff;

    private JobsEnum.Jobs jobRequirement;
    private int levelRequirement;

    private List<int> colorIndex = new List<int>();
    private List<ColorsEnum.Colors> colorList = new List<ColorsEnum.Colors>();
    private List<float> percentajeList = new List<float>();

    private Vector2 scrollPosition;

    public static void OpenWindow()
    {
        var myWindow = GetWindow<MagicAttackWindow>();
        myWindow.wantsMouseMove = true;
        myWindow.title = "Create Magic Attack";
        myWindow.Show();
    }

    private void OnGUI()
    {
        minSize = new Vector2(350, 350);
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true, GUIStyle.none, GUI.skin.verticalScrollbar);
        EditorGUILayout.LabelField("New Magic Attack", CustomStyles.titles);
        EditorGUILayout.Space();
        attackName = EditorGUILayout.TextField("Attack Name", attackName);
        EditorGUILayout.Space();
        attackDescription = EditorGUILayout.TextField("Attack Description", attackDescription);
        EditorGUILayout.Space();
        isBuff = EditorGUILayout.Toggle("Is Buff?", isBuff);
        if (!isBuff)
        {
            EditorGUILayout.Space();
            baseDamage = EditorGUILayout.FloatField("Base Damage", baseDamage);
            if (baseDamage < 0)
            {
                baseDamage = 0;
            }
        }
        else
            baseDamage = 0;
        EditorGUILayout.Space();
        manaCost = EditorGUILayout.FloatField("Mana Cost", manaCost);
        if (manaCost < 0)
        {
            manaCost = 0;
        }
        EditorGUILayout.Space();
        isAreaAttack = EditorGUILayout.Toggle("Is Area Attack?", isAreaAttack);
        EditorGUILayout.Space();
        levelRequirement = EditorGUILayout.IntField("Level Requirement", levelRequirement);
        if (levelRequirement<0)
        {
            levelRequirement = 0;
        }
        EditorGUILayout.Space();
        jobRequirement = (JobsEnum.Jobs)EditorGUILayout.EnumPopup("Job Requirement", jobRequirement);
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Color"))
        {
            colorList.Add(ColorsEnum.Colors.BLACK);
            percentajeList.Add(0);
            colorIndex.Add(percentajeList.Count);
        }
        if (colorIndex.Count>0)
        {
            if (GUILayout.Button("Remove Color"))
            {
                colorList.RemoveAt(colorIndex.Count - 1);
                percentajeList.RemoveAt(colorIndex.Count - 1);
                colorIndex.RemoveAt(colorIndex.Count - 1);
            }
        }
        EditorGUILayout.EndHorizontal();

        foreach (int actualColor in colorIndex)
        {
            int index = colorIndex.IndexOf(actualColor);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("color" + actualColor + ":");
            colorList[index] = (ColorsEnum.Colors)EditorGUILayout.EnumPopup(colorList[index]);
            percentajeList[index] = EditorGUILayout.FloatField(percentajeList[index]);
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Create"))
        {
            if (attackName == null)
                ShowError("Attack must have a name");
            else
                CreateAttack();
        }
        if (GUILayout.Button("Return"))
        {
            AttackWindow.OpenWindow();
            Close();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndScrollView();
    }

    /// <summary>
    /// Creates the desired attack
    /// </summary>
    private void CreateAttack()
    {
        var scriptableAttack = CreateInstance<BaseAttack>();
        var path = AssetDatabase.GenerateUniqueAssetPath("Assets/Scripts/Attacks/MagicAttacks/" + attackName + ".asset");
        scriptableAttack.attackName = attackName;
        scriptableAttack.attackDescription = attackDescription;
        scriptableAttack.baseDamage = baseDamage;
        scriptableAttack.manaCost = manaCost;
        scriptableAttack.isMeleeAttack = false;
        scriptableAttack.isAreaAttack = isAreaAttack;
        scriptableAttack.isBuff = isBuff;
        scriptableAttack.levelRequirement = levelRequirement;
        scriptableAttack.jobsRequirement = jobRequirement;
        scriptableAttack.isCombination = false;
        foreach (int actualColor in colorIndex)
        {
            int index = colorIndex.IndexOf(actualColor);
            scriptableAttack.AddColor(colorList[index], percentajeList[index]);
        }
        AssetDatabase.CreateAsset(scriptableAttack, path);
        EditorUtility.SetDirty(scriptableAttack);
        Save();
    }

    private void Save()
    {
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
    private void ShowError(string error)
    {
        ShowNotification(new GUIContent(error));
    }
}
