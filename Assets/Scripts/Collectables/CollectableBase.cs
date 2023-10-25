using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBase : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DisableCollider();
            OnCollect();
        }
    }

    protected void DisableCollider()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    protected virtual void OnCollect(float timeToDestroy = 0)
    {
        Destroy(gameObject, timeToDestroy);
    }
}
