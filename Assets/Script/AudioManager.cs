using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void Play(String name)
    {
        //Sound s = Array.Find(sounds, sound => sound.name == name);
        Sound s = sounds[0];
        foreach (Sound so in sounds)
        {
            if (so.name == name)
            {
                s = so;
                break;
            }
        }
        s.source.Play();
        if (s.name == "dawn" || s.name == "win" || s.name == "game")
        {
            s.source.loop = true;
        }
    }

    public void Stop()
    {
        foreach (Sound s in sounds)
        {
            if (s.source.isPlaying)
            {
                s.source.Stop();
            }
        }
    }
}
