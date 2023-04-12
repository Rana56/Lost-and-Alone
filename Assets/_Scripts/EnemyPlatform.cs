using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlatform : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    private void OnTriggerEnter(Collider other){            //sets the player to be a child of the moving platform, so they move with it
        Debug.Log("platform enter -chase");
        enemy.GetComponent<AgentController>().setChase();
    }

    private void OnTriggerExit(Collider other){
        Debug.Log("platform exit - patrol");
        enemy.GetComponent<AgentController>().setPatrol();
    }
}
