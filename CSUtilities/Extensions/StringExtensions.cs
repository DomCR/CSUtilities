using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSUtilities.Extensions
{
    /// <summary>
    /// String utility extensions.
    /// </summary>
    internal static class StringExtensions
    {
        /// <summary>
        /// Return an array with all the lines.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string[] GetLines(this string str)
        {
            //Guard
            if (str == null)
                return null;

            str = str.Replace("\r\n", "\n");

            string[] lines = str.Split('\n');

            if (string.IsNullOrEmpty(lines.Last()))
            {
                //Delete the last empty line
                lines = lines.Take(lines.Length - 1).ToArray();
            }

            return lines;
        }
        public static bool IsNumeric(this string s)
        {
            return double.TryParse(s, out _);
        }
        /// <summary>
        /// Gets a string and returns an array of bytes.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] ToByteArray(this string str)
        {
            return Enumerable.Range(0, str.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(str.Substring(x, 2), 16))
                .ToArray();
        }
        /// <summary>
        /// Returns the first string between 2 characters.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <exception cref="ArgumentException">The line is not closed by the 2 characters.</exception>
        /// <returns></returns>
        public static string ReadBetween(this string str, char start, char end)
        {
            #region old code
            //Stack<int> stack = new Stack<int>();
            //bool isFirst = true;
            //string group = "";

            //for (int i = 0; i < line.Length; i++)
            //{
            //    if (line[i] == start)
            //    {
            //        //Save the index of the open character
            //        stack.Push(i);

            //        //Save the index of the first open char
            //        if (isFirst)
            //        {
            //            isFirst = false;
            //            continue;
            //        }
            //    }
            //    else if (line[i] == end)
            //    {
            //        //Check if the sequence contains an open
            //        if (!isFirst)
            //        {
            //            stack.Pop();

            //            //Closing character found
            //            if (!stack.Any())
            //            {
            //                return group;
            //            }
            //        }
            //    }

            //    //If the first open character have been found, start reading the string
            //    if (!isFirst)
            //    {
            //        group += line[i];
            //    }
            //} 
            #endregion

            if (str.TryReadBetween(start, end, out string group))
            {
                return group;
            }
            else
            {
                throw new ArgumentException("Closing character not found, this is an open line.");
            }
        }
        /// <summary>
        /// Reads between 2 characters, but returns a value even if the group is not closed.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        public static bool TryReadBetween(this string s, char start, char end, out string group)
        {
            Stack<int> stack = new Stack<int>();
            bool isFirst = true;
            group = "";

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == start)
                {
                    //Save the index of the open character
                    stack.Push(i);

                    //Save the index of the first open char
                    if (isFirst)
                    {
                        isFirst = false;
                        continue;
                    }
                }
                else if (s[i] == end)
                {
                    //Check if the sequence contains an open
                    if (!isFirst)
                    {
                        stack.Pop();

                        //Closing character found
                        if (!stack.Any())
                        {
                            return true;
                        }
                    }
                }

                //If the first open character have been found, start reading the string
                if (!isFirst)
                {
                    group += s[i];
                }
            }

            return false;
        }
        /// <summary>
        /// Reads a string until it finds a character.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="c">Character to find.</param>
        /// <returns></returns>
        public static string ReadUntil(this string str, char c)
        {
            string value = "";

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == c)
                    break;
                else
                    value += str[i];
            }

            return value;
        }
        /// <summary>
        /// Reads a string until it finds a character.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="c"></param>
        /// <param name="residual">The last part of the string.</param>
        /// <returns></returns>
        public static string ReadUntil(this string str, char c, out string residual)
        {
            string value = "";
            residual = "";

            bool reading = true;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == c && reading)
                {
                    reading = false;
                    continue;
                }
                else if (reading)
                    value += str[i];
                else
                    residual += str[i];
            }

            return value;

        }
        /// <summary>
        /// Remove all the first whitespaces in a string.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveStartWhitespaces(this string str)
        {
            while (str.StartsWith(" "))
            {
                str = str.Remove(0, 1);
            }

            return str;
        }
        /// <summary>
        /// Find the first character equal to the parameter.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="characters"></param>
        /// <returns></returns>
        public static char? FirstEqual(this string str, IEnumerable<char> characters)
        {
            char? token = null;
            int? pos = null;

            foreach (char item in characters)
            {
                //string tmp = m_buffer.Substring(m_currIndex);
                int curr = str.IndexOf(item);

                //Get the next token in the buffer
                if ((pos == null || curr < pos) && curr > -1 /*&& curr >= m_currIndex*/)
                {
                    //pos = m_buffer.IndexOf(item);
                    pos = curr;
                    token = item;
                }
            }

            return token;
        }
        /// <summary>
        /// Split an string by spaces and substrings between " ".
        /// </summary>
        /// <param name="str"></param>
        /// <param name="keepCollons"></param>
        /// <returns></returns>
        public static string[] ToArgs(this string str, bool keepCollons = false)
        {
            List<string> args = new List<string>();
            string word = "";
            bool isReading = false;

            foreach (char c in str)
            {
                //Open or close the string
                if (c == '"')
                {
                    isReading = !isReading;

                    if (keepCollons)
                        word += c;

                    continue;
                }

                //Ignore the whitespaces outside the strings
                if (c == ' ' && !isReading)
                {
                    //Avoid empty words (multiple spaces)
                    if (String.IsNullOrEmpty(word))
                        continue;

                    args.Add(word);
                    word = "";
                }
                else
                {
                    word += c;
                }
            }
            //Add the last word
            if (!String.IsNullOrEmpty(word))
                args.Add(word);

            return args.ToArray();
        }
    }
}
