using System;
using System.Collections;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public static Action<bool> ImCatch;

    [SerializeField] private float huntDistance;

    private Vector3 _startPosition;
    bool _imHunting;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.tag);
        if (other.gameObject.CompareTag("Player")) ImCatch(false);
    }

    #region Hunting & Hide
    #region Hunting
    public void Hunting()
    {
        _imHunting = true;
        StartCoroutine(MoveUp());
    }

    private IEnumerator MoveUp()
    {
        while (Vector3.Distance(transform.position, _startPosition) < huntDistance && _imHunting)
        {
            transform.position += transform.up * Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
    #endregion

    #region Hide
    public void Hide()
    {
        _imHunting = false;
        StartCoroutine(MoveDown());
    }

    private IEnumerator MoveDown()
    {
        while (Vector3.Distance(transform.position, _startPosition) > 0.05f && _imHunting)
        {
            transform.position -= transform.up * Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
    #endregion
    #endregion
}
