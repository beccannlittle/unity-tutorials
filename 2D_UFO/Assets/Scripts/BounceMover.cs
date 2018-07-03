using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceMover : MonoBehaviour {

    public float speed = 1;

    private Rigidbody2D rb;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(getRandomDirection() * speed, ForceMode2D.Impulse);
	}

    private Vector2 getRandomDirection() {
        Vector2 randomDirection = Random.insideUnitCircle;
        randomDirection.Normalize();
        return randomDirection;
    }
	
}
