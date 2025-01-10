using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MoneyManager;

public class Money : MonoBehaviour
{
	[Header("CurrencyText")]
	[Range(0, 1000)]
	public int minMoney;
	[Range(0, 1000)]
	public int maxMoney;

	[Header("CurrencyType")]
	public Currency currency;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			Destroy(gameObject);
			MoneyManager.instance.IncreaseMoney(Random.Range(minMoney, maxMoney), currency);
		}
	}
}
