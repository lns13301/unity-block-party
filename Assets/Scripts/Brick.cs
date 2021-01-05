using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private static string BALL_TAG = "Ball";
    private static Vector2 SHAKE_POWER = new Vector2(0.03f, 0.05f);

    private static Color BRICK_YELLOW_COLOR = new Color32(78, 83, 40, 50);
    private static float INITIALIZE_COLOR_INTENSITY = 10f;

    [SerializeField] private BrickStat brickStat;
    [SerializeField] private Material material;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;

        brickStat.hp = 1;

       StartCoroutine("ChangeGlow");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator ChangeGlow()
    {
        // 초기 Intensity 설정
        material.SetVector("_EmissionColor", BRICK_YELLOW_COLOR * INITIALIZE_COLOR_INTENSITY);

        while (true)
        {
            material.SetVector("_EmissionColor", BRICK_YELLOW_COLOR * MaterialManager.instance.GetColorValue());
            yield return 0.1f;
        }
    }

    public void SetGlow()
    {
        material.SetVector("_EmissionColor", BRICK_YELLOW_COLOR * MaterialManager.instance.GetColorValue());
    }

    private bool isDestroy()
    {
        return brickStat.hp <= 0;
    }

    private void DestroyGameObject()
    {
        if (isDestroy())
        {
            CameraController.instance.PlayShakingCamera(SHAKE_POWER);
            Destroy(gameObject);
        }
    }

    public void takeDamage(int damage)
    {
        brickStat.hp -= damage;
        SoundManager.instance.PlayOneShotEffectSound(0);
        ParticleManager.instance.CreateParticleByType(gameObject, ParticleType.BRICK_BREAK_SKYBLUE);

        DestroyGameObject();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == BALL_TAG)
        {
            takeDamage(1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == BALL_TAG)
        {
            takeDamage(1);
        }
    }

    
}
