using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveScore(int _score, int _tasksRepaired, string _levelName)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/score.fun";

        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data;

        if (!File.Exists(path)) {
            data = new PlayerData(_score, _tasksRepaired, _levelName);
        }
        else
        {
            data = loadScore();
            data.AddScore(_score, _tasksRepaired, _levelName);
        }

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SaveData(PlayerData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/score.fun";

        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData CreateScore()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/score.fun";

        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData();

        formatter.Serialize(stream, data);
        stream.Close();

        return data;
    }

    public static PlayerData loadScore()
    {
        string path = Application.persistentDataPath + "/score.fun";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogWarning("Data not found");
            return CreateScore();
        }
    }
}
