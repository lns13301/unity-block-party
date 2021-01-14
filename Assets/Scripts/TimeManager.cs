using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private static float PAUSE_TIME = 0.005f;
    private static float STOP_TIME = 0.15f;

    public static TimeManager instance;

    [SerializeField] private float slowdownFactor = 0.05f;
    [SerializeField] private float slowdownLength = 2f;

    [SerializeField] private float recoveryCooldown;
    [SerializeField] private bool isStop;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        StopTimeScaleTemporary();
    }

    private void StopTimeScaleTemporary()
    {
        recoveryCooldown += Time.fixedDeltaTime;

        if (isStop && recoveryCooldown > PAUSE_TIME)
        {
            StartTimeScale();
        }
    }

    private void StartTimeScale()
    {
        isStop = false;

        Time.timeScale = 1;
    }

    public void StopTimeScale()
    {
        recoveryCooldown = 0;
        isStop = true;

        Time.timeScale = STOP_TIME;
    }

/*    public void SetTimeScale()
    {
        if (Time.timeScale <= 1)
        {
            Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void DoSlowMotion()
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }*/
}
