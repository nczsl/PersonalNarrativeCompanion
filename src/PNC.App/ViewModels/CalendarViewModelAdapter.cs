using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using PNC.View.Common;
using Avalonia;
using Avalonia.Media;
using System.Linq;

namespace PNC.App.ViewModels;

public class CalendarViewModelAdapter : BaseViewModel
{
    private DateTime _visibleMonth = DateTime.Today;

    public DateTime VisibleMonth
    {
        get => _visibleMonth;
        set => SetProperty(ref _visibleMonth, value);
    }

    public ObservableCollection<DayCellViewModel> Days { get; } = new();

    public ICommand? NextMonthCommand { get; set; }
    public ICommand? PrevMonthCommand { get; set; }
    public ICommand? TodayCommand { get; set; }
    public ICommand? EditPlanCommand { get; set; }

    public CalendarViewModelAdapter()
    {
    // Initialize commands
    PrevMonthCommand = new RelayCommand(new Action<object?>(_ => ChangeMonth(-1)));
    NextMonthCommand = new RelayCommand(new Action<object?>(_ => ChangeMonth(1)));
    TodayCommand = new RelayCommand(new Action<object?>(_ => { VisibleMonth = DateTime.Today; BuildDaysForVisibleMonth(); }));
    EditPlanCommand = new RelayCommand(new Action<object?>(_ => { /* TODO: open editor */ }));

        BuildDaysForVisibleMonth();
    }

    private void ChangeMonth(int months)
    {
        VisibleMonth = VisibleMonth.AddMonths(months);
        BuildDaysForVisibleMonth();
    }

    private void BuildDaysForVisibleMonth()
    {
        Days.Clear();

        var firstOfMonth = new DateTime(VisibleMonth.Year, VisibleMonth.Month, 1);
        // In .NET DayOfWeek: Sunday = 0, Monday = 1 ...
        int firstDayIndex = (int)firstOfMonth.DayOfWeek; // 0..6
        // We want week to start on Monday (optional). If user prefers Sunday, keep firstDayIndex as is.
        // Here keep Sunday-start to match earlier code.
        var startDate = firstOfMonth.AddDays(-firstDayIndex);

        for (int i = 0; i < 42; i++)
        {
            var date = startDate.AddDays(i);
            var isCurrent = date.Month == VisibleMonth.Month;
            var vm = new DayCellViewModel
            {
                Date = date,
                DayNumber = date.Day,
                IsCurrentMonth = isCurrent,
                Foreground = isCurrent ? "#FFFFFFFF" : "#FF888888",
                ForegroundBrush = isCurrent ? new SolidColorBrush(Colors.WhiteSmoke) : new SolidColorBrush(Colors.Gray)
            };

                // sample plans: generate 1-3 random sample plans for visual testing
                var rnd = new Random(date.DayOfYear + date.Month);
                int planCount = rnd.Next(0, 4); // 0..3
                for (int p = 0; p < planCount; p++)
                {
                    var color = (p % 2 == 0) ? "#FFD700" : "#87CEFA";
                    vm.Plans.Add(new PlanItemViewModel { Title = $"示例 @{date:MMM d} #{p + 1}", Color = color });
                }

            Days.Add(vm);
        }
    }
}
