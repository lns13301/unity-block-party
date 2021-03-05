using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combo : MonoBehaviour
{
    private static string COMBOTEXT_EN = " Combo";
    private static float COMBO_RESET__TIMER = 3f;

    public static Combo instance;

    public Text comboText;

    private Animator animator;
    public bool isComboOn;

    [SerializeField] private int combo;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCombo(int value = 1)
    {
        CancelInvoke("ResetCombo");
        animator.SetBool("isFadeOut", false);

        if (value == -1)
        {
            combo = 0;
            comboText.text = "";
        }
        else
        {
            combo += value;
            ChangeCombo(combo);
            FadeOut();
        }
    }

    public void ChangeCombo(int combo)
    {
        comboText.text = combo + COMBOTEXT_EN;
    }

    public void FadeOut()
    {
        animator.SetTrigger("addCombo");
        animator.SetBool("isFadeOut", true);
        Invoke("ResetCombo", COMBO_RESET__TIMER);
    }

    public void ResetCombo()
    {
        combo = 0;
    }
}
