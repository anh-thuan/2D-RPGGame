using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WeaponSO;

public class Ammo : MonoBehaviour
{
	[Header("Weapon")]
	public WeaponSO[] weapons;
	public AmmoType ammoTypes;

	[Header("Ammo Percentage")]
	[Range(0, 100)]
	public int minAmmo;
	[Range(0, 100)]
	public int maxAmmo;


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			RefillAmmo(ammoTypes);
			Destroy(gameObject);
		}
	}

	public void RefillAmmo(AmmoType ammo)
	{
		for ( int i = 0; i < weapons.Length; i++ )
		{
			if (weapons[i].ammoType == ammo)
			{
				weapons[i].totalAmmo += Random.Range(minAmmo, maxAmmo);
			}
		}
	}
}
