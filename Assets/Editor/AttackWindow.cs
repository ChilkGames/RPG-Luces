using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AttackWindow : EditorWindow
{
    private BaseAttack attack;

    [MenuItem("RPG Tools/Attack Creator")]
    public static void OpenWindow()
    {
        var myWindow = GetWindow<AttackWindow>();
        myWindow.wantsMouseMove = true;
        myWindow.title = "Attack Creator";
        myWindow.Show();
    }

    private void OnGUI()
    {
        minSize = new Vector2(300, 300);
        maxSize = new Vector2(300, 300);
        EditorGUILayout.LabelField("Creation area",CustomStyles.subtitles);
        GUILayout.Space(10);
        if (GUILayout.Button("Create Melee Attack"))
        {
            MeleeAttackWindow.OpenWindow();
            Close();
        }
        GUILayout.Space(10);
        if (GUILayout.Button("Create Magic Attack"))
        {
            MagicAttackWindow.OpenWindow();
            Close();
        }
        GUILayout.Space(10);
        if (GUILayout.Button("Create Combination Attack"))
        {
            CombinationAttackWindow.OpenWindow();
            Close();
        }
        GUILayout.Space(22);
        EditorGUI.DrawRect(GUILayoutUtility.GetRect(100, 2), Color.black);
        GUILayout.Space(20);
        EditorGUILayout.LabelField("Edition area", CustomStyles.subtitles);
        GUILayout.Space(10);
        attack = (BaseAttack)EditorGUILayout.ObjectField("Attack", attack, typeof(BaseAttack), false);
        if (attack!=null)
        {
            if (attack.isCombination)
            {
                if (GUILayout.Button("Edit Combination Attack"))
                {
                    LoadCombinationAttack.OpenWindow((CombinationAttack)attack);
                    Close();
                }
            }
            else
            {
                if (attack.isMeleeAttack)
                {
                    if (GUILayout.Button("Edit Melee Attack"))
                    {
                        LoadMeleeWindow.OpenWindow(attack);
                        Close();
                    }
                }
                else
                {
                    if (GUILayout.Button("Edit Magic Attack"))
                    {
                        LoadMagicWindow.OpenWindow(attack);
                        Close();
                    }
                }
            }
        }
    }
}
