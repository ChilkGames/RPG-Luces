using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LoadItemWindow : EditorWindow
{
    private BaseItem item;
    public List<int> statusesIndex = new List<int>();
    private Vector2 scrollPosition;

    public static void OpenWindow(BaseItem baseItem)
    {
        var myWindow = GetWindow<LoadItemWindow>();
        myWindow.item = baseItem;
        myWindow.wantsMouseMove = true;
        myWindow.title = "Edit Item";
        for (int i = 0; i < myWindow.item.statuses.Count; i++)
        {
            myWindow.statusesIndex.Add(i);
        }
        myWindow.Show();
    }

    private void OnGUI()
    {
        minSize = new Vector2(350, 350);
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true, GUIStyle.none, GUI.skin.verticalScrollbar);
        EditorGUILayout.LabelField("Edit Item", CustomStyles.titles);
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField(item.itemName, CustomStyles.subtitles);
        EditorGUILayout.Space();
        item.itemDescription = EditorGUILayout.TextField("Item Description", item.itemDescription);
        EditorGUILayout.Space();
        item.price = EditorGUILayout.IntField("Item Price", item.price);
        if (item.price < 0)
        {
            item.price = 0;
        }
        EditorGUILayout.Space();
        item.useOnEnemies = EditorGUILayout.Toggle("Use on Enemies", item.useOnEnemies);
        if (item.useOnEnemies)
            item.damage = EditorGUILayout.IntField("Damage to deal", item.damage);
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Status"))
        {
            statusesIndex.Add(statusesIndex.Count);
            item.statuses.Add(StatusEnum.Status.HEALING);
            item.percentages.Add(0);
        }
        if (statusesIndex.Count > 0)
        {
            if (GUILayout.Button("Remove Status"))
            {
                item.statuses.RemoveAt(statusesIndex.Count - 1);
                item.percentages.RemoveAt(statusesIndex.Count - 1);
                statusesIndex.RemoveAt(statusesIndex.Count - 1);
            }
        }
        EditorGUILayout.EndHorizontal();

        foreach (var actualStatus in statusesIndex)
        {
            int index = statusesIndex.IndexOf(actualStatus);
            EditorGUILayout.BeginHorizontal();
            item.statuses[index] = (StatusEnum.Status)EditorGUILayout.EnumPopup("Status to inflict: ", item.statuses[index]);
            item.percentages[index] = EditorGUILayout.IntField(item.percentages[index]);
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Save Changes"))
        {
            if (item.itemName != null)
                Save();
            else
                ShowError("Item must have a name");
        }
        if (GUILayout.Button("Cancel"))
        {
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
