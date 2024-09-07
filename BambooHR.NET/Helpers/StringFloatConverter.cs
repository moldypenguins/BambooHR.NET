using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BambooHR.NET.Helpers;

/// <summary>
/// 
/// </summary>
public partial class StringFloatConverter : JsonConverter
{
  /// <summary>
  /// 
  /// </summary>
  public override bool CanWrite => false;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="writer"></param>
  /// <param name="value"></param>
  /// <param name="serializer"></param>
  /// <exception cref="NotImplementedException"></exception>
  public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
  {
    throw new NotImplementedException();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="objectType"></param>
  /// <param name="existingValue"></param>
  /// <param name="serializer"></param>
  /// <returns></returns>
  public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
  {
    var regex = StringFloatRegex();
    var value = reader.Value?.ToString();

    if (value == null || string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(regex.Replace(value, "").Trim()))
    {
      return null;
    }
    var success = float.TryParse(regex.Replace(value, "").Trim(), out var result);
    return success ? result : null;

  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objectType"></param>
  /// <returns></returns>
  public override bool CanConvert(Type objectType)
  {
    if (objectType == typeof(string) || objectType == typeof(float))
    {
      return true;
    }
    return false;
  }

  [GeneratedRegex("[^0-9.,]")]
  private static partial Regex StringFloatRegex();
}
