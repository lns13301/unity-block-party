using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollider : MonoBehaviour
{
    private static string BALL_TAG = "Ball";

    [SerializeField] private Direction direction;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum Direction
{
    LEFT,
    RIGHT,
    TOP,
    BOTTOM
}
