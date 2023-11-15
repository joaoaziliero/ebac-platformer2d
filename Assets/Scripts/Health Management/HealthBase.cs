using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// O HealthBase gerencia os pontos de vida,
/// tanto do jogador quanto dos inimigos.
/// </summary>

public class HealthBase : MonoBehaviour
{
    private FlashColor _flashColor;
    private Animator _animator;

    [SerializeField]
    private int startHealth;
    private int _currentHealth;

    private void Awake()
    {
        _flashColor = GetComponentInChildren<FlashColor>();
        _animator = GetComponentInChildren<Animator>();
        _currentHealth = startHealth;
    }

    private void Kill()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        _animator.SetTrigger("Die");
        Instantiate(GetComponent<EnemyBase>().spawnOnKill).transform.position = transform.position;
        Destroy(gameObject, 0.75f);
    }

    public void Damage(int damage)
    {
        _flashColor.Flash();
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Kill();
        }
    }
}
