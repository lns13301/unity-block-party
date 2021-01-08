using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    private static string BALL_TAG = "Ball";
    private static string DROP_ITEM_TAG = "DropItem";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == DROP_ITEM_TAG)
        {
            collision.GetComponent<DropItem>().GetDropItem();
        }
    }
}
