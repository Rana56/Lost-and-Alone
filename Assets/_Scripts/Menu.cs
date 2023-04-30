using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;
using System;

public class Menu : MonoBehaviour
{
    [SerializeField] private AudioSource menuSound;
    [SerializeField] private AudioSource menuClose;
    [SerializeField] private TMP_Text volumeTextUI;
    [SerializeField] private TMP_Text bestTime;
    public AudioMixer audioMixer;
    

    void Awake(){
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;

        if (TimeScoreManager.Instance != null){
            TimeScoreManager.Instance.resetTime();
        }

        GameObject bgObject = GameObject.Find("BGMusic");
        if(bgObject != null){
            Destroy(bgObject);
        }
	}

    void Start(){
        RemoteHighScoreManager.Instance.GetHighScore(UpdateTimeUI);
    }

    void UpdateTimeUI(float score){
        int mins = (int)(score / 60 );
		int rest = (int)(score % 60);

        if (score > 0) bestTime.text = string.Format("Total Best Time - {0:D2}:{1:D2}", mins, rest);
        else bestTime.text = "No Best Time!";
    }

    public void StartGame(){
        Debug.Log("Start button");
        StartCoroutine(WaitForSound());
    }

    public void Settings(){
        Debug.Log("Settings button");
        menuSound.Play();
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
