using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField, Tooltip("Отключение скелета")] private List<GameObject> disableList;
    [SerializeField] private List<GameObject> enableList;
    [SerializeField] private List<Rigidbody> rigidbodies;
    [SerializeField] private List<Transform> bounds;

    public void Die(Vector3 velocity)
    {
        for (int i = 0; i < disableList.Count; i++)
            disableList[i].SetActive(false);

        for (int i = 0; i < enableList.Count; i++)
            enableList[i].SetActive(true);

        for (int i = 0; i < rigidbodies.Count; i++)
        {
            rigidbodies[i].transform.position = bounds[i].transform.position;
            rigidbodies[i].transform.rotation = bounds[i].transform.rotation;
            rigidbodies[i].velocity = velocity;
        }
    }
}
