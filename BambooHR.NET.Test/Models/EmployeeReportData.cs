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
 * @name EmployeeReportData.cs
 * @version 2024-03-16
 * @summary BambooHR API Client
 **/

namespace BambooHR.NET.Models;

using System.ComponentModel;
using System.Text.Json.Serialization;
using BambooHR.NET.Helpers;
using Newtonsoft.Json;

/// <summary>
/// 
/// </summary>
public class EmployeeReportData : ReportData
{
  //public int Id { get; set; }
  public string? Status { get; set; }
  public int? EmployeeNumber { get; set; }
  public string? FirstName { get; set; }
  public string? LastName { get; set; }

  public string? EmploymentHistoryStatus { get; set; }
  public DateTime? EmployeeStatusDate { get; set; }
  public DateTime? HireDate { get; set; }

  public string? Location { get; set; }
  public string? Division { get; set; }
  public string? Department { get; set; }
  public string? JobTitle { get; set; }

  public string? PaySchedule { get; set; }
  public string? PayType { get; set; }
  public string? PayGroup { get; set; }
  public string? PaidPer { get; set; }

  [JsonProperty("customFTE")]
  [JsonPropertyName("4666")]
  [Newtonsoft.Json.JsonConverter(typeof(StringFloatConverter))]
  public float? FTE { get; set; }

  [JsonProperty("91")]
  [JsonPropertyName("91")]
  public string? ReportingTo { get; set; }

  [JsonProperty("customEffectiveDate17")]
  [JsonPropertyName("4664")]
  public DateTime? HoursPerWeekDate { get; set; }

  [JsonProperty("customHoursperweek")]
  [JsonPropertyName("4665")]
  public string? HoursPerWeek { get; set; }




  public override string ToString()
  {
    return $"ID: {Id}, Status: {Status}, EmployeeNumber: {EmployeeNumber}, Name: {FirstName} {LastName}, Status: {EmploymentHistoryStatus} ({EmployeeStatusDate:yyyy-MM-dd})" + 
      $", Hired: {HireDate:yyyy-MM-dd}, FTE: {FTE}, ReportingTo: {ReportingTo}, HoursPerWeekDate: {HoursPerWeekDate:yyyyMMdd}, HoursPerWeek: {HoursPerWeek}";

  } //end public override string ToString()

} //end public class EmployeeReportData
