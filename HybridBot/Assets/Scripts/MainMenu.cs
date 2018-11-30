using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {


	public void StartGame() {
		Cursor.visible = false;
		Cursor.lockState = UnityEngine.CursorLockMode.Confined;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
	}
	public void LoadMenu() {

	}
	public void Quit() {
		Application.Quit();
	}
}
