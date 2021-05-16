using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Platform : MonoBehaviour
{

	[Header("Moving")]
	[SerializeField] private bool canMove;
	[SerializeField] private bool moveIsStart;
	[SerializeField] private float movingUpDistance;
	[SerializeField] private float movingTime;
	[SerializeField] private float stopTime;

	[Header("Blinking")]
	[SerializeField] private bool canBlink;
	[SerializeField] private bool blinkIsStart;
	[SerializeField] private float visibleTime;
	[SerializeField] private float invisibleTime;
	[SerializeField] private MeshRenderer meshRenderer;
	[SerializeField] private Collider bodyCollider;
	[SerializeField] private Collider triggerCollider;

	//[Header("Additionaly Reference")]

	private List<Transform> movingObjects;

	private void Start()
	{
		movingObjects = new List<Transform>();

		if(moveIsStart && canMove) Move(true);
		if(blinkIsStart && canBlink) Blink(true);
	}

	#region Moving
	public void Move(bool moveUp)
	{
		transform.DOMove(transform.position + transform.up * movingUpDistance * (moveUp ? 1 : -1), movingTime);
		StartCoroutine(ChangeDirection(moveUp));
	}

	private IEnumerator ChangeDirection(bool moveUp)
	{
		yield return new WaitForSeconds(stopTime);
		Move(!moveUp);
	}

	private void AddToMovingList(Transform transform)
    {
		transform.parent = this.transform;
		movingObjects.Add(transform);
    }

	private void DelAtMovingList(Transform transform)
    {
		transform.parent = null;
		movingObjects.Remove(transform);
    }

	private void ClearMovingList()
    {
		if (movingObjects.Count == 0) return;

		for (int i = 0; i < movingObjects.Count; i++)
			movingObjects[i].parent = null;

		movingObjects.Clear();
    }
	#endregion

	#region Blinking
	public void Blink(bool visible)
	{
		meshRenderer.enabled = visible;
		bodyCollider.enabled = visible;
		triggerCollider.enabled = visible;

		if (!visible && canMove) ClearMovingList();

		StartCoroutine(ChangeVisible(visible));
	}

	private IEnumerator ChangeVisible(bool visible)
	{
		yield return new WaitForSeconds(visible ? visibleTime : invisibleTime);
		Blink(!visible);
	}
	#endregion

	#region OnTrigger
	private void OnTriggerEnter(Collider other)
	{
		if(canMove)
			if (other.gameObject.CompareTag("Player"))
				AddToMovingList(other.transform);
	}

	private void OnTriggerExit(Collider other)
	{
		if (canMove)
			if (other.gameObject.CompareTag("Player"))
				DelAtMovingList(other.transform);
	}
	#endregion
}
