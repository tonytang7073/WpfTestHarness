using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTestHarness.model
{
    public class TimeDifference : IEquatable<TimeDifference>
    {
        private int _days;
        private int _hours;
        private int _millliseconds;
        private int _minutes;
        private int _months;
        private int _seconds;
        private int _years;
        private int _daysInMonth;
        private const int DAYS_IN_WEEK = 7;

        public TimeDifference()
        {
            this.Years = 0;
            this.Months = 0;
            this.Days = 0;
            this.Hours = 0;
            this.Minutes = 0;
            this.Seconds = 0;
            this.Milliseconds = 0;
            this.DaysInMonth = 0;
        }

        public TimeDifference(int years, int months, int days, int hours, int minutes, int seconds, int milliseconds, int daysInMonth)
        {
            this.Years = years;
            this.Months = months;
            this.Days = days;
            this.Hours = hours;
            this.Minutes = minutes;
            this.Seconds = seconds;
            this.Milliseconds = milliseconds;
            this.DaysInMonth = daysInMonth;
        }

        public static bool operator ==(TimeDifference left, TimeDifference right)
        {
            bool status = false;

            if (left.Years == right.Years && left.Months == right.Months && left.Days == right.Days && left.Hours == right.Hours && left.Minutes == right.Minutes && left.Seconds == right.Seconds && left.Milliseconds == right.Milliseconds)
                status = true;

            return status;
        }

        public static bool operator !=(TimeDifference left, TimeDifference right)
        {
            bool status = false;

            if (left.Years != right.Years || left.Months != right.Months || left.Days != right.Days || left.Hours != right.Hours || left.Minutes != right.Minutes || left.Seconds != right.Seconds || left.Milliseconds != right.Milliseconds)
                status = true;

            return status;
        }

        public bool Equals(TimeDifference other)
        {
            if (Object.ReferenceEquals(null, other))
                return false;

            if (Object.ReferenceEquals(this, other))
                return true;

            return this.Years.Equals(other.Years) && this.Months.Equals(other.Months) && this.Days.Equals(other.Days) && this.Hours.Equals(other.Hours) && this.Minutes.Equals(other.Minutes) && this.Seconds.Equals(other.Seconds) && this.Milliseconds.Equals(other.Milliseconds);
        }

        public override bool Equals(object obj)
        {
            if (Object.ReferenceEquals(null, obj))
                return false;

            if (Object.ReferenceEquals(this, obj))
                return true;

            return obj.GetType() == GetType() && Equals((TimeDifference)obj);
        }

        public override int GetHashCode()
        {
            int hashCode = Years.GetHashCode();
            hashCode = (hashCode * 397) ^ Months.GetHashCode();
            hashCode = (hashCode * 397) ^ Days.GetHashCode();
            hashCode = (hashCode * 397) ^ Hours.GetHashCode();
            hashCode = (hashCode * 397) ^ Minutes.GetHashCode();
            hashCode = (hashCode * 397) ^ Seconds.GetHashCode();
            hashCode = (hashCode * 397) ^ Milliseconds.GetHashCode();
            hashCode = (hashCode * 397) ^ DaysInMonth.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return Years + "y " + Months + "m " + Days + "d " + Hours + "h " + Minutes + "min " + Seconds + "s " + Milliseconds.ToString("000") + "ms";
        }

        public int Hours
        {
            get
            {
                return this._hours;
            }
            set
            {
                this._hours = value;
            }
        }

        public int Milliseconds
        {
            get
            {
                return this._millliseconds;
            }
            set
            {
                this._millliseconds = value;
            }
        }

        public int Minutes
        {
            get
            {
                return this._minutes;
            }
            set
            {
                this._minutes = value;
            }
        }

        public int Months
        {
            get
            {
                return this._months;
            }
            set
            {
                this._months = value;
            }
        }

        public int Days
        {
            get
            {
                return this._days;
            }
            set
            {
                this._days = value;
            }
        }

        public int DaysInMonth
        {
            get
            {
                return this._daysInMonth;
            }
            set
            {
                this._daysInMonth = value;
            }
        }

        public int Weeks
        {
            get
            {
                return (Days / DAYS_IN_WEEK);
            }
        }

        public int DaysRemainder
        {
            get
            {
                return Days % DAYS_IN_WEEK;
            }
        }

        public int Seconds
        {
            get
            {
                return this._seconds;
            }
            set
            {
                this._seconds = value;
            }
        }

        public int Years
        {
            get
            {
                return this._years;
            }
            set
            {
                this._years = value;
            }
        }
    }
}
