using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonDisplayManager : MonoBehaviour
{
    //Lista de referências aos botões presentes na cena.
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
        // Com este loop, cada botão é animado passando de dimensões nulas
        // até seu tamanho máximo, e a animação do próximo botão fica mais lenta
        // em relação ao botão anterior.
        foreach(var button in buttonReferences)
        {
            button.transform
                .DOScale(0, duration)
                .From()
                .SetDelay(buttonReferences.IndexOf(button) * delay);
        }
    }
}
