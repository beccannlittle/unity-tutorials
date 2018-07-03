using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTagByContact : MonoBehaviour {

    public bool shouldDestroySelf = true;
    public string[] tagsToDestroy;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (string tagtoDestroy in tagsToDestroy)
        {
            if (collision.gameObject.CompareTag(tagtoDestroy))
            {
                Destroy(collision.gameObject);
                if (shouldDestroySelf)
                {
                    Destroy(gameObject);
                }
                break;
            }
        }
    }
}
