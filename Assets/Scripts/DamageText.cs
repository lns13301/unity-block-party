using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float alphaSpeed;
    [SerializeField] private float destroyTimer;
    [SerializeField] private TextMeshPro text;
    [SerializeField] private Color alpha;
    [SerializeField] private int damage;

    [SerializeField] private string sortingLayer;
    [SerializeField] private int sortingOrder;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        if (damage == 0)
        {
            text.text = "Miss";
        }
        else
        {
            text.text = damage.ToString();
        }
        alpha = text.color;
        Invoke("DestroyObject", destroyTimer);

        GetComponent<MeshRenderer>().sortingLayerName = sortingLayer;
        GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        text.color = alpha;
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
