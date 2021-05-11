using System;
using UnityEngine;

public class Savior : MonoBehaviour
{
    public static Action<bool> RestartLvl;

    private void OnCollisionEnter(Collision collision)
    {
        print(collision.transform.tag);
        if (collision.transform.CompareTag("Player")) RestartLvl?.Invoke(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.transform.tag);
        if (other.transform.CompareTag("Player")) RestartLvl?.Invoke(false);

    }
}
