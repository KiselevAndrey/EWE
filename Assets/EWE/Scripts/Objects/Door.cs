using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Parametrs")]
    [SerializeField] private bool imClosed;
    [SerializeField] private float openDistance;
    [SerializeField] private float openSpeed;
    [SerializeField] private float closeSpeed;

    [Header("Reference")]
    [SerializeField] private MeshRenderer meshRenderer;

    [Header("Materials")]
    [SerializeField] private Material closeMaterial;
    [SerializeField] private Material openMaterial;

    private Vector3 _openPosition;
    private Vector3 _closePosition;

    private void Start()
    {
        meshRenderer.material = imClosed ? closeMaterial : openMaterial;
        _openPosition = imClosed ? NewPosition(-openDistance) : transform.position;
        _closePosition = imClosed ? transform.position : NewPosition(openDistance);
    }

    #region Open Close
    public void ChangeStatus()
    {
        imClosed = !imClosed;
        StartCoroutine(imClosed? Closing() : Opening());
    }

    public void Open()
    {
        imClosed = false;
        StartCoroutine(Opening());
    }

    public void Close()
    {
        imClosed = true;
        StartCoroutine(Closing());
    }
    #endregion

    #region Helping Func
    private Vector3 NewPosition(float speed) => transform.position + transform.up * speed;

    private IEnumerator Opening()
    {
        float minDistance = openDistance * 1.5f;

        while (Vector3.Distance(transform.position, _openPosition) <= minDistance && !imClosed)
        {
            minDistance = Vector3.Distance(transform.position, _openPosition);
            transform.position -= transform.up * openSpeed * Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    private IEnumerator Closing()
    {
        float minDistance = openDistance * 1.5f;
        while (Vector3.Distance(transform.position, _closePosition) <= minDistance && imClosed)
        {
            minDistance = Vector3.Distance(transform.position, _closePosition);
            transform.position += transform.up * closeSpeed * Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
    #endregion
}
