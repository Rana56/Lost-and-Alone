using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
	[SerializeField] AudioSource punchSound;

    void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			other.GetComponent<PlayerHealth>().takeDamage(1);
			punchSound.Play();
            Debug.Log("Player Hit");
		}
	}
}
