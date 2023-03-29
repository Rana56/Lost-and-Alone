using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    private void OnTriggerEnter(Collider other){
        PlayerInventory player = other.GetComponent<PlayerInventory>();
        PlayerHealth health = other.GetComponent<PlayerHealth>();

        if (player != null){
            if(gameObject.tag == "Gem"){
                player.GemCollect();
            }
            else if (gameObject.tag =="Heart"){
                health.addHealth(1);
                Debug.Log("health added");
            }
            
            gameObject.SetActive(false);
        }
    }
}
