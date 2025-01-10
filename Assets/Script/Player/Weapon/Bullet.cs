using UnityEngine;

public class Bullet : MonoBehaviour
{
	//private Rigidbody2D rb => GetComponent<Rigidbody2D>();
	[Header("Damage")]
	private int weaponDamage;
	private float bulletDistance;

	[Header("Blood Splash")]
	public GameObject bloodSplash;

	private Vector3 startPosition;
	private float maxDistance;

	[Header("Exp_Level")]
	[Range(0, 10)]
	public int minExp;
	[Range(0, 10)]
	public int maxExp;

	[Header("KnockBack")]
	public float knockBackForce;

	private ExpManager exp_Manager;

	private void Start()
	{
		exp_Manager = GameObject.Find("UI").GetComponent<ExpManager>();
	}
	private void Update()
	{
		//transform.right = rb.velocity;
		float traveledDistance = Vector3.Distance(startPosition, transform.position);
		if (traveledDistance > maxDistance)
		{
			Destroy(gameObject);
		}
	}
	public void BulletDamage(int damage)
	{
		weaponDamage = damage;
	}
	public void BulletRange(Vector3 start, float distance)
	{
		startPosition = start;
		maxDistance = distance;
	}
	public void BulletKnockBack(float knockBack)
	{
		knockBackForce = knockBack;
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		Destroy(gameObject);
		if (collision.gameObject.CompareTag("Enemy"))
		{
			//Knockback
			GameObject.FindWithTag("Enemy").GetComponent<EnemyKnockBack>().KnockBack(transform, knockBackForce);
			//Damage
			IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
			//Blood
			CreateBlood(collision);
			if (damageable != null)
			{
				damageable.Damage(weaponDamage);
			}

			Debug.Log(weaponDamage + " And " + knockBackForce);
			Level();
		}
	}

	private void CreateBlood(Collision2D collision)
	{
		ScoreManager.currentScore += Random.Range(1, 5);

		ContactPoint2D contactPoint = collision.GetContact(0);
		RaycastHit2D hit = new RaycastHit2D
		{
			point = contactPoint.point,
			normal = contactPoint.normal
		};
		GameObject blood = Instantiate(bloodSplash, hit.point, Quaternion.LookRotation(hit.normal));
	}

	public void Level()
	{
		exp_Manager.UpdateExperience(Random.Range(minExp, maxExp));
	}
}
