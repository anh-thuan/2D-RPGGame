using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
	private MoveController moveController;
	public void Start()
	{
		moveController = GameObject.Find("Player").GetComponent<MoveController>();
	}

	public void UnlockDash()
	{
		moveController.canDash = true;
	}

	public void UnlockDashSpeed()
	{
		moveController.dashPower += 2;
	}
}
