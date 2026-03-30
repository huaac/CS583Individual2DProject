using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script is attatched to the items in the window to simulate movement
public class CharacterBehaviour : MonoBehaviour
{

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //Move();
        //Invoke("Move",2);
    }


    // if gameobject touches the frame, it changes directions
    void Update()
    {
        if(rb.linearVelocity.x == 0)
        {
            //Debug.Log("x = 0");
            rb.linearVelocity = new Vector2(Random.Range(1,3), rb.linearVelocity.y);
        }
        else if (rb.linearVelocity.y == 0)
        {
            //Debug.Log("y = 0");
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Random.Range(1,3));
        }
    }

    // void Move()
    // {
    //     float rand = Random.Range(0,2);
    //     if(rand < 1)
    //     {
    //         rb.AddForce(new Vector2(100,-15));
    //     }
    //     else
    //     {
    //         rb.AddForce(new Vector2(-200, -15));
    //     }
    // }
}
