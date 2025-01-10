using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
	public GameObject player;
	private EnemyPathFinding enemy;
	public float distanceBetween;

	private float distance;

	private void Start()
	{
		enemy = GetComponent<EnemyPathFinding>();
	}
	private void Update()
	{
		distance = Vector2.Distance(transform.position, player.transform.position);
		Vector2 direction = player.transform.position - transform.position;
		direction.Normalize();
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

		if (distance < distanceBetween)
		{
			enemy.enabled = false;
			transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, enemy.moveSpeed * Time.deltaTime);
			//transform.rotation = Quaternion.Euler(Vector3.forward * angle);
		}
		else
			enemy.enabled = true;
	}
}
