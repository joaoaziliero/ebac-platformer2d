using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollectableCoin : CollectableBase
{
    private Tween _spawn, _wave, _shrink;

    private void Awake()
    {
        gameObject.transform.localScale = Vector3.zero;

        _spawn =
        gameObject.transform
            .DOScale(Vector3.one, 0.25f);

        _wave =
        gameObject.transform
            .DOLocalRotate(new Vector3(0, 0, -5), 0.25f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);

        _shrink =
        gameObject.transform
            .DOScale(Vector3.zero, 0.1f)
            .SetEase(Ease.InBack)
            .Pause();
    }

    protected override void OnCollect(float timeToDestroy = 0)
    {
        _shrink.Play();
        CollectableManager.Instance.AddCoins();
        Debug.Log(CollectableManager.Instance.coins);
        base.OnCollect(timeToDestroy);
    }

    private void OnDestroy()
    {
        _spawn.Kill();
        _wave.Kill();
        _shrink.Kill();
    }
}
