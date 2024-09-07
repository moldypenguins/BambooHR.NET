/**
 * BambooHR.NET.Test
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
 * @name MainWindow.xaml.cs
 * @version 2024-03-15
 * @summary BambooHR API Client
 **/
using Microsoft.UI.Xaml.Media;
using WinUIEx;

namespace BambooHR.NET.Test;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : WindowEx
{
  public MainWindow()
  {
    this.InitializeComponent();
    ExtendsContentIntoTitleBar = true;
    SystemBackdrop = new DesktopAcrylicBackdrop();
    AppWindow.SetIcon(Path.Combine(AppContext.BaseDirectory, "Assets", "BambooHR.ico"));
    Title = "BambooHR.NET Test";
    Content = new MainPage();

  } //end public MainWindow()

} //end public sealed partial class MainWindow : WindowEx
