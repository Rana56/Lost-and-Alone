using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;

public class GameMenuManager : MonoBehaviour
{
    [SerializeField] AudioSource menuSound;
    [SerializeField] AudioSource menuClose;
    [SerializeField] TMP_Text volumeTextUI;
    public AudioMixer audioMixer;

    public static bool GameIsPaused = false;

    public GameObject pauseUI;
    public GameObject startUI;
    public GameObject victoryUI;
    public GameObject endUI;
    public GameObject deathUI;

    void Awake(){
        //subscribe to event 
        GameManager.OnGameStateChange += GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState gameState)
    {
        //shows UI on start game
        if (gameState == GameState.start){
            PauseStart();
        } 
        else if (gameState == GameState.victory){
            Victory();
        }
        else if (gameState == GameState.death){
            Death();
        }
        else if (gameState == GameState.end){
            EndGame();
        }
    }

    void OnDestroy(){
        //unsubscribe from event
        GameManager.OnGameStateChange -= GameManagerOnGameStateChanged;
    }

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

    //------------------------------Start Menu------------------------------

    void PauseStart(){
        startUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Debug.Log("pauseStart");

        Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
    }

    //------------------------------Victory Menu------------------------------

    private void Victory()
    {
        victoryUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Debug.Log("victoryUI open");

        Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
    }

    //method goes to next level
    public void NextLevel(){
        Debug.Log("New Level");
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //------------------------------End Menu------------------------------

    private void EndGame()
    {
        endUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Debug.Log("endUI open");

        Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
    }

    //method goes to main menu
    public void GoMainMenu(){
        Debug.Log("Main Menu");
        SceneManager.LoadSceneAsync(0);
    }

    //------------------------------Death Menu------------------------------

    private void Death()
    {
        deathUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Debug.Log("DeathUI open");

        Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
    }

    //method goes to restart level
    public void RestartLevel(){
        Debug.Log("Restart Level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //------------------------------Pause Menu------------------------------

    public void Resume (){
        pauseUI.SetActive(false);
        startUI.SetActive(false);
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
