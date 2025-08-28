using System.Collections.ObjectModel;
using PNC.View.Common;
using System;
using Avalonia.Media;

namespace PNC.App.ViewModels;

public class DayCellViewModel : BaseViewModel
{
    private int _dayNumber;
    public int DayNumber
    {
        get => _dayNumber;
        set => SetProperty(ref _dayNumber, value);
    }

    private DateTime _date;
    public DateTime Date
    {
        get => _date;
        set => SetProperty(ref _date, value);
    }

    private bool _isCurrentMonth = true;
    public bool IsCurrentMonth
    {
        get => _isCurrentMonth;
        set => SetProperty(ref _isCurrentMonth, value);
    }

    private string _foreground = "#FF000000";
    // Avalonia can parse color strings into brushes when binding to Foreground
    public string Foreground
    {
        get => _foreground;
        set => SetProperty(ref _foreground, value);
    }

    private IBrush? _foregroundBrush;
    public IBrush? ForegroundBrush
    {
        get => _foregroundBrush;
        set => SetProperty(ref _foregroundBrush, value);
    }

    public ObservableCollection<PlanItemViewModel> Plans { get; } = new();
}
