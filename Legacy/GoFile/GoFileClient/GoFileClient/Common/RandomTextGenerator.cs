using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoFileClient.Common
{
    public static class RandomTextGenerator
    {
        public enum TextType
        {
            Numeric,
            Alpha,
            UpperApha,
            LowerApha,
            NumericUpperApha,
            NumericLowerApha,
            All
        }

        private static string lowerCase = "abcdefghijklmnopqrstuvwxyz";
        private static string upperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static string numbers = "0123456789";
        private static string specials = "!@#$%^&*()_+-=}{[]|><";
        private static Random random = new Random(DateTime.Now.Millisecond);

        public static string GenerateRandomText(TextType TextType, int TextLength)
        {
            string baseText = GetBaseText(TextType);

            return GetRandomText(baseText, TextLength);

        }

        private static string GetRandomText(string baseText, int textLength)
        {
            string finalText = string.Join("", Enumerable.Repeat(baseText, textLength));

            StringBuilder result = new StringBuilder(textLength);
            for(int i = 0; i < textLength; i++)
            {
                result.Append(finalText[random.Next(finalText.Length - 1)]);
            }

            return result.ToString();
        }

        private static string GetBaseText(TextType textType)
        {
            switch (textType)
            {
                case TextType.Numeric:
                    return numbers;
                case TextType.Alpha:
                    return lowerCase + upperCase;
                case TextType.UpperApha:
                    return upperCase;
                case TextType.LowerApha:
                    return lowerCase;
                case TextType.NumericUpperApha:
                    return numbers + upperCase;
                case TextType.NumericLowerApha:
                    return numbers + lowerCase;
                case TextType.All:
                    return lowerCase + upperCase + numbers + specials;
                default:
                    return "0000";
            }
        }
    }
}
