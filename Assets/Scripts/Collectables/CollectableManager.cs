using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using TMPro;

/// <summary>
/// Gerenciador tipo Singleton que trata dos itens coletaveis
/// presentes numa cena (por ora, apenas moedas).
/// </summary>

public class CollectableManager : Singleton<CollectableManager>
{
    public int coins;
    public TMP_Text _coinDisplay;

    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        coins = 0;
    }

    public void AddCoins(int amount = 1)
    {
        coins += amount;

        _coinDisplay
            .text
            = (coins < 10)
            ? "x0" + coins.ToString()
            : "x" + coins.ToString();
    }
}
