using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] AudioSource menuSound;

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

	IEnumerator WaitForSound(){
		menuSound.Play();
		yield return new WaitForSeconds((float)1.5);
		SceneManager.LoadSceneAsync(1);
	}

}
