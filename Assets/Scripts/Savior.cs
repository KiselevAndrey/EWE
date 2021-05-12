using System;
using UnityEngine;

public class Savior : MonoBehaviour
{
    public static Action<bool> RestartLvl;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player")) RestartLvl?.Invoke(false);
    }
}
