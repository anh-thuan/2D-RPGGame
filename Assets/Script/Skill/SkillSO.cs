using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SkillSO : ScriptableObject
{
	public string skillName;
	public int maxLevel;
	public Sprite skillIcon;
	[TextArea]
	public string skillDescription;
}
