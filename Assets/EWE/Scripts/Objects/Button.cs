using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    [Header("Parametrs")]
    [SerializeField] private bool onlyPress;
    [SerializeField] private float timeToRelease;

    [Header("Materials")]
    [SerializeField] private Material onReleasedMaterial;
    [SerializeField] private Material onPressedMaterial;

    [Header("Reference")]
    [SerializeField] private MeshRenderer meshRenderer;

    [Header("Event")]
    [SerializeField] private UnityEvent OnPressed;
    [SerializeField] private UnityEvent OnReleased;

    Vector3 _startPosition;
    bool _imPressed;
    int _countRelease;

    private void Start()
    {
        meshRenderer.material = onReleasedMaterial;
        _startPosition = transform.position;
    }

    #region Press Release
    #region Press
    private void Press()
    {
        _imPressed = true;
        StartCoroutine(MoveDown());
        meshRenderer.material = onPressedMaterial;
        
        OnPressed.Invoke();
    }
    private IEnumerator MoveDown()
    {
        while (Vector3.Distance(transform.position, _startPosition) < 0.45f * transform.localScale.y)
        {
            transform.position -= transform.up * Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
    #endregion

    #region Release
    private void Release()
    {
        _imPressed = false;
        _countRelease++;
        StartCoroutine(WaitRelease(_countRelease));
    }

    private IEnumerator WaitRelease(int releaseCount)
    {
        yield return new WaitForSeconds(timeToRelease);
        if (!_imPressed && releaseCount == _countRelease)
        {
            StartCoroutine(MoveUp());
            meshRenderer.material = onReleasedMaterial;

            OnReleased.Invoke();
        }
    }

    private IEnumerator MoveUp()
    {
        while(Vector3.Distance(transform.position, _startPosition) > 0.1f && !_imPressed)
        {
            transform.position += transform.up * Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
    #endregion
    #endregion

    #region OnTrigger
    private void OnTriggerEnter(Collider other)
    {
        Press();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!onlyPress) Release();
    }
    #endregion
}
