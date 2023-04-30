using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//keeps track of elapsed time
public class TimeScoreManager : MonoBehaviour
{
    private float totalTime;
    public static TimeScoreManager Instance { get; private set; }

    void Awake(){
        // force singleton instance
        if (Instance == null) { Instance = this; }
        else { Destroy (gameObject); }
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        totalTime = 0;    
    }

    public void AddTime(float time){
        totalTime += time;
    }

    public float GetTime(){
        return totalTime;
    }

    public void resetTime(){
        totalTime = 0;
    }

}
