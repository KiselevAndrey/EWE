using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    [SerializeField] List<Platform> platforms;

    public void Activate()
    {
        for (int i = 0; i < platforms.Count; i++)
        {
            platforms[i].Activate();
        }
    }
}
