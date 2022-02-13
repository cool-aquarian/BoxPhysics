using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public Rigidbody ball;
    public float speed;
    public Rigidbody rb;
    public Vector3 startPosition;

    private string axisName;
    private float movement;
    private bool isHorizPaddle = false;

    private static readonly int arena_size = 50;    //100 x 100
    private static readonly float paddle_half_size = 6.5f;
    private static readonly float paddle_half_plus_buffer_size = 20f;
    private static readonly float paddle_max_location = arena_size - paddle_half_plus_buffer_size;
    private static readonly float ai_speed = 0.75f;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        string rbName = rb.gameObject.name;

        switch (rbName)
        {
            case "P1Paddle":
                axisName = "Vertical";
                break;
            case "P3Paddle":
                axisName = "Vertical2";
                break;
            case "P2Paddle":
                axisName = "Horizontal";
                isHorizPaddle = true;
                break;
            case "P4Paddle":
                axisName = "Horizontal2";
                isHorizPaddle = true;
                break;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movement = Input.GetAxisRaw(axisName);
        //Debug.Log("Paddle: axisName=" + axisName + " movement=" + movement);
        if (isHorizPaddle)
        {
            //rb.velocity = new Vector3(movement * speed, 0, 0);

            //Let AI Control this:
            /*if(ball.position.x > rb.position.x) {
                movement = 1;
            } else if(ball.position.x < rb.position.x) {
                movement = -1;
            } else{
                movement = 0;
            }*/
            //ENd AI


            float newXPos = rb.position.x + (speed * movement);
            if (Mathf.Abs(newXPos) > paddle_max_location)
            {
                newXPos = paddle_max_location * movement;
            }
            rb.position = new Vector3(newXPos, rb.position.y, rb.position.z);
        }
        else
        {
            //rb.velocity = new Vector3(0, 0, movement * speed);

            //Let AI Control this:
            if(ball.position.z > rb.position.z) {
                movement = 1;
            } else if(ball.position.z < rb.position.z) {
                movement = -1;
            } else{
                movement = 0;
            }
            //ENd AI

            float newZPos = rb.position.z + (ai_speed * movement);
            if (Mathf.Abs(newZPos) > paddle_max_location)
            {
                newZPos = paddle_max_location * movement;
            }
            rb.position = new Vector3(rb.position.x, rb.position.y, newZPos);
        }

    }

    public void ResetPaddle()
    {
        rb.velocity = Vector3.zero;
        transform.position = startPosition;
    }
}
