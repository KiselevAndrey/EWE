using UnityEngine;

public class Looking : MonoBehaviour
{
    [Header("Looking angles")]
    [SerializeField] private float minAngle;
    [SerializeField] private float maxAngle;

    [Header("Sensitivity")]
    [SerializeField] private float sensitivity;

    private float _angle = 0;

    void Update()
    {
        _angle -= Input.GetAxis("Mouse Y") * sensitivity;
        _angle = Mathf.Clamp(_angle, minAngle, maxAngle);

        transform.localEulerAngles = new Vector3(_angle, 0);
    }
}
