using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("obstacles"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "plr")
        {
            Destroy(gameObject);
        }
    }
}
