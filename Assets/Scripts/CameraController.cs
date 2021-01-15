using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private static float DIVIDE_RESOLUTION_A = 0.25f; // 20 : 9
    private static float DIVIDE_RESOLUTION_B = 0.125f; // 19 : 9
    private static float DIVIDE_RESOLUTION_C = 0.01f; // 16 : 9

    private static float POSITION_Z = -10;

    private static Vector3 cameraPosition = new Vector3(0, 0, -10);

    public static CameraController instance;
    [SerializeField] private float cameraTimer;
    [SerializeField] private Vector3 shakePower;
    [SerializeField] private Vector3 shackingBackUpPosition;
    [SerializeField] private bool isShacking;
    [SerializeField] private float resolutionDivideValue;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        shackingBackUpPosition = cameraPosition;
        isShacking = false;
        cameraTimer = 1;

        InitializeResolutionValue();
    }

    // Update is called once per frame
    void Update()
    {
        ShakeCamera();
    }

    private void InitializeResolutionValue()
    {
        float value = (float) Screen.height / (float) Screen.width;

        if (value < 1.8f)
        {
            resolutionDivideValue = DIVIDE_RESOLUTION_C;
        }
        else if (value > 2.1f)
        {
            resolutionDivideValue = DIVIDE_RESOLUTION_A;
        }
        else
        {
            resolutionDivideValue = DIVIDE_RESOLUTION_B;
        }
    }

    public void PlayShakingCamera(Vector3 shakePower)
    {
        this.shakePower = shakePower;
        cameraTimer = 0;
    }

    public void MovePositionWithPaddle(Vector3 paddlePosition)
    {
        if (isShacking)
        {
            return;
        }

        transform.position = shackingBackUpPosition;
        transform.position = new Vector3(paddlePosition.x * resolutionDivideValue, transform.position.y, transform.position.z);
    }

    public void ShakeCamera()
    {
        if (cameraTimer > 0.1f)
        {
            if (isShacking)
            {
                isShacking = false;
            }

            return;
        }

        if (!isShacking)
        {
            isShacking = true;
            shackingBackUpPosition = transform.position;
        }

        Vector3 paddlePosition = Paddle.instance.transform.position;

        transform.position = new Vector3(
            Random.Range((paddlePosition.x * resolutionDivideValue) - shakePower.x, (paddlePosition.x * resolutionDivideValue) + shakePower.x)
            , Random.Range(cameraPosition.y - shakePower.y, cameraPosition.y + shakePower.y),
            POSITION_Z);

        cameraTimer += Time.deltaTime;
    }
}
