using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class Instrument
    {
        public AudioSource audio;
        [Range(0f, 1f)]
        public float volume = 0f;
    }

    public static AudioManager Instance { private set; get; }

    [SerializeField] private float increasePercentage = 0.334f;
    [SerializeField] private AudioSource musicBase = null;
    [SerializeField] private Instrument[] instruments = null;
    [SerializeField] private AudioSource source = null;
    [Space]
    [SerializeField] private AudioClip hitClip = null;
    [SerializeField] private AudioClip dieClip = null;

    // ---------- Unity messages

    private void Awake()
    {
        Instance = this;
    }

    /*private void OnValidate()
    {
        foreach (Instrument obj in instruments)
        {
            obj.audio.volume = obj.volume;
        }
    }*/

    // ---------- public methods

    public void IncreaseAudio(int index)
    {
        if (index < 0 || index > instruments.Length)
            return;

        instruments[index].volume = Mathf.Clamp(instruments[index].volume + increasePercentage, 0f, 1f);
        instruments[index].audio.volume = instruments[index].volume;
    }

    public void PlayClip(AudioClip clip, bool muteMusic=false)
    {
        source.PlayOneShot(clip);

        if (muteMusic)
            MuteMusic();
    }

    public void PlayShoot()
    {
        source.PlayOneShot(hitClip);
    }

    public void PlayDie()
    {
        source.PlayOneShot(dieClip);
    }

    // ---------- private methods

    private void MuteMusic()
    {
        foreach (Instrument obj in instruments)
        {
            obj.audio.volume = 0f;
        }

        musicBase.volume = 0f;
    }
}
