using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Platform : MonoBehaviour
{
	private enum Type { Moving, Blinking, Falling }
	


	[Header("Moving")]
	[SerializeField] private bool canMove;
	[SerializeField, ConditionalHide(nameof(canMove))] private bool moveIsStart;
	[SerializeField, ConditionalHide(nameof(canMove))] private float movingRightDistance;
	[SerializeField, ConditionalHide(nameof(canMove))] private float movingTime;
	[SerializeField, ConditionalHide(nameof(canMove))] private float changeDirectionTime;

	[Header("Blinking")]
	[SerializeField] private bool canBlink;
	[SerializeField, ConditionalHide(nameof(canBlink))] private bool blinkIsStart;
	[SerializeField, ConditionalHide(nameof(canBlink))] private bool blinkOfTouch;
	[SerializeField, ConditionalHide(nameof(canBlink))] private float visibleTime;
	[SerializeField, ConditionalHide(nameof(canBlink))] private float invisibleTime;
	[SerializeField, ConditionalHide(nameof(canBlink))] private MeshRenderer meshRenderer;
	[SerializeField, ConditionalHide(nameof(canBlink))] private Material normalMaterial;
	[SerializeField, ConditionalHide(nameof(canBlink))] private Material blinkedMaterial;
	[SerializeField, ConditionalHide(nameof(canBlink))] private int countOfChangeMaterial;

	[Header("Reference")]
	[SerializeField, ConditionalHide(new string[] { nameof(canMove), nameof(canBlink) } )] private Collider bodyCollider;
	[SerializeField, ConditionalHide(new string[] { nameof(canMove), nameof(canBlink) } )] private BoxCollider triggerCollider;

	private List<Transform> _movingObjects;

    #region Start
    private void Start()
    {
		_movingObjects = new List<Transform>();

		SetTriggerSize();

		if (moveIsStart && canMove) Move();
		if (canBlink) meshRenderer.material = normalMaterial;
		if (blinkIsStart && canBlink) Blink();
	}

	private void SetTriggerSize()
    {
		Vector3 newSize = transform.localScale;
		newSize.x = (newSize.x + 1.5f) / newSize.x;
		newSize.y = (newSize.y + 1.5f) / newSize.y;
		newSize.z = (newSize.z + 1.5f) / newSize.z;

		triggerCollider.size = newSize;
    }
    #endregion

    #region Actions
    public void Activate()
	{
		if (canMove) Move();
		if (canBlink) Blink();
	}

	#region Moving
	public void Move(bool moveUp = true)
	{
		transform.DOMove(transform.position + transform.right * movingRightDistance * (moveUp ? 1 : -1), movingTime);
		StartCoroutine(ChangeDirection(moveUp));
	}

	private IEnumerator ChangeDirection(bool moveUp)
	{
		yield return new WaitForSeconds(changeDirectionTime);
		Move(!moveUp);
	}

	private void AddToMovingList(Transform transform)
	{
		if (!transform.parent)
		{
			transform.parent = this.transform;
			_movingObjects.Add(transform);
		}
	}

	private void DelAtMovingList(Transform transform)
	{
		transform.parent = null;
		_movingObjects.Remove(transform);
	}

	private void ClearMovingList()
	{
		if (_movingObjects.Count == 0) return;

		for (int i = 0; i < _movingObjects.Count; i++)
			_movingObjects[i].parent = null;

		_movingObjects.Clear();
	}
	#endregion

	#region Blinking
	public void Blink(bool visible = true, bool stoped = false)
	{
		meshRenderer.enabled = visible;
		bodyCollider.enabled = visible;
		triggerCollider.enabled = visible;

		if (!visible && canMove) ClearMovingList();

		if (!stoped)
		{
			if (visible) StartCoroutine(WaitToChangeMaterial());
			StartCoroutine(ChangeVisible(visible));
		}
	}

	private IEnumerator ChangeVisible(bool visible)
	{
		yield return new WaitForSeconds(visible ? visibleTime : invisibleTime);
		Blink(!visible, !visible && blinkOfTouch);
	}

	private IEnumerator WaitToChangeMaterial()
	{
		yield return new WaitForSeconds(visibleTime * 0.8f);
		StartCoroutine(ChangeMaterial(0, true));
	}

	private IEnumerator ChangeMaterial(int count, bool visible)
	{
		meshRenderer.material = visible ? normalMaterial : blinkedMaterial;
		yield return new WaitForSeconds(visibleTime * 0.1f / countOfChangeMaterial);
		if (count < countOfChangeMaterial)
			StartCoroutine(ChangeMaterial(visible ? count : ++count, !visible));
	}
	#endregion
	#endregion

	#region OnTrigger
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			if (canMove)
				AddToMovingList(other.transform);
			if (blinkOfTouch) Blink();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
			if (canMove)
				DelAtMovingList(other.transform);
	}
	#endregion
}
