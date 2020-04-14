using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LoadMeleeWindow : EditorWindow
{
    private BaseAttack baseAttack;

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
        var myWindow = GetWindow<LoadMeleeWindow>();
        myWindow.wantsMouseMove = true;
        myWindow.title = "Edit Melee Attack";
        myWindow.Show();
    }

    private void OnGUI()
    {
        minSize = new Vector2(350, 350);
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true, GUIStyle.none, GUI.skin.verticalScrollbar);
        EditorGUILayout.LabelField("Edit Melee Attack", CustomStyles.titles);
        EditorGUILayout.Space();
        baseAttack = (BaseAttack)EditorGUILayout.ObjectField("Attack", baseAttack, typeof(BaseAttack), true);
        EditorGUILayout.Space();
        if (baseAttack != null)
        {
            baseAttack.attackDescription = EditorGUILayout.TextField("Attack Description", attackDescription);
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
            /*if (GUILayout.Button("Add Color"))
            {
                colorTag = EditorGUILayout.TextField("Color Tag", colorTag);
                actualColor = (ColorsEnum.Colors)EditorGUILayout.EnumPopup("Color", actualColor);
                actualColorPercentaje = EditorGUILayout.IntField("Color Percentaje", actualColorPercentaje);
                if (GUILayout.Button("Confirm"))
                {
                    listOfColors.Add(colorTag, actualColor);
                    colorPercentaje.Add(colorTag, actualColorPercentaje);
                }
            }*/
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Edit"))
            {
                CreateAttack();
            }
            if (GUILayout.Button("Return"))
            {
                AttackWindow.OpenWindow();
                Close();
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndScrollView();
    }

    /// <summary>
    /// Creates the desired attack
    /// </summary>
    private void CreateAttack()
    {
        var scriptableAttack = CreateInstance<BaseAttack>();
        var path = "Assets/Scripts/Attacks/MeleeAttacks/" + attackName + ".asset";
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
