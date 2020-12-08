using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollider : MonoBehaviour
{
    private static string BALL_TAG = "Ball";

    [SerializeField] private Direction direction;

    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine("BallCollisionEnter2D");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == BALL_TAG)
        {
            DealEnteredBall(collision.gameObject);
        }
    }

    private void DealEnteredBall(GameObject ballObject)
    {
        ballObject.GetComponent<Ball>().ChangeDirection(direction);
    }
}

public enum Direction
{
    LEFT,
    RIGHT,
    TOP,
    BOTTOM
}
