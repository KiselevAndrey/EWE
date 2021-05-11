using System;
using UnityEngine;
using UnityEngine.Events;

public class Exit : MonoBehaviour
{
    //[SerializeField] private UnityEvent finish;

    public static Action<bool> Finish;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Finish?.Invoke(true);
        }
    }
}
