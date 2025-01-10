using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : HealthController
{
	[Header("Enemy Other")]
	public EnemyPathFinding pathFinding;
	public CapsuleCollider2D cap;
	public override void Hurt()
	{
		base.Hurt();
	}

	public override void Dealth()
	{
		base.Dealth();
		pathFinding.moveSpeed = 0;
		cap.enabled = false;
		StartCoroutine(EnemyDisappear());
	}

	public override void HealthAndArmorStart()
	{
		currentHealth = maxHealth;
		currentArmor = maxArmor;
	}
	IEnumerator EnemyDisappear()
	{
		yield return new WaitForSeconds(1f);
		Destroy(gameObject);
	}
}
