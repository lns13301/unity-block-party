using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private static float MIN_SPEED = 250;
    private static float MIN_VELOCITY_SPEED = 1f;
    private static float BALL_SPEED = 0.2f;
    private static float MAX_BALL_SPEED = 1f;
    private static float BALL_SPEED_WEIGHT = 0.01f; // 패들에 튕긴 공의 속도 가중치
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
    private static float LINE_CHECK_TIMER_LIMIT = 2.5f;
    private static float LINE_CHECK_TIMER_RESET = 0;
    private static float RESET_FORCE_POWER = 0.4f;
    private static Vector2 RESET_BALL_POSITION = new Vector2(0f, -2f);
    private static Vector2 PADDLE_FORCE_UP_VALUE = new Vector2(0.1f, 0.1f);

    private static string BRICK_TAG = "Brick";
    private static string PADDLE_TAG = "Paddle";
    private static string WALL_TAG = "Wall";

    [SerializeField] private new Rigidbody2D rigidbody;

    [SerializeField] private float ballSpeed;

    [SerializeField] private float lastForceX;
    [SerializeField] private float lastForceY;

    [SerializeField] private float bottomStateTimer;
    [SerializeField] private float leftStateTimer;
    [SerializeField] private float rightStateTimer;

    [SerializeField] private Vector2 velocity;
    [SerializeField] private float ballSpeedWeight;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        ballSpeed = BALL_SPEED;
        ballSpeedWeight = BALL_SPEED_WEIGHT;

        StartCoroutine("Loop");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveBall();

        BottomStateCheck();
        LeftStateCheck();
        RightStateCheck();
        TopStateCheck();
    }

    IEnumerator Loop()
    {
        while (true)
        {
            // CorrectVelocity();
            SetVelocity();
            yield return null;
        }
    }

    private void MoveBall()
    {
        transform.position = new Vector2(transform.position.x + velocity.x, transform.position.y + velocity.y);
        Debug.Log(velocity.x + ", " + velocity.y);
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

    private void SetVelocity()
    {
        if (velocity.x > MAX_BALL_SPEED)
        {
            velocity.x = MAX_BALL_SPEED;
        }

        if (velocity.x < -MAX_BALL_SPEED)
        {
            velocity.x = -MAX_BALL_SPEED;
        }

        if (velocity.y > MAX_BALL_SPEED)
        {
            velocity.y = MAX_BALL_SPEED;
        }

        if (velocity.y < -MAX_BALL_SPEED)
        {
            velocity.y = -MAX_BALL_SPEED;
        }
    }

    public void ShootBall()
    {
/*        lastForceX = FORCE_POWER_X;
        lastForceY = FORCE_POWER_Y;*/

        velocity = new Vector2(FORCE_POWER_X, FORCE_POWER_Y) * ballSpeed;

/*        Debug.Log(velocity.normalized * ballSpeed);
        rigidbody.AddForce(velocity.normalized * ballSpeed);
        rigidbody.gravityScale = GRAVITY_SCALE;*/

        StartCoroutine("MoveBall");
    }

    /*    public void BounceOffBallPaddle(Vector3 colliderPosition)
        {
            rigidbody.velocity = Vector2.zero;

            lastForceX = transform.position.x - colliderPosition.x;
            lastForceY = transform.position.y - colliderPosition.y;

            rigidbody.AddForce(new Vector2(lastForceX, lastForceY).normalized * ballSpeed);
        }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != BRICK_TAG)
        {
            SoundManager.instance.PlayOneShotEffectSound(1);
        }
        else
        {
            ReflectBallByBrick(collision.transform.position);
        }

        if (collision.gameObject.tag == PADDLE_TAG)
        {
            ReflectBallByPaddle(collision.transform.position);
        }

        if (collision.gameObject.tag == WALL_TAG)
        {
            ReflectBallByWall(collision.transform.position, collision.gameObject.GetComponent<WallCollider>().GetDirection());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == WALL_TAG)
        {
            ReflectBallByWall(collision.transform.position, collision.gameObject.GetComponent<WallCollider>().GetDirection());
        }
    }

    /*    private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag != BRICK_TAG)
            {
                SoundManager.instance.PlayOneShotEffectSound(1);
            }
            else
            {
                ReflectBallByBrick(collision.transform.position);
            }

            if (collision.gameObject.tag == PADDLE_TAG)
            {
                ReflectBallByPaddle(collision.transform.position);
            }

            if (collision.gameObject.tag == WALL_TAG)
            {
                ReflectBallByWall(collision.transform.position, collision.gameObject.GetComponent<WallCollider>().GetDirection());
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.tag != BRICK_TAG)
            {
                SoundManager.instance.PlayOneShotEffectSound(1);
            }
            else
            {
                ReflectBallByBrick(collision.transform.position);
            }

            if (collision.gameObject.tag == PADDLE_TAG)
            {
                ReflectBallByPaddle(collision.transform.position);
            }

            if (collision.gameObject.tag == WALL_TAG)
            {
                ReflectBallByWall(collision.transform.position, collision.gameObject.GetComponent<WallCollider>().GetDirection());
            }
        }*/

    /*public void ChangeDirection(Direction direction)
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
            ballSpeed *= 0.7f;
            transform.position = Vector2.zero;
            velocity = new Vector2(FORCE_POWER_X, FORCE_POWER_Y) * ballSpeed;

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
            ballSpeed *= 0.7f;
            transform.position = Vector2.zero;
            velocity = new Vector2(FORCE_POWER_X, FORCE_POWER_Y) * ballSpeed;
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
            ballSpeed *= 0.7f;
            transform.position = Vector2.zero;
            velocity = new Vector2(FORCE_POWER_X, FORCE_POWER_Y) * ballSpeed;
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
            ballSpeed *= 0.7f;
            transform.position = Vector2.zero;
            velocity = new Vector2(FORCE_POWER_X, FORCE_POWER_Y) * ballSpeed;
            rightStateTimer = LINE_CHECK_TIMER_RESET;
        }
    }

    /*    private void AddForceBall(float powerX, float powerY)
        {
            lastForceX = powerX;
            lastForceY = powerY;
            rigidbody.velocity = Vector2.zero;
            rigidbody.AddForce(new Vector2(lastForceX, lastForceY).normalized * ballSpeed);
            transform.position = RESET_BALL_POSITION;
        }*/

    public void ReflectBallByPaddle(Vector3 objectPosition)
    {
        Vector3 position = transform.position - objectPosition;
        Vector2 lastVelocity = velocity;

        ballSpeed += BALL_SPEED_WEIGHT;

        // Debug.Log("부딪힌 좌표 차이 : " + position.x + ", " + position.y);
        // rigidbody.AddForce((transform.position - objectPosition).normalized * ballSpeed / 30);

        // rigidbody.velocity = Vector2.zero;

        if (position.y < 0)
        {
            velocity = new Vector2(velocity.x, -velocity.y);

            return;
        }

        if (position.x <= -0.8f)
        {
            velocity = new Vector2(-Mathf.Abs(ballSpeed * 0.55f), ballSpeed * 0.44f);
        }
        else if (position.x > -0.8f && position.x <= -0.6f)
        {
            velocity = new Vector2(-Mathf.Abs(ballSpeed * 0.44f), ballSpeed * 0.55f);
        }
        else if (position.x > -0.6f && position.x <= -0.4f)
        {
            velocity = new Vector2(-Mathf.Abs(ballSpeed * 0.33f), ballSpeed * 0.66f);
        }
        else if (position.x > -0.4f && position.x <= -0.2f)
        {
            velocity = new Vector2(-Mathf.Abs(ballSpeed * 0.22f), ballSpeed * 0.77f);
        }
        else if (position.x > -0.2f && position.x <= 0)
        {
            ballSpeed -= BALL_SPEED_WEIGHT;
            velocity = new Vector2(-Mathf.Abs(ballSpeed * 0.11f), ballSpeed * 0.88f);
        }
        else if (position.x > 0 && position.x <= 0.2f)
        {
            ballSpeed -= BALL_SPEED_WEIGHT;
            velocity = new Vector2(Mathf.Abs(ballSpeed * 0.11f), ballSpeed * 0.88f);
        }
        else if (position.x > 0.2f && position.x <= 0.4f)
        {
            velocity = new Vector2(Mathf.Abs(ballSpeed * 0.22f), ballSpeed * 0.77f);
        }
        else if (position.x > 0.4f && position.x <= 0.6f)
        {
            velocity = new Vector2(Mathf.Abs(ballSpeed * 0.33f), ballSpeed * 0.66f);
        }
        else if (position.x > 0.6f && position.x <= 0.8f)
        {
            velocity = new Vector2(Mathf.Abs(ballSpeed * 0.44f), ballSpeed * 0.55f);
        }
        else if (position.x > 0.8)
        {
            velocity = new Vector2(Mathf.Abs(ballSpeed * 0.55f), ballSpeed * 0.44f);
        }
    }

    public void ReflectBallByWall(Vector3 objectPosition, Direction wallDirection)
    {
        if (wallDirection == Direction.TOP || wallDirection == Direction.BOTTOM)
        {
            velocity = new Vector2(velocity.x, -velocity.y);
        }
        else
        {
            velocity = new Vector2(-velocity.x, velocity.y);
        }
    }

    public void ReflectBallByBrick(Vector3 objectPosition)
    {
        Debug.Log("실행됨");
        velocity = new Vector2(velocity.x, -velocity.y);
    }
}
