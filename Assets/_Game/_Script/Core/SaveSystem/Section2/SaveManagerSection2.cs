using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManagerSection2 
{
    public static void Save(GameDataSection2 data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fs = new FileStream(GetPath(), FileMode.Create);
        formatter.Serialize(fs, data);
        fs.Close();
    }

    public static GameDataSection2 Load()
    {
        if (!File.Exists(GetPath()))
        {
            GameDataSection2 emptyData = new GameDataSection2();
            Save(emptyData);
            return emptyData;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fs = new FileStream(GetPath(), FileMode.Open);
        GameDataSection2 data = formatter.Deserialize(fs) as GameDataSection2;
        fs.Close();

        return data;
    }
    private static string GetPath()
    {
        return Application.persistentDataPath + "/section2.txt";
    }
}
