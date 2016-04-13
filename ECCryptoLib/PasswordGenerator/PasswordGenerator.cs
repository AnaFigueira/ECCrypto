using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace ECCryptoLib.PasswordGenerator
{
    public enum PasswordStrength { VeryWeak, Weak, Good, Strong, VeryStrong }

    public static class PasswordGenerator
    {
        public static Dictionary<PasswordStrength, SolidColorBrush> StrengthBrushes = new Dictionary<PasswordStrength, SolidColorBrush>()
        {
            { PasswordStrength.VeryWeak, new SolidColorBrush(Colors.Red) },
            { PasswordStrength.Weak, new SolidColorBrush(Colors.Orange) },
            { PasswordStrength.Good, new SolidColorBrush(Colors.Yellow) },
            { PasswordStrength.Strong, new SolidColorBrush(Colors.GreenYellow) },
            { PasswordStrength.VeryStrong, new SolidColorBrush(Colors.Green) }
        };

        // TODO: Acrescentar a opçao do utilizador escolher/acrescentar os simbolos especiais

        /// <summary>
        /// Password normal - 1024 hashes
        /// old passwords - 8192 hashes para guardar em segurança
        /// </summary>
        private static int MinNumberOfCharacters = 10;
        private static string LowerCaseLetters = "abcdefghijklmnopqrstuvwxyz";
        private static string UpperCaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static string Numbers = "1234567890";
        private static string SpecialChars = "*$-+?_&=!%{}/#";

        /// <summary>
        /// Generates a password with a certain size.
        /// </summary>
        /// <param name="length">Number of characters in the password.</param>
        /// <returns></returns>
        public static string GeneratePassword(int length, int numberOfNonAlphanumericCharacters)
        {
            string password = string.Empty;

            string validChars = string.Empty;
            if (numberOfNonAlphanumericCharacters > 0)
                validChars = LowerCaseLetters + UpperCaseLetters + Numbers + SpecialChars;
            else
                validChars = LowerCaseLetters + UpperCaseLetters + Numbers;

            int seed = (int)CryptographicBuffer.GenerateRandomNumber();

            Random rand = new Random(seed);

            int numSpecialChars = 0;
            int numUpperCaseLetters = 0;
            char c;

            bool stayInCycle = true;

            while (stayInCycle)
            {
                numSpecialChars = 0;

                for (int i = 0; i < length; i++)
                {
                    c = validChars[rand.Next(0, validChars.Length)];
                    password += c;
                    if (SpecialChars.Contains(c.ToString()))
                        numSpecialChars++;

                    if (UpperCaseLetters.Contains(c.ToString()))
                        numUpperCaseLetters++;

                    if (numSpecialChars == numberOfNonAlphanumericCharacters)
                        validChars = LowerCaseLetters + UpperCaseLetters + Numbers;
                }

                if ((numberOfNonAlphanumericCharacters > 0 &&
                    numSpecialChars < numberOfNonAlphanumericCharacters) || numUpperCaseLetters == 0)
                {
                    stayInCycle = true;
                    password = string.Empty;
                    numUpperCaseLetters = 0;
                    numSpecialChars = 0;
                }
                else
                    stayInCycle = false;
            }
            return password;
        }


        public static string GeneratePassword(PasswordGeneratorSettings settings)
        {
            string password = string.Empty;

            string validChars = string.Empty;
            if(settings.UseNumbers && settings.UseSpecialCharacters && settings.NumberOfCharacters < settings.NumberOfNumbers + settings.NumberOfSpecialCharacters)
            {
                if (settings.UseLowerCaseLetters)
                    validChars += LowerCaseLetters;

                if (settings.UseUpperCaseLetters)
                    validChars += UpperCaseLetters;
            }

            if (settings.UseNumbers && settings.NumberOfNumbers > 0)
                validChars += Numbers;

            if (settings.UseSpecialCharacters && settings.NumberOfSpecialCharacters > 0)
                validChars += SpecialChars;


            int seed = (int)CryptographicBuffer.GenerateRandomNumber();

            Random rand = new Random(seed);

            int numNumbers = 0;
            int numSpecialChars = 0;
            int numUpperCaseLetters = 0;
            int numLowerCaseLetters = 0;
            char c;

            bool stayInCycle = true;

            while (stayInCycle)
            {
                numNumbers = 0;
                numSpecialChars = 0;
                numUpperCaseLetters = 0;
                numLowerCaseLetters = 0;

                for (int i = 0; i < settings.NumberOfCharacters; i++)
                {
                    c = validChars[rand.Next(0, validChars.Length)];

                    password += c;

                    if (SpecialChars.Contains(c.ToString()))
                        numSpecialChars++;

                    if (UpperCaseLetters.Contains(c.ToString()))
                        numUpperCaseLetters++;

                    if (LowerCaseLetters.Contains(c.ToString()))
                        numLowerCaseLetters++;

                    if (Numbers.Contains(c.ToString()))
                        numNumbers++;

                    if(settings.UseNumbers && settings.UseSpecialCharacters && settings.NumberOfCharacters == settings.NumberOfNumbers + settings.NumberOfSpecialCharacters)
                    {
                        if (numSpecialChars == settings.NumberOfSpecialCharacters && numNumbers < settings.NumberOfNumbers)
                            validChars = Numbers; // Remove special characters 

                        else if (numSpecialChars < settings.NumberOfSpecialCharacters && numNumbers == settings.NumberOfNumbers)
                            validChars = SpecialChars; // Remove numbers 
                      
                        //else if (numSpecialChars < settings.NumberOfSpecialCharacters && numNumbers < settings.NumberOfNumbers)
                        //    validChars = SpecialChars + Numbers; // Remove numbers 

                    }
                    else
                    {

                        if (numSpecialChars == settings.NumberOfSpecialCharacters && numNumbers < settings.NumberOfNumbers)
                            validChars = LowerCaseLetters + UpperCaseLetters + Numbers; // Remove special characters 

                        else if (numSpecialChars == settings.NumberOfSpecialCharacters && numNumbers == settings.NumberOfNumbers)
                            validChars = LowerCaseLetters + UpperCaseLetters; // Remove special characters and numbers 

                        else if (numNumbers == settings.NumberOfNumbers && numSpecialChars < settings.NumberOfSpecialCharacters)
                            validChars = LowerCaseLetters + UpperCaseLetters + SpecialChars; // Remove numbers 
                    }
                }

                if(settings.UseNumbers && numNumbers < settings.NumberOfNumbers)
                {
                    stayInCycle = true;
                    password = string.Empty;
                    continue;
                }

                if (settings.UseSpecialCharacters && numSpecialChars < settings.NumberOfSpecialCharacters)
                {
                    stayInCycle = true;
                    password = string.Empty;
                    continue;
                }

                if (settings.UseLowerCaseLetters && numLowerCaseLetters == 0)
                {
                    stayInCycle = true;
                    password = string.Empty;
                    continue;
                }

                if (settings.UseUpperCaseLetters && numUpperCaseLetters == 0)
                {
                    stayInCycle = true;
                    password = string.Empty;
                    continue;
                }
               
                stayInCycle = false;
            }
            return password;
        }

        public static PasswordStrength CheckPasswordStrength(string password)
        {
            int nUpperLetters = 0;
            int nLowerLetters = 0;
            int nNumbers = 0;
            int nSpecialCharacters = 0;
            int strength = 0;

            foreach (var c in password)
            {
                if (Numbers.Contains(c.ToString()))
                {
                    nNumbers++;
                    continue;
                }
                if (UpperCaseLetters.Contains(c.ToString()))
                {
                    nUpperLetters++;
                    continue;
                }
                if (LowerCaseLetters.Contains(c.ToString()))
                {
                    nLowerLetters++;
                    continue;
                }
                if (SpecialChars.Contains(c.ToString()))
                {
                    nSpecialCharacters++;
                    continue;
                }
            }

            if (password.Length < MinNumberOfCharacters)
                return PasswordStrength.VeryWeak;
            else
                strength++;

            if (nNumbers > 0)
                strength++;

            if (nLowerLetters > 0)
                strength++;

            if (nUpperLetters > 0)
                strength++;

            if (nSpecialCharacters > 0)
                strength++;

            if (strength == 5)
                return PasswordStrength.VeryStrong;

            if (strength == 4)
                return PasswordStrength.Strong;

            if (strength == 3)
                return PasswordStrength.Good;

            if (strength == 2)
                return PasswordStrength.Weak;

            return PasswordStrength.VeryWeak;
        }
    }
}
