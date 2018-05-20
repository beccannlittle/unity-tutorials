using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float speed, tilt, fireRate;
    [SerializeField]
    private Boundary boundary;
    [SerializeField]
    private GameObject boltPrefab;
    [SerializeField]
    private Transform shotSpawn;

    private Rigidbody rb;

	private void Awake() {
        rb = GetComponent<Rigidbody>();
	}

    private float nextFire = 0.0f;

	private void Update() {
        if (Input.GetButton("Fire1") && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            Instantiate(boltPrefab, shotSpawn.position, shotSpawn.rotation);
        }
	}

	void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;
        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
	}
}

[System.Serializable]
public class Boundary {
    public float xMin, xMax, zMin, zMax;
}