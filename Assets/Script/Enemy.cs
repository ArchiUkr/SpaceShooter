using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   public float speed = 4f;

    private Player player;
    Animator anim;


    private void Start()
    {
        player = GetComponent<Player>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y < -6f)
        {
            transform.position = new Vector3(Random.Range(-8f, 8f), 7, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log(name + "gameobj");
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            anim.SetTrigger("IsDead");
            speed = 0f;
            Destroy(gameObject, 3f);

        }
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (player != null)
            {
                player.AddScore(10);
            }
            anim.SetTrigger("IsDead");
            speed = 0f;
            Destroy(gameObject, 3f);
        }
    }
}
