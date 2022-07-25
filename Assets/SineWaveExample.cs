using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System;

public class SineWaveExample : MonoBehaviour
{
    List<Tone> tones;

    class Tone
    {
        public string NOTE;
        public float FREQ_HZ;
        public float WL_CM;
        public float WL_IN;
        public float OCTAVE;
        
        public static Tone FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            Tone tone = new Tone();

            tone.NOTE = Convert.ToString(values[0]);
            tone.FREQ_HZ = floatNullValueCheck(values[1]);
            tone.WL_CM = floatNullValueCheck(values[2]);
            tone.WL_IN = floatNullValueCheck(values[3]);
            tone.OCTAVE = floatNullValueCheck(values[4]);
            return tone;
        }
        public static float floatNullValueCheck(string value)
        {
            float outValue;
            bool success = float.TryParse(value, out outValue);
            return success ? outValue : 0.0f;
        }
    }

    private Tone priorNote1;
    private Tone priorNote2;


    [Range(1, 20000)]  //Creates a slider in the inspector
    public float frequency1;

    [Range(1, 20000)]  //Creates a slider in the inspector
    public float frequency2;

    public float sampleRate = 44100;
    public float waveLengthInSeconds = 2.0f;

    public float playRampTime = 3.0f;
    public float stopRampTime = 1.0f;
    public float playRampAttenuation = 0.01f;
    public float stopRampAttenuation = -0.01f;
    public float minVolume = 0.0f;
    public float maxVolume = 1.0f;

    public bool oddEvenToggle;

    AudioSource audioSource;
    int timeIndex = 0;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0; //force 2D sound
        audioSource.Stop(); //avoids audiosource from starting to play automatically
        tones = File.ReadAllLines("Assets\\tones.csv")
                                           .Skip(1)
                                           .Select(v => Tone.FromCsv(v))
                                           .Where(v => (
                                           v.NOTE.Substring(0,1) == "C" ||
                                           v.NOTE.Substring(0, 1) == "D" ||
                                           v.NOTE.Substring(0, 1) == "E" ||
                                           v.NOTE.Substring(0, 1) == "F" ||
                                           v.NOTE.Substring(0, 2) == "F#" ||
                                           v.NOTE.Substring(0, 1) == "A" ||
                                           v.NOTE.Substring(0, 1) == "B") && v.OCTAVE >=3 && v.OCTAVE <=5)
                                           .ToList();
        priorNote1 = tones[0];
        priorNote2 = tones[0];


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!audioSource.isPlaying)
            {
                //choose random note
                int noteIndex = UnityEngine.Random.Range(0, tones.Count);
                int counterNoteIndex = UnityEngine.Random.Range(0, tones.Count);
                frequency1 = tones[noteIndex].FREQ_HZ;
                if (UnityEngine.Random.Range(0.0f, 1.0f) > 0.9f)
                {
                    frequency2 = tones[noteIndex].FREQ_HZ;
                }
                else
                {
                    frequency2 = tones[counterNoteIndex].FREQ_HZ;
                }
                waveLengthInSeconds = tones[noteIndex].WL_IN;


                if (UnityEngine.Random.Range(0.0f, 1.0f) < 0.3f && !oddEvenToggle)
                {
                    frequency1 = priorNote1.FREQ_HZ;
                    frequency2 = priorNote2.FREQ_HZ;
                    waveLengthInSeconds = priorNote2.WL_IN;
                }

                oddEvenToggle = !oddEvenToggle;
                if (oddEvenToggle)
                {
                    priorNote1 = tones[noteIndex];
                    priorNote2 = tones[counterNoteIndex];
                }
                


                timeIndex = 0;  //resets timer before playing sound
                audioSource.volume = 0;
                audioSource.Play();
                //IEnumerator coroutine = rampVolume(playRampAttenuation, playRampTime, false);
                IEnumerator coroutine = rampVolumeUp(playRampTime);
                StartCoroutine(coroutine);
            }
            else
            {
                //IEnumerator coroutine = rampVolume(stopRampAttenuation, stopRampTime, false);
                IEnumerator coroutine = rampVolumeDown(stopRampTime);
                StartCoroutine(coroutine);
                
                
            }
        }
    }

    IEnumerator rampVolume(float delta, float duration, bool stopAfter)
    {
        var t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            audioSource.volume = Mathf.Clamp(audioSource.volume + delta,0,1);
            yield return null;
        }
        if (stopAfter || audioSource.volume == 0)
        {
            audioSource.Stop();
        }
    }

    IEnumerator rampVolumeUp(float duration)
    {
        var t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            audioSource.volume = Mathf.SmoothStep(audioSource.volume, maxVolume, t/duration);
            yield return null;
        }
        IEnumerator coroutine = rampVolumeDown(stopRampTime);
        StartCoroutine(coroutine);
    }
    IEnumerator rampVolumeDown(float duration)
    {
        var t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            audioSource.volume = Mathf.SmoothStep(audioSource.volume, minVolume, t / duration);
            yield return null;
        }
        if (audioSource.volume == 0)
        { audioSource.Stop(); }
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        for (int i = 0; i < data.Length; i += channels)
        {
            data[i] = CreateSine(timeIndex, frequency1, sampleRate);

            if (channels == 2)
                data[i + 1] = CreateSine(timeIndex, frequency2, sampleRate);

            timeIndex++;

            //if timeIndex gets too big, reset it to 0
            if (timeIndex >= (sampleRate * waveLengthInSeconds))
            {
                timeIndex = 0;
            }
        }
    }

    //Creates a sinewave
    public float CreateSine(int timeIndex, float frequency, float sampleRate)
    {
        return Mathf.Sin(2 * Mathf.PI * timeIndex * frequency / sampleRate);
    }
}
