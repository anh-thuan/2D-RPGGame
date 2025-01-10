using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeManager : MonoBehaviour
{
	public SkillManager skillManager;

	public void HandleAbility(SkillSlot slot)
	{
		string skillName = slot.skillSO.skillName;

		switch (skillName)
		{
			case "Dash":
				skillManager.UnlockDash();
				break;
			case "DashSpeed":
				skillManager.UnlockDashSpeed();
				break;
			default:
				Debug.Log("Unknown skill !");
				break;
		}
	}
}
