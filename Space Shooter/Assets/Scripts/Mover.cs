using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    [SerializeField]
    private float speed;

    private Rigidbody rb;

	private void Awake()
	{
        rb = GetComponent<Rigidbody>();
	}

	private void Start() {
        rb.velocity = transform.forward * speed;
	}

}
