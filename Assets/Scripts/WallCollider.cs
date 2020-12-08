using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollider : MonoBehaviour
{
    private static Vector2 RESET_VELOCITY = new Vector2(0, 0);
    private static string BALL_TAG = "Ball";

    [SerializeField] private Direction direction;

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
            ChangeDirection(collision.gameObject, direction);
        }
    }

    private void ChangeDirection(GameObject gameObject, Direction direction)
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = RESET_VELOCITY;
        PaddleController.instance.BounceOffBallCollider(gameObject, direction);
    }

    IEnumerator BallCollisionEnter2D()
    {
        yield return null;
    }
}

public enum Direction
{
    LEFT,
    RIGHT,
    TOP,
    BOTTOM
}
