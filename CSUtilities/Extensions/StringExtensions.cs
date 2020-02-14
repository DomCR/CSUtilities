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

            return text.Split('\n');
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
        /// <returns></returns>
        public static string ReadBetween(this string line, char start, char end)
        {
            var stack = new Stack<int>();
            bool isFirst = true;
            string group = "";

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
                            break;
                        }
                    }
                }

                //If the first open character have been found, start reading the string
                if (!isFirst)
                {
                    group += line[i];
                }
            }

            return group;
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
                if (line[i] == c)
                    break;
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
    }
}
