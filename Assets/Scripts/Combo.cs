using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combo : MonoBehaviour
{
    private static string COMBOTEXT_EN = " Combo";
    private static float ALPHA_SPEED = 1f;
    private static float COMBO_FADE_OUT_START_TIMER = 3f;

    public static Combo instance;

    public Text comboText;
    private Color alpha;

    public bool isComboOn;

    [SerializeField] private int combo;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCombo(int value = 1)
    {
        StopCoroutine("FadeOutCombo");

        if (value == -1)
        {
            combo = 0;
            comboText.text = "";
        }
        else
        {
            combo += value;
            ChangeCombo(combo);
            Invoke("StartFadeOut", COMBO_FADE_OUT_START_TIMER);
        }
    }

    public void ChangeCombo(int combo)
    {
        comboText.text = combo + COMBOTEXT_EN;
    }

    public void StartFadeOut()
    {
        StartCoroutine("FadeOutCombo");
    }

    IEnumerator FadeOutCombo()
    {
        alpha = comboText.color;

        while (isComboOn)
        {
            alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * ALPHA_SPEED);
            comboText.color = alpha;
            yield return null;
        }
    }
}
