using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System.IO;
public static class PlayerPresetsScript
{
    private static int slotNumber;
    private static string path = Application.dataPath + "/Save.json";
    private static JSONObject playerJson = (JSONObject)JSON.Parse(path);
    public static void CheckSave(int index)
    {

    }
}