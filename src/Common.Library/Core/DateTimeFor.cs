using System;

namespace Framework.Library
{
    public class DateTimeFor
    {
        private DateTime begin;
        private DateTime end;

        public DateTimeFor From(DateTime begin)
        {
            this.begin = begin;
            return this;
        }
        public DateTimeFor To(DateTime end)
        {
            this.end = end;
            return this;
        }

        protected virtual void InnerAction(Action<DateTime> action, string step)
        {
            if (this.begin == DateTime.MinValue || this.end == DateTime.MinValue || this.begin > this.end) return;

            DateTime from = this.begin;
            DateTime to = this.end;

            while (from < to)
            {
                action(from);
                switch (step)
                {
                    case "d":
                        from = from.AddDays(1);
                        break;
                    case "M":
                        from = from.AddMonths(1);
                        break;
                    case "y":
                        from = from.AddYears(1);
                        break;
                    case "h":
                        from = from.AddHours(1);
                        break;
                    case "m":
                        from = from.AddMinutes(1);
                        break;
                    case "s":
                        from = from.AddSeconds(1);
                        break;
                }
            }
        }

        public void ActionByDay(Action<DateTime> action)
        {
            this.InnerAction(action, "d");
        }
        public void ActionByMonth(Action<DateTime> action)
        {
            this.InnerAction(action, "M");
        }
        public void ActionByYear(Action<DateTime> action)
        {
            this.InnerAction(action, "y");
        }
        public void ActionByHour(Action<DateTime> action)
        {
            this.InnerAction(action, "h");
        }
        public void ActionByMinute(Action<DateTime> action)
        {
            this.InnerAction(action, "m");
        }
        public void ActionBySecond(Action<DateTime> action)
        {
            this.InnerAction(action, "s");
        }

        // 返回指定格式的差值
        protected virtual double InnerDiff(string format)
        {
            if (this.begin == DateTime.MinValue || this.end == DateTime.MinValue || this.begin > this.end) return 0;

            double diff = 0d;
            switch (format)
            {
                case "y":
                    diff = (this.end.Year - this.begin.Year) * 12 + (this.end.Month - this.begin.Month) + (this.end.Day - this.begin.Day) / 31.0d * 12;
                    break;
                case "M":
                    diff = (this.end.Year - this.begin.Year) * 12 + (this.end.Month - this.begin.Month) + (this.end.Day - this.begin.Day) / 31.0d;
                    break;
                case "w":
                    diff = (this.end - this.begin).TotalDays / 7;
                    break;
                case "d":
                    diff = (this.end - this.begin).TotalDays;
                    break;
                case "h":
                    diff = (this.end - this.begin).TotalHours;
                    break;
                case "m":
                    diff = (this.end - this.begin).TotalMinutes;
                    break;
                case "s":
                    diff = (this.end - this.begin).TotalSeconds;
                    break;
                case "f":
                    diff = (this.end - this.begin).TotalMilliseconds;
                    break;
                default:
                    diff = 0;
                    break;
            }
            return diff;
        }

        public double TotalDays()
        {
            return this.InnerDiff("d");
        }
        public double TotalMonths()
        {
            return this.InnerDiff("M");
        }
        public double TotalYears()
        {
            return this.InnerDiff("y");
        }
        public double TotalWeeks()
        {
            return this.InnerDiff("w");
        }
        public double TotalHours()
        {
            return this.InnerDiff("h");
        }
        public double TotalMinutes()
        {
            return this.InnerDiff("m");
        }
        public double TotalSeconds()
        {
            return this.InnerDiff("s");
        }

        public string Display()
        {
            double result = this.TotalMinutes();
            if (result > 365 * 24 * 60) return  Math.Floor(result / 365 / 24 / 60).ToString() + "年前";
            if (result > 31 * 24 * 60) return Math.Floor(result / 31 / 24 / 60).ToString() + "月前";
            if (result > 24 * 60) return Math.Floor(result / 24 / 60).ToString() + "天前";
            if (result > 60) return Math.Floor(result / 60).ToString() + "小时前";
            if (result > 1) return result.ToString("0") + "分前";
            return "刚刚";
        }
    }
}
