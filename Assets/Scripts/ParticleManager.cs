using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    // 해당 프로젝트용 파티클 상수
    private static int BRICK_BREAK_RED = 0;
    private static int BRICK_BREAK_ORANGE = 1;
    private static int BRICK_BREAK_YELLOW = 2;
    private static int BRICK_BREAK_GREEN = 3;
    private static int BRICK_BREAK_SKYBLUE = 4;
    private static int BRICK_BREAK_BLUE = 5;
    private static int BRICK_BREAK_PURPLE = 6;

    public GameObject[] effects;
    public static ParticleManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateEffect(Vector2 position, GameObject gameObject, int index = 0)
    {
        GameObject effect = Instantiate(effects[index]);
        effect.transform.SetParent(gameObject.transform.parent);
        effect.transform.position = position;
        effect.transform.localScale = new Vector3(1, 1, 1);
    }

    // 해당 프로젝트용 메소드
    public void CreateParticleByType(GameObject gameObject, ParticleType particleType)
    {
        switch (particleType)
        {
            case ParticleType.BRICK_BREAK_RED:
                CreateEffect(gameObject.transform.position, gameObject, BRICK_BREAK_RED);
                break;
            case ParticleType.BRICK_BREAK_ORANGE:
                CreateEffect(gameObject.transform.position, gameObject, BRICK_BREAK_ORANGE);
                break;
            case ParticleType.BRICK_BREAK_YELLOW:
                CreateEffect(gameObject.transform.position, gameObject, BRICK_BREAK_YELLOW);
                break;
            case ParticleType.BRICK_BREAK_GREEN:
                CreateEffect(gameObject.transform.position, gameObject, BRICK_BREAK_GREEN);
                break;
            case ParticleType.BRICK_BREAK_SKYBLUE:
                CreateEffect(gameObject.transform.position, gameObject, BRICK_BREAK_SKYBLUE);
                break;
            case ParticleType.BRICK_BREAK_BLUE:
                CreateEffect(gameObject.transform.position, gameObject, BRICK_BREAK_BLUE);
                break;
            case ParticleType.BRICK_BREAK_PURPLE:
                CreateEffect(gameObject.transform.position, gameObject, BRICK_BREAK_PURPLE);
                break;
        }
    }
}

// 해당 프로젝트용 파티클 타입
public enum ParticleType
{
    BRICK_BREAK_RED,
    BRICK_BREAK_ORANGE,
    BRICK_BREAK_YELLOW,
    BRICK_BREAK_GREEN,
    BRICK_BREAK_SKYBLUE,
    BRICK_BREAK_BLUE,
    BRICK_BREAK_PURPLE,
}
