using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace CollatzConjectureVerification
{
    internal class Program
    {
        public static Dictionary<BigInteger, HashSet<BigInteger>> verifiedNumRange = new Dictionary<BigInteger, HashSet<BigInteger>>();
        public static HashSet<BigInteger> cache = new HashSet<BigInteger>();
        public static BigInteger range = new BigInteger((int)Math.Pow(2, 15));
        public static BigInteger updateRange = range;
        public static BigInteger max = new BigInteger(0);

        static void Main(string[] args)
        {
            init();
            BigInteger currentValue = new BigInteger(1);
            while (true)
            {
                if (Verify(currentValue))
                {
                    currentValue += 1;
                }
                else
                {
                    Console.WriteLine("Rejected! The number is: " + currentValue.ToString());
                    break;
                }
                if (currentValue % updateRange == 0)
                {
                    while (verifiedNumRange[max].Count == range || verifiedNumRange[max].Count +1 == range)
                    {
                        verifiedNumRange.Remove(max);
                        verifiedNumRange.Add(max + range * 10, new HashSet<BigInteger>());
                        max += range;
                    }
                }
                if (currentValue % 100000 == 0)
                {
                    Console.WriteLine("Verified number till: " + currentValue);
                }

            }
        }

        static void init()
        {
            BigInteger cur = 0;
            for (int i = 0; i < 10; i++)
            {
                verifiedNumRange.Add(cur, new HashSet<BigInteger>());
                cur += range;
            }
            verifiedNumRange[new BigInteger(0)].Add(new BigInteger(1));
        }

        static bool Verify(BigInteger targetNum)
        {
            cache.Clear();
            cache.Add(targetNum);
            while (!CheckIfInSet(targetNum) && targetNum != 1)
            {
                if (targetNum % 2 == 0)
                {
                    targetNum /= 2;
                }
                else
                {
                    targetNum *= 3;
                    targetNum += 1;
                }
                if (!cache.Contains(targetNum))
                {
                    cache.Add(targetNum);
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool CheckIfInSet(BigInteger targetNum)
        {
            if (targetNum < max)
            {
                return true;
            }
            foreach (BigInteger key in verifiedNumRange.Keys)
            {
                if (targetNum >= key && targetNum < key + range)
                {
                    if (verifiedNumRange[key].Contains(targetNum))
                    {
                        return true;
                    }
                    else
                    {
                        verifiedNumRange[key].Add(targetNum);
                        return false;
                    }
                }
            }
            return false;
        }
    }
}
