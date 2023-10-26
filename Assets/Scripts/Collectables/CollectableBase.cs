using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script basico para itens coletaveis em geral.
/// </summary>

public class CollectableBase : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnCollect();
        }
    }
    protected virtual void OnCollect()
    {
        DisableCollider();
    }

    private void DisableCollider()
    {
        GetComponent<Collider2D>().enabled = false;
    }
}
