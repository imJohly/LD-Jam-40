using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public GameObject pauseScreen;
	public GameObject normScreen;

	private bool pause;

	void Update()
	{
		if(pauseScreen != null)
		{
			if(Player.inst.GetComponent<PlayerHealth>().curHealth <= 0 || Player.inst.chickenAmount >= 21)
			{
				normScreen.SetActive(false);
				AudioManager.instance.Stop("Beep");
				AudioManager.instance.Stop("Main Theme");
				GameObject.FindGameObjectWithTag("Dialogue").SetActive(false);
				return;
			}

			if(Input.GetKeyDown(KeyCode.Escape))
				pause = !pause;
			if(pause)
			{
				Time.timeScale = 0;

				pauseScreen.SetActive(true);
				normScreen.SetActive(false);
				GameObject.FindGameObjectWithTag("Dialogue").SetActive(false);

				Cursor.lockState = CursorLockMode.None;
			}
			if(pause == false)
			{
				Time.timeScale = 1;

				pauseScreen.SetActive(false);
				normScreen.SetActive(true);
				GameObject.FindGameObjectWithTag("Dialogue").SetActive(true);
			}
		}
	}

	public void BackToMenu()
	{
		SceneManager.LoadScene(0);
	}

	public void Quit()
	{
		Application.Quit();
	}

	public void Play()
	{
		SceneManager.LoadScene(1);
	}

	public void ResumeFromPause()
	{
		pause = false;
	}

	public void TravelTo(string url)
	{
		Application.OpenURL(url);
	}
}
