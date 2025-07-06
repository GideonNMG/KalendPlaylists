using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace Kalend
{

    public class SkyboxChanger : MonoBehaviour
    {

        public bool changeManually;

        public bool changeAutomatically;

        public bool randomChanges;
      
        [SerializeField]
        [Range(0.5f, 30)]
        private float _periodInSeconds;

        private float _period;

        public Material[] skyboxMaterial;

        public static Material currentSkybox;

        private float _time;

        private float _modularTime;

        private int _skyboxMaterialLength;

        private int _skyboxIndex;

        private int _currentSkyboxIndex;



        // Start is called before the first frame update

        private void Awake()
        {

            if (skyboxMaterial[0] != null)
            {
                RenderSettings.skybox = skyboxMaterial[0];

                currentSkybox = skyboxMaterial[0];

            }

        }

        void Start()
        {


            if (skyboxMaterial != null)
            {

                _skyboxMaterialLength = Mathf.Max(1, skyboxMaterial.Length);
            }


            SetPeriod();

        }

        public void ChangeIndex(int delta)
        {
            int n = _currentSkyboxIndex + _skyboxMaterialLength + delta;


            _currentSkyboxIndex = Mathf.Abs(n % _skyboxMaterialLength);


        }

        public void ChangeSkybox(int i)
        {
            //Debug.Log( "<color=blue> * * Change Skybox Called. * * </color>"); 

            RenderSettings.skybox = skyboxMaterial[i];

            currentSkybox = skyboxMaterial[i];

        }

        private void SetPeriod()
        {
            _period = Mathf.Max(0.5f, _periodInSeconds);

        }

        public void IncrementSkybox()
        {
            ChangeIndex(1);

            ChangeSkybox(_currentSkyboxIndex);
        }

        public void DecrementSkybox()
        {
            ChangeIndex(-1);
            ChangeSkybox(_currentSkyboxIndex);
        }

        public void ChangeRandomly()
        {
            if (_skyboxMaterialLength < 2)
            {

                Debug.LogWarning("Only one Skybox available. Cannot change randonly.");
                return;
            }
            else

            {
                int n = Random.Range(1, _skyboxMaterialLength);

                ChangeIndex(n);
            }

        }


         // Update is called once per frame
         void Update()
         {

              SetPeriod();

              _time += Time.deltaTime;

              _modularTime = (_time % (_skyboxMaterialLength * _period));


              if (skyboxMaterial != null && changeAutomatically)
              {

                  if (_modularTime > _period)
                  {

                      if (randomChanges)
                      {
                          ChangeRandomly();

                      }

                      else
                      {
                          IncrementSkybox();

                      }
                      _skyboxIndex += 1;

                      _currentSkyboxIndex = (_skyboxIndex % _skyboxMaterialLength);

                      _time = 0;

                      ChangeSkybox(_currentSkyboxIndex);

                  }

              }

         }


    }

}

