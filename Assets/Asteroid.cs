using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] float rotateAsteroid = 3f;
    [SerializeField] GameObject explosionPrefab;
    EnemySpawner spawnManager;
    private void Start()
    {
        spawnManager = GameObject.Find("Enemy_Spawner").GetComponent<EnemySpawner>();
    }
    private void Update()
    {
        transform.Rotate(Vector3.forward * rotateAsteroid * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Laser")
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            spawnManager.StartSpawning();
            Destroy(gameObject, 0.5f);
        }
    }
}
