using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace liquidador_web.Extra
{
    public static class PasswordGenerator
    {
        /// <summary>
        /// Generates a random password based on the rules passed in the parameters
        /// </summary>
        /// <param name="includeLowercase">Bool to say if lowercase are required</param>
        /// <param name="includeUppercase">Bool to say if uppercase are required</param>
        /// <param name="includeNumeric">Bool to say if numerics are required</param>
        /// <param name="includeSpecial">Bool to say if special characters are required</param>
        /// <param name="includeSpaces">Bool to say if spaces are required</param>
        /// <param name="lengthOfPassword">Length of password required. Should be between 8 and 128</param>
        /// <returns>Password Generated</returns>
        public static string GeneratePassword(bool includeLowercase = true, bool includeUppercase = true, bool includeNumeric = true, bool includeSpecial = true, bool includeSpaces = false, int lengthOfPassword = 8)
        {
            const int MAXIMUM_IDENTICAL_CONSECUTIVE_CHARS = 2;
            const string LOWERCASE_CHARACTERS = "abcdefghijklmnopqrstuvwxyz";
            const string UPPERCASE_CHARACTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string NUMERIC_CHARACTERS = "0123456789";
            const string SPECIAL_CHARACTERS = @"!#$%&*@\";
            const string SPACE_CHARACTER = " ";
            const int PASSWORD_LENGTH_MIN = 8;
            const int PASSWORD_LENGTH_MAX = 128;

            if (lengthOfPassword < PASSWORD_LENGTH_MIN || lengthOfPassword > PASSWORD_LENGTH_MAX)
                throw new ArgumentOutOfRangeException("El tamaño de la contraseña debe estar entre 8 y 128 caracteres.");

            string characterSet = "";

            if (includeLowercase)
                characterSet += LOWERCASE_CHARACTERS;

            if (includeUppercase)
                characterSet += UPPERCASE_CHARACTERS;

            if (includeNumeric)
                characterSet += NUMERIC_CHARACTERS;

            if (includeSpecial)
                characterSet += SPECIAL_CHARACTERS;

            if (includeSpaces)
                characterSet += SPACE_CHARACTER;

            char[] password = new char[lengthOfPassword];

            for (int characterPosition = 0; characterPosition < lengthOfPassword; characterPosition++)
            {
                password[characterPosition] = characterSet[RandomNumber(0, characterSet.Length)];

                bool moreThanTwoIdenticalInARow =
                    characterPosition > MAXIMUM_IDENTICAL_CONSECUTIVE_CHARS
                    && password[characterPosition] == password[characterPosition - 1]
                    && password[characterPosition - 1] == password[characterPosition - 2];

                if (moreThanTwoIdenticalInARow)
                    characterPosition--;
            }

            return string.Join(null, password);
        }

        /// <summary>
        /// Checks if the password created is valid
        /// </summary>
        /// <param name="includeLowercase">Bool to say if lowercase are required</param>
        /// <param name="includeUppercase">Bool to say if uppercase are required</param>
        /// <param name="includeNumeric">Bool to say if numerics are required</param>
        /// <param name="includeSpecial">Bool to say if special characters are required</param>
        /// <param name="includeSpaces">Bool to say if spaces are required</param>
        /// <param name="password">Generated password</param>
        /// <returns>True or False to say if the password is valid or not</returns>
        public static bool PasswordIsValid(string password, bool includeLowercase = true, bool includeUppercase = true, bool includeNumeric = true, bool includeSpecial = true, bool includeSpaces = false)
        {
            const string REGEX_LOWERCASE = @"[a-z]";
            const string REGEX_UPPERCASE = @"[A-Z]";
            const string REGEX_NUMERIC = @"[\d]";
            const string REGEX_SPECIAL = @"([!#$%&*@\\])+";
            const string REGEX_SPACE = @"([ ])+";

            bool lowerCaseIsValid = !includeLowercase || (includeLowercase && Regex.IsMatch(password, REGEX_LOWERCASE));
            bool upperCaseIsValid = !includeUppercase || (includeUppercase && Regex.IsMatch(password, REGEX_UPPERCASE));
            bool numericIsValid = !includeNumeric || (includeNumeric && Regex.IsMatch(password, REGEX_NUMERIC));
            bool symbolsAreValid = !includeSpecial || (includeSpecial && Regex.IsMatch(password, REGEX_SPECIAL));
            bool spacesAreValid = !includeSpaces || (includeSpaces && Regex.IsMatch(password, REGEX_SPACE));

            return lowerCaseIsValid && upperCaseIsValid && numericIsValid && symbolsAreValid && spacesAreValid;
        }

        public static bool IsValidEmail(string email, string[] domains)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                //return Regex.IsMatch(email, @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" + @"ramajudicial.gov.co", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
                foreach(string domain in domains)
                    if(Regex.IsMatch(email, @"^[a-z0-9\.\-_]+@([a-z0-9\-_]+\.)*" + domain + "$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
                        return true;

                return false;
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private static int RandomNumber(int min, int max)
        {
            if (min > max) throw new ArgumentOutOfRangeException(nameof(min));
            if (min == max) return min;

            using (var rng = new RNGCryptoServiceProvider())
            {
                var data = new byte[4];
                rng.GetBytes(data);

                int generatedValue = Math.Abs(BitConverter.ToInt32(data, startIndex: 0));

                int diff = max - min;
                int mod = generatedValue % diff;
                int normalizedNumber = min + mod;

                return normalizedNumber;
            }
        }
    }
}
