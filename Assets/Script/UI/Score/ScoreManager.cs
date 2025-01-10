using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
	public TMP_Text currentText;
	public TMP_Text highText;

	public static int currentScore;
	public static int highScore;

	private void Start()
	{
		currentScore = 0;
		currentText.text = "Current Score: " + currentScore;

		highScore = PlayerPrefs.GetInt("Score");
		highText.text = "High Score: " + highScore;
	}

	private void Update()
	{
		IncreaseScore();
	}

	private void IncreaseScore()
	{
		if (currentScore > highScore)
		{
			highScore = currentScore;
			PlayerPrefs.SetInt("Score", highScore);
		}

		currentText.text = "Current Score: " + currentScore;
		highText.text = "High Score: " + highScore;
	}
}
