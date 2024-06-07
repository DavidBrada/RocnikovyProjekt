using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
	NextLevel nextLevel;
	public GameObject levelEndUI;
	public GameObject hud;

	void Start()
	{
		nextLevel = FindObjectOfType<NextLevel>();
	}
	public void LoadNextScene()
	{
		nextLevel.levelEnded = false;
		Time.timeScale = 1f;
		nextLevel.levelEndUI.SetActive(false);
		hud.SetActive(true);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void LoadMenu()
	{
		SceneManager.LoadScene(0);
	}
}
