using System;

namespace flatsim
{
    public static class Utils
    {
        public static int digitCount(int number)
        {
            if (number == 0)
            {
                return 1;
            }

            int cnt = 0;
            while (Math.Abs(number) >= 1)
            {
                cnt++;
                number /= 10;
            }
            return cnt;
        }

        public static float shiftRight(float val, int by)
        {
            float newVal = val / (float)Math.Pow(10, by);
            return newVal;
        }

        public static float shiftLeft(float val, int by)
        {
            float newVal = val * (float)Math.Pow(10, by);
            return newVal;
        }
    }
}