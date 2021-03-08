using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRangeAttack : MonoBehaviour
{
    private static int DEFAULT_CHAINING_TIME = 3;
    private static Color DAMAGE_COLOR = new Color32(168, 106, 255, 255);
    private static Color TEXT_COLOR = new Color32(205, 106, 255, 255);

    private Skill skill;
    public GameObject skillEffect;
    public List<Brick> bricks;
    private float StartTimer;

    public int leftChainingTime;

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
        if (StartTimer > 0.3f && leftChainingTime > 0)
        {
            try
            {
                // 연쇄 이펙트를 추가하려면 여기에

                for (int i = 0; i < bricks.Count; i++)
                {
                    DoSkill(i);
                }
            }
            catch (MissingReferenceException)
            {
                Debug.Log("타겟이 이미 제거되었습니다.");
            }
            catch (System.NullReferenceException)
            {
                Debug.Log("타겟이 이미 제거되었습니다.");
            }
            catch (System.ArgumentOutOfRangeException)
            {

            }

            DoChainingAttack(--leftChainingTime);
            Destroy(gameObject);
        }
    }

    private void ShowEffect()
    {
        if (leftChainingTime < DEFAULT_CHAINING_TIME)
        {
            return;
        }

        GameObject effect = Instantiate(skillEffect);
        effect.transform.position = bricks[0].transform.position;
    }

    private void DoSkill(int index)
    {
        // 30% 확률로 발동
        if (Random.Range(0, 10) < 3)
        {
            return;
        }

        bricks[index].TakeDamage(5, DAMAGE_COLOR);
        bricks[index].TakeDamageText("감전!", TEXT_COLOR);
        SoundManager.instance.PlayOneShowSoundFindByName("electricSpark");
    }

    private void DoChainingAttack(int leftChainingTime)
    {
        // 20% 확률로 연쇄 발동
        if (Random.Range(0, 10) < 2)
        {
            GameObject skillRangeAttack = Instantiate(gameObject);
            skillRangeAttack.GetComponent<SkillRangeAttack>().leftChainingTime = leftChainingTime;
            skillRangeAttack.transform.position = skillRangeAttack.transform.position + new Vector3(0, 0.5f, 0);
        }
    }
}
