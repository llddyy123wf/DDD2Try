using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Framework.Library
{
    public class RegexHelper
    {
        /// <summary>
        /// 前置替换, 在替换表达式之前加前置字符串
        /// </summary>
        /// <param name="source">替换源</param>
        /// <param name="pattern">替换表达式</param>
        /// <param name="replacement">前置字符串</param>
        /// <returns>替换后的字符串</returns>
        public static string ReplaceBefore(string source, string pattern, string replacement)
        {
            if (string.IsNullOrEmpty(source)) return source;
            return Regex.Replace(source, pattern, replacement + "$1");
        }

        /// <summary>
        /// 后置替换, 在替换表达式之后加后置字符串
        /// </summary>
        /// <param name="source">替换源</param>
        /// <param name="pattern">替换表达式</param>
        /// <param name="replacement">后置字符串</param>
        /// <returns>替换后的字符串</returns>
        public static string ReplaceAfter(string source, string pattern, string replacement)
        {
            if (string.IsNullOrEmpty(source)) return source;
            return Regex.Replace(source, pattern, "$1" + replacement);
        }
    }
}
