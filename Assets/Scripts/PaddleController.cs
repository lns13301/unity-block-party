using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    private static int BASE_PADDLE = 0;
    private static int BASE_BALL = 0;
    private static int FIRST_TOUCH = 0;
    private static int TOUCH_COUNT = 1;
    private static float PADDLE_BORDER = 2.17f;
    private static float BALL_STOP_SPEED = 0;
    private static float DEFAULT_PADDLE_SCALE = 2;
    private static float UI_POSITION_Y = 180;

    public static PaddleController instance;
    [SerializeField] private float paddleBorder = PADDLE_BORDER;
    [SerializeField] private float ballSpeed = BALL_STOP_SPEED;
    [SerializeField] private float paddleX;

    [SerializeField] private List<GameObject> balls;
    [SerializeField] private List<GameObject> paddles;

    public bool isStart = false;

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
    }

    IEnumerator Loop()
    {
        while (true)
        {
            MovePaddle(paddles[BASE_PADDLE].transform, balls[BASE_BALL].transform);
            ShootBall(balls[BASE_BALL]);
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void MovePaddle(Transform paddleTransform, Transform ballTransform)
    {
        if (Input.GetMouseButton(FIRST_TOUCH) || (Input.touchCount == TOUCH_COUNT && Input.GetTouch(FIRST_TOUCH).phase == TouchPhase.Moved))
        {

            paddleX = Mathf.Clamp(Camera.main.ScreenToWorldPoint(
                Input.GetMouseButton(FIRST_TOUCH) ? Input.mousePosition : (Vector3) Input.GetTouch(FIRST_TOUCH).position
                ).x, -paddleBorder, paddleBorder);

            paddleTransform.position = new Vector2(paddleX, paddleTransform.position.y);
            CameraController.instance.MovePositionWithPaddle(paddleTransform.position);

            if (!isStart)
            {
                ballTransform.position = new Vector2(paddleX, ballTransform.position.y);
            }
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
}
