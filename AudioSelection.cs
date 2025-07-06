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

        public static int startSampleOffset = 256;

        public static int endSampleOffset = 256;

        public static int ModShift(int index, int n, int delta)
        {

            n = Mathf.Max(n, 1);

            delta = (delta % n);

            int result = 0;

            index += n + delta;

            result = Mathf.Abs((index % n));

            return result;


        }


    }


}


