using UnityEngine;
using UnityEngine.Events;

public class Exit : MonoBehaviour
{
    [SerializeField] private UnityEvent finish;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            finish.Invoke();
        }
    }
}
