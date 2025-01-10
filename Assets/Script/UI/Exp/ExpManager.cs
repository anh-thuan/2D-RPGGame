using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpManager : MonoBehaviour
{
	[Header("Experience Level")]
	public int level;
	//
	[Header("Experience Number")]
	public int currentExp;
	public int expToLevel = 10;
	//
	[Header("Experience Increase per level")]
	public float expGrowthMultiplier = 2f;
	//
	[Header("Experience UI")]
	public Slider expSlider;
	public TMP_Text currentExperienceText;
	public TMP_Text currentLevelText;
	public GameObject levelUpUI;

	//[Header("Experience Point")]
	public TMP_Text currentExpPoint;
	public int currentExpPointText;

	private void Start()
	{
		UpdateUI();
	}
	public void UpdateExperience(int amount)
	{
		currentExp += amount;
		if (currentExp >= expToLevel)
		{
			LevelUp();
		}
		UpdateUI();
	}

	public void LevelUp()
	{
		level++;
		currentExp -= expToLevel;
		expToLevel = Mathf.RoundToInt(expToLevel * expGrowthMultiplier);
		currentExpPointText += 3;
		currentExpPoint.text = currentExpPointText.ToString();
		levelUpUI.SetActive(true);
		StartCoroutine(LevelUpIcon());
	}

	IEnumerator LevelUpIcon()
	{
		yield return new WaitForSeconds(1);
		levelUpUI.SetActive(false);
	}

	public void UpdateUI()
	{
		expSlider.maxValue = expToLevel;
		expSlider.value = currentExp;
		currentExperienceText.text = currentExp.ToString() + " xp " + " / " + expToLevel.ToString() + " xp";
		currentLevelText.text = "Level: " + level;
	}
}
