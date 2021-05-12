using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LVLSO")]
public class LVLSO : ScriptableObject
{
    public int currentLVL;

    public List<GameObject> LVLObjects;

    public GameObject GetCurrentLVL()
    {
        if (HaveCurentLVL()) return LVLObjects[currentLVL];
        return null;
    }

    public bool HaveCurentLVL() => LVLObjects.Count > currentLVL;
}
