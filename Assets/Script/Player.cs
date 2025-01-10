using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : HealthController
{
	[Header("Health UI")]
	public Image healthBar;
	public Image armorBar;
	public override void IncreaseHealth(int health)
	{
		base.IncreaseHealth(health);
	}

	public override void IncreaseArmor(int armor)
	{
		base.IncreaseArmor(armor);
	}

	public override void Hurt()
	{
		base.Hurt();
	}

	public override void Dealth()
	{
		base.Dealth();
		GameObject.Find("Player").GetComponent<MoveController>().moveSpeed = 0;
		GameObject.Find("Player").GetComponent<MoveController>().enabled = false;
		GameObject.Find("Player").GetComponent<AimController>().enabled = false;
		GameObject.Find("Player").GetComponent<WeaponController>().enabled = false;
	}

	public override void HealthAndArmorStart()
	{
		currentHealth = maxHealth;
	}

	public override void UpdateStatusUI()
	{
		base.UpdateStatusUI();
		if (healthBar != null)
		{
			healthBar.fillAmount = (float)currentHealth / maxHealth;
		}
		if (armorBar != null)
		{
			armorBar.fillAmount = (float)currentArmor / maxArmor;
		}
	}
}
