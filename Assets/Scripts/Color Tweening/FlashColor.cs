using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// A cada dano recebido pelo player ou por inimigos,
/// o FlashColor sinaliza isso (altera por tweens a cor do personagem).
/// </summary>

public class FlashColor : MonoBehaviour
{
    public Color endColor;
    public float flashDuration;

    private List<Tween> _flashes;

    private void Awake()
    {
        var arrayOfSprites = GetComponentsInChildren<SpriteRenderer>();
        
        _flashes = new List<Tween>(arrayOfSprites.Length);

        foreach (var sprite in arrayOfSprites)
        {
            _flashes
                .Add(sprite
                .DOColor(endColor, flashDuration)
                .SetLoops(2, LoopType.Yoyo)
                .SetAutoKill(false)
                .Pause());
        }
    }

    public void Flash()
    {
        foreach (var flash in _flashes)
        {
            flash.Restart();
        }
    }
}
