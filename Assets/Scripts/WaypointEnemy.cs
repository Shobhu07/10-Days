using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointEnemy : MonoBehaviour
{
    public GameObject[] wayPoints;
    public float Speed = 5f;
    public int health = 100;

    private int currentIndex = 0;
    // Start is called before the first frame update
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
    void Start()
    {
        transform.position = wayPoints[currentIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[currentIndex].transform.position, Speed * Time.deltaTime);

        if (transform.position == wayPoints[currentIndex].transform.position)
        {
            currentIndex++;
            if (currentIndex >= wayPoints.Length)
                currentIndex = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            TakeDamage(50);
            Destroy(collision.gameObject);
        }
    }
}
