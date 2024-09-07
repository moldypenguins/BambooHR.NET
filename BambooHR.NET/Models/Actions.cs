using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BambooHR.NET.Models;

/// <summary>
/// 
/// </summary>
public class Actions
{
  public bool View { get; set; }
  public bool Edit { get; set; }
  public bool Cancel { get; set; }
  public bool Approve { get; set; }
  public bool Deny { get; set; }
  public bool Bypass { get; set; }

} //end public class Actions
