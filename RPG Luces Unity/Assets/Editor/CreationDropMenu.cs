using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreationDropMenu : Editor
{
    [ContextMenu("Creator")]
    public static void Test()
    {
        Debug.Log("Test");
    }
}
