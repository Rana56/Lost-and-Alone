using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;

public class Menu : MonoBehaviour
{
    [SerializeField] AudioSource menuSound;
    [SerializeField] AudioSource menuClose;
    [SerializeField] TMP_Text volumeTextUI;
    public AudioMixer audioMixer;
    

    void Awake(){
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
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
