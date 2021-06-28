using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public GameObject playerC;  
    float speed = 6.8f;
    Rigidbody playerRg;
    void Start()
    {
        playerRg = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        float acceleration = 0.01f;

        playerRg.velocity = new Vector3(playerRg.velocity.x, playerRg.velocity.y, speed += acceleration);

    }
}
