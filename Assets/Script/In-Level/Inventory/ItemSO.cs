using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
	public StatToChange statToChange = new StatToChange();
	public int number;

	//private Wall openWall;
	public bool UseItem()
	{
		if (statToChange == StatToChange.adrenaline)
		{
			MoveController move = GameObject.Find("Player").GetComponent<MoveController>();
			move.moveSpeed += number;
			return true;
		}
		if (statToChange == StatToChange.bandage)
		{
			Player player = GameObject.Find("Player").GetComponentInParent<Player>();
			if (player.currentHealth >= player.maxHealth)
			{
				return false;
			}
			else if (player.currentHealth < player.maxHealth)
			{
				player.IncreaseHealth(number);
				return true;
			}
		}

		if (statToChange == StatToChange.painkiller)
		{
			Player player = GameObject.Find("Player").GetComponentInParent<Player>();
			if (player.currentArmor >= player.maxArmor)
			{
				return false;
			}
			else if (player.currentArmor < player.maxArmor)
			{
				player.IncreaseArmor(number);
				return true;
			}
		}
		return false;
	}
	public enum StatToChange
	{
		none,
		bandage,
		medkit,
		adrenaline,
		painkiller,
		key
	}
}
