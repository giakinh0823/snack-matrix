using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour
{

    [SerializeField]
    private GameObject food;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 5, 5);
    }


    private void Spawn()
    {
        Vector3 spawnLocation = RandomLocation();
        Collider2D overlap = Physics2D.OverlapCircle(spawnLocation, 1f);

        while (overlap != null)
        {
            spawnLocation = RandomLocation();
            overlap = Physics2D.OverlapCircle(spawnLocation, 1f);
        }

        Instantiate(food, spawnLocation, Quaternion.identity);
    }

    private Vector3 RandomLocation()
    {
        float x = Random.Range(-SpawnMatrix.w, SpawnMatrix.w) * SpawnMatrix.k + 0.5f;
        float y = Random.Range(-SpawnMatrix.h, SpawnMatrix.h) * SpawnMatrix.k + 0.5f;
        return new Vector3(x, y, 0);
    }
}
