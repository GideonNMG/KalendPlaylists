using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

namespace Kalend
{
    public class AudioScrubber : AudioPlayer
    {
        public Slider scrubSlider;

        public TextMeshProUGUI scrubValueDisplay;

        [SerializeField]
        [Range(0.001f, 0.25f)]
        private float _resetTime = 0.05f;

        [SerializeField]
        [Range(0.001f, 0.5f)]
        private float _waitTime = 0.05f;

        private bool _set;

        private bool _allowScrubbing;

        public void Awake()
        {
           

            if(scrubSlider!=null && scrubValueDisplay != null && currentAudioSource != null && audioSource!=null)
            {
                audioSource = audioSource.GetComponent<AudioSource>();
                _set = true;

                _allowScrubbing = true;
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

                if (currentAudioSource.time > (currentAudioClip.length -  _resetTime))
                {
                    scrubbing = false;

                    _allowScrubbing = false;

                   
                }


                if (!scrubbing)
                {
                    SetScrubberByAudio();

                }
            } 
        }


        public void SetScrubbing(bool scrub)
        {


            scrubbing = _allowScrubbing ? scrub : false;


            if (showLogs)
            {

                Debug.Log("<color=blue> Scrubbing = </color>" + scrubbing);
            }


        }

        public void ScrubAudio()
        {
            
                if (_set && scrubbing && _allowScrubbing)
                {
                    scrubSlider.value = Mathf.Clamp01(scrubSlider.value);

                    float t = Mathf.Clamp(currentAudioClip.length * scrubSlider.value, 0f, (currentAudioClip.length) - _resetTime);


                    audioSource.time = t;

                    currentClipTime = t;


                }

            
        }


        public void SetScrubberByAudio()
        {
            if (_set && !scrubbing)
            {

                scrubSlider.value = Mathf.Clamp01(currentAudioSource.time / currentAudioClip.length);

            }

        }


        public void ResetScrubber()
        {
            scrubSlider.value = 0f;

            audioSource.time = 0f;

            _allowScrubbing = false;

            StartCoroutine(WaitToAllowScrubbing());

            if (showLogs)
            {

                Debug.Log("<color=Blue> Reset Scrubber Called.</color>");

            }

        }

        public IEnumerator WaitToAllowScrubbing()
        {

            _allowScrubbing = false;
            scrubbing = false;
            scrubSlider.value = 0f;
          

           yield return new WaitForSeconds(_waitTime);

            _allowScrubbing = true;
        }

    }

}


