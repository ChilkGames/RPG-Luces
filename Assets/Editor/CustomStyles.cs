using UnityEngine;

public static class CustomStyles
{
    public static GUIStyle bold = new GUIStyle()
    {
        fontStyle = FontStyle.Bold
    };

    public static GUIStyle titles = new GUIStyle()
    {
        fontStyle = FontStyle.BoldAndItalic,
        fontSize = 15,
        alignment = TextAnchor.MiddleCenter
    };

    public static GUIStyle subtitles = new GUIStyle()
    {
        fontStyle = FontStyle.Bold,
        fontSize = 12,
        alignment = TextAnchor.MiddleLeft
    };
}
