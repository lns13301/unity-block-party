using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    private static int FIRST_TOUCH = 0;
    private static int TOUCH_COUNT = 1;
    private static int TOUCH_TIME_MIN = 2;
    private static float TOUCH_RESET_TIME = 0.3f;
    private static float RESET = 0;
    private static float UI_BORDER = 4.15f;

    public static Paddle instance;

    private static string BALL_TAG = "Ball";
    private static string DROP_ITEM_TAG = "DropItem";

    [SerializeField] private int paddleIndex;
    [SerializeField] private float touchTimer;
    [SerializeField] private int touchTimes;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        ChangePaddleTouchUpdate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == DROP_ITEM_TAG)
        {
            collision.GetComponent<DropItem>().GetDropItem();
        }
    }

    public void ChangePaddle()
    {
        // 일단은 변경될 패들인덱스를 넣어야 함, 임시

        PaddleController.instance.ChangePaddle(paddleIndex);
    }

    public void ChangePaddleTouchUpdate()
    {
        if (touchTimes >= TOUCH_TIME_MIN)
        {
            touchTimes = 0;
            ChangePaddle();
        }

        if (Input.GetMouseButton(FIRST_TOUCH) || (Input.touchCount == TOUCH_COUNT && Input.GetTouch(FIRST_TOUCH).phase == TouchPhase.Moved))
        {
            // UI 부분 터치 시 카운트 시키지 않음
            float paddleY = Mathf.Clamp(Camera.main.ScreenToWorldPoint(
                Input.GetMouseButton(FIRST_TOUCH) ? Input.mousePosition : (Vector3)Input.GetTouch(FIRST_TOUCH).position
                ).y, -UI_BORDER, UI_BORDER);

            if (Input.GetMouseButtonDown(0) && paddleY > -UI_BORDER)
            {
                touchTimes++; // 한번 무작위 터치시에 카운트를 증가시킴
            }
        }

        if (touchTimes > RESET)
        {
            if (touchTimer > TOUCH_RESET_TIME)
            {
                touchTimer = RESET;
                touchTimes--;
            }

            touchTimer += Time.deltaTime;
        }
    }
}
