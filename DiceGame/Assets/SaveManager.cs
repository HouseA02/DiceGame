using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveManager{

    public static void SaveData(Player player)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/ttdData.dat";
        FileStream stream = new FileStream(path, FileMode.Create);
        SaveData data = new SaveData(player);
        binaryFormatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Saved to " + path);
    }
    
    public static SaveData LoadData()
    {
        string path = Application.persistentDataPath + "/ttdData.dat";
        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            SaveData data = (SaveData)binaryFormatter.Deserialize(stream);
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("No save found in " + path);
            return null;
        }
    }

}
