using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

namespace Kalend
{

    [RequireComponent(typeof(AudioSource))]
    public class ClipSelector : AudioSelection
    {
        public AudioSource audioSource;

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


        public bool repeat = true;

        public bool playOnStart = true;

        public bool startUIHidden;

        

        private bool _playing;

      
        [Range(32, 128)]
        private int _displayDelta = 32;

        private int _displayCount = 0;

        [SerializeField]
        [Range(0, 48000)]
        private int _startSampleOffset = 256;

        [SerializeField]
        [Range(0, 48000)]
        private int _endSampleOffset = 256;







        private float _currentClipLength = 5f;

        private float _currentClipTime = 0f;

        private float _displayTime;

        private string _clipName;

        

        private static string _currentClipName = "";      

          

        
   
        private int _loop = 0;

        public virtual void Awake()
        {





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



        private void Start()
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
            }
            

        }
       

        public void PlayAudio()
        {
            if (repeat)
            {
                audioSource.timeSamples = _startSampleOffset;

                _currentClipTime = _startSampleOffset / 48000f;

                //Debug.Log("<color=green>Starting Sample Offset = </color>" + _startSampleOffset + "<color=green> Samples. </color>");
            }

            audioSource.Play();

            _playing = true;



        }

        public void SetAudioSourceLoop()
        {

            audioSource.loop = repeat;
        }

        public void PauseAudio()
        {

            _playing = false;
            audioSource.Pause();

        }


        public void StopAudio()
        {
            _playing = false;
            audioSource.Stop();
            _currentClipTime = 0f;

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

              

            if (repeat)
            {
                _currentClipLength = currentAudioClip.length - ((_startSampleOffset + _endSampleOffset) / 48000f);

                _currentClipTime = (_startSampleOffset / 48000f);

            }

            else
            {
                _currentClipLength = currentAudioClip.length;
                _currentClipTime = 0f;
            }

            
              PlayAudio();
              GetClipName();

        }


        public void GetClipName()
        {
            _currentClipName = audioSource.clip.name;

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



        public void DisplayClipTime()
        {

            if (clipTimeDisplay != null)
            {

                if (_currentClipLength > 0)
                {
                    _displayTime = _currentClipTime % _currentClipLength;


                    int minutes = Mathf.FloorToInt(_displayTime / 60f);
                    int seconds = Mathf.FloorToInt(_displayTime - (minutes * 60));
                    

                    string clockTime = string.Format("{0:0}:{1:00}", minutes, seconds);


                    clipTimeDisplay.text = clockTime;


                }

                
            }
        }


        public void ToggleLooped()
        {
        

            _loop = ((_loop + 1) % 2);

            repeat = (_loop == 0) ? true : false;

            ToggleRepeatImage();

        }

    

     


        public void IncrementClip()
        {

            currentIndex = ModShift(currentIndex, clipCount, 1);

            SetCurrentAudioCLip();

        }

        public void DecrementClip()
        {

            currentIndex = ModShift(currentIndex, clipCount, -1);

            SetCurrentAudioCLip();
        }

        public void Update()
        {


            if (_playing)
            {

                _currentClipTime += Time.deltaTime;

                _displayCount++;

                if (_displayCount > _displayDelta)
                {
                    DisplayClipTime();
                    _displayCount = 0;

                }


            }

          

            if (_currentClipTime >= _currentClipLength )
            {

                _currentClipTime = 0f;

                if (!repeat)
                {
                    IncrementClip();

                }


                else
                {

                    Debug.Log("<color=red>Ending Sample Offset = </color>" + _endSampleOffset + "<color=red> Samples. </color>");
                    SetAudioClip();
                }


            }

        }

    }


}




