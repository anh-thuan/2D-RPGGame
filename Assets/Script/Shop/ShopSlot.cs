using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static MoneyManager;

public class ShopSlot : MonoBehaviour, IPointerClickHandler
{
	[Header("Item Data")]
	public string itemName;
	public int quantity;
	[Range(0, 10)]
	[SerializeField] private int maxNumberOfItem;
	public int itemPrice;
	public Sprite itemSprite;
	public GameObject shopItem;
	[TextArea]
	public string itemDescription;
	public Sprite emptySprite;

	MoneyManager money;

	[Space]
	[Space]

	[Header("Item Slot")]
	[SerializeField] private Image itemImage;
	[SerializeField] private TMP_Text quantityText;

	//Item Description Slot
	public Image itemIconButton;
	public Image itemDescriptionImage;
	public Image outofProductImage;
	public TMP_Text itemDescriptionNameText;
	public TMP_Text itemDescriptionPriceText;
	public TMP_Text itemDescriptionText;
	public GameObject notEnoughMoneyText;
	public GameObject successfullText;
	public Button purchaseButton;

	private void Start()
	{
		quantity = maxNumberOfItem;
		quantityText.text = maxNumberOfItem.ToString();
		itemIconButton.sprite = itemSprite;
	}
	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			OnLeftClick();
		}
	}

	private void OnLeftClick()
	{
		// Show info in item description
		itemDescriptionImage.sprite = itemSprite;
		itemDescriptionNameText.text = itemName;
		itemDescriptionPriceText.text = itemPrice.ToString();
		itemDescriptionText.text = itemDescription;

		// Ensure no duplicate listeners are added
		purchaseButton.onClick.RemoveAllListeners();
		purchaseButton.onClick.AddListener(PurchaseItem);
	}

	private void PurchaseItem()
	{
		// Check if quantity is 0 or less
		if (quantity <= 0)
		{
			outofProductImage.gameObject.SetActive(true);
			quantityText.text = "0";
			return;
		}

		// Check if the player does not have enough money
		else if (MoneyManager.instance.currentCoin < itemPrice)
		{
			//notEnoughMoneyText.SetActive(true);
			//StartCoroutine(NotEnough());
			return;
		}

		//Proceed with the purchase
		//successfullText.SetActive(true);
		//StartCoroutine(GreenAlert());

		MoneyManager.instance.DecreaseMoney(itemPrice, Currency.Coin);

		GameObject itemToDrop = Instantiate(shopItem);
		itemToDrop.name = itemName;

		Transform playerTransform = GameObject.FindWithTag("Player").transform;
		itemToDrop.transform.position = playerTransform.position;

		quantity -= 1;
		quantityText.text = quantity.ToString();
	}

	//IEnumerator GreenAlert()
	//{
	//	yield return new WaitForSeconds(1);
	//	successfullText.SetActive(false);
	//}

	IEnumerator NotEnough()
	{
		yield return new WaitForSeconds(1f);
		notEnoughMoneyText.SetActive(false);
	}
}
