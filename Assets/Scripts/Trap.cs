using System;
using System.Collections;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public static Action<bool> ImCatch;

    [SerializeField] private float huntDistance;
    [SerializeField] private bool imHunting;
    [SerializeField] private Collider collide;

    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position - (imHunting ? transform.up * huntDistance : Vector3.zero);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) ImCatch(false);
    }

    #region Hunting & Hide
    #region Hunting
    public void Hunting()
    {
        imHunting = true;
        collide.enabled = true;
        StartCoroutine(MoveUp());
    }

    private IEnumerator MoveUp()
    {
        while (Vector3.Distance(transform.position, _startPosition) < huntDistance && imHunting)
        {
            transform.position += transform.up * Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
    #endregion

    #region Hide
    public void Hide()
    {
        imHunting = false;
        collide.enabled = false;
        StartCoroutine(MoveDown());
    }

    private IEnumerator MoveDown()
    {
        while (Vector3.Distance(transform.position, _startPosition) > 0.05f && !imHunting)
        {
            transform.position -= transform.up * Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
    #endregion
    #endregion
}
