using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRangeAttack : MonoBehaviour
{
    private static Color DAMAGE_COLOR = new Color32(168, 106, 255, 255);
    private static Color TEXT_COLOR = new Color32(205, 106, 255, 255);

    private Skill skill;
    public GameObject skillEffect;
    public List<Brick> bricks;
    private float StartTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartTimer += Time.deltaTime;
        DoAttack();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Brick")
        {
            bricks.Add(collision.GetComponent<Brick>());
        }
    }

    public void DoAttack()
    {
        if (StartTimer > 0.3f)
        {
            try
            {
                for (int i = 0; i < bricks.Count; i++)
                {
                    // 30 % 확률로 발동
                    if (Random.Range(0,10) < 3)
                    {
                        break;
                    }

                    GameObject effect = Instantiate(skillEffect);
                    effect.transform.position = bricks[i].transform.position;
                    bricks[i].TakeDamage(5, DAMAGE_COLOR);
                    bricks[i].TakeDamageText("감전!", TEXT_COLOR);
                    SoundManager.instance.PlayOneShowSoundFindByName("electricSpark");
                }
            }
            catch (MissingReferenceException)
            {
                Debug.Log("타겟이 이미 제거되었습니다.");
            }

            Destroy(gameObject);
        }
    }
}
