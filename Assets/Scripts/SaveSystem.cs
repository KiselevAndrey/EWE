using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem
{
    #region lvlSO
    static readonly string lvlSOPath = "/lvlSO.st";

    public static void SaveLVLSO(LVLSO lvlSO)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + lvlSOPath;
        FileStream stream = new FileStream(path, FileMode.Create);

        LVLsData data = new LVLsData(lvlSO);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static LVLsData LoadLVLSO()
    {
        string path = Application.persistentDataPath + lvlSOPath;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            LVLsData data = formatter.Deserialize(stream) as LVLsData;

            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("File is not found. Path: " + path);
            return null;
        }
    }
    #endregion
}

#region LVLsData
[System.Serializable]
public class LVLsData
{
    readonly int maxLVL;

    public LVLsData(LVLSO lvlSO)
    {
        maxLVL = lvlSO.maxLVL;
    }

    public void LoadData(ref LVLSO lvlSO)
    {
        lvlSO.maxLVL = maxLVL;
    }
}
#endregion
