using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script basico para itens coletaveis em geral.
/// </summary>

public class CollectableBase : MonoBehaviour
{
    [Header("Sound Effect")]
    public AudioSource audioSource;

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
        PlaySound();
    }

    private void DisableCollider()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    private void PlaySound()
    {
        audioSource.Play();
    }
}
