using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    private GameObject _cannon;

    public GameObject projectilePrefab;
    public int poolSize;

    private List<GameObject> _pooledProjectiles;

    private void Awake()
    {
        _cannon = GameObject.Find("Cannon");
        _pooledProjectiles = new List<GameObject>(poolSize);

        for (int i = 0; i < poolSize; i++)
        {
            _pooledProjectiles.Add(Instantiate(projectilePrefab));
            _pooledProjectiles[i].SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        /*
        var projectile = Instantiate(projectilePrefab);
        projectile.transform.position = _cannon.transform.position;
        */

        for (int i = 0; i < poolSize; i++)
        {
            var projectile = _pooledProjectiles[i];

            if (!projectile.activeInHierarchy)
            {
                projectile.transform.position = _cannon.transform.position;
                projectile.SetActive(true);
                return;
            }
        }
    }
}
