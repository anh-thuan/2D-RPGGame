using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ItemSO;

public class Item : MonoBehaviour
{
	public StatToChange stat;
	public string itemName;
	public int quantity;
	public Sprite sprite;
	public GameObject itemObject;
	[TextArea] public string itemDescription;

	private InventoryManager inventoryManager;
	private void Start()
	{
		inventoryManager = GameObject.Find("UI").GetComponent<InventoryManager>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			int leftOverItems = inventoryManager.AddItem(stat, itemName, quantity, sprite, itemDescription, itemObject);

			if (leftOverItems <= 0)
				gameObject.SetActive(false);
			else
				quantity = leftOverItems;
		}
	}
}
