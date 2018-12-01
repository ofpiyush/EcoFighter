using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {


	public GameObject Controls;
	public GameObject Menu;
	public void StartGame() {
		Cursor.visible = false;
		Cursor.lockState = UnityEngine.CursorLockMode.Confined;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
	}
	public void LoadMenu() {
		Controls.SetActive(false);
		Menu.SetActive(true);
	}

	public void LoadControlMenu() {
		Menu.SetActive(false);
		Controls.SetActive(true);
	}
	public void Quit() {
		Application.Quit();
	}
}
