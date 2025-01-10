using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponSO : ScriptableObject
{
	// Weapon Selection
	[Header("Weapon-Selection")]
	public Sprite weaponSprite;
	public WeaponType weaponType;


	// Gun Info
	[Header("Gun-Info")]
	public GunType gunType;
	public Mode mode;
	public bool canSwitch;
	public string weaponName;
	public int damage;
	[Range(0, 30)]
	public float rateOfFire;
	[Range(0, 30)]
	public float reloadSpeed;
	[Range(0, 100)]
	public float bulletSpeed;
	[Range(0, 30)]
	public float bulletPerShot;
	[Range(0, 100)]
	public float bulletDistance;
	[Range(0, 100)]
	public float knockBack;
	public float accuracy;

	// Ammo Information
	[Header("Ammo")]
	public AmmoType ammoType;
	public int currentAmmo;
	public int ammoCapacity;
	public int totalAmmo;
	public int defaultTotalAmmo;

	// Melee Weapon
	[Header("Melee Weapon")]
	public int meleeDamage;
	public MeleeType meleeType;
	[Range(0, 30)]
	public float attackSpeed;
	[Range(0, 100)]
	public int blockChance;
	[Range(0, 100)]
	public int criticalChance;
	[Range(0, 100)]
	public int currentDurability = 0;
	[Range(0, 100)]
	public int totalDurability = 0;
	public MeleeSkill meleeSkill;

	// Enums
	public enum WeaponType
	{
		Melee,
		Range
	}

	public enum MeleeType
	{
		None,
		Blunt,
		Blade
	}

	public enum GunType
	{
		None,
		Pistol,
		Magnum,
		Shotgun,
		Smg,
		AssaultRifle,
		Dmr,
		Snipe,
		MachineGun,
		Explosivegun
	}

	public enum AmmoType
	{
		None,
		r9mm,
		r45acp,
		r12gauge,
		r7_62mm,
		r5_56mm,
		r357,
		r300m,
		r50cal
	}

	public enum Mode
	{
		None,
		Single,
		Auto
	}

	//Melee Skill
	public enum MeleeSkill 
	{
		None,
		Blood,
		Knockback,
		Penetrate
	}
}
