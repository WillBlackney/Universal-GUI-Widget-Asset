using System;
using System.Security.Cryptography;

namespace BlackneyStudios.GuiWidget
{
    public static class RandomGenerator
    {
        private static readonly RNGCryptoServiceProvider _generator = new RNGCryptoServiceProvider();
        public static int NumberBetween(int minimumValue, int maximumValue)
        {
            byte[] randomNumber = new byte[1];

            _generator.GetBytes(randomNumber);

            double asciiValueOfRandomCharacter = Convert.ToDouble(randomNumber[0]);

            // Substract 0.00000000001,to ensure "multiplier" will always be between 0.0 and .99999999999.
            // Otherwise, it's possible for it to be "1", which will cause problems in the rounding.
            double multiplier = Math.Max(0, (asciiValueOfRandomCharacter / 255d) - 0.00000000001d);

            // Add one to the range, to allow for the rounding done with Math.Floor
            int range = maximumValue - minimumValue + 1;

            double randomValueInRange = Math.Floor(multiplier * range);

            return (int)(minimumValue + randomValueInRange);
        }
        public static float NumberBetween(float minimumValue, float maximumValue)
        {
            byte[] randomNumber = new byte[1];

            _generator.GetBytes(randomNumber);

            double asciiValueOfRandomCharacter = Convert.ToDouble(randomNumber[0]);

            // Substract 0.00000000001,to ensure "multiplier" will always be between 0.0 and .99999999999.
            // Otherwise, it's possible for it to be "1", which will cause problems in the rounding.
            double multiplier = Math.Max(0, (asciiValueOfRandomCharacter / 255d) - 0.00000000001d);

            // Add one to the range, to allow for the rounding done with Math.Floor
            float range = maximumValue - minimumValue + 1;

            double randomValueInRange = Math.Floor(multiplier * range);

            return (float)(minimumValue + randomValueInRange);
        }
    }
}



