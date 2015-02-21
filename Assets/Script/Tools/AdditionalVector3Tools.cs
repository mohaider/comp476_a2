using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Assets.Script.Tools
{
    class AdditionalVector3Tools
    {
        /// <summary>
        /// limits the magnitude of vector 3. if the magnitude(input) > max, then normalize the vector3
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Vector3 Limit(Vector3 input, float max)
        {
            float mag = Mathf.Sqrt((input.x * input.x) + (input.y * input.y))+ (input.z *input.z);
            if (mag > max)
            {
                
                return Vector3.Normalize(input)*max;

            }
            else return input;
        }


        /// <summary>
        /// will map a value according to a specified range.
        /// </summary>
        /// <param name="val"></param>
        /// <param name="inMin"></param>
        /// <param name="inMax"></param>
        /// <param name="outMin"></param>
        /// <param name="outMax"></param>
        /// <returns></returns>

        public static float map(float val, float inMin, float inMax, float outMin, float outMax)
        {

            return outMin + (outMax - outMin) * ((val - inMin) / (inMax - inMin));
        }

        public static float mapAngleToRange(float angle)
        {

            float remainder = (int)angle - angle;
            float output = 0f;
            if (Mathf.Sign(angle) < 0)
                output = (float)((int)(angle - 180) % 360) + 180 + remainder;
            else
                output = (float)((int)(angle + 180) % 360) - 180 + remainder;

            return output;

        }
    }
}
