using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockBack : MonoBehaviour
{
	private Rigidbody2D rb;
	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();	
	}
	public void KnockBack(Transform playerTransform, float knockBackForce)
	{
		Vector2 direction = (transform.position - playerTransform.position).normalized;
		rb.velocity = direction * knockBackForce;
	}
}
