using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script que interage com o HealthBase do player.
/// Pode spawnar um item coletavel quando o inimigo morre.
/// </summary>

public class EnemyBase : MonoBehaviour
{
    public int damage;
    public GameObject spawnOnKill;

    private Animator _animator;

    private bool _isRunning;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();

        _isRunning = true;

        GameObject
            .Find("PFB_Menu")
            .GetComponent<Button>()
            .onClick
            .AddListener(() => _isRunning = false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var obj = collision.gameObject;

        if(obj.CompareTag("Player"))
        {
            _animator.SetTrigger("Attack");
            obj.GetComponent<HealthBase>().Damage(damage);
        }
    }

    private void OnApplicationQuit()
    {
        _isRunning = false;
    }

    private void OnDestroy()
    {
        if (spawnOnKill != null && _isRunning)
        {
            Instantiate(spawnOnKill).transform.position = gameObject.transform.position;
        }
    }
}
