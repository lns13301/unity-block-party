using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    private static string BALL_TAG = "Ball";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == BALL_TAG)
        {
            float value = transform.position.x - collision.transform.position.x;

            PaddleController.instance.BounceOffBall(collision.gameObject, value);
        }
    }
}
