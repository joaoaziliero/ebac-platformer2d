using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using TMPro;

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
        _coinDisplay.text = (coins < 10) ? "x0" + coins.ToString() : "x" + coins.ToString();
    }
}
