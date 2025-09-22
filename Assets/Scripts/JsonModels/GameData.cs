//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Newtonsoft.Json;
//using System.IO;

//[System.Serializable]
//public class GameData
//{
//    public int level;
//    public bool soundOn;
//    public bool needPlaySoundClick;

//    static string fileName = "GameData.json";
//    static string path;

//    public static void ToJson(GameData gameData)
//    {
//        string data = JsonConvert.SerializeObject(gameData, Formatting.Indented);
//        File.WriteAllText(path, data);
//    }

//    public static GameData FromJson()
//    {
//        path = Path.Combine(Application.streamingAssetsPath, fileName);
//        string data = File.ReadAllText(path);
//        return JsonConvert.DeserializeObject<GameData>(data);
//    }
//}
