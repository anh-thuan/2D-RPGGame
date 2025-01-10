using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ItemSO;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
	[Header("Item_Slot")]
	public ItemSlot[] itemSlot;

	[Header("Item_SO")]
	public ItemSO[] itemSO;

	#region Item
	public bool UseItem(StatToChange stat)
	{
		for (int i = 0; i < itemSO.Length; i++)
		{
			if (itemSO[i].statToChange == stat)
			{
				bool usable = itemSO[i].UseItem();
				return usable;
			}
		}
		return false;
	}
	public int AddItem(StatToChange stat, string itemName, int quantity, Sprite itemSprite, string itemDescription, GameObject itemObject)
	{
		for (int i = 0; i < itemSlot.Length; i++)
		{
			if (itemSlot[i].isFull == false && itemSlot[i].itemName == itemName || itemSlot[i].quantity == 0)
			{
				int leftOverItems = itemSlot[i].AddItem(stat, itemName, quantity, itemSprite, itemDescription, itemObject);
				if (leftOverItems > 0)
					leftOverItems = AddItem(stat, itemName, leftOverItems, itemSprite, itemDescription, itemObject);

				return leftOverItems;
			}
		}
		return quantity;
	}
	public void DeselectAllSlots()
	{
		for (int i = 0; i < itemSlot.Length; i++)
		{
			itemSlot[i].thisItemSelected = false;
		}
	}
	#endregion
}
