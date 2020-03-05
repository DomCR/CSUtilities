using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSUtilities.Extensions
{
    internal static class StringExtensions
    {
        /// <summary>
        /// Return an array with all the lines.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string[] GetLines(this string text)
        {
            //Guard
            if (text == null)
                return null;

            text = text.Replace("\r\n", "\n");

            string[] lines = text.Split('\n');

            if (string.IsNullOrEmpty(lines.Last()))
            {
                //Delete the last empty line
                lines = lines.Take(lines.Length - 1).ToArray();
            }

            return lines;
        }
        /// <summary>
        /// Gets a string and returns an array of bytes.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] ToByteArray(this string value)
        {
            return Enumerable.Range(0, value.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(value.Substring(x, 2), 16))
                .ToArray();
        }
        /// <summary>
        /// Returns the first string between 2 characters.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <exception cref="ArgumentException">The line is not closed by the 2 characters.</exception>
        /// <returns></returns>
        public static string ReadBetween(this string line, char start, char end)
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

            if(line.TryReadBetween(start, end, out string group))
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
        /// <param name="line"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        public static bool TryReadBetween(this string line, char start, char end, out string group)
        {
            Stack<int> stack = new Stack<int>();
            bool isFirst = true;
            group = "";

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == start)
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
                else if (line[i] == end)
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
                    group += line[i];
                }
            }

            return false;
        }
        /// <summary>
        /// Reads a string until it finds a character.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="c">Character to find.</param>
        /// <returns></returns>
        public static string ReadUntil(this string line, char c)
        {
            string value = "";

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == c)
                    break;
                else
                    value += line[i];
            }

            return value;
        }
        public static string ReadUntil(this string line, char c, out string rest)
        {
            string value = "";
            rest = "";

            bool reading = true;
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == c && reading)
                {
                    reading = false;
                    continue;
                }
                else if (reading)
                    value += line[i];
                else
                    rest += line[i];
            }

            return value;

        }
        public static string RemoveStartWhitespaces(this string line)
        {
            while (line.StartsWith(" "))
            {
                line = line.Remove(0, 1);
            }

            return line;
        }
        /// <summary>
        /// Find the first character equal to the parameter.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="characters"></param>
        /// <returns></returns>
        public static char? FirstEqual(this string s, IEnumerable<char> characters)
        {
            char? token = null;
            int? pos = null;

            foreach (char item in characters)
            {
                //string tmp = m_buffer.Substring(m_currIndex);
                int curr = s.IndexOf(item);

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
        /// <param name="s"></param>
        /// <param name="keepCollons"></param>
        /// <returns></returns>
        public static string[] ToArgs(this string s, bool keepCollons = false)
        {
            List<string> args = new List<string>();
            string word = "";
            bool isReading = false;

            foreach (char c in s)
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
