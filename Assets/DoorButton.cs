using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    public GameObject DoorOpen;
    public GameObject DoorClose;
    public GameObject DoorColli;
    // Start is called before the first frame update
    void Start()
    {
        DoorOpen.SetActive(false);
        DoorColli.SetActive(true);
        DoorClose.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            DoorOpen.SetActive(true);
            DoorColli.SetActive(false);
            DoorClose.SetActive(false);
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
