using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveManagerSection1 
{
    public static void Save(GameDataSection1 data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fs = new FileStream(GetPath(), FileMode.Create);
        formatter.Serialize(fs, data);
        fs.Close();
    }

    public static GameDataSection1 Load()
    {
        if (!File.Exists(GetPath()))
        {
            GameDataSection1 emptyData = new GameDataSection1();
            Save(emptyData);
            return emptyData;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fs = new FileStream(GetPath(), FileMode.Open);
        GameDataSection1 data = formatter.Deserialize(fs) as GameDataSection1;
        fs.Close();

        return data;
    }
    private static string GetPath()
    {
        return Application.persistentDataPath + "/section1.txt";
    }
}
