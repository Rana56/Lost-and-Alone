using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartGame(){
        Debug.Log("Start button");
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);
    }

    public void Settings(){
        Debug.Log("Settings button");
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

}
