using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Kalend
{

    public class CameraRotator : MonoBehaviour
    {
		public Slider rotationSpeedSlider;

		public bool useSlider;

		[SerializeField]

		private bool _stop;

		[SerializeField]
		[Range(-0.5f, 0.5f)]      // Negative values rotatate to the left; positive values to the right.
		private float _speed = 1;

	     /*
		[Range(-0.5f, 0.5f)] 
	    public float speed = 0.1f;
		 */

		[SerializeField]
		[Range(-1, 1)]
		private float[] _rotationVectorComponents = { 0, 1, 0 };  //Defaults to Vector3.up

		private Vector3 _rotationVector = new Vector3(0, 1, 0);


        private void SetRotationVector()
		{
			for (int i = 0; i < 3; i++)
			{

				_rotationVector[i] = _rotationVectorComponents[i];

			}

		}
	

		public void ToggleStop()
        {

			_stop = _stop ? false : true;
        }

		public void SetSpeed()
        {


			if (useSlider && rotationSpeedSlider != null)
			{

				_speed = rotationSpeedSlider.value;

			}



		}

		void Update()

		{
			SetRotationVector();

			

		}


        private void LateUpdate()
        {
			if (!_stop)
			{
				transform.Rotate(_rotationVector * (_speed) * 0.3f);
			}
		}
    }


}