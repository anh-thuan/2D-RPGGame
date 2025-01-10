using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static LevelSO;
using static MoneyManager;

public class LevelSlot : MonoBehaviour, IPointerClickHandler
{
	[Header("Level Data")]
	public LevelToChange level;
	public string levelName;
	[Range(0, 5)]
	public int quantity;
	public int maxLevel;
	public int price;
	public Sprite levelSprite;
	[TextArea]
	public string levelDescription;

	[Space]
	[Space]

	[Header("Level Slot")]
	public Image levelImage;
	public Image levelBar;

	[Space]
	[Space]

	[Header("Level Description")]
	public Image levelIcon;
	public Image fullUpggradeImage;

	[Header("Level Bar")]
	public Slider progressBar;

	[Header("Player Bar")]
	public Image playerBar;

	[Header("Level Info")]
	public TMP_Text levelDescriptionNameText;
	public TMP_Text levelDescriptionPriceText;
	public TMP_Text levelDescriptionText;
	public Button upgradeButton;
	public GameObject redAlert;

	private LevelManager managementSystem;
	private MoneyManager money;


	private void Start()
	{
		progressBar.value = 0;
		levelImage.sprite = levelSprite;
		managementSystem = GameObject.Find("UI").GetComponent<LevelManager>();
	}
	public void OnPointerClick(PointerEventData eventData)
	{
		levelImage.sprite = levelSprite;
		//quantityText.text = quantity.ToString();

		levelIcon.sprite = levelSprite;
		levelDescriptionNameText.text = levelName;
		levelDescriptionPriceText.text = price.ToString();
		levelDescriptionText.text = levelDescription;

		upgradeButton.onClick.RemoveAllListeners();
		upgradeButton.onClick.AddListener(UpgradeSkill);
	}

	private void UpgradeSkill()
	{
		if (quantity == maxLevel)
		{
			fullUpggradeImage.gameObject.SetActive(true);
			quantity = maxLevel;
			//quantityText.text = maxNumberOfLevel.ToString();
			return;
		}

		if (MoneyManager.instance.currentCoin < price)
		{
			redAlert.SetActive(true);
			StartCoroutine(ShowAlert());
			return;
		}

		MoneyManager.instance.DecreaseMoney(price, Currency.Coin);
		price += 50;
		levelDescriptionPriceText.text = price.ToString();

		managementSystem.UpgradeLevel(level);
		quantity += 1;
		//quantityText.text = quantity.ToString();
		progressBar.value += 0.2f;

		if (playerBar != null)
		{
			playerBar.rectTransform.sizeDelta += new Vector2(20, 0);
		}
		else
		{
			return;
		}
		//Debug.Log(levelName);
	}

	IEnumerator ShowAlert()
	{
		yield return new WaitForSeconds(1);
		redAlert.SetActive(false);
	}
}
