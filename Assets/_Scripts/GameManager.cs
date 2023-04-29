using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //event for other scripts to subscribe to - check game state
    public static event Action<GameState> OnGameStateChange;

    [SerializeField] private PlayerUI ui;

    //game state of game
    public GameState State;

    void Awake(){
        Instance = this;
    }

    void Start(){
        UpdateGameState(GameState.start);
    }

    public void UpdateGameState(GameState newState){
        State = newState;

        switch(newState){
            case GameState.start:
                break;
            case GameState.victory:
                //run method when player complete level
                HandleVictory();
                break;
            case GameState.death:
                //run method when player dies
                HandleDeath();
                break;
            case GameState.end:
                HandleEnd();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        //triggers event - checks if there are subscribers, if true triggers event
        OnGameStateChange?.Invoke(newState);
    }

    private void HandleEnd()
    {
        Debug.Log("End event Invoke");
    }

    private void HandleVictory()
    {
        Debug.Log("Victory event Invoke");
        float time = ui.GetTime();
        Debug.Log(time);
        /*
        if(PlayerPrefs.HasKey("time")){
            float current = PlayerPrefs.GetFloat("time");

            if(time < current){
                PlayerPrefs.SetFloat("time", time);
            }
        }
        else {
            PlayerPrefs.SetFloat("time", time);
            Debug.Log(PlayerPrefs.GetFloat("time"));
        }*/

        if (time > PlayerPrefs.GetFloat("time", 0)){
            PlayerPrefs.SetFloat("time", time);
             
        }
        //should save score here
    }

    private void HandleDeath()
    {
        Debug.Log("Death event Invoke");
    }

    public void checkEndGame(Collider player){
        //gets collected gems
        int gems = player.GetComponent<PlayerInventory>().NumberOfGems;
        int totalGems = GameObject.FindGameObjectsWithTag("Gem").Length;

        //gems == totalGems - final
        //gems >= 2 - testing
        if (gems >= 2){
            if(SceneManager.GetActiveScene().name == "Map 2"){
                //check if final scence
                Debug.Log("End");
                UpdateGameState(GameState.end);
            } 
            else{
                Debug.Log("Victory");
                UpdateGameState(GameState.victory);
            }
        }

    }
}


public enum GameState {
    start,
    victory,
    death,
    end,
}