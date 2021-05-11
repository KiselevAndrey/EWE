using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    [Header("Parametrs")]
    [SerializeField] private bool onlyPress;

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

    private void Start()
    {
        meshRenderer.material = onReleasedMaterial;
        _startPosition = transform.position;
    }

    #region Press Release
    private void Press()
    {
        _imPressed = true;
        StartCoroutine(MoveDown());
        meshRenderer.material = onPressedMaterial;
        
        OnPressed.Invoke();
    }

    private void Release()
    {
        _imPressed = false;
        StartCoroutine(MoveUp());
        meshRenderer.material = onReleasedMaterial;

        OnReleased.Invoke();
    }

    private IEnumerator MoveDown()
    {
        while(Vector3.Distance(transform.position, _startPosition) < 0.45f * transform.localScale.y && _imPressed)
        {
            transform.position -= transform.up * Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
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
