using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// Script associado a amoras coletaveis,
/// implementa animacao por tweening
/// e interage com o Gerenciador de Coletaveis.
/// </summary>

public class CollectableBerry : CollectableBase
{
    [Header("Particle System")]
    public ParticleSystem emitParticlesOnCollect;
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

    protected override void OnCollect()
    {
        base.OnCollect();
        emitParticlesOnCollect.Play();
        _shrink.Play();
        CollectableManager.Instance.AddBerries();
        Destroy(gameObject, 1);
    }

    private void OnDestroy()
    {
        _spawn.Kill();
        _wave.Kill();
        _shrink.Kill();
    }
}
