using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

namespace Kalend
{

    [RequireComponent(typeof(AudioSource))]
    public class PlaylistSelector : ClipSelector
    {

        public TextMeshProUGUI playlistNameDisplay;

        public GameObject playlistDisplay;

        public MusicPlaylist[] playlists;

        public bool usePlaylist;

        private bool _playlistDisplayActive = false;

        private int _playlistCount;

        private int _currentPlaylistIndex;

        private string _playlistName;

        private static string _currentPlaylistName;



        public override void Awake()
        {
           

            if (playlists != null && usePlaylist)
            {


                _playlistCount = playlists.Length;

                SetClipsFromPlaylist(0);

                SetStartingClip();

                _playlistDisplayActive = true;

            }

            if (playlistDisplay != null)
            {

                playlistDisplay.SetActive(_playlistDisplayActive);
            }


            base.Awake();



        }



        public void SetClipsFromPlaylist(int index)
        {
            _currentPlaylistIndex = index;

            clipCount = playlists[index].playListAudioClips.Length;

            audioClips = new AudioClip[clipCount];

            for (int i = 0; i < clipCount; i++)
            {
                audioClips[i] = playlists[index].playListAudioClips[i];

            }

            GetPlayListName();

        }


        public void GetPlayListName()
        {

            _currentPlaylistName = playlists[_currentPlaylistIndex].name;

            SetPlayListName();
        }


        public void SetPlayListName()
        {

            _playlistName = _currentPlaylistName;
            DisplayPlayListName();
        }



        public void DisplayPlayListName()
        {
            if (playlistNameDisplay != null)
            {
                playlistNameDisplay.text = _playlistName;
            }


        }


        public void IncrementPlaylist()
        {

            StopAudio();

            _currentPlaylistIndex = ModShift(_currentPlaylistIndex, _playlistCount, 1);

            SetClipsFromPlaylist(_currentPlaylistIndex);

            currentIndex = 0;

            SetCurrentAudioCLip();

        }


        public void DecrementPlaylist()
        {
            StopAudio();

            _currentPlaylistIndex = ModShift(_currentPlaylistIndex, _playlistCount, -1);

            SetClipsFromPlaylist(_currentPlaylistIndex);

            currentIndex = 0;

            SetCurrentAudioCLip();
        }


    }


}
