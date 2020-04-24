using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CombinationAttackWindow : EditorWindow
{
    private string attackName;
    private string attackDescription;

    private float baseDamage;
    private float manaCost;

    private bool isAreaAttack;
    private bool isBuff;

    private List<int> colorIndex = new List<int>();
    private List<ColorsEnum.Colors> colorList = new List<ColorsEnum.Colors>();
    private List<float> percentajeList = new List<float>();

    private List<int> attacksIndex = new List<int>();
    private List<BaseAttack> requiredAttacks = new List<BaseAttack>();

    private Vector2 scrollPosition;

    public static void OpenWindow()
    {
        var myWindow = GetWindow<CombinationAttackWindow>();
        myWindow.attacksIndex.Add(0);
        myWindow.attacksIndex.Add(1);
        myWindow.requiredAttacks.Add(null);
        myWindow.requiredAttacks.Add(null);
        myWindow.wantsMouseMove = true;
        myWindow.title = "Create Combination Attack";
        myWindow.Show();
    }

    private void OnGUI()
    {
        minSize = new Vector2(350, 350);
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true, GUIStyle.none, GUI.skin.verticalScrollbar);
        EditorGUILayout.LabelField("New Combination Attack", CustomStyles.titles);
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
        manaCost = EditorGUILayout.FloatField("Mana Cost", manaCost);
        if (manaCost < 0)
        {
            manaCost = 0;
        }
        EditorGUILayout.Space();
        isBuff = EditorGUILayout.Toggle("Is Buff?", isBuff);
        EditorGUILayout.Space();
        isAreaAttack = EditorGUILayout.Toggle("Is Area Attack?", isAreaAttack);
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Color"))
        {
            colorList.Add(ColorsEnum.Colors.BLUE);
            percentajeList.Add(0);
            colorIndex.Add(percentajeList.Count);
        }
        if (colorIndex.Count > 0)
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

        if (requiredAttacks.Count == 2)
        {
            if (GUILayout.Button("Add Attack"))
            {
                attacksIndex.Add(2);
                requiredAttacks.Add(null);
            }
        }
        else
        {
            if (requiredAttacks.Count == 3)
            {
                if (GUILayout.Button("Remove Attack"))
                {
                    attacksIndex.Remove(2);
                    requiredAttacks.RemoveAt(2);
                }
            }
        }

        foreach (int actualAttack in attacksIndex)
        {
            int index = attacksIndex.IndexOf(actualAttack);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Attack" + actualAttack + ":");
            requiredAttacks[index] = (BaseAttack)EditorGUILayout.ObjectField(requiredAttacks[index], typeof(BaseAttack), true); ;
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Create"))
        {
            if (attackName == null)
            {
                ShowError("Attack must have a name");   
            }
            else
            {
                foreach (BaseAttack actualAttack in requiredAttacks)
                {
                    if (actualAttack == null)
                    {
                        ShowError("Must fill all required attack fields");
                        OnGUI();
                    }
                }
                CreateAttack();
            }        
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
        var scriptableAttack = CreateInstance<CombinationAttack>();
        var path = AssetDatabase.GenerateUniqueAssetPath("Assets/Scripts/Attacks/Combinations/" + attackName + ".asset");
        scriptableAttack.attackName = attackName;
        scriptableAttack.attackDescription = attackDescription;
        scriptableAttack.baseDamage = baseDamage;
        scriptableAttack.manaCost = manaCost;
        scriptableAttack.isMeleeAttack = false;
        scriptableAttack.isAreaAttack = isAreaAttack;
        scriptableAttack.isBuff = isBuff;
        scriptableAttack.levelRequirement = 1;
        scriptableAttack.jobsRequirement = JobsEnum.Jobs.ALL;
        scriptableAttack.isCombination = true;
        foreach (int actualColor in colorIndex)
        {
            int index = colorIndex.IndexOf(actualColor);
            scriptableAttack.AddColor(colorList[index], percentajeList[index]);
        }
        scriptableAttack.combinationOfAttacks = requiredAttacks;
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
        ShowNotification(new GUIContent (error));
    }
}
