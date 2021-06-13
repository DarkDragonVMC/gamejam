using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public static AudioManager instance;

    public Sprite soundOn;
    public Sprite soundOff;

    public Sprite musicOn;
    public Sprite musicOff;

    private bool audioOn = true;
    private bool themeOn = true;

    private PlayerMovement pm;


    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
        Play("theme");
    }

    public void MuteAudio()
    {
        if(audioOn)
        {
            foreach (Sound s in sounds)
            {
                s.source.enabled = false;
            }
            GameObject.Find("MuteAudio").GetComponent<Image>().sprite = soundOff;
            pm.ripRope();
            if (pm.hooks != 3) pm.hooks += 1;
            audioOn = false;
        } 
        else
        {
            foreach (Sound s in sounds)
            {
                s.source.enabled = true;
            }
            GameObject.Find("MuteAudio").GetComponent<Image>().sprite = soundOn;
            pm.ripRope();
            if (pm.hooks != 3) pm.hooks += 1;
            audioOn = true;
        }
    }

    public void MuteTheme()
    {
        if(themeOn)
        {
            Sound theme = Array.Find(sounds, sound => sound.name == "theme");
            theme.source.Stop();
            GameObject.Find("MuteTheme").GetComponent<Image>().sprite = musicOff;
            pm.ripRope();
            if (pm.hooks != 3) pm.hooks += 1;
            themeOn = false;
        } 
        else
        {
            Sound theme = Array.Find(sounds, sound => sound.name == "theme");
            theme.source.Play();
            GameObject.Find("MuteTheme").GetComponent<Image>().sprite = musicOn;
            pm.ripRope();
            if (pm.hooks != 3) pm.hooks += 1;
            themeOn = true;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogError("Couldn't find the sound:" + name);
            return;
        }
        s.source.Play();
    }
}
