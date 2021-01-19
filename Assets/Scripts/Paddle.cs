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

        if (Input.GetMouseButtonDown(0))
        {
            touchTimes++; // 한번 무작위 터치시에 카운트를 증가시킴
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
