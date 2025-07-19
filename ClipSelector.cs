using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

namespace Kalend
{

   
    public class ClipSelector : AudioPlayer
    {


        public AudioMixer audioMixer;

        public AudioMixerGroup audioMixerGroup;

        public AudioClip[] audioClips;

        public AudioClip startingClip;

        public TextMeshProUGUI clipNameDisplay;

        public TextMeshProUGUI clipTimeDisplay;

        public GameObject repeatImage;

        public GameObject[] startUIHiddenObjects;

        public GameObject[] startUIVisibleObjects;

        public GameObject[] alwaysHideObjects;

        public GameObject[] alwaysShowObjects;



        public bool playOnStart = false;

        public bool startUIHidden;

        public bool shuffle;


        private bool _playing;

        [HideInInspector]
        public bool firstPlay;

      
        [Range(32, 128)]
        private int _displayDelta = 32;

        private int _displayCount = 0;


        private float _currentClipLength = 5f;

        private float _displayTime;

        private string _clipName;
  

        private static string _currentClipName = "";      


        public virtual void Awake()
        {


            firstPlay = true;

            if (audioClips != null)
            {
                clipCount = audioClips.Length;

                SetStartingClip();             
           
            }

            else
            {

                Debug.LogWarning("Audio Clips are null!");

            }

            ToggleRepeatImage();


            if (startUIVisibleObjects != null)
            {

                foreach (GameObject go in startUIVisibleObjects)
                {
                    go.SetActive(!startUIHidden);
                }
            }



            if (startUIHiddenObjects != null)
            {

                foreach (GameObject go in startUIHiddenObjects)
                {
                    go.SetActive(startUIHidden);
                }
            }


            if (alwaysHideObjects != null)
            {

                foreach (GameObject go in alwaysHideObjects)
                {
                    go.SetActive(false);
                }
            }

            if (alwaysShowObjects != null)
            {

                foreach (GameObject go in alwaysShowObjects)
                {
                    go.SetActive(true);
                }
            }


          
            if(clipTimeDisplay != null)
            {

                clipTimeDisplay.text = _displayTime.ToString();
            }

        }



        public virtual void Start()
        {
       
            if (playOnStart)
            {

                PlayAudio();
            }
        }


        
        public void ToggleRepeatImage()
        {

            if (repeatImage != null)
            {

                repeatImage.SetActive(repeat);
                Debug.Log("<color=green>Toggle Repeat Image Called.</color>");
            }

        }

        

        public override void StopAudio()
        {

            base.StopAudio();

            AudioEvents.ResetClip();

            DisplayClipTime();        

        }



        public void SetStartingClip()
        {

            if (startingClip != null)
            {

                currentIndex = System.Array.IndexOf(audioClips, startingClip);

                currentAudioClip = startingClip;

            }


            else
            {
               

                currentIndex = 0;

                currentAudioClip = audioClips[0];
            }

            SetAudioClip();

        }



        public void SetCurrentAudioCLip()
        {
            currentAudioClip = audioClips[currentIndex];

            SetAudioClip();
        }



        public void SetAudioClip()
        {


            audioSource.clip = currentAudioClip;

            currentAudioSource = audioSource;

              

            if (repeat)
            {
                _currentClipLength = currentAudioClip.length - ((startSampleOffset + endSampleOffset) / 48000f);

                currentClipTime = (startSampleOffset / 48000f);

            }

            else
            {
                _currentClipLength = currentAudioClip.length;
                currentClipTime = 0f;
            }


            if(firstPlay && !playOnStart)
            {
                GetClipName();

            }

            else
            {
                PlayAudio();
                GetClipName();

            }

            firstPlay = false;

        }



        public void GetClipName()
        {

            _currentClipName = audioClips[currentIndex].name;

            SetClipName();
       }


        public void SetClipName()
        {

            _clipName = _currentClipName;
            DisplayClipName();
           
        }



        public void DisplayClipName()
        {

            clipNameDisplay.text = _clipName;
    
        }



        public void ClearClipName()
        {

            clipNameDisplay.text = "";
        }



        public void DisplayClipTime()
        {

            if (clipTimeDisplay != null)
            {

                if (_currentClipLength > 0)
                {
                    _displayTime = currentClipTime % _currentClipLength;

                    int minutes = Mathf.FloorToInt(_displayTime / 60f);
                    int seconds = Mathf.FloorToInt(_displayTime - (minutes * 60));             

                    string clockTime = string.Format("{0:0}:{1:00}", minutes, seconds);

                    clipTimeDisplay.text = clockTime;

                }
                
            }
        }



        public void ToggleLooped()
        {

            repeat = !repeat;

            ToggleRepeatImage();

        }


        public void ToggleShuffle(bool s)
        {

            shuffle = s;
        }



        public void IncrementClip()
        {

         
            currentIndex = ModShift(currentIndex, clipCount, 1);

            SetCurrentAudioCLip();

            AudioEvents.NextClip();

        }


        public void DecrementClip()
        {

            currentIndex = ModShift(currentIndex, clipCount, -1);

            SetCurrentAudioCLip();
            AudioEvents.NextClip();
        }



        public void ShuffleAudioClips()
        {


            audioClips.Shuffle();

            Debug.Log("<color=magenta>Audio Clips Shuffled.</color>.");

            currentIndex = 0;

            audioSource.clip = audioClips[0];

            ClearClipName();

            if (!firstPlay)
            {
                GetClipName();

            }
        
            AudioEvents.ShuffleAudio();

            if (playOnStart)
            {
                PlayAudio();

            }    

        }


        public void Update()
        {


            if (_playing)
            {

                currentClipTime += Time.deltaTime;

                _displayCount++;

                if (_displayCount > _displayDelta)
                {
                    DisplayClipTime();
                    _displayCount = 0;

                }

            }
        

            if (currentClipTime >= _currentClipLength )
            {

                currentClipTime = 0f;

                if (repeat == false)
                {
                    IncrementClip();

                }


                else
                {

                    Debug.Log("<color=red>Ending Sample Offset = </color>" + endSampleOffset + "<color=red> Samples. </color>");
                    SetAudioClip();
                }


            }

        }

    }


}




