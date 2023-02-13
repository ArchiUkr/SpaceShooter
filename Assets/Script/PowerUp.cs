using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    float speed = 3.0f;
    [SerializeField] int powerID;
    
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if(transform.position.y < -4.5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Player player = collision.transform.GetComponent<Player>();
            if(player != null)
            {   
                
                switch (powerID)
                {
                    case 0:
                        player.TripleShotOn();
                        break;
                    case 1:
                        player.SpeedBoostOn();
                        Debug.Log("Speed active");
                        break;
                    case 2:
                        player.ShieldsAvtive();
                        Debug.Log("Shield");
                        break;
                    default:
                        Debug.Log("Default");
                        break;
                }

            }
            Destroy(gameObject);
        }
    }
}
