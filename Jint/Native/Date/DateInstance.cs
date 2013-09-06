﻿using System;
using Jint.Native.Object;

namespace Jint.Native.Date
{
    public class DateInstance : ObjectInstance, IPrimitiveType
    {
        // Maximum allowed value to prevent DateTime overflow
        private static readonly double Max = (DateTime.MaxValue - new DateTime(170, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;

        // Minimum allowed value to prevent DateTime overflow
        private static readonly double Min = -(new DateTime(170, 1, 1, 0, 0, 0, DateTimeKind.Utc) - DateTime.MinValue).TotalMilliseconds;

        public DateInstance(Engine engine)
            : base(engine)
        {
        }

        public override string Class
        {
            get
            {
                return "Date";
            }
        }

        TypeCode IPrimitiveType.TypeCode
        {
            get { return TypeCode.DateTime; }
        }

        object IPrimitiveType.PrimitiveValue
        {
            get { return PrimitiveValue; }
        }

        public DateTime ToDateTime()
        {
            if (double.IsNaN(PrimitiveValue) || PrimitiveValue > Max || PrimitiveValue < Min)
            {
                return DateTime.MinValue;
            }
            else
            {
                return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(PrimitiveValue);
            }
        }

        public double PrimitiveValue { get; set; }
    }
}