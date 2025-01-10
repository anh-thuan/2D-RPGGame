using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static ItemSO;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
	//Item Data
	public StatToChange stat;
	public string itemName;
	public int quantity;
	public Sprite itemSprite;
	public bool isFull;
	public string itemDescription;
	public GameObject itemObject;

	public int maxNumberOfItems;

	//1.Item Slot
	[SerializeField] private TMP_Text quantityText;
	[SerializeField] private Image itemImage;

	//2.Item Description
	public Image itemDescriptionImage;
	public Sprite emptySprite;
	public TMP_Text itemDescriptionName;
	public TMP_Text itemDescriptionInfo;

	public Button useButton;
	public Button dropButton;

	//public GameObject selectedShader;
	public bool thisItemSelected;

	private InventoryManager inventoryManager;

	private void Start()
	{
		inventoryManager = GameObject.Find("UI").GetComponent<InventoryManager>();
	}
	public int AddItem(StatToChange stat, string itemName, int quantity, Sprite itemSprite, string itemDescription, GameObject itemObject)
	{
		if (isFull)
			return quantity;
		this.stat = stat;

		this.itemName = itemName;
		this.itemDescription = itemDescription;

		this.itemSprite = itemSprite;
		itemImage.sprite = itemSprite;

		this.itemObject = itemObject;

		this.quantity += quantity;
		if (this.quantity >= maxNumberOfItems)
		{
			quantityText.text = maxNumberOfItems.ToString();
			quantityText.enabled = true;
			isFull = true;

			int extraItems = this.quantity - maxNumberOfItems;
			this.quantity = maxNumberOfItems;
			return extraItems;
		}

		quantityText.text = this.quantity.ToString();
		quantityText.enabled = true;

		return 0;
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
		inventoryManager.DeselectAllSlots();
		thisItemSelected = true;

		itemDescriptionName.text = itemName;
		itemDescriptionImage.sprite = itemSprite;
		itemDescriptionInfo.text = itemDescription;

		useButton.onClick.RemoveAllListeners();
		useButton.onClick.AddListener(UseItem);

		dropButton.onClick.RemoveAllListeners();
		dropButton.onClick.AddListener(DropItem);
	}

	private void DropItem()
	{
		GameObject droppedItem = Instantiate(itemObject);
		droppedItem.SetActive(true);
		droppedItem.name = itemName;

		Transform playerTransform = GameObject.FindWithTag("Player").transform;
		droppedItem.transform.position = playerTransform.position + new Vector3(2f, 0f, 0f);

		this.quantity -= 1;
		quantityText.text = this.quantity.ToString();

		if (this.quantity <= 0)
			EmptySlot();
	}
	private IEnumerator DestroyItemAfterUseOrDrop()
	{
		yield return new WaitForSeconds(0.5f); // Wait for 2 seconds
		Destroy(gameObject); // Destroy the object
	}

	private void UseItem()
	{
		bool usable = inventoryManager.UseItem(stat);
		if (usable)
		{
			this.quantity -= 1;
			quantityText.text = this.quantity.ToString();
			if (this.quantity <= 0)
				EmptySlot();
		}
	}
	private void EmptySlot()
	{
		//Item Slot
		quantityText.enabled = false;
		itemImage.sprite = emptySprite;

		quantity = 0;
		isFull = false;
		itemName = "";
		itemSprite = emptySprite;
		itemObject = null;
		itemDescription = "";

		//Item Description
		itemDescriptionImage.sprite = emptySprite;
		itemDescriptionName.text = "";
		itemDescriptionInfo.text = "";
	}
}
