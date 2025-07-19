using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace Kalend
{
    public class EndScrubberSlide : MonoBehaviour, IPointerUpHandler
    {
        public AudioScrubber audioScrubber;

        public void Awake()
        {

            if (audioScrubber != null)
            {

                audioScrubber = audioScrubber.GetComponent<AudioScrubber>();
            }

        }

        public void OnPointerUp(PointerEventData eventData)
        {
            //Debug.Log("Sliding finished");
        }
    }
}
