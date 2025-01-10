using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
	public static MoneyManager instance;
	[HideInInspector] public int currentCoin;
	[HideInInspector] public int currentRuby;

	public TMP_Text coinText;
	public TMP_Text rubyText;

	private void Awake()
	{
		instance = this;
	}
	private void Start()
	{
		//Coin
		currentCoin = PlayerPrefs.GetInt("Money", currentCoin);
		coinText.text = currentCoin.ToString();

		//Ruby
		currentRuby = PlayerPrefs.GetInt("Ruby", currentRuby);
		rubyText.text = currentRuby.ToString();
	}
	private void Update()
	{
		PlayerPrefs.SetInt("Money", currentCoin);
		coinText.text = currentCoin.ToString();

		PlayerPrefs.SetInt("Ruby", currentRuby);
		rubyText.text = currentRuby.ToString();
	}
	public void IncreaseMoney(int amount, Currency i)
	{
		if (i == Currency.Coin)
		{
			currentCoin += amount;
		}
		else if (i == Currency.Ruby)
		{
			currentRuby += amount;
		}
	}
	public void DecreaseMoney(int amount, Currency i)
	{
		if (i == Currency.Coin)
		{
			currentCoin -= amount;
		}
		else if (i == Currency.Ruby)
		{
			currentRuby -= amount;
		}
	}
	public enum Currency
	{
		Coin,
		Ruby
	}
}
