using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Aqui definimos o comportamento dos projeteis
/// de acordo com a orientacao espacial do jogador.
/// </summary>

public class ProjectileBase : MonoBehaviour
{
    private GameObject _player;

    public int damage;
    public float translationSpeed;
    public float timeToDisable;

    private float _sign;

    private void Awake()
    {
        _player = GameObject.Find("Player");
    }

    private void OnEnable()
    {
        _sign = Mathf.Sign(_player.transform.localScale.x);
        StartCoroutine(Disable());
    }

    private void Update()
    {
        Move(_sign * translationSpeed * Vector3.right);
    }

    private void OnDisable()
    {
        StopCoroutine(Disable());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var obj = collision.gameObject;

        if (obj.CompareTag("Enemy"))
        {
            obj.GetComponent<HealthBase>().Damage(damage);
            gameObject.SetActive(false);
        }
    }

    private void Move(Vector3 direction)
    {
        transform.Translate(direction * Time.deltaTime);
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(timeToDisable);
        gameObject.SetActive(false);
    }
}
