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
 * @name TimeOffRequest.cs
 * @version 2024-03-14
 * @summary BambooHR API Client
 **/

namespace BambooHR.NET.Models;

/// <summary>
/// TimeOffRequest
/// </summary>
public class TimeOffRequest
{
  public int Id { get; set; }
  public int EmployeeId { get; set; }
  public string Name { get; set; }
  public TimeOffStatus Status { get; set; }
  public DateTime Start { get; set; }
  public DateTime End { get; set; }
  public DateTime Created { get; set; }
  public TimeOffType Type { get; set; }
  public TimeOffAmount Amount { get; set; }
  public Actions Actions { get; set; }
  public Dictionary<DateTime, double>? Dates { get; set; }
  public TimeOffNotes Notes { get; set; }
  public override string ToString()
  {
    return $"ID: {Id}, Type: {Type}, EmployeeId: {EmployeeId}, Name: {Name}, Status: {Status} " + 
      $"Start: {Start.ToString("yyyy-MM-dd")}, End: {End.ToString("yyyy-MM-dd")}, Created: {Created.ToString("yyyy-MM-dd")} " + 
      $"Type: {Type}, Amount: {Amount}";

  } //end public override string ToString

} //end public class TimeOffRequest


/// <summary>
/// TimeOffStatus
/// </summary>
public class TimeOffStatus
{
  public DateTime LastChanged { get; set; }

  public int LastChangedByUserId { get; set; }

  public string Status { get; set; }

  public override string ToString()
  {
    return $"{Status}";

  } //end public override string ToString

} //end public class Status


/// <summary>
/// TimeOffType
/// </summary>
public class TimeOffType
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string Icon { get; set; }

  public override string ToString()
  {
    return $"{Name}";

  } //end public override string ToString

} //end public class TimeOffType


/// <summary>
/// TimeOffAmount
/// </summary>
public class TimeOffAmount
{
  public string Unit { get; set; }
  public double Amount { get; set; }

  public override string ToString()
  {
    return $"{Amount} {Unit}";

  } //end public override string ToString

} //end public class TimeOffAmount



/// <summary>
/// TimeOffNotes
/// </summary>
public class TimeOffNotes
{
  public string Employee { get; set; }
  public int Manager { get; set; }

} //end public class TimeOffNotes
