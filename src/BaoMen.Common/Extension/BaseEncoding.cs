﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BaoMen.Common.Extension
{
    /// <summary>
    /// Class representing a Base32 number.  Based on Douglas Crockford"s Base32: http://www.crockford.com/wrmg/base32.html
    /// </summary>
    public struct Base32
    {
        /// <summary>
        /// Base32 containing the maximum supported value for this type
        /// </summary>
        public static readonly Base32 MaxValue = new Base32(long.MaxValue);

        /// <summary>
        /// Base32 containing the minimum supported value for this type
        /// </summary>
        public static readonly Base32 MinValue = new Base32(long.MinValue + 1);

        private static readonly char[] chars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'V', 'W', 'X', 'Y', 'Z' };

        private long numericValue;

        private static readonly IDictionary<char, byte> charDict;

        static Base32()
        {
            charDict = new Dictionary<char, byte>();
            for (byte i = 0; i < chars.Length; i++)
                charDict.Add(chars[i], i);
        }

        /// <summary>
        /// Instantiate a Base32 number from a long value
        /// </summary>
        /// <param name="NumericValue">The long value to give to the Base32 number</param>
        public Base32(long NumericValue)
        {
            numericValue = 0; //required by the struct.
            this.NumericValue = NumericValue;
        }


        /// <summary>
        /// Instantiate a Base32 number from a Base32 string
        /// </summary>
        /// <param name="Value">The value to give to the Base32 number</param>
        public Base32(string Value)
        {
            numericValue = 0; //required by the struct.
            this.Value = Value;
        }


        /// <summary>
        /// Get or set the value of the type using a base-10 long integer
        /// </summary>
        public long NumericValue
        {
            get
            {
                return numericValue;
            }
            set
            {
                //Make sure value is between allowed ranges
                if (value <= long.MinValue || value > long.MaxValue)
                {
                    throw new ArgumentOutOfRangeException(value.ToString());
                }

                numericValue = value;
            }
        }


        /// <summary>
        /// Get or set the value of the type using a Base32 string
        /// </summary>
        public string Value
        {
            get
            {
                return ToBase32(numericValue);
            }
            set
            {
                try
                {
                    numericValue = ToNumber(value);
                }
                catch
                {
                    //Catch potential errors
                    throw new ArgumentOutOfRangeException(value.ToString());
                }
            }
        }


        /// <summary>
        /// Static method to convert a Base32 string to a long integer (base-10)
        /// </summary>
        /// <param name="base32Value">The number to convert from</param>
        /// <returns>The long integer</returns>
        public static long ToNumber(string base32Value)
        {
            // Make sure we have passed something
            if (base32Value == "")
            {
                throw new ArgumentOutOfRangeException(base32Value);
            }

            // Make sure the number is in upper case:
            StringBuilder sb = new StringBuilder(base32Value.ToUpper());

            // Account for negative values:
            bool isNegative = false;
            if (base32Value[0] == '-')
            {
                sb.Remove(0, 1);
                isNegative = true;
            }

            // Remove any other "-" and *~$=U
            foreach (var chr in new string[] { "-", "*", "~", "$", "=", "U" })
                sb.Replace(chr, "");

            // Convert confusing characters to standard format
            sb.Replace('O', '0'); // Capital O to zero
            sb.Replace('I', '1'); // Capital I to one
            sb.Replace('L', '1'); // Capital L to one

            // Done modifying the input, let's write it back
            base32Value = sb.ToString();

            //Loop through our string and calculate its value
            try
            {
                //Keep a running total of the value
                long returnValue = Base32DigitToNumber(base32Value[base32Value.Length - 1]);

                //Loop through the character in the string (right to left) and add
                //up increasing powers as we go.
                for (int i = 1; i < base32Value.Length; i++)
                {
                    returnValue += ((long)Math.Pow(32, i) * Base32DigitToNumber(base32Value[base32Value.Length - (i + 1)]));
                }

                //Do negative correction if required:
                return returnValue * (isNegative ? -1 : 1);
            }
            catch
            {
                //If something goes wrong, this is not a valid number
                throw new ArgumentOutOfRangeException(base32Value);
            }
        }


        /// <summary>
        /// Public static method to convert a long integer (base-10) to a Base32 number
        /// </summary>
        /// <param name="NumericValue">The base-10 long integer</param>
        /// <returns>A Base32 representation</returns>
        public static string ToBase32(long NumericValue)
        {
            try
            {
                //Handle negative values:
                if (NumericValue < 0)
                {
                    return string.Concat("-", PositiveNumberToBase32(Math.Abs(NumericValue)));
                }
                else
                {
                    return PositiveNumberToBase32(NumericValue);
                }
            }
            catch
            {
                throw new ArgumentOutOfRangeException(NumericValue.ToString());
            }
        }


        private static string PositiveNumberToBase32(long NumericValue)
        {
            //This is a clever recursively called function that builds
            //the base-32 string representation of the long base-10 value
            if (NumericValue < 32)
            {
                //The get out clause; fires when we reach a number less than 
                //32 - this means we can add the last digit.
                return NumberToBase32Digit((byte)NumericValue).ToString();
            }
            else
            {
                //Add digits from left to right in powers of 32 
                //(recursive)
                return string.Concat(PositiveNumberToBase32(NumericValue / 32), NumberToBase32Digit((byte)(NumericValue % 32)).ToString());
            }
        }


        private static byte Base32DigitToNumber(char base32Digit)
        {
            return charDict[base32Digit];
        }


        private static char NumberToBase32Digit(byte numericValue)
        {
            // Converts a number to it's base-32 value.
            // Only works for numbers <= 31.
            if (numericValue > 31)
                throw new ArgumentOutOfRangeException(numericValue.ToString());

            return chars[numericValue];
        }

        /// <summary>
        /// Operator overload
        /// </summary>
        /// <param name="LHS"></param>
        /// <param name="RHS"></param>
        /// <returns></returns>
        public static bool operator >(Base32 LHS, Base32 RHS)
        {
            return LHS.numericValue > RHS.numericValue;
        }


        /// <summary>
        /// Operator overload
        /// </summary>
        /// <param name="LHS"></param>
        /// <param name="RHS"></param>
        /// <returns></returns>
        public static bool operator <(Base32 LHS, Base32 RHS)
        {
            return LHS.numericValue < RHS.numericValue;
        }


        /// <summary>
        /// Operator overload
        /// </summary>
        /// <param name="LHS"></param>
        /// <param name="RHS"></param>
        /// <returns></returns>
        public static bool operator >=(Base32 LHS, Base32 RHS)
        {
            return LHS.numericValue >= RHS.numericValue;
        }


        /// <summary>
        /// Operator overload
        /// </summary>
        /// <param name="LHS"></param>
        /// <param name="RHS"></param>
        /// <returns></returns>
        public static bool operator <=(Base32 LHS, Base32 RHS)
        {
            return LHS.numericValue <= RHS.numericValue;
        }


        /// <summary>
        /// Operator overload
        /// </summary>
        /// <param name="LHS"></param>
        /// <param name="RHS"></param>
        /// <returns></returns>
        public static bool operator ==(Base32 LHS, Base32 RHS)
        {
            return LHS.numericValue == RHS.numericValue;
        }


        /// <summary>
        /// Operator overload
        /// </summary>
        /// <param name="LHS"></param>
        /// <param name="RHS"></param>
        /// <returns></returns>
        public static bool operator !=(Base32 LHS, Base32 RHS)
        {
            return !(LHS == RHS);
        }


        /// <summary>
        /// Operator overload
        /// </summary>
        /// <param name="LHS"></param>
        /// <param name="RHS"></param>
        /// <returns></returns>
        public static Base32 operator +(Base32 LHS, Base32 RHS)
        {
            return new Base32(LHS.numericValue + RHS.numericValue);
        }


        /// <summary>
        /// Operator overload
        /// </summary>
        /// <param name="LHS"></param>
        /// <param name="RHS"></param>
        /// <returns></returns>
        public static Base32 operator -(Base32 LHS, Base32 RHS)
        {
            return new Base32(LHS.numericValue - RHS.numericValue);
        }


        /// <summary>
        /// Operator overload
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static Base32 operator ++(Base32 Value)
        {
            return new Base32(Value.numericValue++);
        }


        /// <summary>
        /// Operator overload
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static Base32 operator --(Base32 Value)
        {
            return new Base32(Value.numericValue--);
        }


        /// <summary>
        /// Operator overload
        /// </summary>
        /// <param name="LHS"></param>
        /// <param name="RHS"></param>
        /// <returns></returns>
        public static Base32 operator *(Base32 LHS, Base32 RHS)
        {
            return new Base32(LHS.numericValue * RHS.numericValue);
        }


        /// <summary>
        /// Operator overload
        /// </summary>
        /// <param name="LHS"></param>
        /// <param name="RHS"></param>
        /// <returns></returns>
        public static Base32 operator /(Base32 LHS, Base32 RHS)
        {
            return new Base32(LHS.numericValue / RHS.numericValue);
        }


        /// <summary>
        /// Operator overload
        /// </summary>
        /// <param name="LHS"></param>
        /// <param name="RHS"></param>
        /// <returns></returns>
        public static Base32 operator %(Base32 LHS, Base32 RHS)
        {
            return new Base32(LHS.numericValue % RHS.numericValue);
        }


        /// <summary>
        /// Converts type Base32 to a base-10 long
        /// </summary>
        /// <param name="Value">The Base32 object</param>
        /// <returns>The base-10 long integer</returns>
        public static implicit operator long(Base32 Value)
        {
            return Value.numericValue;
        }


        /// <summary>
        /// Converts type Base32 to a base-10 integer
        /// </summary>
        /// <param name="Value">The Base32 object</param>
        /// <returns>The base-10 integer</returns>
        public static implicit operator int(Base32 Value)
        {
            try
            {
                return (int)Value.numericValue;
            }
            catch
            {
                throw new OverflowException("Overflow: Value too large to return as an integer");
            }
        }


        /// <summary>
        /// Converts type Base32 to a base-10 short
        /// </summary>
        /// <param name="Value">The Base32 object</param>
        /// <returns>The base-10 short</returns>
        public static implicit operator short(Base32 Value)
        {
            try
            {
                return (short)Value.numericValue;
            }
            catch
            {
                throw new OverflowException("Overflow: Value too large to return as a short");
            }
        }


        /// <summary>
        /// Converts a long (base-10) to a Base32 type
        /// </summary>
        /// <param name="Value">The long to convert</param>
        /// <returns>The Base32 object</returns>
        public static implicit operator Base32(long Value)
        {
            return new Base32(Value);
        }


        /// <summary>
        /// Converts type Base32 to a string; must be explicit, since
        /// Base32 > string is dangerous!
        /// </summary>
        /// <param name="Value">The Base32 type</param>
        /// <returns>The string representation</returns>
        public static explicit operator string(Base32 Value)
        {
            return Value.Value;
        }


        /// <summary>
        /// Converts a string to a Base32
        /// </summary>
        /// <param name="Value">The string (must be a Base32 string)</param>
        /// <returns>A Base32 type</returns>
        public static implicit operator Base32(string Value)
        {
            return new Base32(Value);
        }

        /// <summary>
        /// Returns a string representation of the Base32 number
        /// </summary>
        /// <returns>A string representation</returns>
        public override string ToString()
        {
            return Base32.ToBase32(numericValue);
        }


        /// <summary>
        /// A unique value representing the value of the number
        /// </summary>
        /// <returns>The unique number</returns>
        public override int GetHashCode()
        {
            return numericValue.GetHashCode();
        }


        /// <summary>
        /// Determines if an object has the same value as the instance
        /// </summary>
        /// <param name="obj">The object to compare</param>
        /// <returns>True if the values are the same</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Base32))
            {
                return false;
            }
            else
            {
                return this == (Base32)obj;
            }
        }

        /// <summary>
        /// Returns a string representation padding the leading edge with
        /// zeros if necessary to make up the number of characters
        /// </summary>
        /// <param name="MinimumDigits">The minimum number of digits that the string must contain</param>
        /// <returns>The padded string representation</returns>
        public string ToString(int MinimumDigits)
        {
            string base32Value = Base32.ToBase32(numericValue);

            if (base32Value.Length >= MinimumDigits)
            {
                return base32Value;
            }
            else
            {
                string padding = new string('0', (MinimumDigits - base32Value.Length));
                return string.Format("{0}{1}", padding, base32Value);
            }
        }

    }

    /// <summary>
    /// Class representing a Base36 number
    /// </summary>
    public struct Base36
    {
        #region Constants (and pseudo-constants)

        /// <summary>
        /// Base36 containing the maximum supported value for this type
        /// </summary>
        public static readonly Base36 MaxValue = new Base36(long.MaxValue);
        /// <summary>
        /// Base36 containing the minimum supported value for this type
        /// </summary>
        public static readonly Base36 MinValue = new Base36(long.MinValue + 1);

        #endregion

        #region Fields

        private long numericValue;

        #endregion

        #region Constructor

        /// <summary>
        /// Instantiate a Base36 number from a long value
        /// </summary>
        /// <param name="NumericValue">The long value to give to the Base36 number</param>
        public Base36(long NumericValue)
        {
            numericValue = 0; //required by the struct.
            this.NumericValue = NumericValue;
        }


        /// <summary>
        /// Instantiate a Base36 number from a Base36 string
        /// </summary>
        /// <param name="Value">The value to give to the Base36 number</param>
        public Base36(string Value)
        {
            numericValue = 0; //required by the struct.
            this.Value = Value;
        }


        #endregion

        #region Properties

        /// <summary>
        /// Get or set the value of the type using a base-10 long integer
        /// </summary>
        public long NumericValue
        {
            get
            {
                return numericValue;
            }
            set
            {
                //Make sure value is between allowed ranges
                if (value <= long.MinValue || value > long.MaxValue)
                {
                    throw new ArgumentOutOfRangeException(value.ToString());
                }

                numericValue = value;
            }
        }


        /// <summary>
        /// Get or set the value of the type using a Base36 string
        /// </summary>
        public string Value
        {
            get
            {
                return Base36.NumberToBase36(numericValue);
            }
            set
            {
                try
                {
                    numericValue = Base36.Base36ToNumber(value);
                }
                catch
                {
                    //Catch potential errors
                    throw new ArgumentOutOfRangeException(value.ToString());
                }
            }
        }


        #endregion

        #region Public Static Methods

        /// <summary>
        /// Static method to convert a Base36 string to a long integer (base-10)
        /// </summary>
        /// <param name="Base36Value">The number to convert from</param>
        /// <returns>The long integer</returns>
        public static long Base36ToNumber(string Base36Value)
        {
            //Make sure we have passed something
            if (Base36Value == "")
            {
                throw new ArgumentOutOfRangeException(Base36Value);
            }

            //Make sure the number is in upper case:
            Base36Value = Base36Value.ToUpper();

            //Account for negative values:
            bool isNegative = false;

            if (Base36Value[0] == '-')
            {
                Base36Value = Base36Value.Substring(1);
                isNegative = true;
            }

            //Loop through our string and calculate its value
            try
            {
                //Keep a running total of the value
                long returnValue = Base36DigitToNumber(Base36Value[Base36Value.Length - 1]);

                //Loop through the character in the string (right to left) and add
                //up increasing powers as we go.
                for (int i = 1; i < Base36Value.Length; i++)
                {
                    returnValue += ((long)Math.Pow(36, i) * Base36DigitToNumber(Base36Value[Base36Value.Length - (i + 1)]));
                }

                //Do negative correction if required:
                if (isNegative)
                {
                    return returnValue * -1;
                }
                else
                {
                    return returnValue;
                }
            }
            catch
            {
                //If something goes wrong, this is not a valid number
                throw new ArgumentOutOfRangeException(Base36Value);
            }
        }


        /// <summary>
        /// Public static method to convert a long integer (base-10) to a Base36 number
        /// </summary>
        /// <param name="NumericValue">The base-10 long integer</param>
        /// <returns>A Base36 representation</returns>
        public static string NumberToBase36(long NumericValue)
        {
            try
            {
                //Handle negative values:
                if (NumericValue < 0)
                {
                    return string.Concat("-", PositiveNumberToBase36(Math.Abs(NumericValue)));
                }
                else
                {
                    return PositiveNumberToBase36(NumericValue);
                }
            }
            catch
            {
                throw new ArgumentOutOfRangeException(NumericValue.ToString());
            }
        }


        #endregion

        #region Private Static Methods

        private static string PositiveNumberToBase36(long NumericValue)
        {
            //This is a clever recursively called function that builds
            //the base-36 string representation of the long base-10 value
            if (NumericValue < 36)
            {
                //The get out clause; fires when we reach a number less than 
                //36 - this means we can add the last digit.
                return NumberToBase36Digit((byte)NumericValue).ToString();
            }
            else
            {
                //Add digits from left to right in powers of 36 
                //(recursive)
                return string.Concat(PositiveNumberToBase36(NumericValue / 36), NumberToBase36Digit((byte)(NumericValue % 36)).ToString());
            }
        }


        private static byte Base36DigitToNumber(char Base36Digit)
        {
            //Converts one base-36 digit to it's base-10 value
            if (!char.IsLetterOrDigit(Base36Digit))
            {
                throw new ArgumentOutOfRangeException(Base36Digit.ToString());
            }

            if (char.IsDigit(Base36Digit))
            {
                //Handles 0 - 9
                return byte.Parse(Base36Digit.ToString());
            }
            else
            {
                //Handles A - Z
                return (byte)((int)Base36Digit - 55);
            }
        }


        private static char NumberToBase36Digit(byte NumericValue)
        {
            //Converts a number to it's base-36 value.
            //Only works for numbers <= 35.
            if (NumericValue > 35)
            {
                throw new ArgumentOutOfRangeException(NumericValue.ToString());
            }

            //Numbers:
            if (NumericValue <= 9)
            {
                return NumericValue.ToString()[0];
            }
            else
            {
                //Note that A is code 65, and in this
                //scheme, A = 10.
                return (char)(NumericValue + 55);
            }
        }


        #endregion

        #region Operator Overloads

        /// <summary>
        /// Operator overload
        /// </summary>
        /// <param name="LHS"></param>
        /// <param name="RHS"></param>
        /// <returns></returns>
        public static bool operator >(Base36 LHS, Base36 RHS)
        {
            return LHS.numericValue > RHS.numericValue;
        }


        /// <summary>
        /// Operator overload
        /// </summary>
        /// <param name="LHS"></param>
        /// <param name="RHS"></param>
        /// <returns></returns>
        public static bool operator <(Base36 LHS, Base36 RHS)
        {
            return LHS.numericValue < RHS.numericValue;
        }


        /// <summary>
        /// Operator overload
        /// </summary>
        /// <param name="LHS"></param>
        /// <param name="RHS"></param>
        /// <returns></returns>
        public static bool operator >=(Base36 LHS, Base36 RHS)
        {
            return LHS.numericValue >= RHS.numericValue;
        }


        /// <summary>
        /// Operator overload
        /// </summary>
        /// <param name="LHS"></param>
        /// <param name="RHS"></param>
        /// <returns></returns>
        public static bool operator <=(Base36 LHS, Base36 RHS)
        {
            return LHS.numericValue <= RHS.numericValue;
        }


        /// <summary>
        /// Operator overload
        /// </summary>
        /// <param name="LHS"></param>
        /// <param name="RHS"></param>
        /// <returns></returns>
        public static bool operator ==(Base36 LHS, Base36 RHS)
        {
            return LHS.numericValue == RHS.numericValue;
        }


        /// <summary>
        /// Operator overload
        /// </summary>
        /// <param name="LHS"></param>
        /// <param name="RHS"></param>
        /// <returns></returns>
        public static bool operator !=(Base36 LHS, Base36 RHS)
        {
            return !(LHS == RHS);
        }


        /// <summary>
        /// Operator overload
        /// </summary>
        /// <param name="LHS"></param>
        /// <param name="RHS"></param>
        /// <returns></returns>
        public static Base36 operator +(Base36 LHS, Base36 RHS)
        {
            return new Base36(LHS.numericValue + RHS.numericValue);
        }


        /// <summary>
        /// Operator overload
        /// </summary>
        /// <param name="LHS"></param>
        /// <param name="RHS"></param>
        /// <returns></returns>
        public static Base36 operator -(Base36 LHS, Base36 RHS)
        {
            return new Base36(LHS.numericValue - RHS.numericValue);
        }


        /// <summary>
        /// Operator overload
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static Base36 operator ++(Base36 Value)
        {
            return new Base36(Value.numericValue++);
        }


        /// <summary>
        /// Operator overload
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static Base36 operator --(Base36 Value)
        {
            return new Base36(Value.numericValue--);
        }


        /// <summary>
        /// Operator overload
        /// </summary>
        /// <param name="LHS"></param>
        /// <param name="RHS"></param>
        /// <returns></returns>
        public static Base36 operator *(Base36 LHS, Base36 RHS)
        {
            return new Base36(LHS.numericValue * RHS.numericValue);
        }


        /// <summary>
        /// Operator overload
        /// </summary>
        /// <param name="LHS"></param>
        /// <param name="RHS"></param>
        /// <returns></returns>
        public static Base36 operator /(Base36 LHS, Base36 RHS)
        {
            return new Base36(LHS.numericValue / RHS.numericValue);
        }


        /// <summary>
        /// Operator overload
        /// </summary>
        /// <param name="LHS"></param>
        /// <param name="RHS"></param>
        /// <returns></returns>
        public static Base36 operator %(Base36 LHS, Base36 RHS)
        {
            return new Base36(LHS.numericValue % RHS.numericValue);
        }


        /// <summary>
        /// Converts type Base36 to a base-10 long
        /// </summary>
        /// <param name="Value">The Base36 object</param>
        /// <returns>The base-10 long integer</returns>
        public static implicit operator long(Base36 Value)
        {
            return Value.numericValue;
        }


        /// <summary>
        /// Converts type Base36 to a base-10 integer
        /// </summary>
        /// <param name="Value">The Base36 object</param>
        /// <returns>The base-10 integer</returns>
        public static implicit operator int(Base36 Value)
        {
            try
            {
                return (int)Value.numericValue;
            }
            catch
            {
                throw new OverflowException("Overflow: Value too large to return as an integer");
            }
        }


        /// <summary>
        /// Converts type Base36 to a base-10 short
        /// </summary>
        /// <param name="Value">The Base36 object</param>
        /// <returns>The base-10 short</returns>
        public static implicit operator short(Base36 Value)
        {
            try
            {
                return (short)Value.numericValue;
            }
            catch
            {
                throw new OverflowException("Overflow: Value too large to return as a short");
            }
        }


        /// <summary>
        /// Converts a long (base-10) to a Base36 type
        /// </summary>
        /// <param name="Value">The long to convert</param>
        /// <returns>The Base36 object</returns>
        public static implicit operator Base36(long Value)
        {
            return new Base36(Value);
        }


        /// <summary>
        /// Converts type Base36 to a string; must be explicit, since
        /// Base36 > string is dangerous!
        /// </summary>
        /// <param name="Value">The Base36 type</param>
        /// <returns>The string representation</returns>
        public static explicit operator string(Base36 Value)
        {
            return Value.Value;
        }


        /// <summary>
        /// Converts a string to a Base36
        /// </summary>
        /// <param name="Value">The string (must be a Base36 string)</param>
        /// <returns>A Base36 type</returns>
        public static implicit operator Base36(string Value)
        {
            return new Base36(Value);
        }


        #endregion

        #region Public Override Methods

        /// <summary>
        /// Returns a string representation of the Base36 number
        /// </summary>
        /// <returns>A string representation</returns>
        public override string ToString()
        {
            return Base36.NumberToBase36(numericValue);
        }


        /// <summary>
        /// A unique value representing the value of the number
        /// </summary>
        /// <returns>The unique number</returns>
        public override int GetHashCode()
        {
            return numericValue.GetHashCode();
        }


        /// <summary>
        /// Determines if an object has the same value as the instance
        /// </summary>
        /// <param name="obj">The object to compare</param>
        /// <returns>True if the values are the same</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Base36))
            {
                return false;
            }
            else
            {
                return this == (Base36)obj;
            }
        }


        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a string representation padding the leading edge with
        /// zeros if necessary to make up the number of characters
        /// </summary>
        /// <param name="MinimumDigits">The minimum number of digits that the string must contain</param>
        /// <returns>The padded string representation</returns>
        public string ToString(int MinimumDigits)
        {
            string base36Value = Base36.NumberToBase36(numericValue);

            if (base36Value.Length >= MinimumDigits)
            {
                return base36Value;
            }
            else
            {
                string padding = new string('0', (MinimumDigits - base36Value.Length));
                return string.Format("{0}{1}", padding, base36Value);
            }
        }


        #endregion

    }

    //public class Base62
    //{
    //    private const string bst = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

    //    public string base62(long number)
    //    {
    //        string result = "";
    //        do
    //        {
    //            long a = number % 62;
    //            result = bst[(int)a] + result;
    //            number = (number - a) / 62;
    //        } while (number > 0);
    //        return result.PadLeft(4, '0');
    //    }
    //}
}
