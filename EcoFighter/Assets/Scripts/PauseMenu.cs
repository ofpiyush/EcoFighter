using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Gameplay: MonoBehaviour {
	public static bool IsPaused = false;
	public static bool IsGameEnded = false;

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
	private AudioSource happy;
	public AudioSource mainMusic;

	private void Awake() {
		happy = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (IsGameEnded) {
			return;
		}
		if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 7")) {
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
		mainMusic.Stop();
		happy.Play(0);
	}
	public void Resume() {
		happy.Stop();
		mainMusic.Play(0);
		PauseUI.SetActive(false);
		DoResume();
	}


	public void RestartLevel() {
		Resume();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	public void LoadMenu() {
		SceneManager.LoadScene(0);
	}
	public void LoadCredits() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
	}
	public void Quit() {
		Application.Quit();
	}
}
