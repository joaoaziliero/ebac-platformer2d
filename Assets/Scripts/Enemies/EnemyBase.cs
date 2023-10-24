using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBase : MonoBehaviour
{
    public int damage;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var health = collision.gameObject.GetComponent<HealthBase>();

        if(health != null)
        {
            _animator.SetTrigger("Attack");
            health.Damage(damage);
        }
    }
}
