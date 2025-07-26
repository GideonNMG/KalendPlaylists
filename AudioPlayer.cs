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
        [Header("Audio")]
        public AudioSource audioSource;

        [HideInInspector]//This bool is exposed (as a private bool) in "Clip Selector."
        public bool repeat;

        //Advanced features. Comment out the [HideInInspector] header to make them visible in the inspector. 
        [HideInInspector]
        public bool showLogs; //Useful if you want to modify or extend the class. 
      

        [HideInInspector]//Comment out to manually set sample rate. (Only affects start and end sample offsets.)
        public SampleRate sampleRate = SampleRate.FortyEight;
      

        [HideInInspector]//Comment out to manually set an offeset for when the song starts (when Repeat == true). 
        [Range(0, 48000)]
        public  int startSampleOffset = 256;

        [HideInInspector]//Comment out to manually set an offeset for when the song ends (when Repeat == true). 
        [Range(0, 48000)]
        public int endSampleOffset = 256;




        //These should probably be left hidden and not modified (unless you want to see their values during Play Mode in the inspector). 
        [HideInInspector]
        public bool playing;

        [HideInInspector]//Default sample rate. Will be set by the enum "sampleRate" if you expose it above. 
        public int samples = 48000;


        [HideInInspector]
        public bool scrubbing;




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
