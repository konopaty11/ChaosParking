using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ManagerToggle : MonoBehaviour
{
    [SerializeField] Toggle toggle;
    [SerializeField] Image soundImage;
    [SerializeField] Sprite soundOnSprite;
    [SerializeField] Sprite soundOffSprite;

    List<ManagerSound> managersSound;

    public static bool SoundOn { get; private set; }

    void Awake()
    {
        managersSound = FindObjectsByType<ManagerSound>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();        
    }

    void Start()
    {
        SoundOn = YG2.saves.soundEnabled;
        toggle.isOn = SoundOn;
        ChangeSpriteToggle();
    }

    public void ChangeSpriteToggle()
    {
        YG2.saves.soundEnabled = toggle.isOn;

        managersSound = FindObjectsByType<ManagerSound>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();
        if (toggle.isOn)
        {
            soundImage.sprite = soundOnSprite;
            SoundOn = true;
            foreach (ManagerSound manager in managersSound)
                manager.Unmute();
        }
        else
        {
            soundImage.sprite = soundOffSprite;
            SoundOn = false;
            foreach (ManagerSound manager in managersSound)
                manager.Mute();
        }
    }
}
