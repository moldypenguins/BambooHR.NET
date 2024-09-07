using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BambooHR.NET.Helpers;

/// <summary>
/// 
/// </summary>
public class StringBooleanConverter : JsonConverter
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
  public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
  {
    var value = reader.Value;

    if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
    {
      return false;
    }

    string[] truthy = ["yes", "true"];
    if (truthy.Contains(value.ToString()?.ToLower()))
    {
      return true;
    }

    return false;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objectType"></param>
  /// <returns></returns>
  public override bool CanConvert(Type objectType)
  {
    if (objectType == typeof(string) || objectType == typeof(bool))
    {
      return true;
    }
    return false;
  }
}
