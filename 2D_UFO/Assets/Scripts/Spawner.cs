using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject asteroidPrefab;

	// Use this for initialization
	void Start () {
        Instantiate(asteroidPrefab, transform.position, transform.rotation);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
