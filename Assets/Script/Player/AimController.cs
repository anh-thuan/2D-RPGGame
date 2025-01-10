using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
public class AimController : MonoBehaviour
{
	private Transform aimTransform;
	[HideInInspector] public Vector3 mousePosition;
	[HideInInspector] public Vector3 aimDirection;
	private void Awake()
	{
		aimTransform = transform.Find("Aim");
	}

	private void Update()
	{
		HandleAiming();
	}

	private void HandleAiming()
	{
		mousePosition = UtilsClass.GetMouseWorldPosition();
		aimDirection = (mousePosition - transform.position).normalized;
		float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
		aimTransform.eulerAngles = new Vector3(0, 0, angle);

		Vector3 aimLocalScale = Vector3.one;
		if (angle > 90 || angle < -90)
		{
			aimLocalScale.y = -1f;
		}
		else
		{
			aimLocalScale.y = +1f;
		}
		//Other turning to change Idle
		// Debug.Log with corrected conditions
		if (angle >= 45 && angle <= 135)
		{
			//Debug.Log("Top");
		}
		else if (angle <= -45 && angle >= -135)
		{
			//Debug.Log("Down");
		}
		else if (angle > 135 || angle < -135)
		{
			//Debug.Log("Left");
		}
		else if (angle > -45 && angle < 45)
		{
			//Debug.Log("Right");
		}
		aimTransform.localScale = aimLocalScale;
	}
}
