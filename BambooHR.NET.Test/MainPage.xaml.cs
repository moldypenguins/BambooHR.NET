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
 * @name MainPage.xaml.cs
 * @version 2024-03-15
 * @summary BambooHR API Client
 **/

using BambooHR.NET.Models;
using BambooHR.NET.Test.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System.Configuration;

namespace BambooHR.NET.Test;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainPage : Page
{
  private readonly List<PaneItem> PaneItems = [];
  private BambooHRClient? bClient = null;


  /// <summary>
  /// 
  /// </summary>
  public MainPage()
  {
    this.InitializeComponent();
    this.Loaded += MainPage_Loaded;

    //Pane Items
    PaneItems.Add(new("PaneItem_1", "GetTimeOffWhosOut()", PaneItem_1_Button_Click, MainPage_SplitView.Visibility));
    PaneItems.Add(new("PaneItem_2", "GetCustomeFTE()", PaneItem_2_Button_Click, MainPage_SplitView.Visibility));
    PaneItems.Add(new("PaneItem_3", "GetTimeOffRequests()", PaneItem_3_Button_Click, MainPage_SplitView.Visibility));
    PaneItems.Add(new("PaneItem_4", "GetCustomReport()", PaneItem_4_Button_Click, MainPage_SplitView.Visibility));

  } //end public MainPage


  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void MainPage_Loaded(object sender, RoutedEventArgs e)
  {
    try
    {
      //initiate a new BambooHRClient instance
      bClient = new BambooHRClient(Properties.Settings.Default.COMPANY_SUBDOMAIN, Properties.Settings.Default.API_KEY);

      //add PaneItems to SplitPane
      foreach (var pItem in PaneItems)
      {
        var stackPanel = new StackPanel()
        {
          Margin = new Thickness(0, 0, 0, 12),
          HorizontalAlignment = HorizontalAlignment.Stretch,
          VerticalAlignment = VerticalAlignment.Top,
          Orientation = Orientation.Horizontal
        };
        var button = new Button()
        {
          Name = pItem.ButtonName,
          Width = 24,
          Height = 24,
          Padding = new Thickness(0),
          HorizontalAlignment = HorizontalAlignment.Left,
          VerticalAlignment = VerticalAlignment.Center,
          Content = new FontIcon()
          {
            Glyph = "\uE8B5",
            FontFamily = (FontFamily)App.Current.Resources["SymbolThemeFontFamily"],
            FontSize = 12
          }
        };
        button.Click += pItem.Click;
        ToolTipService.SetToolTip(button, new ToolTip() { Content = pItem.Text });
        var textBlock = new TextBlock()
        {
          Name = pItem.TextBlockName,
          Margin = new Thickness(12, 0, 12, 0),
          VerticalAlignment = VerticalAlignment.Center,
          FontSize = 12,
          Text = pItem.Text,
          Visibility = pItem.Visibility
        };
        stackPanel.Children.Add(button);
        stackPanel.Children.Add(textBlock);
        MainPage_SplitViewPane.Children.Add(stackPanel);
      }

    }
    catch (Exception ex)
    {
      //TODO: error handling
      System.Diagnostics.Debug.WriteLine(ex.Message);
    }
    
  } //end private void MainPage_Loaded


  /// <summary>
  /// Sets the status TextBlock
  /// </summary>
  /// <param name="status"></param>
  private void SetStatus(string status)
  {
    ContentStatus.Text = $"STATUS: {status}";

  } //end private void SetStatus



  #region Pane Toggling

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void PaneToggle_Button_Click(object sender, RoutedEventArgs e)
  {
    if (MainPage_SplitView.IsPaneOpen)
    {
      MainPage_SplitView.IsPaneOpen = false;
      PaneToggle_Button_Icon.Glyph = "\uE970";
      PaneToggle_Button.Padding = new Thickness(2, 0, 0, 0);
      CR_OpenPane_BitmapIcon.Visibility = Visibility.Collapsed;
      CR_CompactPane_BitmapIcon.Visibility = Visibility.Visible;
      PaneHeader.Visibility = Visibility.Collapsed;
      TogglePaneItemVisibility(false);
    }
    else
    {
      MainPage_SplitView.IsPaneOpen = true;
      PaneToggle_Button_Icon.Glyph = "\uE96F";
      PaneToggle_Button.Padding = new Thickness(0, 0, 2, 0);
      CR_OpenPane_BitmapIcon.Visibility = Visibility.Visible;
      CR_CompactPane_BitmapIcon.Visibility = Visibility.Collapsed;
      PaneHeader.Visibility = Visibility.Visible;
      TogglePaneItemVisibility(true);
    }

  } //end private void PaneToggle_Button_Click


  /// <summary>
  /// Sets the visibility of PaneItem TextBlocks
  /// </summary>
  /// <param name="visible"></param>
  private void TogglePaneItemVisibility(bool visible)
  {
    var a = PaneItems.Select(b => { return b.Visibility; });
    foreach (var pi in PaneItems)
    {
      // Find the control by name
      var tb = (TextBlock)MainPage_SplitViewPane.FindName(pi.TextBlockName);
      if (tb != null)
      {
        tb.Visibility = visible ? Visibility.Visible : Visibility.Collapsed;
      }
      else
      {
        System.Diagnostics.Debug.WriteLine("Control not found.");
      }
    }

  } //end private void TogglePaneItemVisibility

  #endregion //Pane Toggling


  #region PaneItem Event Handlers

  /// <summary>
  /// Pane Item GetTimeOffWhosOut()
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private async void PaneItem_1_Button_Click(object sender, RoutedEventArgs e)
  {
    Content_TextBlock.Text = string.Empty;
    if (bClient != null)
    {
      SetStatus("Running...");
      try
      {
        var result = await bClient.GetTimeOffWhosOut(new DateTime(2024, 3, 2), new DateTime(2024, 3, 15));
        if (result.Count > 0)
        {
          foreach (var t in result)
          {
            Content_TextBlock.Text += t.ToString() + Environment.NewLine;
          }

        }
        SetStatus("Done.");
      }
      catch (Exception ex)
      {
        SetStatus($"Error.");
        Content_TextBlock.Text = ex.Message;
      }

    }

  } //end private async void PaneItem_1_Button_Click


  /// <summary>
  /// Pane Item GetCustomeFTE()
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private async void PaneItem_2_Button_Click(object sender, RoutedEventArgs e)
  {
    Content_TextBlock.Text = string.Empty;
    if (bClient != null)
    {
      SetStatus("Running...");
      try
      {
        var result = await bClient.GetTabularData<FullTimeEquivalentData>(511, "customFull-TimeEquivalent");
        if (result.Count > 0)
        {
          foreach (var t in result)
          {
            Content_TextBlock.Text += t.ToString() + Environment.NewLine;
          }

        }
        SetStatus("Done.");
      }
      catch (Exception ex)
      {
        SetStatus($"Error.");
        Content_TextBlock.Text = ex.Message;
      }

    }

  } //end private void PaneItem_2_Button_Click


  /// <summary>
  /// Pane Item GetTimeOffRequests()
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private async void PaneItem_3_Button_Click(object sender, RoutedEventArgs e)
  {
    Content_TextBlock.Text = string.Empty;
    if (bClient != null)
    {
      SetStatus("Running...");
      try
      {
        var result = await bClient.GetTimeOffRequests(null, 268, new DateTime(2024, 2, 12), new DateTime(2024, 2, 23));
        if (result.Count > 0)
        {
          foreach (var t in result)
          {
            Content_TextBlock.Text += t.ToString() + Environment.NewLine;
          }

        }
        SetStatus("Done.");
      }
      catch (Exception ex)
      {
        SetStatus($"Error.");
        Content_TextBlock.Text = ex.Message;
      }

    }
  } //end private void PaneItem_3_Button_Click


  /// <summary>
  /// Pane Item GetCustomReport()
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private async void PaneItem_4_Button_Click(object sender, RoutedEventArgs e)
  {
    Content_TextBlock.Text = string.Empty;
    if (bClient != null)
    {
      SetStatus("Running...");
      try
      {
        var result = await bClient.GetCustomReport<EmployeeReportData>();
        if (result?.Employees != null)
        {
          foreach (var emp in result.Employees)
          {
            Content_TextBlock.Text += emp.ToString() + Environment.NewLine;
          }

        }
        SetStatus("Done.");
      }
      catch (Exception ex)
      {
        SetStatus($"Error.");
        Content_TextBlock.Text = ex.Message;
      }

    }
  } //end private void PaneItem_4_Button_Click

  #endregion //PaneItem Event Handlers






}
