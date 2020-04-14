using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AttackWindow : EditorWindow
{
    BaseAttack meleeAttack;
    BaseAttack magicAttack;
    BaseAttack combinationAttack;

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
        }
        GUILayout.Space(22);
        EditorGUI.DrawRect(GUILayoutUtility.GetRect(100, 2), Color.black);
        GUILayout.Space(20);
        EditorGUILayout.LabelField("Edition area", CustomStyles.subtitles);
        GUILayout.Space(10);
        if (GUILayout.Button("Edit Melee Attack"))
        {
            LoadMeleeWindow.OpenWindow();
        }
        GUILayout.Space(10);
        if (GUILayout.Button("Edit Magic Attack"))
        {
            meleeAttack = (BaseAttack)EditorGUILayout.ObjectField("Attack", meleeAttack, typeof(BaseAttack), false);
            Debug.Log("Cargo magia");
        }
        GUILayout.Space(10);
        if (GUILayout.Button("Edit Combination Attack"))
        {
            meleeAttack = (BaseAttack)EditorGUILayout.ObjectField("Attack", meleeAttack, typeof(BaseAttack), false);
            Debug.Log("Cargo combinacion");
        }
    }
}
