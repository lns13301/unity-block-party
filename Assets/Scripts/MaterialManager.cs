using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    private float COLOR_VALUE_MIN = 0;
    private float COLOR_VALUE_MAX = 2;
    private float COLOR_VALUE_CORRECTION = 0.8f;

    public static MaterialManager instance;

    public float colorValue = 0;
    public bool isIntenseUp = true;

    public Material brickEmission;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        SetIntenseTimer();
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
}
