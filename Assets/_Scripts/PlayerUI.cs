using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text timeText;

    void Update(){
        float t = Time.timeSinceLevelLoad;
		int mins = (int)( t / 60 );
		int rest = (int)(t % 60);
		timeText.text = string.Format("Playtime - {0:D2}:{1:D2}", mins, rest);
    }
}

