using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;
    public ItemDataFile itemDataFile;

    private string spritePathHero = "Images/Character/";
    private string spritePathWeapon = "Images/Weapon/";
    private string effectsPath = "Effects/";
    private string spritePathTalent = "Images/UI/Stat/TalentItem/";
    private string spritePathWeaponTalent = "Images/UI/Stat/WeaponItem/";
    private string fileName = "itemdatas";

    public Dictionary<int, Item> itemDatas = new Dictionary<int, Item>();

    private void Awake()
    {
        instance = this;
    }

    public List<Item> itemDB = new List<Item>();

    public GameObject entityItemPrefab;
    public Vector2[] pos;

    private void Start()
    {
        itemDataFile = new ItemDataFile();
        itemDataFile.itemDatas = new List<Item>();

        //SaveItemData();
        LoadItemData();

        // 딕셔너리에 아이템 정보 입력
        for (int i = 0; i < itemDB.Count; i++)
        {
            itemDatas.Add(itemDB[i].code, itemDB[i]);
        }
    }

    public int findItemDBPositionByCode(int code)
    {
        for (int i = 0; i < itemDB.Count; i++)
        {
            if (itemDB[i].code == code)
            {
                return i;
            }
        }

        return -1;
    }

/*    public Item findItemByName(string ko)
    {
        for (int i = 0; i < itemDB.Count; i++)
        {
            if (itemDB[i].koName == ko)
            {
                return itemDB[i];
            }
        }
        return null;
    }*/

    /*    public Item findItemByCode(int code)
        {
            for (int i = 0; i < itemDB.Count; i++)
            {
                if (itemDB[i].code == code)
                {
                    return itemDB[i];
                }
            }
            return null;
        }*/

    public Item findItemByCode(int code)
    {
        return itemDatas[code];
    }

    public Item pickRandomItem()
    {
        return itemDB[Random.Range(0, itemDB.Count)];
    }

    [ContextMenu("From Json Data")]
    public Sprite loadSprite(string path)
    {
        return Resources.Load<Sprite>(path);
    }

    [ContextMenu("To Json Data")]
    public void SaveItemData()
    {
        Debug.Log("저장 성공");
        itemDataFile.itemDatas = new List<Item>();

        // itemDataFile.itemDatas.Add(new Item(0, "Amber", "엠버", 1, ItemType.CHARACTER, Grade.UNIQUE, spritePathHero + "Amber", new Character(0, Element.PYRO, Grade.UNIQUE, 1000, 10408, 10310)));

        string jsonData = JsonUtility.ToJson(itemDataFile, true);

        File.WriteAllText(SaveOrLoad(false, true, fileName), jsonData);
    }

    [ContextMenu("From Json Data")]
    public void LoadItemData()
    {
        try
        {
            Debug.Log("아이템 정보 로드 성공");
            itemDataFile = JsonUtility.FromJson<ItemDataFile>(Resources.Load<TextAsset>(fileName).ToString());

            for (int i = 0; i < itemDataFile.itemDatas.Count; i++)
            {
                itemDataFile.itemDatas[i].sprite = loadSprite(itemDataFile.itemDatas[i].spritePath);
                itemDB.Add(itemDataFile.itemDatas[i]);
            }
        }
        catch (FileNotFoundException)
        {
            Debug.Log("로드 오류");

            string jsonData = JsonUtility.ToJson(itemDataFile, true);

            File.WriteAllText(SaveOrLoad(false, false, fileName), jsonData);
            LoadItemData();
        }
    }

    public string SaveOrLoad(bool isMobile, bool isSave, string fileName)
    {
        if (isSave)
        {
            if (isMobile)
            {
                // 모바일 저장
                return Path.Combine(Application.persistentDataPath, fileName + ".json");
            }
            else
            {
                // pc 저장
                return Path.Combine(Application.dataPath, fileName + ".json");
            }
        }
        else
        {
            if (isMobile)
            {
                // 모바일 로드
                return Path.Combine(Application.persistentDataPath, fileName + ".json");
            }
            else
            {
                // pc 로드
                return Path.Combine(Application.dataPath, fileName + ".json");
            }
        }
    }
}

[System.Serializable]
public class ItemDataFile
{
    public List<Item> itemDatas;
}