using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartHealth : MonoBehaviour
{
    public Sprite fullHeart, halfHeart, emptyHeart;
    Image heartImg;

    private void Awake(){
        heartImg = GetComponent<Image>();
    }

    public void SetHeartImg(HeartStatus status){
        if (status == HeartStatus.empty){
            heartImg.sprite = emptyHeart;
        }
        else if (status == HeartStatus.half){
            heartImg.sprite = halfHeart;
        }
        else if (status == HeartStatus.full){
            heartImg.sprite = fullHeart;
        }
    }
}

public enum HeartStatus{
    empty = 0,
    half = 1,
    full = 2
}