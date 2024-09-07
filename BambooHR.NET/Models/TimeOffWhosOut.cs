﻿/**
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
 * @name TimeOffWhosOut.cs
 * @version 2024-03-14
 * @summary BambooHR API Client
 **/

namespace BambooHR.NET.Models;

/// <summary>
/// 
/// </summary>
public class TimeOffWhosOut
{
  /// <summary>
  /// 
  /// </summary>
  public int Id { get; set; }
  /// <summary>
  /// 
  /// </summary>
  public string Type { get; set; }
  /// <summary>
  /// 
  /// </summary>
  public string EmployeeId { get; set; }
  /// <summary>
  /// 
  /// </summary>
  public string Name { get; set; }
  /// <summary>
  /// 
  /// </summary>
  public DateTime? Start { get; set; }
  /// <summary>
  /// 
  /// </summary>
  public DateTime? End { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public bool IsTimeOff
  {
    get { return Type.ToLower() == "timeoff"; }
  }

  /// <summary>
  /// 
  /// </summary>
  public bool IsHoliday
  {
    get
    { return Type.ToLower() == "holiday"; }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override string ToString()
  {
    return $"ID: {Id}, Type: {Type}, EmployeeId: {EmployeeId}, Name: {Name}, Start: {Start.Value.ToString("yyyy-MM-dd")}, End: {End.Value.ToString("yyyy-MM-dd")}";
  }

} //end public class TimeOffWhosOut
