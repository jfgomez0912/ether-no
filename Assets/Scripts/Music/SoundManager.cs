using UnityEngine.Audio;
using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;

    public string song = "Lobby1";

    public static SoundManager soundManager;
    void Awake()
    {
        if (SoundManager.soundManager == null)
        {
            SoundManager.soundManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("Destroy SM");
        }
        
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volumen;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    private void Start()
    {
        FindFirstObjectByType<SoundManager>().Play(song);
    }

    public void Change(string name)
    {
        Stop(song);
        Play(name);
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null){
            Debug.LogWarning($"Sound: {name} not found!");
            return;
        }
        if (name != "Cargando"){
            song = name;
        }            
        s.source.Play();
        
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null){
            Debug.LogWarning($"Sound: {name} not found!");
            return;
        }
        s.source.Stop();
    }
}
