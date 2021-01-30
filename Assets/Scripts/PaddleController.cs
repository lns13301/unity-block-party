using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    private static int BASE_PADDLE = 0;
    private static int BASE_BALL = 0;
    private static int FIRST_TOUCH = 0;
    private static int SECOND_TOUCH = 1;
    private static int TOUCH_COUNT = 1;
    private static float PADDLE_BORDER = 2.17f;
    private static float UI_BORDER = 4.15f;
    private static float BALL_STOP_SPEED = 0;
    private static float DEFAULT_PADDLE_SCALE = 2;
    private static float UI_POSITION_Y = 180;

    public static PaddleController instance;
    [SerializeField] private float paddleBorder = PADDLE_BORDER;
    [SerializeField] private float ballSpeed = BALL_STOP_SPEED;
    [SerializeField] private float paddleX;

    [SerializeField] private List<GameObject> balls;
    [SerializeField] private List<GameObject> paddles;

    [SerializeField] private int paddleIndex;
    [SerializeField] private int ballIndex;
       
    public bool isStart = false;

    public bool isTouching = false;
    [SerializeField] private float firstTouchPositionY;
    [SerializeField] private float lastTouchOverTwoCooldown; // 이전 터치가 2개 이상일 때 1개로 돌아가는 과정에서 패들이 중간 좌표로 이동되는 현상 막기위함

#if (UNITY_ANDROID)
    void Awake()
    {
        // Screen.SetResolution(1080, 1920, false);
    }
#else
    void Awake()
    {
        Screen.SetResolution(540, 960, false);
    }
#endif
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        InitializeGameObject(GameObject.Find("Paddles"), paddles);
        InitializeGameObject(GameObject.Find("Balls"), balls);
        StartCoroutine("Loop");

        paddleIndex = BASE_PADDLE; // base paddle 외 paddle 의 gameObject false 추가해주기

        ballIndex = BASE_BALL;
    }

    IEnumerator Loop()
    {
        while (true)
        {
            // 모든 패들을 터치 위치로 이동
            for (int i = 0; i < paddles.Count; i++)
            {
                MovePaddle(paddles[paddleIndex].transform, balls[ballIndex].transform);
            }

            ShootBall(balls[ballIndex]);
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void MovePaddle(Transform paddleTransform, Transform ballTransform)
    {
        // 이전 터치가 2개 이상일 때 1개로 돌아가는 과정에서 패들이 중간 좌표로 이동되는 현상 막기위함
        if (lastTouchOverTwoCooldown > 0)
        {
            lastTouchOverTwoCooldown -= Time.deltaTime;
        }

        if (Input.GetMouseButton(FIRST_TOUCH) || (Input.touchCount == TOUCH_COUNT && Input.GetTouch(FIRST_TOUCH).phase == TouchPhase.Moved))
        {
            // 터치 시작 좌표 입력
            if (!isTouching)
            {
                isTouching = true;

                firstTouchPositionY = Mathf.Clamp(Camera.main.ScreenToWorldPoint(
                    Input.GetMouseButton(FIRST_TOUCH) ? Input.mousePosition : (Vector3)Input.GetTouch(FIRST_TOUCH).position
                    ).y, -UI_BORDER, UI_BORDER);
            }

            float paddleY = Mathf.Clamp(Camera.main.ScreenToWorldPoint(
                Input.GetMouseButton(FIRST_TOUCH) ? Input.mousePosition : (Vector3)Input.GetTouch(FIRST_TOUCH).position
                ).y, -UI_BORDER, UI_BORDER);

            // 2개 이상 터치 시 패들 고정
            if (Input.GetMouseButton(SECOND_TOUCH))
            {
/*                float paddleY2 = Mathf.Clamp(Camera.main.ScreenToWorldPoint(
                    Input.GetMouseButton(FIRST_TOUCH) ? Input.mousePosition : (Vector3)Input.GetTouch(FIRST_TOUCH).position
                    ).y, -UI_BORDER, UI_BORDER);

                if (paddleY2 <= -UI_BORDER)
                {
                    return;
                }*/

                lastTouchOverTwoCooldown = 0.2f;

                return;
            }

            if (lastTouchOverTwoCooldown > 0)
            {
                return;
            }
            
            // UI 부분 터치 시 패들 고정
            if (firstTouchPositionY <= -UI_BORDER)
            {
                return;
            }

            paddleX = Mathf.Clamp(Camera.main.ScreenToWorldPoint(
                Input.GetMouseButton(FIRST_TOUCH) ? Input.mousePosition : (Vector3) Input.GetTouch(FIRST_TOUCH).position
                ).x, -paddleBorder, paddleBorder);

            paddleTransform.position = new Vector2(paddleX, paddleTransform.position.y);
            CameraController.instance.MovePositionWithPaddle(paddleTransform.position);

            // 게임 시작 전 공 위치 고정
            if (!isStart)
            {
                ballTransform.position = new Vector2(paddleX, ballTransform.position.y);
            }
        }
        else
        {
            isTouching = false;
        }
    }

    private void ShootBall(GameObject ballObject)
    {
        if (!isStart && (Input.GetMouseButtonUp(FIRST_TOUCH) || (Input.touchCount == TOUCH_COUNT && Input.GetTouch(FIRST_TOUCH).phase == TouchPhase.Ended)))
        {
            isStart = true;

            ballObject.GetComponent<Ball>().ShootBall();
        }
    }

    private void InitializeGameObject(GameObject parentObject, List<GameObject> childObjects)
    {
        for (int i = 0; i < parentObject.transform.childCount; i++)
        {
            childObjects.Add(parentObject.transform.GetChild(i).gameObject);
        }
    }

    public void ChangePaddle(int paddleIndex)
    {
        int lastPaddleIndex = this.paddleIndex;

        this.paddleIndex = ++paddleIndex;

        // 패들 인덱스 초과 시 0번 인덱스로
        if (this.paddleIndex >= 2)
        {
            this.paddleIndex = 0;
        }

        paddles[lastPaddleIndex].gameObject.SetActive(false);
        paddles[this.paddleIndex].gameObject.SetActive(true);
    }
}
