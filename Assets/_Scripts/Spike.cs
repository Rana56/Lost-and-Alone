using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] private Transform resetPos;

    private void OnTriggerEnter(Collider player){            //sets the player to be a child of the moving platform, so they move with it
        player.GetComponent<PlayerHealth>().takeDamage(2);

        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = resetPos.position;
        player.GetComponent<CharacterController>().enabled = true;
    }
}
