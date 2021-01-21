using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

namespace MiniShop.Infrastructure
{
    public static class StringExtension
    {
        /// <summary>
        /// Produces optional, URL-friendly version of a title, "like-this-one". 
        /// hand-tuned for speed, reflects performance refactoring contributed
        /// by John Gietzen (user otac0n) 
        /// </summary>
        public static string URLFriendly(this string title)
        {
            if (title == null) return "";

            const int maxlen = 80;
            int len = title.Length;
            bool prevdash = false;
            var sb = new StringBuilder(len);
            char c;

            for (int i = 0; i < len; i++)
            {
                c = title[i];
                if ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9'))
                {
                    sb.Append(c);
                    prevdash = false;
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    // tricky way to convert to lowercase
                    sb.Append((char)(c | 32));
                    prevdash = false;
                }
                else if (c == ' ' || c == ',' || c == '.' || c == '/' ||
                    c == '\\' || c == '-' || c == '_' || c == '=')
                {
                    if (!prevdash && sb.Length > 0)
                    {
                        sb.Append('-');
                        prevdash = true;
                    }
                }
                else if ((int)c >= 128)
                {
                    int prevlen = sb.Length;
                    sb.Append(RemapInternationalCharToAscii(c));
                    if (prevlen != sb.Length) prevdash = false;
                }
                if (i == maxlen) break;
            }

            if (prevdash)
                return sb.ToString().Substring(0, sb.Length - 1);
            else
                return sb.ToString();
        }
        private static string RemapInternationalCharToAscii(char c)
        {
            string s = c.ToString().ToLowerInvariant();
            if ("àåáâäãåąăầắấằẩẫậặạả".Contains(s))
            {
                return "a";
            }
            else if ("èéêëęếềẹệễẻẽ".Contains(s))
            {
                return "e";
            }
            else if ("ìíîïı".Contains(s))
            {
                return "i";
            }
            else if ("òóôõöøőðởớờồốỗộọợỡở".Contains(s))
            {
                return "o";
            }
            else if ("ùúûüŭůủưứừựữử".Contains(s))
            {
                return "u";
            }
            else if ("çćčĉ".Contains(s))
            {
                return "c";
            }
            else if ("żźž".Contains(s))
            {
                return "z";
            }
            else if ("śşšŝ".Contains(s))
            {
                return "s";
            }
            else if ("ñń".Contains(s))
            {
                return "n";
            }
            else if ("ýÿỹỳ".Contains(s))
            {
                return "y";
            }
            else if ("ğĝ".Contains(s))
            {
                return "g";
            }
            else if (c == 'ř')
            {
                return "r";
            }
            else if (c == 'ł')
            {
                return "l";
            }
            else if (c == 'đ' || c == 'Đ')
            {
                return "d";
            }
            else if (c == 'ß')
            {
                return "ss";
            }
            else if (c == 'Þ')
            {
                return "th";
            }
            else if (c == 'ĥ')
            {
                return "h";
            }
            else if (c == 'ĵ')
            {
                return "j";
            }
            else
            {
                return "";
            }
        }
        public static string DecodeHtml(this string content)
        {
            StringWriter myWriter = new StringWriter();
            // Decode the encoded string.
            HttpUtility.HtmlDecode(content, myWriter);

            return myWriter.ToString();
        }
        public static string TakeWords(this string content,int take)
        {
            var ars = content.Split(' ');
            string result = "";

            for (int i = 0; i < ars.Length - 1; i++)
            {
                if(i < take)
                {
                    result += $"{ars[i]} ";
                }
                else
                {
                    break;
                }    
            }
            return result;
        }
        public static string FormatCurrencyK(this string content)
        {
            _ = decimal.TryParse(content, out decimal value);
            value /= 1000;
            return $"{value:n0}K";
        }
    }
}
