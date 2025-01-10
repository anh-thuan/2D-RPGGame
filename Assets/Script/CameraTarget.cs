using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
	[SerializeField] public Camera cam;
	[SerializeField] public Transform player;
	[SerializeField] public float threeHold;

	void Update()
	{
		Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
		Vector3 targetPos = (player.position + mousePos) / 2f;

		targetPos.x = Mathf.Clamp(targetPos.x, -threeHold + player.position.x, threeHold + player.position.x);
		targetPos.y = Mathf.Clamp(targetPos.y, -threeHold + player.position.y, threeHold + player.position.y);

		this.transform.position = targetPos;
	}
}
