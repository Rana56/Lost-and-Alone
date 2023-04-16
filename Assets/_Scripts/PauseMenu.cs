using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] AudioSource menuSound;
    [SerializeField] AudioSource menuClose;
    [SerializeField] TMP_Text volumeTextUI;
    public AudioMixer audioMixer;

    public static bool GameIsPaused = false;
    public GameObject pauseUI;

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(GameIsPaused){
                Resume();
            }
            else {
                Pause();
            }
        }
    }

    public void Resume (){
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        CloseMenu();

        Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
    }

    void Pause(){
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
    }

    public void Settings(){
        Debug.Log("Settings button");
        menuSound.Play();
    }

    public void SaveGame(){
        Debug.Log("Save Game");
    }

    public void Quit(){
        Debug.Log("Quit pressed");

        if (Application.isEditor){
            EditorApplication.ExitPlaymode();
        }
        else{
            Application.Quit();
        }
    }

    public void SetVolume(float volume){
        audioMixer.SetFloat("Volume", volume);

        float percent = (volume+80)/80 * 100;
        volumeTextUI.text = Mathf.Round(percent).ToString();
    }

    public void CloseMenu(){
        Debug.Log("Close menu");
        menuClose.Play();
    }

	IEnumerator WaitForSound(){
		menuSound.Play();
		yield return new WaitForSeconds((float)1.5);
		SceneManager.LoadSceneAsync(1);
	}

}
