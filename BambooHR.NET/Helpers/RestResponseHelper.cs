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
 * @name BambooHRClient.cs
 * @version 2024-03-11
 * @summary BambooHR API Client
 **/
using RestSharp;
using System.Linq;
using System.Text;

namespace BambooHR.NET.Helpers;
internal static class RestResponseHelper
{
  private static readonly string _errorMessageHeaderName = "X-BambooHR-Error-Message";

  internal static string GetBambooHrErrorMessage(this RestResponse response)
  {
    var error = response?.Headers?.FirstOrDefault(x => x.Name == _errorMessageHeaderName);
    return (error?.Value != null ? error.Value.ToString() : string.Empty);
  }



}
