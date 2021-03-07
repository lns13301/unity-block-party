using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricBall : MonoBehaviour
{
    private static string BRICK_TAG = "Brick";

    public bool isSkillOn;
    public Skill skill;

    public GameObject skillEffect;
    public GameObject attackRangeRadius;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoAttack()
    {
        GameObject effect = Instantiate(skillEffect);
        effect.transform.position = transform.position;

        GameObject range = Instantiate(attackRangeRadius);
        range.transform.position = transform.position + new Vector3(0, 0.5f, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isSkillOn)
        {
            return;
        }

        if (collision.gameObject.tag == BRICK_TAG)
        {
            DoAttack();
        }
    }

    public void CreateBonusAttack()
    {

    }
}
