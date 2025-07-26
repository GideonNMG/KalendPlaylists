using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Kalend
{

    public abstract class AudioSelection : MonoBehaviour
    {
        public static string currentClipName;

        public static string currentPlayListName;

        public static AudioClip currentAudioClip;

        public static AudioSource currentAudioSource;

        public static AudioMixer currentAudioMixer;

        public static AudioMixerGroup currentAudioMixerGroup;

        public static AudioClip[] currentAudioClips;

        public static int clipCount = 1;

        public static int currentIndex = 0;

        public static float currentClipTime = 0f;



        public enum SampleRate
        {
            FortyFourPointOne,
            FortyEight,
            NinteySix,
            Other

        };


        public static int SampleCount(SampleRate rate)
        {

            int result = 48000;

            switch (rate)
            {
                case SampleRate.FortyFourPointOne:

                    result = 41100;

                    break;

                case SampleRate.FortyEight:

                    result = 48000;

                    break;



                case SampleRate.NinteySix:

                    result = 96000;

                    break;



                default:

                    result = 48000;

                    break;

            }

            return result;
        }


        public static int SampleCount(int custom)
        {

            int result = Mathf.Clamp(custom, 10000, 192000);

            return result;
        }


       public static int ModShift(int index, int n, int delta)
        {

            n = Mathf.Max(n, 1);

            delta = (delta % n);

            index += n + delta;

            int result = Mathf.Abs((index % n));

            return result;


        }


    }


}


