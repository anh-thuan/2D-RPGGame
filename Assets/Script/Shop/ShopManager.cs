using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
	public GameObject shopAlert;
	public GameObject shopUI;
	public bool isTouching;

	private MoveController moveController;

	private void Start()
	{
		moveController = GameObject.Find("Player").GetComponent<MoveController>();
	}

	private void Update()
	{
		OpenShop();
	}

	private void OpenShop()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && isTouching)
		{
			shopUI.SetActive(true);
			moveController.moveSpeed = 0;
		}
	}

	public void CloseShop()
	{
		if (shopUI.activeInHierarchy == true)
		{
			shopUI.SetActive(false);
			moveController.moveSpeed += 5;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			isTouching = true;
			shopAlert.SetActive(true);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			isTouching = false;
			shopAlert.SetActive(false);
		}
	}
}
