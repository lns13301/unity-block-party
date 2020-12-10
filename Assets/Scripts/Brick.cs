using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private static string BALL_TAG = "Ball";

    [SerializeField] private BrickStat brickStat;

    // Start is called before the first frame update
    void Start()
    {
        brickStat.hp = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool isDestroy()
    {
        return brickStat.hp <= 0;
    }

    private void destroyGameObject()
    {
        if (isDestroy())
        {
            Destroy(gameObject);
        }
    }

    public void takeDamage(int damage)
    {
        brickStat.hp -= damage;

        destroyGameObject();
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
