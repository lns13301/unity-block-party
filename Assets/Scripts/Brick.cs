using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private static string BALL_TAG = "Ball";
    private static Vector2 SHAKE_POWER = new Vector2(0.03f, 0.05f);

    [SerializeField] private BrickStat brickStat;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Material material;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        material = spriteRenderer.material;

        brickStat.hp = 1;
    }

    // Update is called once per frame
    void Update()
    {
        material.SetColor("_EmissionColor", Color.red * 10);
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
        ParticleManager.instance.CreateEffect(transform.position, gameObject, 0);

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
