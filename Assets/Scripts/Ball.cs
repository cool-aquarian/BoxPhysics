using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ball : MonoBehaviour
{

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        String collisionObjName = collision.gameObject.name;
        String collisionObjTag = collision.gameObject.tag;

        switch(collisionObjTag) {
            case "GameWall":
                Debug.Log("Collision Happened with Wall at: " + DateTime.Now);
                switch(collisionObjName) {
                    case "LeftBar":
                    if(gameManager.is4PModeGame()) {
                        gameManager.p1ScoredAgainst();
                    } else {
                        gameManager.p2Scored();
                    }
                    break;
                    case "RightBar":
                    if(gameManager.is4PModeGame()) {
                        gameManager.p3ScoredAgainst();
                    } else {
                        gameManager.p2Scored();
                    }
                    break;
                    case "TopBar":
                    if(gameManager.is4PModeGame()) {
                        gameManager.p2ScoredAgainst();
                    } else {
                        gameManager.p1Scored();
                    }
                    break;
                    case "BottomBar":
                    if(gameManager.is4PModeGame()) {
                        gameManager.p4ScoredAgainst();
                    } else {
                        gameManager.p1Scored();
                    }
                    break;

                }
                //gameManager.refreshScorecard();
                //StartCoroutine(gameManager.Launch(false));
            break;
            case "GamePaddle":
                Debug.Log("Collision Happened with Paddle at: " + DateTime.Now);
                switch(collisionObjName) {
                    case "P1Paddle":
                    break;
                    case "P3Paddle":
                    break;
                    case "P2Paddle":
                    break;
                    case "P4Paddle":
                    break;
                    
                }
            break;
        }
    }  
}
