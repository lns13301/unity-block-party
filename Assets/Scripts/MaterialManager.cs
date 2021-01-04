using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    private float YIELD_RETURN_IMMEDIATELY = 0.05f;
    private float COLOR_VALUE_CORRECTION = 0.5f;

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
        if (isIntenseUp && colorValue > 2)
        {
            isIntenseUp = false;
        }
        else if (!isIntenseUp && colorValue < 0)
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

/*    IEnumerator InvokeGlow(Color[] color)
    {
        brickEmission.hdr
        brickEmission.SetVector("_EmissionColor", new Vector4(brickEmission.color.r, brickEmission.color.g, brickEmission.color.b, brickEmission.color.a) * colorTimer);
    }*/
}
