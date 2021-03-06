﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricBall : MonoBehaviour
{
    private static int DEFAULT_CHAINING_TIME = 3;
    private static string BRICK_TAG = "Brick";

    public bool isSkillOn;
    public Skill skill;

    public GameObject skillEffect;
    public GameObject attackRangeRadius;

    private float duration;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChangeDuration();
    }

    public void DoAttack()
    {
        GameObject effect = Instantiate(skillEffect);
        effect.transform.position = transform.position;

        GameObject range = Instantiate(attackRangeRadius);
        range.GetComponent<SkillRangeAttack>().leftChainingTime = DEFAULT_CHAINING_TIME;
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

    public void Activate()
    {
        duration = 5f;
        isSkillOn = true;
    }

    private void ChangeDuration()
    {
        if (duration > 0)
        {
            duration -= Time.deltaTime;
        }
        else
        {
            isSkillOn = false;
        }
    }
}
