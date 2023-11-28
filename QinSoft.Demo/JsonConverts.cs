using System.Text.Json;
using System.Text.Json.Serialization;

namespace QinSoft.Demo.Api
{

    public class DateTimeJsonConverter : JsonConverter<DateTime>
    {
        private string _format;
        public DateTimeJsonConverter(string format = "yyyy-MM-dd HH:mm:ss")
        {
            this._format = format;
        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if (DateTime.TryParse(reader.GetString(), out DateTime date))
                    return date;
            }
            return reader.GetDateTime();
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }

    public class NullableDateTimeJsonConverter : JsonConverter<DateTime?>
    {
        private string _format;
        public NullableDateTimeJsonConverter(string format = "yyyy-MM-dd HH:mm:ss")
        {
            this._format = format;
        }

        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if (string.IsNullOrEmpty(reader.GetString()))
                {
                    return null;
                }
                if (DateTime.TryParse(reader.GetString(), out DateTime date))
                    return date;
            }
            return reader.GetDateTime();
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value?.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }

    public class DateTimeOffsetJsonConverter : JsonConverter<DateTimeOffset>
    {
        private string _format;
        private bool _autoLocal;
        public DateTimeOffsetJsonConverter(string format = "yyyy-MM-dd HH:mm:ss", bool autoLocal = true)
        {
            this._format = format;
            this._autoLocal = autoLocal;
        }

        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if (DateTimeOffset.TryParse(reader.GetString(), out DateTimeOffset date))
                    return _autoLocal ? date.ToLocalTime() : date;
            }
            return reader.GetDateTimeOffset();
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            writer.WriteStringValue((_autoLocal ? value.ToLocalTime() : value).ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }

    public class NullableDateTimeOffsetJsonConverter : JsonConverter<DateTimeOffset?>
    {
        private string _format;
        private bool _autoLocal;
        public NullableDateTimeOffsetJsonConverter(string format = "yyyy-MM-dd HH:mm:ss", bool autoLocal = true)
        {
            this._format = format;
            this._autoLocal = autoLocal;
        }

        public override DateTimeOffset? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if (string.IsNullOrEmpty(reader.GetString()))
                {
                    return null;
                }
                if (DateTimeOffset.TryParse(reader.GetString(), out DateTimeOffset date))
                    return _autoLocal ? date.ToLocalTime() : date;
            }
            return reader.GetDateTimeOffset();
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset? value, JsonSerializerOptions options)
        {
            writer.WriteStringValue((_autoLocal ? value?.ToLocalTime() : value)?.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }

    public class TimeSpanJsonConverter : JsonConverter<TimeSpan>
    {
        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if (TimeSpan.TryParse(reader.GetString(), out TimeSpan date))
                    return date;
            }
            return default;
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }

    public class NullableTimeSpanJsonConverter : JsonConverter<TimeSpan?>
    {
        public override TimeSpan? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if (string.IsNullOrEmpty(reader.GetString()))
                {
                    return null;
                }
                if (TimeSpan.TryParse(reader.GetString(), out TimeSpan date))
                    return date;
            }
            return default;
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan? value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value?.ToString());
        }
    }
}
