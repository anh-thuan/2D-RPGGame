using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelSO;

public class LevelManager : MonoBehaviour
{
	[Header("Level_SO")]
	public LevelSO[] levelSO;

	#region Level
	public void UpgradeLevel(LevelToChange level)
	{
		for (int i = 0; i < levelSO.Length; i++)
		{
			if (levelSO[i].levelToChange == level)
			{
				levelSO[i].UpgradeLevel();
			}
		}
	}
	#endregion
}
