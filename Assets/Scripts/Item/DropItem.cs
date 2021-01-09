using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    private static Color WHITE_COLOR = new Color32(255, 255, 255, 30);
    private static Color RED_COLOR = new Color32(191, 94, 93, 30);
    private static Color ORANGE_COLOR = new Color32(191, 120, 93, 30);
    private static Color YELLOW_COLOR = new Color32(191, 187, 93, 30);
    private static Color GREEN_COLOR = new Color32(103, 191, 93, 30);
    private static Color SKYBLUE_COLOR = new Color32(93, 194, 192, 30);
    private static Color BLUE_COLOR = new Color32(93, 104, 191, 30);
    private static Color PURPLE_COLOR = new Color32(135, 93, 191, 30);
    private static Color BLACK_COLOR = new Color32(0, 0, 0, 30);

    private static float COLOR_VALUE_MIN = 0;
    private static float COLOR_VALUE_MAX = 1f;
    private static float COLOR_VALUE_CORRECTION = 0.35f;
    private static float INITIALIZE_COLOR_INTENSITY = 0f;
    private static float GRAVITY_SCALE = 0.02f;

    [SerializeField] private Item item;
    [SerializeField] private Material material;
    [SerializeField] private Color currentColor;

    public ColorType colorType;
    public float colorValue = 0;
    public bool isIntenseUp = true;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
        currentColor = GetColorType(colorType);
        StartCoroutine("ChangeGlow");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SetIntenseTimer();
        AddGravity();
    }

    IEnumerator ChangeGlow()
    {
        while (true)
        {
            // 초기 설정한 컬러값 + 변화하는 컬러값
            material.SetVector("_EmissionColor", (currentColor * INITIALIZE_COLOR_INTENSITY) + (currentColor * GetColorValue()));
            yield return null;
        }
    }

    // 아이템 획득 처리
    public void GetDropItem()
    {
        SoundManager.instance.PlayOneShotEffectSound(2);

        Destroy(gameObject);
    }

    private void AddGravity()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - GRAVITY_SCALE);
    }

    private void SetIntenseTimer()
    {
        if (isIntenseUp && colorValue > COLOR_VALUE_MAX)
        {
            isIntenseUp = false;
        }
        else if (!isIntenseUp && colorValue < COLOR_VALUE_MIN)
        {
            isIntenseUp = true;
        }

        if (isIntenseUp)
        {
            colorValue += Time.deltaTime;
        }
        else
        {
            colorValue -= Time.deltaTime;
        }
    }

    public float GetColorValue()
    {
        return colorValue * COLOR_VALUE_CORRECTION;
    }

    public Color GetColorType(ColorType colorType)
    {
        switch (colorType)
        {
            case ColorType.RED:
                return RED_COLOR;
            case ColorType.ORANGE:
                return ORANGE_COLOR;
            case ColorType.YELLOW:
                return YELLOW_COLOR;
            case ColorType.GREEN:
                return GREEN_COLOR;
            case ColorType.SKYBLUE:
                return SKYBLUE_COLOR;
            case ColorType.BLUE:
                return BLUE_COLOR;
            case ColorType.PURPLE:
                return PURPLE_COLOR;
            case ColorType.BLACK:
                return BLACK_COLOR;
            default:
                return WHITE_COLOR;
        }
    }
}

public enum ColorType
{
    WHITE,
    BLACK,
    RED,
    ORANGE,
    YELLOW,
    GREEN,
    SKYBLUE,
    BLUE,
    PURPLE,
}
