using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : Gameplay {


	public GameObject Controls;
	public GameObject Menu;

	private void Start() {
		DoResume();
		Cursor.lockState = UnityEngine.CursorLockMode.None;
		Cursor.visible = true;

	}

	public void StartGame() {
		Cursor.visible = false;
		Cursor.lockState = UnityEngine.CursorLockMode.Confined;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
	}

	public void LoadMenu() {
		Controls.SetActive(false);
		Menu.SetActive(true);
	}

	public void LoadCredits() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+2);
	}

	public void Quit() {
		Application.Quit();
	}
	public void LoadControlMenu() {
		Menu.SetActive(false);
		Controls.SetActive(true);
	}
}
