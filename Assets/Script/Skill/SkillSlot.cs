using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillSlot : MonoBehaviour, IPointerClickHandler
{
	public List<SkillSlot> prerequisiteSkillSlots;
	public Image[] line;
	public int currentLevel;
	public bool isUnlocked;
	public bool unlockOtherSkill;
	public SkillTreeManager skillTreeManager;

	public SkillSO skillSO;

	[Header("Button info")]
	public Image ButtonImage;
	public Sprite FullButtonImage;
	public Image skillIcon;
	public GameObject lockedSkill;

	[Header("Description info")]
	public Image descriptionIcon;
	public TMP_Text descriptionName;
	public TMP_Text descriptionInfo;
	public TMP_Text skillLevelText;
	public Button updateButton;
	public GameObject notEnoughText;

	[Header("Ex Point")]
	public ExpManager expPoint;

	private void Start()
	{
		expPoint = GameObject.Find("UI").GetComponent<ExpManager>();
		skillLevelText.text = "0" + "/" + "0";
		skillTreeManager = GameObject.Find("UI").GetComponent<SkillTreeManager>();
	}
	private void Update()
	{
		if (isUnlocked)
		{
			lockedSkill.SetActive(false);
			skillIcon.sprite = skillSO.skillIcon;
			//skillLevelText.text = currentLevel.ToString() + "/" + skillSO.maxLevel.ToString();
		}
	}
	public void OnPointerClick(PointerEventData eventData)
	{
		Debug.Log(isUnlocked);
		if (!isUnlocked)
		{
			descriptionIcon.sprite = null;
			descriptionName.text = "Unknown";
			descriptionInfo.text = "Unlock the previous skill first !";
			skillLevelText.text = "0" + "/" + "0";
			updateButton.onClick.RemoveAllListeners();
			return;
		}

		descriptionIcon.sprite = skillSO.skillIcon;
		descriptionName.text = skillSO.skillName;
		descriptionInfo.text = skillSO.skillDescription;
		skillLevelText.text = currentLevel.ToString() + "/" + skillSO.maxLevel.ToString();

		updateButton.onClick.RemoveAllListeners();
		updateButton.onClick.AddListener(UpdateUI);
	}
	public void UpdateUI()
	{
		if (!isUnlocked)
		{
			Debug.Log("Unlock first!");
			return;
		}

		skillIcon.sprite = skillSO.skillIcon;

		// No unlock if not enough expPoints
		if (expPoint.currentExpPointText <= 0)
		{
			notEnoughText.SetActive(true);
			StartCoroutine(NotEnoughText());
			return;
		}

		// Reduce expPoints and update UI
		if (currentLevel < skillSO.maxLevel)
		{
			skillTreeManager.HandleAbility(this);
			currentLevel++;
			expPoint.currentExpPointText -= 1;
			expPoint.currentExpPoint.text = expPoint.currentExpPointText.ToString();
			skillLevelText.text = currentLevel.ToString() + "/" + skillSO.maxLevel.ToString();
		}

		// Unlock immediately if at max level
		if (currentLevel == skillSO.maxLevel)
		{
			//skillIcon.color = Color.yellow;
			foreach (Image img in line)
			{
				img.color = Color.yellow;
			}
			ButtonImage.sprite = FullButtonImage;
			UnlockOtherSkill();
		}
	}

	public void UnlockOtherSkill()
	{
		isUnlocked = true; // Unlock the current skill
		foreach (SkillSlot slot in prerequisiteSkillSlots)
		{
			if (!slot.isUnlocked)
			{
				slot.isUnlocked = true;
				//skillTreeManager.HandleAbility(slot);
			}
		}
	}

	IEnumerator NotEnoughText()
	{
		yield return new WaitForSeconds(1);
		notEnoughText.SetActive(false);
	}
}
