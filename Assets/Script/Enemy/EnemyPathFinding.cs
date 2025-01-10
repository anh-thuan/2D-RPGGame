using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathFinding : MonoBehaviour
{
	public float moveSpeed = 2f;

	private Rigidbody2D rb;
	private Vector2 moveDir;

	private void Awake()
	{
		rb= GetComponent<Rigidbody2D>();
	}
	private void Update()
	{
		rb.MovePosition(rb.position + moveDir * (moveSpeed* Time.fixedDeltaTime));
	}
	public void MoveTo(Vector2 targetPosition)
	{
		moveDir = targetPosition;
	}
}
