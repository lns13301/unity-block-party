using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    public static DropManager instance;

    public GameObject[] dropItems;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateDropItem(Vector2 position, GameObject gameObject, int index = 0, float percentage = 100)
    {
        if (!IsRandom(percentage))
        {
            return;
        }

        GameObject dropItem = Instantiate(dropItems[index]);
        dropItem.transform.SetParent(gameObject.transform.parent);
        dropItem.transform.position = position;
        dropItem.transform.localScale = new Vector3(2, 2, 1);
    }

    // 소수점 이하 셋 째 자리까지
    public bool IsRandom(float percentage)
    {
        int value = (int) percentage * 1000;
        int randomValue = Random.Range(0, 100000);

        // Debug.Log("랜덤 값 : " + randomValue + ", 확률 값 : " + value);

        return randomValue < value;
    }
}
