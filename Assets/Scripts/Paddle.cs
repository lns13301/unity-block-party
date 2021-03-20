using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paddle : MonoBehaviour
{
    private static int FIRST_TOUCH = 0;
    private static int TOUCH_COUNT = 1;
    private static int TOUCH_TIME_MIN = 2;
    private static float TOUCH_RESET_TIME = 0.3f;
    private static float RESET = 0;
    private static float UI_BORDER = 4.15f;
    private static Color DAMAGED_COLOR = new Color32(202, 0, 255, 255);

    public static Paddle instance;

    private static string BALL_TAG = "Ball";
    private static string DROP_ITEM_TAG = "DropItem";

    [SerializeField] private int paddleIndex;
    [SerializeField] private float touchTimer;
    [SerializeField] private int touchTimes;

    // HealthBar text
    public int damagedTimer;
    public GameObject healthBarBackground;
    public Image healthBar;
    public float delayHP;
    public float healthPoint;
    public float healthPointMax;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        InitializeHealthBar();
    }

    // Update is called once per frame
    void Update()
    {
        ChangePaddleTouchUpdate();
        RefreshHealthBar();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == DROP_ITEM_TAG)
        {
            collision.GetComponent<DropItem>().GetDropItem();
        }
    }

    public void InitializeHealthBar()
    {
        healthBarBackground = transform.GetChild(0).GetChild(0).gameObject;
        healthBar = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
        healthBar.fillAmount = 1.0f;
    }

    public void RefreshHealthBar()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, healthPoint / healthPointMax, Time.deltaTime * 3f);
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

    public void TakeDamage(float damage)
    {
        healthPoint -= damage;
        TakeDamageText(damage.ToString(), DAMAGED_COLOR);
    }

    public void TakeDamageText(string text, Color textColor)
    {
        TMPManager.instance.CreateText(text, gameObject, new Vector2(transform.position.x, transform.position.y - 0.2f), textColor, 5, 1);
    }
}
