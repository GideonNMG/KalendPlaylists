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

        public bool showLogs;

        public SampleRate sampleRate = SampleRate.FortyEight;

        [HideInInspector]
        public bool scrubbing;

        [HideInInspector]
        public int samples = 48000;

        [Range(0, 48000)]
        public  int startSampleOffset = 256;

      
        [Range(0, 48000)]
        public int endSampleOffset = 256;

        //[HideInInspector]
        //public static float currentClipTime = 0f;


        public void PlayAudio()
        {
            if (repeat)
            {
                audioSource.timeSamples = startSampleOffset;

                currentClipTime = startSampleOffset / (float)SampleCount(sampleRate);

            }

            audioSource.Play();

            playing = true;

            scrubbing = false;


            //if (currentClipTime > 0f && currentClipTime < currentAudioClip.length)
            //{
            //    audioSource.Play();

            //    playing = true;

            //    scrubbing = false;

            //}

        }

        public void PauseAudio()
        {

            playing = false;
            scrubbing = false;
            audioSource.Pause();
           
        }


        public virtual void StopAudio()
        {

            playing = false;
            scrubbing = false;
            audioSource.Stop();
            currentClipTime = 0f;
            

        }


    }
}
