using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text bestTime;
    [SerializeField] private TMP_Text gemText;
    private int totalGems;
    private int collectedGem = 0;

    private float t;

    void Start(){
        totalGems = GameObject.FindGameObjectsWithTag("Gem").Length;
        gemText.text = string.Format("Gems: {0}/{1}", collectedGem, totalGems);
        getBest();
    }

    void Update(){
        t = Time.timeSinceLevelLoad;
		int mins = (int)( t / 60 );
		int rest = (int)(t % 60);
		timeText.text = string.Format("Playtime - {0:D2}:{1:D2}", mins, rest);
    }

    public void UpdateGemText(PlayerInventory player){
        collectedGem = player.NumberOfGems;
        gemText.text = string.Format("Gems: {0}/{1}", collectedGem, totalGems);
    }

    private void getBest(){
        t = PlayerPrefs.GetFloat("time");
		int mins = (int)( t / 60 );
		int rest = (int)(t % 60);
        bestTime.text = string.Format("Best Time: {0:D2}:{1:D2}", mins, rest);
    }

    public float GetTime(){
        return t;
    }
}

