using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPlatform : MonoBehaviour
{
    private void OnTriggerEnter(Collider player){            //sets the player to be a child of the moving platform, so they move with it
        Debug.Log("platform enter - check gem");
        GameManager.Instance.checkEndGame(player);
    }
}
