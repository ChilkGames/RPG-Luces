using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MeleeAttackWindow : EditorWindow
{
    private string attackName;
    private string attackDescription;

    private float baseDamage;
    private float manaCost;

    private bool isAreaAttack;
    private bool isMeleeAttack;
    private bool isBuff;

    private JobsEnum.Jobs jobRequirement;
    private int levelRequirement;

    private string colorTag;
    private ColorsEnum.Colors actualColor;
    private int actualColorPercentaje;
    private Dictionary<string, ColorsEnum.Colors> listOfColors;
    private Dictionary<string, int> colorPercentaje;

    private Vector2 scrollPosition;


    public static void OpenWindow()
    {
        var myWindow = GetWindow<MeleeAttackWindow>();
        myWindow.wantsMouseMove = true;
        myWindow.title = "Create Melee Attack";
        myWindow.Show();
    }

    private void OnGUI()
    {
        minSize = new Vector2(350,350);
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true, GUIStyle.none, GUI.skin.verticalScrollbar);
        EditorGUILayout.LabelField("New Melee Attack", CustomStyles.titles);
        EditorGUILayout.Space();
        attackName = EditorGUILayout.TextField("Attack Name", attackName);
        EditorGUILayout.Space();
        attackDescription = EditorGUILayout.TextField("Attack Description", attackDescription);
        EditorGUILayout.Space();
        baseDamage = EditorGUILayout.FloatField("Base Damage", baseDamage);
        if (baseDamage < 0)
        {
            baseDamage = 0;
        }
        EditorGUILayout.Space();
        isAreaAttack = EditorGUILayout.Toggle("Is Area Attack?", isAreaAttack);
        EditorGUILayout.Space();
        levelRequirement = EditorGUILayout.IntField("Level Requirement", levelRequirement);
        if (levelRequirement < 0)
        {
            levelRequirement = 0;
        }
        EditorGUILayout.Space();
        jobRequirement = (JobsEnum.Jobs)EditorGUILayout.EnumPopup("Job Requirement", jobRequirement);
        EditorGUILayout.Space();
        if (GUILayout.Button("Add Color"))
        {
            colorTag = EditorGUILayout.TextField("Color Tag", colorTag);
            actualColor = (ColorsEnum.Colors)EditorGUILayout.EnumPopup("Color", actualColor);
            actualColorPercentaje = EditorGUILayout.IntField("Color Percentaje", actualColorPercentaje);
            if (GUILayout.Button("Confirm"))
            {
                listOfColors.Add(colorTag, actualColor);
                colorPercentaje.Add(colorTag, actualColorPercentaje);
            }
        }
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Create"))
        {
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
        var path = AssetDatabase.GenerateUniqueAssetPath("Assets/Scripts/Attacks/MeleeAttacks/" + attackName + ".asset");
        scriptableAttack.attackName = attackName;
        scriptableAttack.attackDescription = attackDescription;
        scriptableAttack.baseDamage = baseDamage;
        scriptableAttack.manaCost = 0;
        scriptableAttack.isMeleeAttack = true;
        scriptableAttack.isAreaAttack = isAreaAttack;
        scriptableAttack.isBuff = false;
        scriptableAttack.levelRequirement = levelRequirement;
        scriptableAttack.jobsRequirement = jobRequirement;
        AssetDatabase.CreateAsset(scriptableAttack, path);
        EditorUtility.SetDirty(scriptableAttack);
        Save();
    }

    private void Save()
    {
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
