using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private static float MIN_SPEED = 250;
    private static float MIN_VELOCITY_SPEED = 1f;
    private static float BALL_SPEED = 450;
    private static float CORRECT_BALL_SPEED = 30;
    private static float FORCE_POWER_X = 0.3f;
    private static float FORCE_POWER_Y = 0.9f;
    private static float FORCE_POWER_LEFT = 0.4f;
    private static float FORCE_POWER_RIGHT = -0.4f;
    private static float FORCE_POWER_ZERO = 0;
    private static float GRAVITY_SCALE = 0;
    private static float CORRECT_VALUE = 0.8f;
    private static float BOTTOM_LINE_Y_LIMIT = -4.5f;
    private static float LEFT_LINE_X_LIMIT = -2.6f;
    private static float RIGHT_LINE_X_LIMIT = 2.6f;
    private static float LINE_CHECK_TIMER_LIMIT = 4f;
    private static float LINE_CHECK_TIMER_RESET = 0;
    private static float RESET_FORCE_POWER = 0.4f;
    private static Vector2 RESET_BALL_POSITION = new Vector2(0f, -2f);
    private static Vector2 PADDLE_FORCE_UP_VALUE = new Vector2(0.1f, 0.1f);
    private static string BRICK_TAG = "Brick";
    private static string PADDLE_TAG = "Paddle";

    [SerializeField] private new Rigidbody2D rigidbody;

    [SerializeField] private float ballSpeed;

    [SerializeField] private float lastForceX;
    [SerializeField] private float lastForceY;

    [SerializeField] private float bottomStateTimer;
    [SerializeField] private float leftStateTimer;
    [SerializeField] private float rightStateTimer;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        ballSpeed = BALL_SPEED;

        //StartCoroutine("Loop");
    }

    // Update is called once per frame
    void Update()
    {
        BottomStateCheck();
        LeftStateCheck();
        RightStateCheck();
        TopStateCheck();
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
            lastForceX = (lastForceX * CORRECT_VALUE);
            lastForceY = (lastForceY * CORRECT_VALUE);
            rigidbody.AddForce(new Vector2(lastForceX, lastForceY).normalized * CORRECT_BALL_SPEED);
        }
    }

    public void ShootBall()
    {
        ballSpeed = BALL_SPEED;
        lastForceX = FORCE_POWER_X;
        lastForceY = FORCE_POWER_Y;

        rigidbody.AddForce(new Vector2(FORCE_POWER_X, FORCE_POWER_Y).normalized * ballSpeed);
        rigidbody.gravityScale = GRAVITY_SCALE;
    }

    public void BounceOffBallPaddle(Vector3 colliderPosition)
    {
        rigidbody.velocity = Vector2.zero;

        lastForceX = transform.position.x - colliderPosition.x;
        lastForceY = transform.position.y - colliderPosition.y;

        rigidbody.AddForce(new Vector2(lastForceX, lastForceY).normalized * ballSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != BRICK_TAG)
        {
            SoundManager.instance.PlayOneShotEffectSound(1);
        }

        if (collision.gameObject.tag == PADDLE_TAG)
        {
            ReflectBall(collision.transform.position);
        }
    }

    /*    public void ChangeDirection(Direction direction)
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
    }*/

    // 버그 발생시 조정
    private void TopStateCheck()
    {
        if (transform.position.y > -BOTTOM_LINE_Y_LIMIT)
        {
            bottomStateTimer += Time.deltaTime;
        }
        else
        {
            bottomStateTimer = LINE_CHECK_TIMER_RESET;
        }

        if (bottomStateTimer > LINE_CHECK_TIMER_LIMIT)
        {
            AddForceBall(RESET_FORCE_POWER, FORCE_POWER_Y);
            bottomStateTimer = LINE_CHECK_TIMER_RESET;
        }
    }

    private void BottomStateCheck()
    {
        if (transform.position.y < BOTTOM_LINE_Y_LIMIT)
        {
            bottomStateTimer += Time.deltaTime;
        }
        else
        {
            bottomStateTimer = LINE_CHECK_TIMER_RESET;
        }

        if (bottomStateTimer > LINE_CHECK_TIMER_LIMIT)
        {
            AddForceBall(RESET_FORCE_POWER, FORCE_POWER_Y);
            bottomStateTimer = LINE_CHECK_TIMER_RESET;
        }
    }

    private void LeftStateCheck()
    {
        if (transform.position.x < LEFT_LINE_X_LIMIT)
        {
            leftStateTimer += Time.deltaTime;
        }
        else
        {
            leftStateTimer = LINE_CHECK_TIMER_RESET;
        }

        if (leftStateTimer > LINE_CHECK_TIMER_LIMIT)
        {
            AddForceBall(FORCE_POWER_LEFT, RESET_FORCE_POWER);
            leftStateTimer = LINE_CHECK_TIMER_RESET;
        }
    }

    private void RightStateCheck()
    {
        if (transform.position.x > RIGHT_LINE_X_LIMIT)
        {
            rightStateTimer += Time.deltaTime;
        }
        else
        {
            rightStateTimer = LINE_CHECK_TIMER_RESET;
        }

        if (rightStateTimer > LINE_CHECK_TIMER_LIMIT)
        {
            AddForceBall(FORCE_POWER_RIGHT, RESET_FORCE_POWER);
            rightStateTimer = LINE_CHECK_TIMER_RESET;
        }
    }

    private void AddForceBall(float powerX, float powerY)
    {
        lastForceX = powerX;
        lastForceY = powerY;
        rigidbody.velocity = Vector2.zero;
        rigidbody.AddForce(new Vector2(lastForceX, lastForceY).normalized * ballSpeed);
        transform.position = RESET_BALL_POSITION;
    }

    public void ReflectBall(Vector3 objectPosition)
    {
        rigidbody.AddForce((transform.position - objectPosition).normalized * ballSpeed / 30);
    }
}
