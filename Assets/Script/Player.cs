using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform lasersGun;
    [SerializeField] GameObject lasers;
    [SerializeField] GameObject tripleShootPrefab;
    [SerializeField] float speed = 3f;
    [SerializeField] float speedMultiplayer = 2;
    [SerializeField] int lives = 3;
    float fireRate = 0.5f;
    float canFire = -1f;
    EnemySpawner spawnManager;

   [SerializeField] bool tripleShoot = false;
   [SerializeField] bool isSpeedBosstOn = false;
    [SerializeField] bool isShieldActive = false;

    [SerializeField] GameObject shield;
    [SerializeField] int score;

    private UIManager uiManager;
    
    private void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        spawnManager = GameObject.Find("Enemy_Spawner").GetComponent<EnemySpawner>();
        isSpeedBosstOn = false;

        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>(); 

        if(spawnManager == null)
        {
            Debug.Log("SpawnMAnager is null");
        }
        if(uiManager == null)
        {
            Debug.LogError("is 0");
        }
    }

    private void Update()
    {
        PlayerMovement();
        Lasers();
    }

    private void Lasers()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > canFire)
        {
            canFire = Time.time + fireRate;
            if(tripleShoot == true)
            {
                Instantiate(tripleShootPrefab, transform.position, Quaternion.identity);
            }
            else
            {
               Instantiate(lasers, lasersGun.position, Quaternion.identity);
            }
           
        }
    }

    private void PlayerMovement()
    {
        float hotizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(hotizontalInput, verticalInput, 0);
        transform.Translate(direction * speed * Time.deltaTime);
        
        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }

        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }


    public void Damage()
    {

        if(isShieldActive == true)
        {
            isShieldActive = false;
            shield.SetActive(false);
            return;
        }

        lives--;

        uiManager.UpdateLives(lives);

        if(lives < 1)
        {
            spawnManager.OnPlayerDeath();
            Destroy(gameObject);
        }
    }

    public void TripleShotOn()
    {
        tripleShoot = true;
        StartCoroutine(OffTripleShot());
    }

    IEnumerator OffTripleShot()
    {
        
        yield return new WaitForSeconds(5f);
        tripleShoot = false;
    }
    public void SpeedBoostOn()
    {
        isSpeedBosstOn = true;
        speed *= speedMultiplayer;
        StartCoroutine(SpeedBoostRoutine());
    }
    IEnumerator SpeedBoostRoutine()
    {
        yield return new WaitForSeconds(5f);
        isSpeedBosstOn = false;
        speed /= speedMultiplayer;
    }

    public void ShieldsAvtive()
    {
        shield.SetActive(true);
        isShieldActive = true;
    }
    public void AddScore(int points)
    {
        score += points;
        uiManager.UpdateScore(score);

    }
}
