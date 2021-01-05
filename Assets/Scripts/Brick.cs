using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private static float START_GAME_SPEED = 1f;
    private static float STOP_GAME_SPEED = 0f;

    private static string BALL_TAG = "Ball";
    private static Vector2 SHAKE_POWER = new Vector2(0.0216f, 0.0384f);
    private static int DEAD_HEALTH = 0;

    private static Color BRICK_YELLOW_COLOR = new Color32(78, 83, 40, 50);
    private static float INITIALIZE_COLOR_INTENSITY = 1.6f;

    [SerializeField] private BrickStat brickStat;
    [SerializeField] private Material material;
    [SerializeField] private Color currentColor;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
        currentColor = BRICK_YELLOW_COLOR;

        brickStat.hp = 1;

       StartCoroutine("ChangeGlow");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator ChangeGlow()
    {
        while (true)
        {
            // 초기 설정한 컬러값 + 변화하는 컬러값
            material.SetVector("_EmissionColor", (currentColor * INITIALIZE_COLOR_INTENSITY) + (currentColor * MaterialManager.instance.GetColorValue()));
            yield return null;
        }
    }

    IEnumerator StopGameTemporary()
    {
        Time.timeScale = STOP_GAME_SPEED;

        yield return new WaitForSecondsRealtime(1f);

        Time.timeScale = START_GAME_SPEED;
    }

    private bool IsDestroy()
    {
        return brickStat.hp <= 0;
    }

    private void DestroyGameObject()
    {
        if (IsDestroy())
        {
            CameraController.instance.PlayShakingCamera(SHAKE_POWER);
            // StartCoroutine("StopGameTemporary");
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        brickStat.hp -= damage;
        SoundManager.instance.PlayOneShotEffectSound(0);
        ParticleManager.instance.CreateParticleByType(gameObject, ParticleType.BRICK_BREAK_YELLOW);

        if (brickStat.hp <= DEAD_HEALTH)
        {
            DestroyGameObject();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == BALL_TAG)
        {
            TakeDamage(1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == BALL_TAG)
        {
            TakeDamage(1);
        }
    }    
}
