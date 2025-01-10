using UnityEngine;

public class Blade : MonoBehaviour
{
	public WeaponSO damage;
	public int totalDamage;
	public GameObject bloodSplash;

	[Header("Exp_Level")]
	[Range(0, 10)]
	public int minExp;
	[Range(0, 10)]
	public int maxExp;

	private ExpManager exp_Manager;
	private void Start()
	{
		exp_Manager = GameObject.Find("UI").GetComponent<ExpManager>();
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy"))
		{
			damage.currentDurability--;
			GameObject.FindWithTag("Enemy").GetComponent<EnemyKnockBack>().KnockBack(transform, damage.knockBack);
			IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
			ScoreManager.currentScore += Random.Range(1, 5);

			if (damage.currentDurability > 0)
			{
				totalDamage = damage.damage;
				Debug.Log("Current damage: " + totalDamage);
			}
			//If durability is 0
			else if (damage.currentDurability <= 0)
			{
				totalDamage = Mathf.RoundToInt(damage.damage * (10f / 100f));
				Debug.Log("New damage: " + totalDamage);
			}
			Level();
			if (damageable != null)
			{
				damageable.Damage(totalDamage);
			}
		}
	}

	public void Level()
	{
		exp_Manager.UpdateExperience(Random.Range(minExp, maxExp));
	}
}
