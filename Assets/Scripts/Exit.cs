using UnityEngine;
using UnityEngine.EventSystems;

public class Exit : MonoBehaviour
{
    public EventSystem finish;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
        }
    }
}
