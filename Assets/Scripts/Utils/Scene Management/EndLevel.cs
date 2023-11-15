using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Script para retornar ao menu
/// quando o jogador chega ao fim da fase.
/// </summary>

public class EndLevel : MonoBehaviour
{
    [Header("Associated Scene Loader")]
    public LoadScene loadScene;
    [Header("Event to Trigger")]
    public UnityEvent selectScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            selectScene.Invoke();
        }
    }

    private void OnDestroy()
    {
        selectScene.RemoveAllListeners();
    }
}
