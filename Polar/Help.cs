using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;

namespace Polar
{
    public static class Help
    {
        /// <summary>
        /// Shuffle array - generic
        /// </summary>
        public static void Shuffle<T>(this Random rng, T[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }
    }
}
