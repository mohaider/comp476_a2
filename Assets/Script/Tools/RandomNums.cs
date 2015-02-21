using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.Tools
{
    class RandomNums
    {
        public static float NextGaussianFloat()
        {
            float u, V, S;

            do
            {
                u = 2.0f * UnityEngine.Random.value - 1.0f;
                V = 2.0f * UnityEngine.Random.value - 1.0f;
                S = u * u + V * V;
            }
            while (S >= 1.0f);

            float fac = (float)Math.Sqrt(-2.0 * Math.Log((double)S) / (double)S);

            return u * fac;
        }


        public static float NormalizedRandom(float minVal, float maxVal)
        {
            float val = 0f;
            float mean = (minVal + maxVal) / 2;
            float sigma = (maxVal - mean) / 3;
            bool notInRange = true;
            while (notInRange)
            {
                val = NextRandom(mean, sigma);
                if (val < minVal || val > maxVal)
                    continue;
                else
                    notInRange = false;
            }
            return val;


        }
        public static float NextRandom(float mean, float sigma)
        {
            return (sigma * RandomNums.NextGaussianFloat() + mean);

        }

        public static float RandomBinomial()
        {
            return UnityEngine.Random.Range(0f, 1f) - UnityEngine.Random.Range(0f, 1f);
        }

    }
}
