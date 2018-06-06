using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour {

    public Vector2 startWaitRange;
    public Vector2 maneuverTimeRange;
    public Vector2 maneiverWaitRange;
    public float dodge;
    public float smoothing;
    public float tilt;
    public Boundary boundary;

    private float currentSpeed;
    private float targetManeuver;
    private Rigidbody rb;

	void Start () 
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = rb.velocity.z;
        StartCoroutine(Evade());
	}

    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWaitRange.x, startWaitRange.y));
        while (true)
        {
            targetManeuver = Random.Range(1, dodge * -Mathf.Sign(transform.position.x));
            yield return new WaitForSeconds(Random.Range(maneuverTimeRange.x, maneuverTimeRange.y));
            targetManeuver = 0;
            yield return new WaitForSeconds(Random.Range(maneiverWaitRange.x, maneiverWaitRange.y));
        }
    }
	
	void FixedUpdate () 
    {
        float newManeuver = Mathf.MoveTowards(rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);
        rb.velocity = new Vector3(newManeuver, 0.0f, currentSpeed);
        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
	}
}
