using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text timeText;

    void Update(){
        timeText.text = String.Format("Playtime - {0:00:00}", Time.timeSinceLevelLoad);
    }
}

