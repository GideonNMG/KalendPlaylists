using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace Kalend
{
    public class SkyboxRotator : MonoBehaviour
    {
        public Material skybox;

        private Material _skybox;

        public bool useSkyboxChanger;

        public bool useRotationSlider;

        public Slider rotationSlider;

        public bool rotateSkybox;

        [Range(-10f, 10f)]
        public float rotationSpeed = 2f;

        private float _rotationSpeed = 2f;

        [SerializeField]
        [Range(-1f, 1f)]
        private float _startingSliderValue = 0.25f;

        [Range(-1f, 1f)]
        private float _sliderValue = 0f;



        private void Awake()
        {
            if (rotateSkybox)
            {
                _sliderValue = _startingSliderValue;

                if (skybox != null && !useSkyboxChanger)
                {
                    _skybox = skybox;

                }         

            }

            _rotationSpeed = rotationSpeed;
        }


        public void SetRotationSpeed()
        {

            if (useRotationSlider && rotationSlider != null)
            {

                _sliderValue = Mathf.Clamp(rotationSlider.value, -1f, 1f);

            }

        }


        public void StopRotation()
        {
            rotateSkybox = false;

            rotationSpeed = 0f;

            //Debug.Log("<color=red>Skybox Rotation Stopped.</color>");
        }


        public void StartRotation()
        {
            rotateSkybox = true;

            rotationSpeed = _rotationSpeed;
        }


        // Update is called once per frame
        void Update()
        {

               //if (useSkyboxChanger)
               // {

               //     _skybox = SkyboxChanger.currentSkybox;

               //     skybox = _skybox;
               // }
          


            if (rotateSkybox) 
            {

                _skybox.SetFloat("_Rotation", Time.time * rotationSpeed * _sliderValue);  


            }

        }


    }


}




