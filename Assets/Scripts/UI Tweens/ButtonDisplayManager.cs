using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonDisplayManager : MonoBehaviour
{
    public List<GameObject> buttonReferences;

    [Header("Animation Settings")]
    // duration: duração da animação para cada botão;
    [SerializeField] public float duration;
    // delay: duração relativa ao intervalo entre a animação de um botão e a do próximo.
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
