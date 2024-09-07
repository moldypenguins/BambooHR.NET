using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;

namespace BambooHR.NET.Test.Models;

public class PaneItem(string name, string text, RoutedEventHandler click, Visibility visibility)
{
  public string Name { get; internal set; } = name;
  public string Text { get; internal set; } = text;
  public RoutedEventHandler Click { get; internal set; } = click;

  public string ButtonName => $"{Name}_Button";
  public string TextBlockName => $"{Name}_TextBlock";

  public Visibility Visibility { get; internal set; } = visibility;

} //end public class PaneItem
