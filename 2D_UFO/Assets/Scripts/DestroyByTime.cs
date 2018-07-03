using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour 
{
    public float minLifetime;
    public float maxLifetime;

	private void Start()
	{
        Destroy(gameObject, Random.Range(minLifetime, maxLifetime));
	}
}
