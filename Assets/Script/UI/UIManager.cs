using UnityEngine;

public class UIManager : MonoBehaviour
{
	[Header("Pause")]
	public GameObject pause_UI;

	[Header("Inventory")]
	public GameObject inventory_UI;

	[Header("Map")]
	public GameObject map_UI;
	[HideInInspector] public bool isOpeningMap;
	private void Update()
	{
		Map();
	}
	public void Pause()
	{
		if (pause_UI.activeInHierarchy == false)
		{
			Time.timeScale = 0;
			pause_UI.SetActive(true);
		}
		else if (pause_UI.activeInHierarchy == true)
		{
			Time.timeScale = 1;
			pause_UI.SetActive(false);
		}
	}
	public void Inventory()
	{
		if (inventory_UI.activeInHierarchy == false)
		{
			inventory_UI.SetActive(true);
		}
		else if (inventory_UI.activeInHierarchy == true)
		{
			inventory_UI.SetActive(false);
		}
	}
	public void Map()
	{
		if (Input.GetKeyDown(KeyCode.I) && !isOpeningMap)
		{
			isOpeningMap = true;
			map_UI.SetActive(true);
		}
		else if (Input.GetKeyDown(KeyCode.I) && isOpeningMap)
		{
			isOpeningMap = false;
			map_UI.SetActive(false);
		}
	}
	public void Exit()
	{
		Application.Quit();
	}
}
