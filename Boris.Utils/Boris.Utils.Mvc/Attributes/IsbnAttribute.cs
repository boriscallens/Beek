using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Boris.Utils.Strings;
namespace Boris.Utils.Mvc.Attributes
{
    public class IsbnAttribute: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            var isbn = (string) value;
            isbn = isbn.Trim().ToUpperInvariant();
            isbn = isbn.Remove("ISBN");
            isbn = isbn.Remove(new List<char> {' ', '-'});

            var length = isbn.Length;
            if (length != 13 && length != 10)
            {
                return false;
            }

            int[] digits = new int[length];
            for (int i = 0; i < length; i++)
            {
                if (isbn[i].Equals('X'))
                {
                    digits[i] = 10;
                }
                else
                {
                    try
                    {
                        digits[i] = isbn[i] - '0';
                    }
                    catch
                    {
                        return false;
                    }    
                }
            }
            int[] baseInts;
            int mod;
            if (length == 10)
            {
                // ISBN 10: CheckDigit = ( [a b c d e f g h i] * [1 2 3 4 5 6 7 8 9] ) mod 11
                baseInts = new[] {1, 2, 3, 4, 5, 6, 7, 8, 9};
                int sum = Enumerable.Range(0, baseInts.Length).Sum(i => digits[i] * baseInts[i]);
                mod = sum % 11;
                return mod == digits[9];
            }
            else
            {
                // ISBN 11: CheckDigit = ( [a b c d e f g h i j k l] * [1 3 1 3 1 3 1 3 1 3 1 3] ) mod 10
                baseInts = new[] { 1, 3, 1, 3, 1, 3, 1, 3, 1, 3, 1, 3 };
                int sum = Enumerable.Range(0, baseInts.Length).Sum(i => digits[i] * baseInts[i]);
                mod = 10 - (sum % 10);
                if (mod == 10)
                    mod = 0;
            }
            return mod == digits[digits.Length-1];
        }
    }
}