using System;

namespace Framework.Library
{
    /// <summary>
    /// 随机帮助类
    /// RandomGenerator.Create(length,enforce,seed)
    /// RandomGenerator.CreateNumber(length)
    /// </summary>
    public class RandomHelper
    {
        /// <summary>
        /// 根据种子生成随机字符串
        /// </summary>
        /// <param name="seed">生成字符串的种子, 默认为字母及数字[a-z,A-Z,0-9]</param>
        /// <param name="length">生成字符串的长度</param>
        /// <param name="enforce">强制随机, 适合短时间多次使用随机数</param>
        /// <returns>随机字符串</returns>
        public static string Random(int length, bool enforce = false, string seed = "")
        {
            if (length < 1) return string.Empty;
            if (string.IsNullOrEmpty(seed)) seed = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            char[] target = new char[length];
            int root = enforce ? Guid.NewGuid().GetHashCode() : DateTime.Now.Millisecond;
            Random r = new Random(root);
            for (int i = 0; i < length; i++)
            {
                target[i] = seed[r.Next(0, seed.Length - 1)];
            }
            return new string(target);
        }

        /// <summary>
        /// 随机数字
        /// </summary>
        /// <param name="length">随机数字的位数</param>
        /// <returns></returns>
        public static int RandomNumber(int length)
        {
            if (length < 1 || length > 9) throw new ArgumentException("Invalid length");

            int maxLength = Convert.ToInt32(Math.Pow(10, length));
            Random r = new Random();
            return r.Next(maxLength);
        }

        /// <summary>
        /// 返回Guid用于数据库操作，特定的时间代码可以提高检索效率
        /// </summary>
        /// <returns>COMB类型 Guid 数据</returns>
        public static Guid NewComb()
        {
            byte[] guidArray = Guid.NewGuid().ToByteArray();
            DateTime dtBase = new DateTime(1900, 1, 1);
            DateTime dtNow = DateTime.Now;
            //获取用于生成byte字符串的天数与毫秒数
            TimeSpan days = new TimeSpan(dtNow.Ticks - dtBase.Ticks);
            TimeSpan msecs = new TimeSpan(dtNow.Ticks - (new DateTime(dtNow.Year, dtNow.Month, dtNow.Day).Ticks));
            //转换成byte数组
            //注意SqlServer的时间计数只能精确到1/300秒
            byte[] daysArray = BitConverter.GetBytes(days.Days);
            byte[] msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333));

            //反转字节以符合SqlServer的排序
            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);

            //把字节复制到Guid中
            Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
            Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);
            return new Guid(guidArray);
        }
    }
}
