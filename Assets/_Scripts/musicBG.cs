using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicBG : MonoBehaviour
{
    [SerializeField] private AudioClip[] music;
    [SerializeField] private AudioSource bg;

    void Start(){
        playMusic();
        StartCoroutine(isPlaying());
    }

    void Awake(){
        DontDestroyOnLoad(this);
    }

    private void playMusic(){
        int selection = Random.Range(0, music.Length);
        
        Debug.Log(selection);

        bg.clip = music[selection];
        bg.Play();
    }    

    IEnumerator isPlaying(){
        //Debug.Log("couroutine start");
        while(true){
            yield return new WaitForSeconds(30);
            if(!bg.isPlaying) {                 
                playMusic();
            }
        }

    }
}
