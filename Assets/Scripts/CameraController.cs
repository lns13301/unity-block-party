using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private static float POSITION_Z = -10;

    private static Vector3 cameraPosition;

    public static CameraController instance;
    [SerializeField] private float cameraTimer;
    [SerializeField] private Vector2 shakePower;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        cameraPosition = transform.position;
        cameraTimer = 1;
    }

    // Update is called once per frame
    void Update()
    {
        ShakeCamera();
    }

    public void PlayShakingCamera(Vector2 shakePower)
    {
        this.shakePower = shakePower;
        cameraTimer = 0;
    }

    public void ShakeCamera()
    {
        if (cameraTimer > 0.1f)
        {
            transform.position = cameraPosition;
            return;
        }

        transform.position = new Vector3(
            Random.Range(cameraPosition.x - shakePower.x, cameraPosition.x + shakePower.x)
            , Random.Range(cameraPosition.y - shakePower.y, cameraPosition.y + shakePower.y),
            POSITION_Z);

        cameraTimer += Time.deltaTime;
    }
}
