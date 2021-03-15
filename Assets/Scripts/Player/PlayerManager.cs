using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private static int ITEM_SLOT_ITEM_IMAGE_INDEX = 0;
    private static int SLOT_MAX_COUNT = 2;
    private static Color32 HIDE_COLOR = new Color32(255, 255,255, 0);
    private static Color32 SHOW_COLOR = new Color32(255, 255,255, 255);

    public static PlayerManager instance;

    public GameObject itemSlot;
    public GameObject skillSlot;

    [SerializeField] private PlayerData playerData;
    [SerializeField] private List<InGameItem> inGameItems;
    [SerializeField] private List<GameObject> inGameItemSlots;
    [SerializeField] private Transform ballParent;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        ballParent = GameObject.Find("2D Object").transform.Find("Balls");
        InitializeDropItemSlot();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddInGameItem(InGameItem inGameItem)
    {
        if (inGameItems.Count == SLOT_MAX_COUNT)
        {
            return;
        }

        inGameItems.Add(inGameItem);

        RefreshItemSlot();
    }

    public void RefreshItemSlot()
    {
        for (int i = 0; i < inGameItems.Count; i++)
        {
            if (inGameItems[i] != null)
            {
                inGameItemSlots[i].transform.GetChild(ITEM_SLOT_ITEM_IMAGE_INDEX).GetComponent<Image>().color = SHOW_COLOR;
                inGameItemSlots[i].transform.GetChild(ITEM_SLOT_ITEM_IMAGE_INDEX).GetComponent<Image>().sprite = inGameItems[i].LoadSprite();
            }
            else
            {
                inGameItemSlots[i].transform.GetChild(ITEM_SLOT_ITEM_IMAGE_INDEX).GetComponent<Image>().color = HIDE_COLOR;
            }
        }
    }

    // 슬롯 아이템 사용
    public void ButtonUseInGameItem(int slotIndex)
    {
        try
        {
            if (inGameItems[slotIndex] != null)
            {
                SoundManager.instance.PlayOneShowSoundFindByName("electricSpark");
                UseItem(inGameItems[slotIndex]);
                inGameItemSlots[inGameItems.Count - 1].transform.GetChild(ITEM_SLOT_ITEM_IMAGE_INDEX).GetComponent<Image>().color = HIDE_COLOR; // 마지막 슬롯 이미지 지우기
                inGameItems.RemoveAt(slotIndex);
            }
        }
        catch
        {
            // 아이템이 없는 슬롯을 눌렀을 때 발생하는 예외 캐치
        }
    }

    // 초기화
    private void InitializeDropItemSlot()
    {
        for (int i = 0; i < itemSlot.transform.childCount; i++)
        {
            inGameItemSlots.Add(itemSlot.transform.GetChild(i).gameObject);
            inGameItemSlots[i].transform.GetChild(ITEM_SLOT_ITEM_IMAGE_INDEX).GetComponent<Image>().color = HIDE_COLOR;
        }
    }

    private void UseItem(InGameItem inGameItem)
    {
        switch(inGameItem.code)
        {
            case 0:
                ballParent.GetChild(0).GetComponent<ElectricBall>().Activate();
                // 추후에 반복문으로 생성된 모든 공에 적용시켜줘야함
                break;
        }
    }
}
