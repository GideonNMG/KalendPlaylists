using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kalend
{

    public static class Utilities
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


        //Algorithms

        public static System.Random randomNumber = new System.Random();


        // Fisher Yates Shuffle Algorithm

        public static void Shuffle<T>(this IList<T> list)
        {

            int n = list.Count;

            if (n >= 1)
            {

                while (n > 1)
                {

                    n--;

                    int k = randomNumber.Next(n + 1);

                    T value = list[k];

                    list[k] = list[n];

                    list[n] = value;

                }

            }
            else
            {

                return;
            }


        }

        public static void Swap<T>(T a, T b)
        {


            T temp = a;

            a = b;

            b = temp;

        }

        // Swap items in a list 

        public static void Swap<T>(this T[] a, int i, int j)
        {
            T temp = a[i];
            a[i] = a[j];
            a[j] = temp;
        }


        //QuickSort

        public static int Partition(int[] list, int lo, int hi)
        {


            int pivot = list[lo];

            while (true)
            {
                while (list[lo] < pivot)
                {
                    lo++;
                }


                while (list[hi] > pivot)
                {

                    hi--;
                }



                if (lo < hi)
                {

                    if (list[lo] == list[hi])
                    {

                        return hi;
                    }

                    Swap(list, lo, hi);
                }

                else
                {
                    return hi;

                }

            }

        }



        public static int Partition(float[] list, int lo, int hi)
        {


            float pivot = list[lo];

            while (true)
            {
                while (list[lo] < pivot)
                {
                    lo++;
                }


                while (list[hi] > pivot)
                {

                    hi--;
                }


                if (lo < hi)
                {

                    if (list[lo] == list[hi])
                    {

                        return hi;
                    }

                    Swap(list, lo, hi);
                }

                else
                {
                    return hi;

                }

            }

        }


        public static void QuickSort(int[] a, int lo, int hi)
        {

            if (lo < hi)
            {

                int pivot = Partition(a, lo, hi);

                if (pivot > 1)
                {

                    QuickSort(a, lo, pivot - 1);


                }


                if (pivot + 1 < hi)
                {

                    QuickSort(a, pivot + 1, hi);

                }

            }



        }

        public static void QuickSort(float[] a, int lo, int hi)
        {

            if (lo < hi)
            {

                int pivot = Partition(a, lo, hi);

                if (pivot > 1)
                {

                    QuickSort(a, lo, pivot - 1);


                }


                if (pivot + 1 < hi)
                {

                    QuickSort(a, pivot + 1, hi);

                }

            }

        }



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
                                t =  t * t * t * (t * (t * 6 - 15) + 10);
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

