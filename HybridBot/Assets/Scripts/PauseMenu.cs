﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Gameplay: MonoBehaviour {
	public static bool IsPaused = false;

	protected void DoPause() {
		Time.timeScale = 0f;
		IsPaused = true;
		Cursor.lockState = UnityEngine.CursorLockMode.None;
		Cursor.visible = true;
	}

	protected void DoResume() {
		Cursor.visible = false;
		Cursor.lockState = UnityEngine.CursorLockMode.Confined;
		IsPaused = false;
		Time.timeScale = 1f;
	}
}

public class PauseMenu : Gameplay {

	public GameObject PauseUI;

	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			if(IsPaused) {
				Resume();
			} else {
				Pause();
			}

		}
	}


	void Pause() {
		DoPause();
		PauseUI.SetActive(true);
	}
	public void Resume() {
		PauseUI.SetActive(false);
		DoResume();
	}


	public void RestartLevel() {
		Resume();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	public void LoadMenu() {

	}
	public void Quit() {
		Application.Quit();
	}
}
