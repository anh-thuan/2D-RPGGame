using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
	public int damage;
	public bool attack = true;
	public float attackCoolDown;
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			if (attack)
			{
				attack = false;
				IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
				if (damageable != null) 
				{
					damageable.Damage(damage);
				}
				StartCoroutine(AttackCoolDown());
			}
		}
	}

	IEnumerator AttackCoolDown()
	{
		yield return new WaitForSeconds(attackCoolDown); 
		attack = true; 
	}
}
