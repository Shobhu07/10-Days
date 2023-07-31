using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class fallingplatform : MonoBehaviour
{
    private float falldelay =1f;
    [SerializeField]private Rigidbody2D rb;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "plr")
        {
            StartCoroutine(fall());
        }
    }
    private IEnumerator fall()
    {
        yield return new WaitForSeconds(falldelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
