using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayer : MonoBehaviour
{
    [SerializeField] private Transform resetPos;

    private void OnTriggerEnter(Collider player){            //sets the player to be a child of the moving platform, so they move with it
        Debug.Log("Reset Player");
        
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = resetPos.position;
        player.GetComponent<CharacterController>().enabled = true;
    }

}
