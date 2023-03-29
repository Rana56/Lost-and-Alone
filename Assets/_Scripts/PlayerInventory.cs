using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public int NumberOfGems {get; private set;}

    public UnityEvent<PlayerInventory> OnGemCollected;

    public void GemCollect(){
        NumberOfGems++;
        OnGemCollected.Invoke(this);
    }
}
