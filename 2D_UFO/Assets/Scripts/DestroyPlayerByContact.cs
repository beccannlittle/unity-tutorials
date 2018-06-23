using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlayerByContact : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collided");
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().Die();
        }
    }
}
