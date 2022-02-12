using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Rigidbody ball;
    //public Vector3 ballInitForce;

    public int initialVelocity;

    // Start is called before the first frame update
    void Start()
    {
        int x = Random.Range(0, 2)==0?-1:1;
        int z = Random.Range(0, 2)==0?-1:1;
        int xzRatio = Random.Range(0, initialVelocity);

        float zVelocity = Mathf.Sqrt(initialVelocity - xzRatio);
        float xVelocity = Mathf.Sqrt(xzRatio);

        Vector3 direction = new Vector3(xVelocity*x, 0f, zVelocity*z);
        //ball.velocity += ballInitForce;
        ball.velocity+=direction;

        Debug.Log("x="+ x + ", z=" + z + " xV=" + xVelocity + " zV=" + zVelocity + " velocity=" + getBallSpeed());
    }

    // Update is called once per frame
    void Update()
    {
        //Relaunch on pressing 'R'
        if(Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        } else if(Input.GetKeyDown(KeyCode.V)) {
            logBallSpeed();
        }
    }

    public void logBallSpeed() {
        Debug.Log("Ball speed is: " + getBallSpeed() + " at " + System.DateTime.Now);
    }

    public float getBallSpeed() {
        return Mathf.Sqrt(Mathf.Pow(ball.velocity.x, 2) + Mathf.Pow(ball.velocity.z, 2));
    }
}
