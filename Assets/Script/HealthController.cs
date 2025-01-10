using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour, IDamageable
{
	[Header("Health")]
	public int currentHealth;
	public int maxHealth;

	[Header("Armor")]
	public int currentArmor;
	public int maxArmor;

	[Header("Animation")]
	private Animator animator;

	private void Start()
	{
		animator = GetComponent<Animator>();
		HealthAndArmorStart();
		UpdateStatusUI();
	}
	public void Damage(int damage)
	{
		if (currentArmor > 0)
		{
			currentArmor -= damage;
			Hurt();
		}
		else if (currentArmor <= 0)
		{
			currentHealth -= damage;
			Hurt();
		}

		if (currentHealth <= 0)
		{
			currentHealth = 0;
			Dealth();
		}
		UpdateStatusUI();
	}
	public virtual void IncreaseHealth(int health)
	{
		currentHealth += health;

		if (currentHealth > maxHealth)
			currentHealth = maxHealth;

		UpdateStatusUI();
	}

	public virtual void IncreaseArmor(int armor)
	{
		currentArmor += armor;

		if (currentArmor > maxArmor)
			currentArmor = maxArmor;

		UpdateStatusUI();
	}
	public virtual void UpdateStatusUI()
	{

	}
	public virtual void Hurt()
	{
		if (animator != null)
		{
			animator.SetBool("Damage", true);
		}
		StartCoroutine(DisableHurt());
	}

	IEnumerator DisableHurt()
	{
		yield return new WaitForSeconds(0.1f);
		if (animator != null)
		{
			animator.SetBool("Damage", false);
		}
	}

	public virtual void Dealth()
	{
		if (animator != null)
		{
			animator.SetTrigger("Death");
		}
	}

	public virtual void HealthAndArmorStart()
	{

	}
}
