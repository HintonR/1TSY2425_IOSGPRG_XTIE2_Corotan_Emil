using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject _enemy;
    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(6f);
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = new Vector3(1f, 6f, 0);
        Quaternion spawnRotation = Quaternion.Euler(0, 0, -90); 
        Instantiate(_enemy, spawnPosition, spawnRotation);
    }
}

