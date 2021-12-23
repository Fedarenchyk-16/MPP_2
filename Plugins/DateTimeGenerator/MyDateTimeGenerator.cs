using System;
using FakerLibrary.Generator;

namespace DateTimeGenerator
{
    public class MyDateTimeGenerator : TypeGenerator<DateTime>
    {
        protected override DateTime Generation(Random random)
        {
            int year = random.Next(DateTime.MinValue.Year, DateTime.MaxValue.Year + 1);
            int month = random.Next(DateTime.MinValue.Month, DateTime.MaxValue.Month + 1);

            return new DateTime(year, month, random.Next(1, DateTime.DaysInMonth(year, month)), 
                                random.Next(DateTime.MinValue.Hour, DateTime.MaxValue.Hour), 
                                random.Next(DateTime.MinValue.Minute, DateTime.MaxValue.Minute), 
                                random.Next(DateTime.MinValue.Second, DateTime.MaxValue.Second), 
                                random.Next(DateTime.MinValue.Millisecond, DateTime.MaxValue.Millisecond));
        }
    }
}
