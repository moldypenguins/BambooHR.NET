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
 * @name Filter.cs
 * @version 2024-03-15
 * @summary BambooHR API Client
 **/

namespace BambooHR.NET.Models;

/// <summary>
/// 
/// </summary>
public class Filter
{
  public string Name { get; set; }
  public string Value { get; set; }

  public Filter(string name, string value)
  {
    Name = name;
    Value = value;

  } //end public Filter

} //end public class Filter
