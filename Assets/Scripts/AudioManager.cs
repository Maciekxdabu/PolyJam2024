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
    [SerializeField] private Instrument[] instruments = null;

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
}
