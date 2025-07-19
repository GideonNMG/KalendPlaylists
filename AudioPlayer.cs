using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;


namespace Kalend
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayer : AudioSelection
    {

        public AudioSource audioSource;

        public bool repeat;

        public bool playing;

        [Range(0, 48000)]
        public  int startSampleOffset = 256;

      
        [Range(0, 48000)]
        public int endSampleOffset = 256;

        [HideInInspector]
        public float currentClipTime = 0f;


        public void PlayAudio()
        {
            if (repeat)
            {
                audioSource.timeSamples = startSampleOffset;

                currentClipTime = startSampleOffset / 48000f;

            }

            audioSource.Play();

            playing = true;

        }

        public void PauseAudio()
        {

            playing = false;
            audioSource.Pause();

        }


        public virtual void StopAudio()
        {

            playing = false;
            audioSource.Stop();
            currentClipTime = 0f;

        }


    }
}
