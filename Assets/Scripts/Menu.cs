﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

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
}
