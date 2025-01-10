using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelSO : ScriptableObject
{
	public LevelToChange levelToChange = new LevelToChange();
	public float number;
	public float anotherAmountToChange;

	private MoveController playerMove;
	public void UpgradeLevel()
	{
		if (levelToChange == LevelToChange.Speed)
		{
			playerMove = GameObject.Find("Player").GetComponent<MoveController>();
			playerMove.moveSpeed += number;
		}
	}
	public enum LevelToChange
	{
		None,
		Health,
		Shield,
		Speed,
	}
}
