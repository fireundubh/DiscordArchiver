using System;
using Newtonsoft.Json;

namespace DiscordArchiver
{
    internal class DecimalJsonConverter : JsonConverter
    {
        public override bool CanRead => false;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("Unnecessary because CanRead is false. The type will skip the converter.");
        }

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(decimal) || objectType == typeof(float) || objectType == typeof(double));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteRawValue(IsWholeValue(value)
                ? JsonConvert.ToString(Convert.ToInt64(value))
                : JsonConvert.ToString(value));
        }

        private static bool IsWholeValue(object value)
        {
            if (value is decimal)
            {
                decimal decimalValue = (decimal)value;
                int precision = (decimal.GetBits(decimalValue)[3] >> 16) & 0x000000FF;
                return precision == 0;
            }

            if (!(value is float) && !(value is double)) return false;

            double doubleValue = (double)value;
            return Math.Abs(doubleValue - Math.Truncate(doubleValue)) < 0;
        }
    }
}
