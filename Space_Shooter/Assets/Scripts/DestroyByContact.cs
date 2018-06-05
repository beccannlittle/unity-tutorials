using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

    [SerializeField]
    private GameObject explosionPrefab, playerExplosionPrefab;

    public int scoreValue;

    private GameController gameController;

	private void Start()
	{
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find gamecontroller");
        }
	}

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
            gameController.GameOver();
        }
        gameController.AddScore(scoreValue);
        Destroy(other.gameObject);
        Destroy(gameObject);
	}
}
