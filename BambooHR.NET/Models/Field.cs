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
 * @name Field.cs
 * @version 2024-03-15
 * @summary BambooHR API Client
 **/


namespace BambooHR.NET.Models;

/// <summary>
/// 
/// </summary>
/// <param name="id"></param>
public class Field(string id)
{
  public string Id { get; set; } = id;
  public string? Type { get; set; }
  public string? Name { get; set; }
  public string? Alias { get; set; }

} //end public class Field
