using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private static float MIN_SPEED = 250;
    private static float MIN_VELOCITY_SPEED = 1f;
    private static float BALL_SPEED = 300;
    private static float CORRECT_BALL_SPEED = 30;
    private static float FORCE_POWER_X = 0.3f;
    private static float FORCE_POWER_Y = 0.9f;

    [SerializeField] private new Rigidbody2D rigidbody;

    [SerializeField] private float ballSpeed = BALL_SPEED;

    [SerializeField] private float lastForceX;
    [SerializeField] private float lastForceY;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        StartCoroutine("Loop");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Loop()
    {
        while (true)
        {
            CorrectVelocity();
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void CorrectVelocity()
    {
        if (PaddleController.instance.isStart && Mathf.Abs(rigidbody.velocity.y) != 0 && Mathf.Abs(rigidbody.velocity.y) < MIN_VELOCITY_SPEED)
        {
            Debug.Log("속도 조정 발동!");
            rigidbody.AddForce(new Vector2(lastForceX, lastForceY).normalized * CORRECT_BALL_SPEED);
        }
    }

    public void ShootBall()
    {
        ballSpeed = BALL_SPEED;
        lastForceX = FORCE_POWER_X;
        lastForceY = FORCE_POWER_Y;

        rigidbody.AddForce(new Vector2(FORCE_POWER_X, FORCE_POWER_Y).normalized * ballSpeed);
    }

    public void BounceOffBallPaddle(Vector3 colliderPosition)
    {
        rigidbody.velocity = Vector2.zero;

        lastForceX = transform.position.x - colliderPosition.x;
        lastForceY = transform.position.y - colliderPosition.y;

        rigidbody.AddForce(new Vector2(lastForceX, lastForceY).normalized * ballSpeed);
    }

    public void ChangeDirection(Direction direction)
    {
        rigidbody.velocity = Vector2.zero;
        ballSpeed = BALL_SPEED;
        BounceOffBallCollider(direction);
    }

    private void BounceOffBallCollider(Direction direction)
    {
        switch (direction)
        {
            case Direction.LEFT:
            case Direction.RIGHT:
                lastForceX = -lastForceX;
                rigidbody.AddForce(new Vector2(lastForceX, lastForceY).normalized * ballSpeed);
                break;
            case Direction.TOP:
            case Direction.BOTTOM:
                lastForceY = -lastForceY;
                rigidbody.AddForce(new Vector2(lastForceX, lastForceY).normalized * ballSpeed);
                break;
        }
    }
}
