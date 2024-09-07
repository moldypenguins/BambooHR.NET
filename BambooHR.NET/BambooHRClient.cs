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

using BambooHR.NET.Models;
using BambooHR.NET.Helpers;
using RestSharp;
using RestSharp.Authenticators;
using System.Net;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.ComponentModel;
using BambooHR.NET.Helpers;

namespace BambooHR.NET;

/// <summary>
/// 
/// </summary>
public class BambooHRClient
{
  /// <summary>
  /// 
  /// </summary>
  private readonly RestClient _RestClient;
  /// <summary>
  /// 
  /// </summary>
  private readonly string _DateFormat = "yyyy-MM-dd";

  private readonly string _APIBaseUri = "https://api.bamboohr.com/api/gateway.php";
  private readonly string _APIVersion = "v1";

  private readonly string _CompanySubdomain = string.Empty;



  /// <summary>
  /// 
  /// </summary>
  /// <param name="companySubdomain"></param>
  /// <param name="apiKey"></param>
  public BambooHRClient(string companySubdomain, string apiKey)
  {
    _CompanySubdomain = companySubdomain;
    _RestClient = new RestClient(new RestClientOptions(string.Join("/", [_APIBaseUri, _CompanySubdomain, _APIVersion]))
    {
      Authenticator = new HttpBasicAuthenticator(apiKey, "x"),
    });
    _RestClient.AddDefaultHeader("User-Agent", "BambooHR.NET/2.0.0");
    _RestClient.AddDefaultHeader("Content-Type", "application/json");
    _RestClient.AddDefaultHeader("Accept", "application/json");
    _RestClient.AddDefaultHeader("Encoding", "utf-8");

    

  } //end public BambooHRClient


  /// <summary>
  /// 
  /// </summary>
  /// <param name="companySubdomain"></param>
  /// <param name="apiKey"></param>
  /// <param name="dateFormat"></param>
  public BambooHRClient(string companySubdomain, string apiKey, string dateFormat) : this(companySubdomain, apiKey)
  {
    _DateFormat = dateFormat;

  } //end public BambooHRClient

  /// <summary>
  /// 
  /// </summary>
  /// <param name="companySubdomain"></param>
  /// <param name="apiKey"></param>
  /// <param name="apiUri"></param>
  /// <param name="apiVersion"></param>
  public BambooHRClient(string companySubdomain, string apiKey, string apiUri, string apiVersion) : this(companySubdomain, apiKey)
  {
    _APIBaseUri = apiUri;
    _APIVersion = apiVersion;

  } //end public BambooHRClient

  /// <summary>
  /// 
  /// </summary>
  /// <param name="companySubdomain"></param>
  /// <param name="apiKey"></param>
  /// <param name="apiUri"></param>
  /// <param name="apiVersion"></param>
  /// <param name="dateFormat"></param>
  public BambooHRClient(string companySubdomain, string apiKey, string apiUri, string apiVersion, string dateFormat) : this(apiUri, apiVersion, companySubdomain, apiKey)
  {
    _DateFormat = dateFormat;

  } //end public BambooHRClient



  /// <inheritdoc/>
  public async Task<List<TabularData>> GetTabularData<TabularData>(int employeeId, string tableName)
  {
    var api_path = $"/employees/{employeeId}/tables/{tableName}/";
    var request = new RestRequest(api_path, Method.Get);
    RestResponse<List<TabularData>> response;
    try
    {
      response = await _RestClient.ExecuteAsync<List<TabularData>>(request);
    }
    catch (Exception ex)
    {
      throw new Exception("Error executing Bamboo request to " + api_path, ex);
    }
    if (response.ErrorException != null)
    {
      throw new Exception("Error executing Bamboo request to " + api_path, response.ErrorException);
    }
    if (response.StatusCode == HttpStatusCode.OK)
    {
      if (response.Data != null)
      {
        return response.Data;
      }
      throw new Exception("Bamboo Response does not contain data.");
    }
    throw new Exception($"Bamboo Response threw error code {response.StatusCode} ({response.StatusDescription}) {response.GetBambooHrErrorMessage()} in {nameof(GetTabularData)}");
  
  } //end public async Task<List<T>> GetTabularData<T>


  /// <inheritdoc/>
  public async Task<List<TimeOffRequest>> GetTimeOffRequests(int? Id = null, int? employeeId = null, DateTime? startDate = null, DateTime? endDate = null, int[]? types = null, string[]? statuses = null)
  {
    var api_path = "/time_off/requests/";
    var request = new RestRequest(api_path, Method.Get);
    if (Id.HasValue)
    {
      request.AddParameter("id", Id.Value.ToString(), ParameterType.QueryString);
    }
    if (employeeId.HasValue)
    {
      request.AddParameter("employeeId", employeeId.Value.ToString(), ParameterType.QueryString);
    }
    if (startDate.HasValue)
    {
      request.AddParameter("start", startDate.Value.ToString(_DateFormat), ParameterType.QueryString);
    }
    if (endDate.HasValue)
    {
      request.AddParameter("end", endDate.Value.ToString(_DateFormat), ParameterType.QueryString);
    }
    RestResponse response;
    try
    {
      response = await _RestClient.ExecuteAsync(request);
    }
    catch (Exception ex)
    {
      throw new Exception(string.Format("Error executing Bamboo request to {0} for employee ID {1}", api_path, employeeId), ex);
    }
    if (response.ErrorException != null)
    {
      throw new Exception(string.Format("Error executing Bamboo request to {0} for employee ID {1}", api_path, employeeId), response.ErrorException);
    }
    if (response.Content == null || response.Content.Replace("[]", string.Empty) == string.Empty)
    {
      throw new Exception(string.Format("Empty Response to Request from BambooHR to {0} for employee ID {1} Code {2}", api_path, employeeId, response.StatusCode));
    }
    if (response.StatusCode == HttpStatusCode.OK)
    {
      var raw = RemoveTroublesomeCharacters(response.Content.Replace("Date\":\"0000-00-00\"", "Date\":null"));
      var package = JsonConvert.DeserializeObject<List<TimeOffRequest>>(raw);
      if (package != null)
      {
        return package;
      }
      throw new Exception("Bamboo Response does not contain data.");
    }
    throw new Exception($"Bamboo Response threw error code {response.StatusCode} ({response.StatusDescription}) {response.GetBambooHrErrorMessage()} in {nameof(GetTimeOffRequests)}");


  } //end public async Task<List<TimeOffRequest>> GetTimeOffRequests


  /// <inheritdoc/>
  public async Task<List<TimeOffWhosOut>> GetTimeOffWhosOut(DateTime? startDate = null, DateTime? endDate = null)
  {
    const string api_path = "/time_off/whos_out/";
    var request = new RestRequest(api_path, Method.Get);
    if (startDate.HasValue)
    {
      request.AddParameter("start", startDate.Value.ToString(_DateFormat), ParameterType.QueryString);
    }
    if (endDate.HasValue)
    {
      request.AddParameter("end", endDate.Value.ToString(_DateFormat), ParameterType.QueryString);
    }
    RestResponse response;
    try
    {
      response = await _RestClient.ExecuteGetAsync(request);
    }
    catch (Exception ex)
    {
      throw new Exception(string.Format("Error executing Bamboo request to {0}", api_path), ex);
    }
    if (response.ErrorException != null)
    {
      throw new Exception(string.Format("Error executing Bamboo request to {0}", api_path), response.ErrorException);
    }
    if (string.IsNullOrWhiteSpace(response.Content))
    {
      throw new Exception(string.Format("Empty Response to Request from BambooHR, Code: {0}", response.StatusCode));
    }
    if (response.StatusCode == HttpStatusCode.OK)
    {
      var raw = RemoveTroublesomeCharacters(response.Content.Replace("Date\":\"0000-00-00\"", "Date\":null"));
      var package = JsonConvert.DeserializeObject<List<TimeOffWhosOut>>(raw);
      if (package != null)
      {
        return package;
      }
      throw new Exception("Bamboo Response does not contain data.");
    }
    throw new Exception($"Bamboo Response threw error code {response.StatusCode} ({response.StatusDescription}) {response.GetBambooHrErrorMessage()} in {nameof(GetTimeOffWhosOut)}");

  } //end public async Task<List<TimeOffWhosOut>> GetTimeOffWhosOut






  /// <inheritdoc/>
  public async Task<CustomReport<ReportData>> GetCustomReport<ReportData>()
  {
    const string api_path = "/reports/custom/";
    var request = new RestRequest(api_path, Method.Post);

    var a = typeof(ReportData);
    var fields = a.GetProperties();

    if (fields.Length < 1)
    {
      throw new Exception("Error required fields");
    }

    request.AddBody(JsonConvert.SerializeObject(new
    {
      title = "Custom Report",
      fields = fields.Select(f => {
        var j = f.GetCustomAttribute<JsonPropertyNameAttribute>();
        if (j != null)
        {
          return j.Name;
        }
        return f.Name;
      }).ToArray()
    }));

    RestResponse response;
    try
    {
      response = await _RestClient.ExecutePostAsync(request);
    }
    catch (Exception ex)
    {
      throw new Exception(string.Format("Error executing Bamboo request to {0}", api_path), ex);
    }
    if (response.ErrorException != null)
    {
      throw new Exception(string.Format("Error executing Bamboo request to {0}", api_path), response.ErrorException);
    }
    if (string.IsNullOrWhiteSpace(response.Content) || response.Content == "[]")
    {
      throw new Exception(string.Format("Empty Response to Request from BambooHR, Code: {0}", response.StatusCode));
    }
    if (response.StatusCode == HttpStatusCode.OK)
    {
      var raw = RemoveTroublesomeCharacters(response.Content.Replace(":\"0000-00-00\"", ":null"));
      var package = JsonConvert.DeserializeObject<CustomReport<ReportData>>(raw);
      if (package != null)
      {
        return package;
      }
      throw new Exception("Bamboo Response does not contain data.");
    }
    throw new Exception($"Bamboo Response threw error code {response.StatusCode} ({response.StatusDescription}) {response.GetBambooHrErrorMessage()} in {nameof(GetTimeOffWhosOut)}");

  } //end public async Task<CustomReport> GetCustomReport















  /// <summary>
  /// Removes control characters and other non-UTF-8 characters
  /// </summary>
  /// <param name="inString">The string to process</param>
  /// <returns>A string with no control characters or entities above 0x00FD</returns>
  /// <remarks>
  /// From http://stackoverflow.com/a/20777/51
  /// </remarks>
  private static string RemoveTroublesomeCharacters(string inString)
  {
    if (inString == null)
    {
      return string.Empty;
    }
    var newString = new StringBuilder();
    char ch;
    foreach (var t in inString)
    {
      ch = t;
      // remove any characters outside the valid UTF-8 range as well as all control characters
      // except tabs and new lines
      if ((ch < 0x00FD && ch > 0x001F) || ch == '\t' || ch == '\n' || ch == '\r')
      {
        newString.Append(ch);
      }
    }
    newString.Replace(@"\u00a0", " ").Replace(@"\u200e", "").Replace(@"\u00e9", "é");
    return newString.ToString();

  } //end internal static string RemoveTroublesomeCharacters


} //end public class BambooHRClient
