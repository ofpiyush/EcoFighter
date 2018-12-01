using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class AudioFile {
    public string Name;
    public AudioClip Sound;

    [Range(0f,1f)]
    public float Volume = 1f;
    private AudioSource audioSource;


    public void SetSource(AudioSource source) {
        audioSource = source;
        audioSource.clip = Sound;
        audioSource.loop = false;
    }

    public void Play() {
        audioSource.Play();
    }


}
public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public GameObject HealthBar;
    public float SunMultiplier = 0f;
    public float PollutionPercentage = 0f;
    public bool isIntroScene = true;

	public List<AudioFile> audioFiles;

    private Dictionary<string, AudioFile> Sounds;

    void Awake()
    {
        if (GameManager.instance == null)
        {
            GameManager.instance = this;
        }
        else if (GameManager.instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        Sounds = new Dictionary<string, AudioFile>();
        for (int i = 0; i <audioFiles.Count; i++) {
            GameObject obj = new GameObject("Audio_file_"+audioFiles[i].Name);
            audioFiles[i].SetSource(obj.AddComponent<AudioSource>());
            Sounds.Add(audioFiles[i].Name,audioFiles[i]);
        }
    }

    public void PlayAudio(string which) {
        Sounds[which].Play();
    }

    public void GameOver() {
        LevelManager lm = FindObjectOfType<LevelManager>();
        if (lm) {
            lm.GameOver();
        }
    }

}
