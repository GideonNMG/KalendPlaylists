using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

namespace Kalend
{
    public static class FadeMixerGroup
    {
        public static IEnumerator FadeGroup(AudioMixer audioMixer, string groupVolume, float duration, float targetVolume)
        {
            float elapsedTime = 0;
            float currentVol;
            audioMixer.GetFloat(groupVolume, out currentVol);
            currentVol = Mathf.Pow(10, currentVol / 20);
            float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float newVol = Mathf.Lerp(currentVol, targetValue, elapsedTime / duration);
                audioMixer.SetFloat(groupVolume, Mathf.Log10(newVol) * 20);
                yield return null;
            }
            yield break;
        }
    }

}
