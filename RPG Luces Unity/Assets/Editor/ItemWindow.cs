using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ItemWindow : EditorWindow
{
    public string itemName;
    public string itemDescription;
    public int price;

    public bool useOnEnemies;

    public List<int> statusesIndex = new List<int>();
    public List<StatusEnum.Status> statuses = new List<StatusEnum.Status>();
    public List<int> statusesPercentage = new List<int>();

    public int damage;
    public int restore;

    private Vector2 scrollPosition;

    [MenuItem("RPG Tools/Item Creator")]
    public static void OpenWindow()
    {
        var myWindow = GetWindow<ItemWindow>();
        myWindow.wantsMouseMove = true;
        myWindow.title = "Create Item";
        myWindow.Show();
    }

    private void OnGUI()
    {
        minSize = new Vector2(350, 350);
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true, GUIStyle.none, GUI.skin.verticalScrollbar);
        EditorGUILayout.LabelField("New Item", CustomStyles.titles);
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        itemName = EditorGUILayout.TextField("Item Name", itemName);
        EditorGUILayout.Space();
        itemDescription = EditorGUILayout.TextField("Item Description", itemDescription);
        EditorGUILayout.Space();
        price = EditorGUILayout.IntField("Item Price", price);
        if (price<0)
        {
            price = 0;
        }
        EditorGUILayout.Space();
        useOnEnemies = EditorGUILayout.Toggle("Use on Enemies", useOnEnemies);
        if (useOnEnemies)
            damage = EditorGUILayout.IntField("Damage to deal", damage);
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Status"))
        {
            statusesIndex.Add(statusesIndex.Count);
            statuses.Add(StatusEnum.Status.HEALING);
            statusesPercentage.Add(0);
        }
        if (statusesIndex.Count > 0)
        {
            if (GUILayout.Button("Remove Status"))
            {
                statuses.RemoveAt(statusesIndex.Count - 1);
                statusesPercentage.RemoveAt(statusesIndex.Count - 1);
                statusesIndex.RemoveAt(statusesIndex.Count - 1);
            }
        }
        EditorGUILayout.EndHorizontal();

        foreach (var actualStatus in statusesIndex)
        {
            int index = statusesIndex.IndexOf(actualStatus);
            EditorGUILayout.BeginHorizontal();
            statuses[index] = (StatusEnum.Status)EditorGUILayout.EnumPopup("Status to inflict: ", statuses[index]);
            statusesPercentage[index] = EditorGUILayout.IntField(statusesPercentage[index]);
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Create"))
        {
            if (itemName != null)
                CreateItem();
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

    private void CreateItem()
    {
        var scriptableAttack = CreateInstance<BaseItem>();
        var path = AssetDatabase.GenerateUniqueAssetPath("Assets/Scripts/Items/" + itemName + ".asset");
        scriptableAttack.itemName = itemName;
        scriptableAttack.itemDescription = itemDescription;
        scriptableAttack.price = price;
        if (useOnEnemies)
            scriptableAttack.damage = damage;       
        else       
            scriptableAttack.damage = 0;       
        if (statusesIndex.Count>0)
        {
            foreach (int actualStatus in statusesIndex)
            {
                int index = statusesIndex.IndexOf(actualStatus);
                scriptableAttack.statuses.Add(statuses[index]);
                scriptableAttack.percentages.Add(statusesPercentage[index]);
            }
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
