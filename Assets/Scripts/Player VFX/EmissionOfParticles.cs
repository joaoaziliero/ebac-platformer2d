using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controle do sistema de partículas
/// associado ao movimento do jogador.
/// </summary>

public class EmissionOfParticles : MonoBehaviour
{
    public Player player;
    public ParticleSystem dustOnWalk;

    private ParticleSystem.EmissionModule _psEmit;
    private ParticleSystem.ShapeModule _psShape;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _psEmit = dustOnWalk.emission;
        _psShape = dustOnWalk.shape;
        _rb = player.GetComponent<Rigidbody2D>();
        player.vfx.AddListener(() => EmitDust());
    }

    private void Update()
    {
        if (_rb.velocity.x == 0 && _rb.velocity.y < 0)
        {
            _psEmit.enabled = false;
        }
        else
        {
            _psEmit.enabled = true;
        }
    }

    public void EmitDust()
    {
        if (_rb.velocity.y > 0)
        {
            _psShape.rotation = new Vector3(0, 0, -180);
        }
        else
        {
            _psShape.rotation = Vector3.zero;
        }
       
        dustOnWalk.Play();
    }

    private void OnDisable()
    {
        player.vfx.RemoveListener(() => EmitDust());
    }
}
