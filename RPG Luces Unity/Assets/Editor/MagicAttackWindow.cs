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

    private bool isMultipleAttack;
    private int attackQty;

    private JobsEnum.Jobs jobRequirement;
    private int levelRequirement;

    private List<int> colorIndex = new List<int>();
    private List<ColorsEnum.Colors> colorList = new List<ColorsEnum.Colors>();

    private List<int> tagIndex = new List<int>();
    private List<AttackTagsEnum.Tags> tagList = new List<AttackTagsEnum.Tags>();

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

        isMultipleAttack = EditorGUILayout.Toggle("Is Multiple Attack?", isMultipleAttack);
        if (isMultipleAttack)
        {
            EditorGUILayout.Space();
            attackQty = EditorGUILayout.IntField("Attacks Quantity", attackQty);
            if (attackQty < 2)
                attackQty = 2;
        }
        else
            attackQty = 1;
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
            colorIndex.Add(colorList.Count);
        }
        if (colorIndex.Count>0)
        {
            if (GUILayout.Button("Remove Color"))
            {
                colorList.RemoveAt(colorIndex.Count - 1);
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
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Tag"))
        {
            tagList.Add(AttackTagsEnum.Tags.SLASH);
            tagIndex.Add(tagList.Count);
        }
        if (colorIndex.Count > 0)
        {
            if (GUILayout.Button("Remove Tag"))
            {
                tagList.RemoveAt(tagIndex.Count - 1);
                tagIndex.RemoveAt(tagIndex.Count - 1);
            }
        }
        EditorGUILayout.EndHorizontal();
        foreach (int actualTag in tagIndex)
        {
            int index = tagIndex.IndexOf(actualTag);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Tag " + actualTag + ":");
            tagList[index] = (AttackTagsEnum.Tags)EditorGUILayout.EnumPopup(tagList[index]);
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.Space();
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
        GUILayout.Space(15);
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
        scriptableAttack.attackQty = attackQty;
        scriptableAttack.levelRequirement = levelRequirement;
        scriptableAttack.jobsRequirement = jobRequirement;
        scriptableAttack.isCombination = false;
        foreach (int actualColor in colorIndex)
        {
            int index = colorIndex.IndexOf(actualColor);
            scriptableAttack.listOfColors.Add(colorList[index]);
        }
        foreach (int actualTag in tagIndex)
        {
            int index = tagIndex.IndexOf(actualTag);
            scriptableAttack.listOfTags.Add(tagList[index]);
        }
        AssetDatabase.CreateAsset(scriptableAttack, path);
        EditorUtility.SetDirty(scriptableAttack);
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
