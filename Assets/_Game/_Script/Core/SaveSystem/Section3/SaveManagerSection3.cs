using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveManagerSection3 
{
    public static void Save(GameDataSection3 data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fs = new FileStream(GetPath(), FileMode.Create);
        formatter.Serialize(fs, data);
        fs.Close();
    }

    public static GameDataSection3 Load()
    {
        if (!File.Exists(GetPath()))
        {
            GameDataSection3 emptyData = new GameDataSection3();
            Save(emptyData);
            return emptyData;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fs = new FileStream(GetPath(), FileMode.Open);
        GameDataSection3 data = formatter.Deserialize(fs) as GameDataSection3;
        fs.Close();

        return data;
    }
    private static string GetPath()
    {
        return Application.persistentDataPath + "/section3.txt";
    }
}
