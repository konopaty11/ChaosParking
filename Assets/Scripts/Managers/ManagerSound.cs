using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ManagerSound : MonoBehaviour
{
    List<AudioSource> audioSources;

    public static ManagerSound Instance;

    void Awake()
    {
        Instance = this;
        audioSources = GetComponents<AudioSource>().ToList();
    }

    void Start()
    {
        if (ManagerToggle.SoundOn)
            Unmute();
        else
            Mute();
    }

    public void Unmute()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.mute = false;
        }
    }

    public void Mute()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.mute = true;
        }
    }
}
