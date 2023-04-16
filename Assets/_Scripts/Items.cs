using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    [SerializeField] AudioClip collectSound;
    
    private void OnTriggerEnter(Collider other){
        PlayerInventory player = other.GetComponent<PlayerInventory>();
        PlayerHealth health = other.GetComponent<PlayerHealth>();

        if (player != null){
            if(gameObject.tag == "Gem"){
                player.GemCollect();
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
                Debug.Log("Gem collected");
            }
            else if (gameObject.tag =="Heart"){
                health.addHealth(1);
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
                Debug.Log("health added");
            }
            
            gameObject.SetActive(false);
        }
    }
}
