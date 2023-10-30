using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using TMPro;

/// <summary>
/// Gerenciador tipo Singleton que trata dos itens coletaveis
/// presentes numa cena.
/// </summary>

public class CollectableManager : Singleton<CollectableManager>
{
    public SOInt collectables;

    public TMP_Text coinHud;
    public TMP_Text berryHud;
    public TMP_Text bombHud;

    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        collectables.totalCoins = 0;
        collectables.totalBerries = 0;
        collectables.totalBombs = 0;
    }

    public void AddCoins(int amount = 1)
    {
        collectables.totalCoins += amount;
        coinHud.text = (collectables.totalCoins < 10)
            ? "x0" + collectables.totalCoins.ToString()
            : "x" + collectables.totalCoins.ToString();
    }

    public void AddBerries(int amount = 1)
    {
        collectables.totalBerries += amount;
        berryHud.text = collectables.totalBerries.ToString();
    }

    public void AddBombs(int amount = 1)
    {
        collectables.totalBombs += amount;
        bombHud.text = collectables.totalBombs.ToString();
    }
}
