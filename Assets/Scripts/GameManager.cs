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

        Debug.Log("x="+ x + ", z=" + z + " xV=" + xVelocity + " zV=" + zVelocity + " velocity=" + Mathf.Sqrt(xVelocity*xVelocity + zVelocity*zVelocity));

        Vector3 direction = new Vector3(xVelocity*x, 0f, zVelocity*z);
        //ball.velocity += ballInitForce;
        ball.velocity+=direction;
    }

    // Update is called once per frame
    void Update()
    {
        //Relaunch on pressing 'R'
        if(Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
