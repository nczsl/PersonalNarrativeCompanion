using PNC.View.Common;
using Avalonia.Media;
using System.Globalization;

namespace PNC.App.ViewModels;

public class PlanItemViewModel : BaseViewModel {
  private string? _title;
  public string? Title {
    get => _title;
    set => SetProperty(ref _title, value);
  }

  private string? _color;
  public string? Color {
    get => _color;
    set {
      if (SetProperty(ref _color, value))
      {
        UpdateForegroundBrushFromColor(_color);
      }
    }
  }

  private IBrush? _foregroundBrush;
  public IBrush? ForegroundBrush {
    get => _foregroundBrush;
    set => SetProperty(ref _foregroundBrush, value);
  }

  private void UpdateForegroundBrushFromColor(string? colorStr)
  {
    if (string.IsNullOrWhiteSpace(colorStr))
    {
      ForegroundBrush = Brushes.Black;
      return;
    }

    try
    {
  if (Avalonia.Media.Color.TryParse(colorStr, out var c))
      {
        // compute relative luminance
        double r = c.R / 255.0;
        double g = c.G / 255.0;
        double b = c.B / 255.0;
        double lum = 0.2126 * r + 0.7152 * g + 0.0722 * b;
        // threshold 0.6: light background -> dark text
        ForegroundBrush = lum > 0.6 ? Brushes.Black : Brushes.White;
        return;
      }

      ForegroundBrush = Brushes.Black;
    }
    catch
    {
      ForegroundBrush = Brushes.Black;
    }
  }
}
