using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private enum Direction { Up, Down, Left, Right, Forward, Back }

    [Header("Parametrs")]
    [SerializeField] private Direction openDirection;
    [SerializeField] private bool imClosed;
    [SerializeField] private float openDistance;
    [SerializeField] private float openSeconds;
    [SerializeField] private float closeSeconds;

    [Header("Reference")]
    [SerializeField] private MeshRenderer meshRenderer;

    [Header("Materials")]
    [SerializeField] private Material closeMaterial;
    [SerializeField] private Material openMaterial;

    private int _countIteration;
    private Vector3 _closePosition;

    private void Start()
    {
        meshRenderer.material = imClosed ? closeMaterial : openMaterial;
        _closePosition = imClosed ? transform.position : NewPosition(GetDirection(openDirection, im)
    }

    public void ChangeStatus()
    {
        imClosed = !imClosed;
        StartCoroutine(Moving());
    }

    public void Open()
    {
        imClosed = false;
    }

    public void Close()
    {
        imClosed = true;
    }

    private IEnumerator Moving()
    {
        yield return new WaitForSeconds(Time.deltaTime);
    }

    #region Helping Func
    private Vector3 NewPosition(Direction direction, float speed)
    {
        Vector3 temp = transform.position;

        switch (direction)
        {
            case Direction.Up:
                temp.y += speed;
                break;

            case Direction.Down:
                temp.y -= speed;
                break;

            case Direction.Left:
                temp.x -= speed;
                break;

            case Direction.Right:
                temp.x += speed;
                break;

            case Direction.Forward:
                temp.z += speed;
                break;

            case Direction.Back:
                temp.z -= speed;
                break;
        }

        return temp;
    }

    private Direction GetDirection()
    {
        return GetDirection(openDirection, !imClosed);
    }

    private Direction GetDirection(Direction direction, bool trueDirection)
    {
        if (trueDirection) return direction;

        switch (direction)
        {
            case Direction.Up:
                return Direction.Down;

            case Direction.Down:
                return Direction.Up;

            case Direction.Left:
                return Direction.Right;

            case Direction.Right:
                return Direction.Left;

            case Direction.Forward:
                return Direction.Back;

            case Direction.Back:
                return Direction.Forward;

            default:
                return direction;
        }
    }

    private float GetSpeed(float speedModifier)
    {
        return (imClosed ? closeSeconds : openSeconds) * speedModifier;
    }
    #endregion
}
