using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject enemyContainer;

    [SerializeField] GameObject[] powerUps;
  

    bool stopSpawning = false;
    private void Start()
    {
       // StartSpawning();

    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnPowerUP());
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(3f);
        while (stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
           GameObject enemies = Instantiate(enemy, posToSpawn, Quaternion.identity);
            enemies.transform.parent = enemyContainer.transform;
            yield return new WaitForSeconds(5f);
        }
       
    }

    IEnumerator SpawnPowerUP()
    {
        yield return new WaitForSeconds(3f);
        while (stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomPowerUp = Random.Range(0, 3);
            Instantiate(powerUps[randomPowerUp], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }
  
    public void OnPlayerDeath()
    {
        stopSpawning = true;
    }
}
