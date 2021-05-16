using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkedPlatform : Platform
{
	[Header("Blinking")]
	[SerializeField] private bool blinkIsStart;
	[SerializeField] private float visibleTime;
	[SerializeField] private float invisibleTime;
	[SerializeField] private MeshRenderer meshRenderer;
	[SerializeField] private Collider bodyCollider;

	private void Start()
	{
		if (blinkIsStart) Activate(true);
	}

	#region Blinking
	public new void Activate(bool visible)
	{
		meshRenderer.enabled = visible;
		bodyCollider.enabled = visible;

		StartCoroutine(ChangeVisible(visible));
	}

	private IEnumerator ChangeVisible(bool visible)
	{
		yield return new WaitForSeconds(visible ? visibleTime : invisibleTime);
		Activate(!visible);
	}
	#endregion
}
