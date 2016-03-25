using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Text.RegularExpressions;

namespace Framework.Library.Web
{
    public class HttpHelper
    {
        /// <summary>
        /// 获得http请求数据
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="method">请求方式</param>
        /// <param name="postData">发送数据</param>
        /// <param name="encoding">编码</param>
        /// <param name="timeout">超时值</param>
        /// <returns></returns>
        public static string GetRequestData(string url, string postData, Encoding encoding = null, string method = "GET", int timeout = 3600)
        {
            if (!url.StartsWith("http://") && !url.StartsWith("https://")) url = "http://" + url;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Accept = "text/html, application/xhtml+xml, */*,zh-CN";
            request.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";
            request.KeepAlive = true;
            request.AllowAutoRedirect = true;
            request.Timeout = timeout;
            request.Method = string.IsNullOrEmpty(method) ? "GET" : method.ToUpper();
            request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
            request.ContentType = request.Method == "POST" ? "application/x-www-form-urlencoded" : "text/html";

            Encoding reqEncoding = encoding == null ? Encoding.Default : encoding;
            try
            {
                if (!string.IsNullOrEmpty(postData) && request.Method == "POST")
                {
                    byte[] buffer = reqEncoding.GetBytes(postData);
                    request.ContentLength = buffer.Length;
                    request.GetRequestStream().Write(buffer, 0, buffer.Length);
                }

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response == null || response.StatusCode != HttpStatusCode.OK) return string.Empty;
                    if (encoding == null)
                    {
                        string cType = response.ContentType.ToLower();
                        Match charSetMatch = Regex.Match(cType, "(?<=charset=)([^<]*)*", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string webCharSet = charSetMatch.ToString();
                        encoding = System.Text.Encoding.GetEncoding(webCharSet);
                    }

                    StreamReader reader = null;
                    if (response.ContentEncoding != null && response.ContentEncoding.Equals("gzip", StringComparison.InvariantCultureIgnoreCase))
                    {
                        using (reader = new StreamReader(new GZipStream(response.GetResponseStream(), CompressionMode.Decompress), encoding))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                    else
                    {
                        using (reader = new StreamReader(response.GetResponseStream(), encoding))
                        {
                            try
                            {
                                return reader.ReadToEnd();
                            }
                            catch
                            {
                                return null;
                            }
                        }
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获得http请求状态
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="method">请求方式</param>
        /// <param name="postData">发送数据</param>
        /// <param name="encoding">编码</param>
        /// <param name="timeout">超时值</param>
        /// <returns></returns>
        public static HttpStatusCode GetRequestStatus(string url, string postData, Encoding encoding = null, string method = "GET", int timeout = 3600)
        {
            if (!url.StartsWith("http://") && !url.StartsWith("https://")) url = "http://" + url;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Accept = "text/html, application/xhtml+xml, */*,zh-CN";
            request.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";
            request.KeepAlive = true;
            request.AllowAutoRedirect = true;
            request.Timeout = timeout;
            request.Method = string.IsNullOrEmpty(method) ? "GET" : method.ToUpper();
            request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
            request.ContentType = request.Method == "POST" ? "application/x-www-form-urlencoded" : "text/html";

            Encoding reqEncoding = encoding == null ? Encoding.Default : encoding;
            try
            {
                if (!string.IsNullOrEmpty(postData) && request.Method == "POST")
                {
                    byte[] buffer = reqEncoding.GetBytes(postData);
                    request.ContentLength = buffer.Length;
                    request.GetRequestStream().Write(buffer, 0, buffer.Length);
                }

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response == null) return HttpStatusCode.BadRequest;
                    return response.StatusCode;
                }
            }
            catch
            {
                return HttpStatusCode.BadRequest;
            }
        }

        /// <summary>
        /// 移除Html标签
        /// </summary>
        /// <param name="html"></param>
        /// <returns>返回移除了Html标签的字符串</returns>
        public static string RemoveHtml(string html)
        {
            if (string.IsNullOrEmpty(html)) return html;

            // 删除脚本和嵌入式CSS   
            html = Regex.Replace(html, @"<script[^>]*?>.*?</script>", string.Empty, RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"<style[^>]*?>.*?</style>", string.Empty, RegexOptions.IgnoreCase);

            // 删除HTML   
            var regex = new Regex("<.+?>", RegexOptions.IgnoreCase);
            html = regex.Replace(html, string.Empty);
            html = Regex.Replace(html, @"<(.[^>]*)>", string.Empty, RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"([\r\n])[\s]+", string.Empty, RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"-->", string.Empty, RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"<!--.*", string.Empty, RegexOptions.IgnoreCase);

            //Html解码
            html = Regex.Replace(html, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&#(\d+);", string.Empty, RegexOptions.IgnoreCase);

            return html.Replace("<", string.Empty).Replace(">", string.Empty).Replace("\r\n", string.Empty);
        }

        /// <summary>
        /// 移除Html标签, 仅移除尖括号类
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string RemoveHtmlTag(string html)
        {
            if (string.IsNullOrEmpty(html)) return html;
            return Regex.Replace(html, "<[^>]*>", string.Empty);
        }   
    }
}
