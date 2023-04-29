using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int maxHealth = 6;
    public int currentHealth;
    
    [SerializeField] private HealthBar health_bar;

    public void Start(){
        //maxHealth = 6;
        currentHealth = maxHealth;
    }

    public int getMaxHealth(){
        return maxHealth;
    }

    public void takeDamage(int damage){
        int newHealth = currentHealth - damage;
        currentHealth = Mathf.Max(newHealth, 0);            //ensurse that health doesn't go negative
        health_bar.DrawHearts();

        if (IsDead()){
            Death();
            GameManager.Instance.UpdateGameState(GameState.death);
        }
    }

    public void addHealth(int heal){
        int newHealth = currentHealth + heal;
        currentHealth = Mathf.Min(newHealth, maxHealth);
        health_bar.DrawHearts();
    }

    //heals to full and increase max health
    public void MegaHealth(){
        maxHealth += 2;
        currentHealth = maxHealth;
        health_bar.DrawHearts();
    }

    public bool IsDead(){
        if (currentHealth == 0){
            return true;
        }
        else {
            return false;
        }
    }

    void Death(){
        GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        GetComponent<PlayerControl>().enabled = false;
    }

}
