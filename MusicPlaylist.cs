using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kalend
{
    [CreateAssetMenu(menuName = "Kalend/Music Playlist")]
    public class MusicPlaylist : ScriptableObject
    {


        public string playListName = "playlist";


        public AudioClip[] playListAudioClips;


#if UNITY_EDITOR

        public void OnValidate()
        {

            if (playListName == "playlist" || playListName == "")
            {
                playListName = this.name;

            }

        }

#endif

        //public void OnEnable()
        //{

        //    if(playListName == "playlist")
        //    {
        //        playListName = this.name;

        //    }

        //}


    }

}