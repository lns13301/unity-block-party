using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private Sound[] sounds;
    [SerializeField] private Sound[] effectSounds;

    public Dictionary<string, Sound> soundMap;

    public int timer;

    private void Awake()
    {
        instance = this;

        sounds = new Sound[transform.GetChild(0).childCount];
        effectSounds = new Sound[transform.GetChild(1).childCount];
        soundMap = new Dictionary<string, Sound>();

        // 음악 등록
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            sounds[i] = transform.GetChild(0).GetChild(i).GetComponent<Sound>();
            soundMap.Add(sounds[i].soundName, sounds[i]);
        }

        // 효과음 등록
        for (int i = 0; i < transform.GetChild(1).childCount; i++)
        {
            effectSounds[i] = transform.GetChild(1).GetChild(i).GetComponent<Sound>();
            soundMap.Add(effectSounds[i].soundName, effectSounds[i]);
        }
    }

    public void PlayMusic(int index)
    {
        sounds[index].PlaySound();
    }

    public void PlayEffectSound(int index)
    {
        effectSounds[index].PlaySound();
    }

    public void PlayOneShotEffectSound(int index)
    {
        effectSounds[index].StopSound();
        effectSounds[index].PlaySound();
    }

    public void PlayOneShowSoundFindByName(string name)
    {
        try
        {
            soundMap[name].StopSound();
            soundMap[name].PlaySound();
        }
        catch (NullReferenceException)
        {
            Debug.LogError("찾을 수 없음: " + name);
        }
    }

    public void StopSound(int index)
    {
        sounds[index].StopSound();
    }

    public void StopEffectSound(int index)
    {
        effectSounds[index].StopSound();
    }

    public void RefreshSounds()
    {
        CancelInvoke();
        StopAllSounds();
    }

    public void StopAllSounds()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].StopSound();
        }
    }

    public void PlayButtonEffectSound()
    {
        PlayEffectSound(0);
    }
}
