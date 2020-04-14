using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EquipamentWindow : EditorWindow
{
    [MenuItem("RPG Tools/Equipement Creator")]
    public static void OpenWindow()
    {
        var myWindow = GetWindow<EquipamentWindow>();
        myWindow.wantsMouseMove = true;
        myWindow.title = "Equipement Creator";
        myWindow.Show();
    }

    private void OnGUI()
    {
        
    }
}
