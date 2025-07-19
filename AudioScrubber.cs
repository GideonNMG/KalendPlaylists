using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

namespace Kalend
{
    public class AudioScrubber : AudioSelection
    {
        public Slider scrubSlider;

        public AudioSource audioSource;

        public TextMeshProUGUI scrubValueDisplay;

        private float _scrubValue;

        private bool _scrubbing;

        private bool _set;

        public void Awake()
        {
           

            if(scrubSlider!=null && scrubValueDisplay != null && currentAudioSource != null && audioSource!=null)
            {
                audioSource = audioSource.GetComponent<AudioSource>();
                _set = true;
            }

            else
            {
                Debug.Log("<color=red> Audio Source is null!</color>");
            }


            AudioEvents.reset += ResetScrubber;
         

        }


        public void OnDisable()
        {
            AudioEvents.reset -= ResetScrubber;
        }

        // Update is called once per frame
        void Update()
        {
           

            if (_set )
            {


                float f = (Mathf.Clamp01(currentAudioSource.time/ currentAudioClip.length)) * 100f;

                int c = Mathf.FloorToInt(f);

                scrubValueDisplay.text = c.ToString() + " %"; ;

                if (currentAudioSource.time >= (currentAudioClip.length - 0.02f))
                {

                    ResetScrubber();

                }

                if (!_scrubbing)
                {
                    SetScrubberByAudio();

                }
            } 
        }


        public void SetScrubbing(bool scrub)
        {

            _scrubbing = scrub;

            //Debug.Log("<color=blue> Scrubbing = </color>" + _scrubbing);
        }

        public void ScrubAudio()
        {
            
                if (_set && _scrubbing)
                {
                    scrubSlider.value = Mathf.Clamp01(scrubSlider.value);

                    float t = Mathf.Clamp(currentAudioClip.length * scrubSlider.value, 0f, (currentAudioClip.length - 0.01f));

                 audioSource.time = Mathf.Min(currentAudioClip.length, t);

                }

        }

        public void SetScrubberByAudio()
        {
            if (_set && !_scrubbing)
            {

                scrubSlider.value = Mathf.Clamp01(currentAudioSource.time / currentAudioClip.length);

            }

        }


        public void ResetScrubber()
        {
            scrubSlider.value = 0f;

            audioSource.time = 0f;

            Debug.Log("<color=Blue> Reset Scrubber Called.</color>");


        }

    }

}


