using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kalend
{
    public static class LerpTypes
    {


        // Interpolation Types

        public enum InterpolationType
        {
            Linear,
            EaseOut,
            EaseIn,
            SmoothStep,
            SmootherStep,
            SmootherIn,
            LogIn,
            SmoothLog,
            LogLog,
            Exponential,

        };


        public static float LerpType(float t, InterpolationType interpolationType)

        {

            switch (interpolationType)
            {
                case InterpolationType.Linear:


                    //_interpolationTypeFormula[(int)interpolationType.Linear] = t;
                    break;

                case InterpolationType.EaseOut:
                    t = Mathf.Sin(t * Mathf.PI * 0.5f);

                    //_interpolationTypeFormula[(int)interpolationType.EaseOut] = Mathf.Sin(t * Mathf.PI * 0.5f);

                    break;

                case InterpolationType.EaseIn:
                    t = 1 - Mathf.Cos(t * Mathf.PI * 0.5f);

                    //_interpolationTypeFormula[(int)interpolationType.EaseOut] = 1 - Mathf.Cos(t * Mathf.PI * 0.5f);
                    break;


                case InterpolationType.SmoothStep:
                    t = t * t * (3 - 2 * t);
                    //_interpolationTypeFormula[(int)interpolationType.SmoothStep]= t = t * t * (3 - 2 * t);
                    break;

                case InterpolationType.SmootherStep:
                    t = t * t * t * t * ((t * 6 - 15) + 10);
                    //_interpolationTypeFormula[(int)interpolationType.SmootherStep] = t = t * t * t * (t * (t * 6 - 15) + 10);
                    break;


                case InterpolationType.SmootherIn:


                    while (t < 1 / 2)

                    {
                        t = t * t * t * (t * (t * 6 - 15) + 10);
                        t = 1 - Mathf.Cos(t * Mathf.PI * 0.5f);
                        t = Mathf.Clamp(t, 0f, 1f);


                        break;

                    }


                    t = t * t * t * (t * (t * 6 - 15) + 10);
                    break;


                case InterpolationType.LogIn:


                    t = Mathf.Clamp(Mathf.Log(1 + t), 0f, 1f);


                    break;


                case InterpolationType.SmoothLog:

                    while (t < 1 / 2f)
                    {
                        t = Mathf.Log(1 + t);
                        t = t * t * t * (t * (t * 6 - 15) + 10);
                        t = Mathf.Clamp(t, 0f, 1f);

                        break;

                    }

                    t = Mathf.Clamp(Mathf.Log(1 + t), 0f, 1f);

                    break;


                case InterpolationType.LogLog:


                    t = Mathf.Clamp(Mathf.Log(1 + (Mathf.Log(1 + t))), 0f, 1f);


                    break;

                case InterpolationType.Exponential:


                    t = Mathf.Clamp((Mathf.Exp(t) - 1f) / 1.7f, 0f, 1f);

                    break;

                default:

                    break;

            }


            float result = Mathf.Clamp(t, 0f, 1f);

            return result;

        }


    }


}
