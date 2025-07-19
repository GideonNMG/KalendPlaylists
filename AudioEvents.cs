using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kalend
{
    public static class AudioEvents 
    {

        public delegate void ClipStart();

        public static event ClipStart startClip;


        public delegate void ClipEnd();

        public static event ClipEnd endClip;



        public delegate void ClipNext();

        public static event ClipNext nextClip;


        public delegate void Reset();

        public static event Reset reset;



        public delegate void PlayListNext();

        public static event PlayListNext nextPlaylist;



        public delegate void AudioShuffle();

        public static event AudioShuffle shuffleAudio;




        public static void StartClip()
        {
            if (startClip != null)
            {

                startClip();

            }

        }


        public static void EndClip()
        {
            if (endClip != null)
            {

                endClip();

                Debug.Log("<color=green>End Clip Invoked.</color>");

            }

        }


        public static void NextClip()
        {
            if (nextClip != null)
            {

                nextClip();

            }

        }

        public static void ResetClip()
        {
            if (reset != null)
            {

                reset();

            }

        }


        public static void NextPlaylist()
        {
            if (nextPlaylist != null)
            {

                nextPlaylist();

            }

        }

        public static void ShuffleAudio()
        {
            if (shuffleAudio != null)
            {

                shuffleAudio();

            }

        }


    }

}


