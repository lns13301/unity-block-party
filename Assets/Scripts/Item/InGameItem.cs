using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InGameItem
{
    public int code;
    public string[] names;
    public int count;
    public ItemType type;
    public Grade grade;
    public string spritePath;

    public Sprite sprite;

    public InGameItem(int code, string[] names, int count, ItemType type, Grade grade, string spritePath)
    {
        this.code = code;
        this.names = names;
        this.count = count;
        this.type = type;
        this.grade = grade;
        this.spritePath = spritePath;

        sprite = LoadSprite(spritePath);
    }

    public Sprite LoadSprite(string path)
    {
        return Resources.Load<Sprite>(path);
    }

    public Sprite LoadSprite()
    {
        return Resources.Load<Sprite>(spritePath);
    }

    public InGameItem MakeNewInGameItem()
    {
        return new InGameItem(code, names, count, type, grade, spritePath);
    }
}
