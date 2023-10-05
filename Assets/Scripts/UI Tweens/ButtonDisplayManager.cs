using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonDisplayManager : MonoBehaviour
{
    public List<GameObject> buttonReferences;

    [Header("Animation Settings")]
    // duration: dura��o da anima��o para cada bot�o;
    [SerializeField] public float duration;
    // delay: dura��o relativa ao intervalo entre a anima��o de um bot�o e a do pr�ximo.
    [SerializeField] public float delay;

    private void Awake()
    {
        foreach(var button in buttonReferences)
        {
            button.SetActive(true);
        }
    }

    private void OnEnable()
    {
        foreach(var button in buttonReferences)
        {
            button.transform
                .DOScale(0, duration)
                .From()
                .SetDelay(buttonReferences.IndexOf(button) * delay);
        }
    }
}
