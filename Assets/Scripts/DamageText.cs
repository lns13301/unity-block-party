using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    private static int MISS_VALUE = 0;
    private static string MISS = "Miss";
    private static string CRITICAL = "Critical!";
    private static float LOCAL_SCALE_X = 0.5f;
    private static float SCALE_UP_TIME = 0.1f;
    private static float SCALE_DOWN_TIME = 0.3f;
    private static float SCALE_REPEATING_TIMER = 0.02f;
    private static float TEXT_MOVING_X_MIN = 100;
    private static float TEXT_MOVING_X_MAX = 700;
    private static float TEXT_MOVING_UNIT = 0.001f;
    private static float ALPHA_TIMER_START = 0.5f;

    [SerializeField] private float speedX;
    [SerializeField] private float speedY;
    [SerializeField] private float alphaSpeed;
    [SerializeField] private float scaleSpeed;
    [SerializeField] private float alphaTimer;

    [SerializeField] private float destroyTimer;
    [SerializeField] private TextMeshPro text;
    [SerializeField] private Color alpha;
    [SerializeField] private Vector3 localScale;
    [SerializeField] private int damage;
    [SerializeField] private string damageText;

    [SerializeField] private float textMaxSize;
    [SerializeField] private float textMinSize;

    [SerializeField] private string sortingLayer;
    [SerializeField] private int sortingOrder;

    [SerializeField] private bool isMovingX;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshPro>();

        SetRandomDirection();
        InitializeText();

        alpha = text.color;
        localScale = GetComponent<RectTransform>().localScale;
        localScale.x = LOCAL_SCALE_X;

        Invoke("DestroyObject", destroyTimer);

        GetComponent<MeshRenderer>().sortingLayerName = sortingLayer;
        GetComponent<MeshRenderer>().sortingOrder = sortingOrder;

        InvokeRepeating("SetScaleUp", SCALE_UP_TIME, SCALE_REPEATING_TIMER);
        Invoke("StopScaleUp", SCALE_DOWN_TIME);
        InvokeRepeating("SetScaleDown", SCALE_DOWN_TIME, SCALE_REPEATING_TIMER);
    }

    // Update is called once per frame
    void Update()
    {
        PlayTextMotion();
    }

    private void InitializeText()
    {
        if (damage == MISS_VALUE)
        {
            if (damageText == null)
            {
                text.text = MISS;
            }
        }
        else
        {
            text.text = damage.ToString();
        }
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
    
    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    public void SetText(string damageText)
    {
        this.damageText = damageText;
    }

    private void PlayTextMotion()
    {
        alphaTimer += Time.deltaTime;
        transform.Translate(new Vector3(speedX * Time.deltaTime, speedY * Time.deltaTime, 0));

        if (alphaTimer < ALPHA_TIMER_START)
        {
            return;
        }

        alpha.a = Mathf.Lerp(alpha.a, 0, (Time.deltaTime - ALPHA_TIMER_START) * alphaSpeed);
        text.color = alpha;
    }

    private void SetRandomDirection()
    {
        if (!isMovingX)
        {
            return;
        }
      
        speedX = (float) (Random.Range(TEXT_MOVING_X_MIN, TEXT_MOVING_X_MAX) * TEXT_MOVING_UNIT);
    }

    public void SetScaleUp()
    {
        localScale.x = Mathf.Lerp(localScale.x, textMaxSize, Time.deltaTime * scaleSpeed);
        localScale.y = Mathf.Lerp(localScale.y, textMaxSize, Time.deltaTime * scaleSpeed);

        GetComponent<RectTransform>().localScale = localScale;
    }

    public void StopScaleUp()
    {
        CancelInvoke("SetScaleUp");
    }

    public void SetScaleDown()
    {
        localScale.x = Mathf.Lerp(localScale.x, textMinSize, Time.deltaTime * scaleSpeed / 3);
        localScale.y = Mathf.Lerp(localScale.y, textMinSize, Time.deltaTime * scaleSpeed / 3);

        GetComponent<RectTransform>().localScale = localScale;
    }
}
