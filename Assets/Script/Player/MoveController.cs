using System;
using System.Collections;
using UnityEngine;

public class MoveController : MonoBehaviour
{
	[Header("Component")]
	[HideInInspector] public Rigidbody2D rb;
	[HideInInspector] public Animator animator;
	[HideInInspector] public TrailRenderer tr;

	[Header("Move")]
	public float moveSpeed;
	[HideInInspector] public float x, y;
	private Vector3 moveDirection;

	[Header("Dash")]
	public bool canDash;
	private bool isDashing;
	private bool readyToDash = true;
	public float dashPower;
	public float dashFinishTime;
	public float dashCoolDownTime;

	//Vector2 movement;
	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		tr = GetComponent<TrailRenderer>();
	}
	private void Update()
	{
		x = Input.GetAxisRaw("Horizontal") * moveSpeed;
		y = Input.GetAxisRaw("Vertical") * moveSpeed;

		Move();
		Dash();
	}

	private void Move()
	{
		bool isIdle = x == 0 && y == 0;
		if (isIdle)
		{
			animator.SetBool("isMoving", false);
			rb.velocity = Vector2.zero;
			moveDirection = rb.velocity;
		}
		else
		{
			animator.SetBool("isMoving", true);
			animator.SetFloat("x", rb.velocity.x);
			animator.SetFloat("y", rb.velocity.y);
			rb.velocity = new Vector2(x, y);
			moveDirection = rb.velocity;
		}
	}

	public void Dash()
	{
		if (!canDash)
		{
			return;
		}
		else
		{
			if (Input.GetKeyDown(KeyCode.LeftShift))
			{
				isDashing = true;
				if (isDashing && readyToDash)
				{
					rb.MovePosition(transform.position + moveDirection.normalized * dashPower);
					tr.emitting = true;
					isDashing = false;
					StartCoroutine(DashTime());
				}
				else
					return;
			}
		}
	}

	IEnumerator DashTime()
	{
		yield return new WaitForSeconds(dashFinishTime);
		tr.emitting = false;
		readyToDash = false;
		yield return new WaitForSeconds(dashCoolDownTime);
		readyToDash = true;
		Debug.Log("Ready To Dash");
	}
}


