using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]

public class Sound
{
    public AudioMixerGroup audioMixerGroup;

    private AudioSource source;

    public string clipName;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [Range(0f, 3f)]
    public float pitch;

    public bool loop = false;
    public bool playOneAwake = false;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.pitch = pitch;
        source.volume = volume;
        source.playOnAwake = playOneAwake;
        source.loop = loop;
        source.outputAudioMixerGroup = audioMixerGroup;
    }

    public void Play()
    {
        source.Play();
    }
}

public class AudioManager : MonoBehaviour
{
    //reference to audio mixer
    public AudioMixer audioMixer;

    //function to set the master level
    public void SetMasterVolume(float masterLv)
    {
        audioMixer.SetFloat("MasterVolume", masterLv);
    }

    //function to set the sound effects level
    public void SetSFXVolume(float sfxLv)
    {
        audioMixer.SetFloat("SFXVolume", sfxLv);
    }

    //function to set the music level
    public void SetMusicVolume(float musicLv)
    {
        audioMixer.SetFloat("MusicVolume", musicLv);
    }

    public static AudioManager instance;

    [SerializeField]
    Sound[] sound;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < sound.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sound[i].clipName);
            _go.transform.SetParent(this.transform);
            sound[i].SetSource(_go.AddComponent<AudioSource>());
        }

        PlaySound("Bumhunter Theme");
    }

    public void PlaySound(string _name)
    {
        for (int i = 0; i < sound.Length; i++)
        {
            if (sound[i].clipName == _name)
            {
                sound[i].Play();
                return;
            }
        }
    }
}
