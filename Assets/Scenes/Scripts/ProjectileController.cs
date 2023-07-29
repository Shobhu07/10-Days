using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;


    private Vector2 direction;
    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "plr")
        {
            Destroy(gameObject);
        }
    }


}

