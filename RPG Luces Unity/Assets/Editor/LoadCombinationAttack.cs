using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LoadCombinationAttack : EditorWindow
{
    private CombinationAttack baseAttack;

    private List<int> colorIndex = new List<int>();

    private List<int> attacksIndex = new List<int>();

    private Vector2 scrollPosition;

    public static void OpenWindow(CombinationAttack attack)
    {
        var myWindow = GetWindow<LoadCombinationAttack>();
        myWindow.wantsMouseMove = true;
        myWindow.title = "Edit Melee Attack";
        myWindow.baseAttack = attack;
        for (int i = 0; i < attack.percentageOfColor.Count; i++)
        {
            myWindow.colorIndex.Add(i);
        }
        for (int i = 0; i < attack.combinationOfAttacks.Count; i++)
        {
            myWindow.attacksIndex.Add(i);
        }
        myWindow.Show();
    }

    private void OnGUI()
    {
        minSize = new Vector2(350, 350);
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true, GUIStyle.none, GUI.skin.verticalScrollbar);
        EditorGUILayout.LabelField("Edit Combination Attack", CustomStyles.titles);
        EditorGUILayout.Space();
        baseAttack.attackName = EditorGUILayout.TextField("Attack Name", baseAttack.attackName);
        EditorGUILayout.Space();
        baseAttack.attackDescription = EditorGUILayout.TextField("Attack Description", baseAttack.attackDescription);
        EditorGUILayout.Space();
        baseAttack.baseDamage = EditorGUILayout.FloatField("Base Damage", baseAttack.baseDamage);
        if (baseAttack.baseDamage < 0)
        {
            baseAttack.baseDamage = 0;
        }
        EditorGUILayout.Space();
        baseAttack.manaCost = EditorGUILayout.FloatField("Mana Cost", baseAttack.manaCost);
        if (baseAttack.manaCost < 0)
        {
            baseAttack.manaCost = 0;
        }
        EditorGUILayout.Space();
        baseAttack.isBuff = EditorGUILayout.Toggle("Is Buff?", baseAttack.isBuff);
        EditorGUILayout.Space();
        baseAttack.isAreaAttack = EditorGUILayout.Toggle("Is Area Attack?", baseAttack.isAreaAttack);
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

        EditorGUILayout.Space();

        if (baseAttack.combinationOfAttacks.Count == 2)
        {
            if (GUILayout.Button("Add Attack"))
            {
                attacksIndex.Add(2);
                baseAttack.combinationOfAttacks.Add(null);
            }
        }
        else
        {
            if (baseAttack.combinationOfAttacks.Count == 3)
            {
                if (GUILayout.Button("Remove Attack"))
                {
                    attacksIndex.Remove(2);
                    baseAttack.combinationOfAttacks.RemoveAt(2);
                }
            }
        }

        foreach (int actualAttack in attacksIndex)
        {
            int index = attacksIndex.IndexOf(actualAttack);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Attack" + actualAttack + ":");
            baseAttack.combinationOfAttacks[index] = (BaseAttack)EditorGUILayout.ObjectField(baseAttack.combinationOfAttacks[index], typeof(BaseAttack), true); ;
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Save Changes"))
        {
            foreach (BaseAttack actualAttack in baseAttack.combinationOfAttacks)
            {
                if (actualAttack == null)
                {
                    ShowError("There´s a null attack");
                    OnGUI();
                }
            }
            Save();
        }
        if (GUILayout.Button("Return"))
        {
            AttackWindow.OpenWindow();
            Close();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndScrollView();
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
