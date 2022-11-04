using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GraphicsSaveSystem
{
    public static void SetGraphics(int level)
    {
        string id = $"Graphics";
        PlayerPrefs.SetInt(id, level);
    }

    public static int GetGraphicsLevel()
    {
        string id = $"Graphics";
        return PlayerPrefs.GetInt(id, 0);
    }
}
