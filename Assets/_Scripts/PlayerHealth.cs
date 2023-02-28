using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 6;
    public int currentHealth;

    public void Start(){
        //maxHealth = 6;
        currentHealth = maxHealth;
    }

    public void takeDamage(int damage){
        int newHealth = currentHealth - damage;
        currentHealth = Mathf.Max(newHealth, 0);            //ensurse that health doesn't go negative
    }

    public void addHealth(int heal){
        int newHealth = currentHealth + heal;
        currentHealth = Mathf.Max(newHealth, maxHealth);
    }


}
