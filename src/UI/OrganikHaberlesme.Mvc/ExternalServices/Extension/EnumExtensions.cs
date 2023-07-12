using System;

namespace OrganikHaberlesme.Mvc.ExternalServices.Extension
{
  
    public static class EnumExtensions
    {
        public static TEnum ToEnum<TEnum>(this string value) where TEnum : struct
        {
            if (string.IsNullOrWhiteSpace(value))
                return default(TEnum);

            return Enum.TryParse(value, true, out TEnum result) ? result : default(TEnum);

        }
    }
}
