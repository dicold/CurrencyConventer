using System.Runtime.CompilerServices;
using System.Text;

namespace CurrencyConventerServer.Controllers
{
    public class CurrencyConventer
    {
        static readonly Dictionary<int, string> OneDigitNumber = new()
            {
                {1, "one"},
                {2, "two"},
                {3, "three"},
                {4, "four"},
                {5, "five"},
                {6, "six"},
                {7, "seven"},
                {8, "eight"},
                {9, "nine"}
            };

        static readonly Dictionary<int, string> TeenNumber = new()
        {
                {0, "ten"},
                {1, "eleven"},
                {2, "twelve"},
                {3, "thirteen"},
                {4, "fourteen"},
                {5, "fifteen"},
                {6, "sixteen"},
                {7, "seventeen"},
                {8, "eighteen"},
                {9, "nineteen"}
            };

        static readonly Dictionary<int, string> SecondDigitNumber = new()
            {
                {2, "twenty"},
                {3, "thirty"},
                {4, "fourty"},
                {5, "fifty"},
                {6, "sixty"},
                {7, "seventy"},
                {8, "eighty"},
                {9, "ninety"}
            };

        static readonly string[] currencies = { "", " thousand", " million" };

        public static string NumberIntoString(decimal number)
        {
            // integer part of the number (dollars)
            int integerPart = (int)Math.Truncate(number);

            // fraction part of the number (cents)
            int fractionPart = (int)((number - integerPart) * 100);

            var result = new StringBuilder();

            string dollars = integerPart == 1 ? "dollar" : "dollars";

            if (integerPart == 0)
                result.Append("zero ");
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    int tmp = integerPart % 1000;
                    if (tmp != 0)
                        result.Insert(0, $"{ThreeDigitsNumberConverter(tmp)}{currencies[i]} ");
                    integerPart /= 1000;
                }
            }
            result.Append(dollars);

            if (fractionPart != 0)
            {
                result.Append(" and ");
                result.Append(ThreeDigitsNumberConverter(fractionPart));
                result.Append(" cent");
                if (fractionPart > 1)
                    result.Append('s');
            }
            
            return result.ToString();
        }

        private static string ThreeDigitsNumberConverter(int number)
        {
            int ones = number % 10;
            int tens = (number / 10) % 10;
            int hundreds = number / 100;

            // if number is 1 digit only
            if (tens == 0 && hundreds == 0)
                return OneDigitNumber[ones];

            var result = new StringBuilder();

            // the number is 3 digits
            if (hundreds > 0)
                result.Append($"{OneDigitNumber[hundreds]} hundred ");

            // last two digits is teen numbers: 11, 12, ... 19
            if (tens == 1)
            {
                result.Append(TeenNumber[ones]);
                return result.ToString();
            }

            // process the last two digits of the number
            if (tens != 0)
            {
                result.Append(SecondDigitNumber[tens]);

                if (ones != 0)
                {
                    result.Append('-');
                    result.Append(OneDigitNumber[ones]);
                }
            }

            return result.ToString();
        }
    }
}
