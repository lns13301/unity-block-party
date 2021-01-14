using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TMPManager : MonoBehaviour
{
    public static TMPManager instance;
    public GameObject[] tmps;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateText(string text, GameObject gameObject, Vector2 position, float fontSize = 3.5f, int index = 0)
    {
        GameObject tmp = Instantiate(tmps[index]);
        tmp.transform.SetParent(gameObject.transform.parent);
        tmp.transform.position = position;
        tmp.transform.localScale = new Vector3(1, 1, 1);
        tmp.GetComponent<TextMeshPro>().fontSize = fontSize;

        try
        {
            tmp.GetComponent<DamageText>().SetDamage(int.Parse(text));
        }
        catch
        {
            tmp.GetComponent<DamageText>().SetText(text);
        }
    }
}
