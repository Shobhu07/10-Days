using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour
{
    [SerializeField] float fs = 2f;
    [SerializeField] float Xoffset = 1f;
    [SerializeField] private Transform PlayerCam;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(PlayerCam.position.x +2f, PlayerCam.position.y +4f, +Xoffset -5f);
        transform.position = Vector3.Slerp(transform.position, newPos, fs*Time.deltaTime);    
    }


}
