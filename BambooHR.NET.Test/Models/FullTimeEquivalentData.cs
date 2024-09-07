/**
 * BambooHR.NET
 * Copyright (c) 2024 CR Development
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see https://www.gnu.org/licenses/gpl-3.0.html
 *
 * @name FullTimeEquivalentData.cs
 * @version 2024-03-14
 * @summary BambooHR API Client
 **/
using BambooHR.NET.Helpers;
using System.Text.Json.Serialization;
using BambooHR.NET.Models;
using Newtonsoft.Json;

namespace BambooHR.NET.Test.Models;

/// <summary>
/// 
/// </summary>
internal class FullTimeEquivalentData : TabularData
{
  //public int Id { get; set; }
  //public int EmployeeId { get; set; }

  [JsonPropertyName("customEffectiveDate17")]
  public DateTime HoursPerWeekDate { get; set; }

  [JsonPropertyName("customHoursperweek")]
  public double HoursPerWeek { get; set; }

  [JsonPropertyName("customFTE")]
  [Newtonsoft.Json.JsonConverter(typeof(StringFloatConverter))]
  public float FTE { get; set; }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override string ToString()
  {
    return $"ID: {Id}, EmployeeId: {EmployeeId}, Effective: {HoursPerWeekDate:yyyy-MM-dd}, Hours/Week: {HoursPerWeek}, FTE: {FTE}";
  }

}
