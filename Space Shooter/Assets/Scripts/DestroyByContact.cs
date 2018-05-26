using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

    [SerializeField]
    private GameObject explosionPrefab, playerExplosionPrefab;

	private void OnTriggerEnter(Collider other)
	{
        if (other.tag == "Boundary") 
        {
            return;
        }
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        if (other.tag == "Player")
        {
            Instantiate(playerExplosionPrefab, other.transform.position, other.transform.rotation);
        }
        Destroy(other.gameObject);
        Destroy(gameObject);
	}
}
