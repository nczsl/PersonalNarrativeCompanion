using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace PNC.App {
  public partial class MainWindow : Window {
    public MainWindow() {
      InitializeComponent();
    }

    private void InitializeComponent() {
      AvaloniaXamlLoader.Load(this);
    }

    private void Minimize_Click(object? sender, RoutedEventArgs e) {
      WindowState = WindowState.Minimized;
    }

    private void MaximizeRestore_Click(object? sender, RoutedEventArgs e) {
      WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
    }

    private void Close_Click(object? sender, RoutedEventArgs e) {
      Close();
    }

    private void TitleBar_PointerPressed(object? sender, PointerPressedEventArgs e) {
      if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed) {
        BeginMoveDrag(e);
      }
    }

    private void TitleBar_DoubleTapped(object? sender, RoutedEventArgs e) {
      // Toggle maximize on double click
      MaximizeRestore_Click(sender, e);
    }
  }
}
