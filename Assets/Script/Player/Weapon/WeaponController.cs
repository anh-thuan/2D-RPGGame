using System.Collections;
using UnityEngine;
using static WeaponSO;

public class WeaponController : MonoBehaviour
{
	[Header("Weapon Control")]
	public Weapon[] weapons;
	public Transform weapon;

	//Gun
	[Header("Gun")]
	public Transform gunPoint;
	public GameObject gunSpark;

	//Empty gun sound
	[Header("Empty Gun Sound")]
	public AudioSource emptyGunSound;

	//Melee
	[Header("Melee")]
	public PolygonCollider2D blade;
	public Animator meleeAnimator;

	//Number
	private int weaponNumber;
	private int totalNumber;

	[Header("Bullet")]
	public GameObject bulletPrefab;
	public float bulletSpeed;
	private float lastAttackTime;

	private int currentWeapon = 0;

	[Header("UI")]
	public GameObject reload_UI;
	private bool startReload;
	private bool isShooting;
	private bool isReloading;
	private bool isSwitching;

	//Pause
	private bool isPause;
	private WeaponSO currentWeaponData => weapons[currentWeapon].weaponSO;
	private AimController aimController;
	private int selectNumber;
	private bool isAuto;
	private void Start()
	{
		SwitchWeapon(currentWeapon);
		FirstReload();
		SetUpMeleeDamage();
		weaponNumber = 0;
		aimController = GetComponent<AimController>();
	}
	private void Update()
	{
		for (int i = 0; i <= weapons.Length; i++)
		{
			totalNumber = i;
		}
		SwitchWeaponWithScroll();
		SwitchButton();
		WeaponPerform();
		Reload();
		SelectMode();
		//Melee();
	}

	//Shoot
	#region Shoot
	public void SelectMode()
	{
		if (!currentWeaponData.canSwitch)
		{
			return;
		}
		else
		{
			if (Input.GetKeyDown(KeyCode.Q) && !isAuto)
			{
				isAuto = true;
				SwitchMode(Mode.Auto);
			}
			else if (Input.GetKeyDown(KeyCode.Q) && isAuto)
			{
				isAuto = false;
				SwitchMode(Mode.Single);
			}
		}
	}

	public void SwitchMode(Mode fireMode)
	{
		currentWeaponData.mode = fireMode;

		switch (fireMode)
		{
			case Mode.Single:
				break;
			case Mode.Auto:
				break;
		}
	}
	private void WeaponPerform()
	{
		if (startReload)
		{
			return;
		}
		else
		{
			//Melee-Weapon
			if (currentWeaponData.weaponType == WeaponType.Melee)
			{
				if (Input.GetKeyDown(KeyCode.Mouse0))
				{
					MeleeSpeed();
				}
			}
			//Range-Weapon
			else if (currentWeaponData.weaponType == WeaponType.Range)
			{
				if (currentWeaponData.mode == Mode.Single)
				{
					if (Input.GetKeyDown(KeyCode.Mouse0))
					{
						isShooting = true;
						RateOfFire();
					}
				}
				else
				{
					if (Input.GetKey(KeyCode.Mouse0))
					{
						isShooting = true;
						RateOfFire();
					}
				}
			}
		}
	}
	public void RateOfFire()
	{
		if (Time.time > lastAttackTime + 1 / currentWeaponData.rateOfFire)
		{
			lastAttackTime = Time.time;
			FireSingleBullet();
		}
	}

	private void FireSingleBullet()
	{
		if (currentWeaponData.currentAmmo <= 0)
		{
			//startReload = true;
			//StartCoroutine(ReloadGun());
			emptyGunSound.Play();
			return;
		}

		//else if (currentWeaponData.currentAmmo <= 0 && currentWeaponData.totalAmmo <= 0)
		//{
		//	emptyGunSound.Play();
		//	return;
		//}

		weapons[currentWeapon].gunSound.Play();
		HaveBullets();

		for (int i = 0; i < currentWeaponData.bulletPerShot; i++)
		{
			//Spread
			float bulletSpread = Random.Range(-currentWeaponData.accuracy, currentWeaponData.accuracy);
			Vector3 spreadDirection = Quaternion.Euler(0, 0, bulletSpread) * aimController.aimDirection.normalized;

			//Bullet
			GameObject bullet = Instantiate(bulletPrefab, gunPoint.position, Quaternion.identity);
			bullet.GetComponent<Rigidbody2D>().velocity = (spreadDirection * currentWeaponData.bulletSpeed);
			//bullet.GetComponent<EnemyKnockBack>().KnockBack(transform, knockBackForce);

			//Spark
			gunSpark.SetActive(true);
			StartCoroutine(GunSpark());

			//Damaged
			Bullet bulletScript = bullet.GetComponent<Bullet>();
			bulletScript.BulletDamage(Mathf.RoundToInt(currentWeaponData.damage));
			bulletScript.BulletKnockBack(Mathf.RoundToInt(currentWeaponData.knockBack));
			bulletScript.BulletRange(gunPoint.position, currentWeaponData.bulletDistance);
		}
	}

	IEnumerator GunSpark()
	{
		yield return new WaitForSeconds(0.1f);
		gunSpark.SetActive(false);
	}
	#endregion

	//Switch
	#region Switch
	private void SwitchButton()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
			SwitchWeapon(0);
		else if (Input.GetKeyDown(KeyCode.Alpha2))
			SwitchWeapon(1);
	}

	private void SwitchWeapon(int i)
	{
		if (i < 0 || i >= weapons.Length) return;

		weapons[currentWeapon].gameObject.SetActive(false);

		currentWeapon = i;

		weapons[currentWeapon].gameObject.SetActive(true);

		weapon = weapons[currentWeapon].transform;

		if (currentWeaponData.weaponType == WeaponType.Range)
		{
			gunPoint = weapons[currentWeapon].gunPoint;
			gunSpark = weapons[currentWeapon].spark;
		}
		else if (currentWeaponData.weaponType == WeaponType.Melee)
		{
			blade = weapons[currentWeapon].blade;
			meleeAnimator = weapons[currentWeapon].animator;
		}
		startReload = false;
		reload_UI.SetActive(false);

	}

	private void SwitchWeaponWithScroll()
	{
		if (Input.GetAxisRaw("MouseScrollWheel") < 0f)
		{
			weaponNumber++;
			SwitchWeapon(weaponNumber);
			if (weaponNumber >= totalNumber)
			{
				weaponNumber = 0;
				SwitchWeapon(weaponNumber);
			}
		}
		else if (Input.GetAxisRaw("MouseScrollWheel") > 0f)
		{
			weaponNumber--;
			SwitchWeapon(weaponNumber);
			if (weaponNumber < 0)
			{
				weaponNumber = totalNumber - 1;
				SwitchWeapon(weaponNumber);
			}
		}
	}
	#endregion

	//Reload
	#region Reload
	private void FirstReload()
	{
		foreach (var weapon in weapons)
		{
			weapon.weaponSO.currentAmmo = weapon.weaponSO.ammoCapacity;
			weapon.weaponSO.totalAmmo = weapon.weaponSO.defaultTotalAmmo;
			weapon.weaponSO.currentDurability = weapon.weaponSO.totalDurability;
		}
	}
	public void Reload()
	{
		if (currentWeaponData.currentAmmo == currentWeaponData.ammoCapacity)
		{
			return;
		}
		else
		{
			if (currentWeaponData.totalAmmo == 0 && currentWeaponData.currentAmmo == 0)
			{
				return;
			}
			else
			{
				if (Input.GetKeyDown(KeyCode.R))
				{
					if (isReloading)
					{
						return;
					}
					else
					{
						startReload = true;
						StartCoroutine(ReloadGun());
					}
				}
			}
		}
	}

	IEnumerator ReloadGun()
	{
		reload_UI.SetActive(true);
		isReloading = true;
		weapons[currentWeapon].reloadSound.Play();
		yield return new WaitForSeconds(currentWeaponData.reloadSpeed);

		RefillBullet();
		isReloading = false;
		reload_UI.SetActive(false);
		startReload = false;
	}

	private void RefillBullet()
	{
		if (currentWeaponData.gunType == GunType.Shotgun)
		{
			currentWeaponData.currentAmmo += 1;
			int bulletsToReload = currentWeaponData.ammoCapacity;
			if (currentWeaponData.currentAmmo > bulletsToReload)
			{
				currentWeaponData.currentAmmo = bulletsToReload;
				return;
			}
			currentWeaponData.totalAmmo -= 1;

			if (bulletsToReload > currentWeaponData.totalAmmo)
				bulletsToReload = currentWeaponData.totalAmmo;

			if (currentWeaponData.totalAmmo < 0)
				currentWeaponData.totalAmmo = 0;

			if (!isShooting)
			{
				StartCoroutine(ReloadGun());
			}
		}
		else
		{
			currentWeaponData.totalAmmo += currentWeaponData.currentAmmo;

			int bulletsToReload = currentWeaponData.ammoCapacity;

			if (bulletsToReload > currentWeaponData.totalAmmo)
				bulletsToReload = currentWeaponData.totalAmmo;

			currentWeaponData.totalAmmo -= bulletsToReload;
			currentWeaponData.currentAmmo = bulletsToReload;

			if (currentWeaponData.totalAmmo < 0)
				currentWeaponData.totalAmmo = 0;
		}
	}

	private bool HaveBullets()
	{
		if (currentWeaponData.currentAmmo <= 0)
		{
			ReloadGun();
			return false;
		}

		currentWeaponData.currentAmmo--;
		return true;
	}
	#endregion

	//Melee
	#region Melee
	public void SetUpMeleeDamage()
	{
		foreach (var weapon in weapons)
		{
			if (weapon.weaponSO.weaponType == WeaponType.Melee)
			{
				weapon.weaponSO.damage = weapon.weaponSO.meleeDamage;
			}
		}
	}
	public void MeleeSpeed()
	{
		if (Time.time > lastAttackTime + 1 / currentWeaponData.attackSpeed)
		{
			lastAttackTime = Time.time;
			Melee();
		}
	}
	public void Melee()
	{
		blade.GetComponentInChildren<PolygonCollider2D>().enabled = true;
		meleeAnimator.SetTrigger("Attack");
		StartCoroutine(MeleeCoolDown());
	}

	IEnumerator MeleeCoolDown()
	{
		yield return new WaitForSeconds(0.1f);
		blade.GetComponentInChildren<PolygonCollider2D>().enabled = false;
	}

	#endregion
}
