using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LVLSO")]
public class LVLSO : ScriptableObject
{
    public int currentLVL;
    public int maxLVL;

    public List<GameObject> LVLObjects;

    public GameObject GetCurrentLVL()
    {
        if (HaveCurentLVL()) return LVLObjects[currentLVL];
        return null;
    }

    public bool HaveCurentLVL() => LVLObjects.Count > currentLVL;

    public void CheckMaxLVL(int lvl)
    {
        lvl = Mathf.Min(lvl, LVLObjects.Count);

        maxLVL = Mathf.Max(maxLVL, lvl);
    }

    public void CheckMaxLVL() => CheckMaxLVL(currentLVL);
}
