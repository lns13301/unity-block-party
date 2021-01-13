using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.PlayMusic(0);
        SoundManager.instance.PlayMusic(1);

        OnApplicationFocus(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnApplicationFocus(bool focusStatus)
    {
        if (focusStatus)
        {
            DisableSystemUI.DisableNavUI();
        }
    }
}
