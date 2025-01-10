using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WeaponSO;

public class Weapon : MonoBehaviour
{
	public WeaponSO weaponSO;
	[Header("Gun")]
	public Transform gunPoint;
	public GameObject spark;
	public AudioSource gunSound;
	public AudioSource reloadSound;

	[Header("Melee")]
	public PolygonCollider2D blade;
	public Animator animator;
}
