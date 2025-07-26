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

        [Header("Playlist Options")]

        public bool usePlaylist;


        public TextMeshProUGUI playlistNameDisplay;

        public TextMeshProUGUI previousTrackNameDisplay;

        public TextMeshProUGUI nextTrackNameDisplay;

        public GameObject playlistDisplay;

        public MusicPlaylist[] playlists;

      
        private bool _playlistDisplayActive = false;

        private int _playlistCount;

        private int _currentPlaylistIndex;

        private string _playlistName;

        private string[] trackName;

        private static string _currentPlaylistName;

        private bool _listCompleted;


        public override void Awake()
        {


            firstPlay = true;
           


            if (playlists != null && usePlaylist)
            {


                _playlistCount = playlists.Length;

                SetClipsFromPlaylist(0);

                if (shuffle)
                {
                    ShuffleAudioClips();
                }

                SetStartingClip();

                _playlistDisplayActive = true;

            }

            if (playlistDisplay != null&& usePlaylist)
            {

                playlistDisplay.SetActive(_playlistDisplayActive);

                AudioEvents.nextClip += DisplayTrackNames;
                AudioEvents.shuffleAudio += SetTrackNames;
                AudioEvents.shuffleAudio += ClearTrackNames;

            }

            base.Awake();

        }

        public override void Start()
        {


            base.Start();

            if (usePlaylist)
            {
                ClearTrackNames();
            }

            else
            {
                
                playlistDisplay.SetActive(false);

                nextTrackNameDisplay.text = "";
                previousTrackNameDisplay.text = "";

            }
     
        }



        public void OnDisable()
        {

            if (usePlaylist)
            {
                AudioEvents.nextClip -= DisplayTrackNames;
                AudioEvents.shuffleAudio -= SetTrackNames;
                AudioEvents.shuffleAudio -= ClearTrackNames;
            }
           
        }


        public void ClearTrackNames()
        {

                nextTrackNameDisplay.text = trackName[1];
                previousTrackNameDisplay.text = "";      
         
        }


        public void DisplayTrackNames()
        {

            if(currentIndex == clipCount - 1)
            {

                _listCompleted = true;
            }



               nextTrackNameDisplay.text = (currentIndex < clipCount-1 ) ? trackName[currentIndex + 1] : trackName[0];

            if (_listCompleted)
            {


                previousTrackNameDisplay.text = (currentIndex > 0) ? trackName[currentIndex - 1] : trackName[clipCount - 1];
            }

            else
            {
                previousTrackNameDisplay.text = (currentIndex > 0) ? trackName[currentIndex - 1] : "";
            }

        }


        public void SetTrackNames()
        {
            trackName = new string[clipCount];
            for (int i = 0; i < clipCount; i++)
            {
               
                trackName[i] = audioClips[i].name;
            }

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

            SetTrackNames();

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

            _listCompleted = false;

            _currentPlaylistIndex = ModShift(_currentPlaylistIndex, _playlistCount, 1);

            SetClipsFromPlaylist(_currentPlaylistIndex);

            if (shuffle)
            {
                ShuffleAudioClips();
            }


            currentIndex = 0;

            SetCurrentAudioCLip();

            ClearTrackNames();
        }


        public void DecrementPlaylist()
        {
            StopAudio();

            _listCompleted = false;

            _currentPlaylistIndex = ModShift(_currentPlaylistIndex, _playlistCount, -1);

            SetClipsFromPlaylist(_currentPlaylistIndex);

            if (shuffle)
            {
                ShuffleAudioClips();
            }

            currentIndex = 0;

            SetCurrentAudioCLip();

            ClearTrackNames();

        }


    }


}
