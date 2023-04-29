using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject heartPrefab;
    //public float health, maxHealth;
    public PlayerHealth player;

    List<HeartHealth> hearts = new List<HeartHealth>();         //list of hearts and update state

    public void Start(){
        DrawHearts();
    }

    public void DrawHearts(){
        RemoveHearts();

        //1 heart = 2 health points - find out how much hearts to make based on max health
        float remainder = player.getMaxHealth() % 2; //Checks if health is odd or even
        int totalHearts = (int) ((player.getMaxHealth() / 2) + remainder);

        for(int i = 0; i < totalHearts; i++){
            addEmptyHeart();
        }

        for(int j = 0; j < hearts.Count; j++){
            int heartStatus = (int)Mathf.Clamp(player.currentHealth - (j * 2), 0, 2);   //if value less than 0 or more than 2, returned will be either 0 or 2 otherwise regular value returned
            Debug.Log("heartstat: "+heartStatus);
            hearts[j].SetHeartImg((HeartStatus)heartStatus);
        }
    }

    public void addEmptyHeart(){
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);                //sets the new heart as a child of the health bar gameobject

        HeartHealth heartComponent = newHeart.GetComponent<HeartHealth>();
        heartComponent.SetHeartImg(HeartStatus.empty);
        hearts.Add(heartComponent);
    }

    public void RemoveHearts(){
        foreach(Transform t in transform){
            Destroy(t.gameObject);
        }
        hearts = new List<HeartHealth>();               //Removes heart gameobjects under the health bar parent in canavs
    }

}
