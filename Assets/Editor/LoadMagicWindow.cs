using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LoadMagicWindow : EditorWindow
{
    private BaseAttack baseAttack;

    private List<int> colorIndex = new List<int>();

    private List<int> tagIndex = new List<int>();

    private Vector2 scrollPosition;

    private bool isMultipleAttack;

    public static void OpenWindow(BaseAttack attack)
    {
        var myWindow = GetWindow<LoadMagicWindow>();
        myWindow.wantsMouseMove = true;
        myWindow.title = "Edit Melee Attack";
        myWindow.baseAttack = attack;

        for (int i = 0; i < attack.listOfColors.Count; i++)
        {
            myWindow.colorIndex.Add(i);
        }

        for (int i = 0; i < attack.listOfTags.Count; i++)
        {
            myWindow.tagIndex.Add(i);
        }

        if (myWindow.baseAttack.attackQty < 2)
            myWindow.isMultipleAttack = false;
        else
            myWindow.isMultipleAttack = true;
        myWindow.Show();
    }

    private void OnGUI()
    {
        minSize = new Vector2(350, 350);
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true, GUIStyle.none, GUI.skin.verticalScrollbar);
        EditorGUILayout.LabelField("Edit Magic Attack", CustomStyles.titles);
        EditorGUILayout.Space();
        if (baseAttack != null)
        {
            EditorGUILayout.LabelField(baseAttack.attackName, CustomStyles.subtitles);
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            baseAttack.attackDescription = EditorGUILayout.TextField("Attack Description", baseAttack.attackDescription);
            EditorGUILayout.Space();
            baseAttack.isBuff = EditorGUILayout.Toggle("Is Buff?", baseAttack.isBuff);
            if (!baseAttack.isBuff)
            {
                EditorGUILayout.Space();
                baseAttack.baseDamage = EditorGUILayout.FloatField("Base Damage", baseAttack.baseDamage);
                if (baseAttack.baseDamage < 0)
                {
                    baseAttack.baseDamage = 0;
                }
            }
            else
                baseAttack.baseDamage = 0;
            EditorGUILayout.Space();
            baseAttack.manaCost = EditorGUILayout.FloatField("Mana Cost", baseAttack.manaCost);
            if (baseAttack.manaCost < 0)
            {
                baseAttack.manaCost = 0;
            }
            EditorGUILayout.Space();

            baseAttack.isAreaAttack = EditorGUILayout.Toggle("Is Area Attack?", baseAttack.isAreaAttack);
            EditorGUILayout.Space();

            isMultipleAttack = EditorGUILayout.Toggle("Is Multiple Attack?", isMultipleAttack);
            if (isMultipleAttack)
            {
                EditorGUILayout.Space();
                baseAttack.attackQty = EditorGUILayout.IntField("Attacks Quantity", baseAttack.attackQty);
                if (baseAttack.attackQty < 2)
                    baseAttack.attackQty = 2;
            }
            else
                baseAttack.attackQty = 1;
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
                colorIndex.Add(baseAttack.listOfColors.Count);
            }
            if (colorIndex.Count > 0)
            {
                if (GUILayout.Button("Remove Color"))
                {
                    baseAttack.listOfColors.RemoveAt(colorIndex.Count - 1);
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
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Add Tag"))
            {
                baseAttack.listOfTags.Add(AttackTagsEnum.Tags.SLASH);
                tagIndex.Add(baseAttack.listOfTags.Count);
            }
            if (colorIndex.Count > 0)
            {
                if (GUILayout.Button("Remove Tag"))
                {
                    baseAttack.listOfTags.RemoveAt(tagIndex.Count - 1);
                    tagIndex.RemoveAt(tagIndex.Count - 1);
                }
            }
            EditorGUILayout.EndHorizontal();

            foreach (int actualTag in tagIndex)
            {
                int index = tagIndex.IndexOf(actualTag);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Tag " + actualTag + ":");
                baseAttack.listOfTags[index] = (AttackTagsEnum.Tags)EditorGUILayout.EnumPopup(baseAttack.listOfTags[index]);
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Save Changes"))
            {
                Save();
            }
            if (GUILayout.Button("Return"))
            {
                AttackWindow.OpenWindow();
                Close();
            }
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(15);
        }
        EditorGUILayout.EndScrollView();
    }

    private void Save()
    {
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Close();
    }
}
