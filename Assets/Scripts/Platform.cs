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
	[SerializeField] private float blinkTime;
	[SerializeField] private MeshRenderer meshRenderer;
	[SerializeField] private Collider bodyCollider;

	//[Header("Additionaly Reference")]
	//[SerializeField] private Collider triggerCollider;

	private void Start()
	{
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
	#endregion

	#region Blinking
	public void Blink(bool visible)
	{
		meshRenderer.enable(visible);
		bodyCollider.enable(visible);
	}

	private IEnumerator ChangeVisible(bool visible)
	{
		yield return new WaitForSeconds(blinkTime);
		Blinck(!visible);
	}
	#endregion

	#region OnTrigger
	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
			other.gameObject.transform.parent = this.transform;
	}

	private void OnTriggerExit(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
			other.gameObject.transform.parent = null;
	}
	#endregion
}
