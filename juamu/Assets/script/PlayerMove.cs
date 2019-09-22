using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rb;
    float movex = 0f;
    float movey = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movex = Input.GetAxis("Horizontal")*3;
        movey = Input.GetAxis("Vertical")*3;
        //movex=-1.0f;
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(movex, movey);          //移動
    }
}
