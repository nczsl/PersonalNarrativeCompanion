using System;

namespace PNC.Domain.Models;

/*
 非入库（内存/配置/UI）实体建议前缀：
  - Cfg_ : 配置类（通常从配置文件/Settings.json 读取并写回），不入关系型数据库。
  - State_: 运行时状态或短期缓存，随进程生命周期存在，通常不持久化。
  - Ui_  : 页面/视图聚合模型，仅用于 UI 层的数据组织。
  - Tmp_ : 临时计算/中间结构，可短期存在于内存中。

 参考 `Entity.Db.cs` 的命名约定并保持一致。下面给出常用场景的示例实体。
*/

public class Cfg_AppSettings {
  // 主题与外观
  public string ThemeName { get; set; } = "default"; // 可为 "dark"/"light" 等
  public bool UseDarkTheme { get; set; } = true;

  // 计时器与通知默认值
  public int DefaultTimerMinutes { get; set; } = 25;
  public int NotificationDurationSeconds { get; set; } = 3;

  // 日历/周起始
  public DayOfWeek DefaultStartOfWeek { get; set; } = DayOfWeek.Monday;

  // 本地化/显示格式
  public string DateFormat { get; set; } = "yyyy-MM-dd";
  public string TimeFormat { get; set; } = "HH:mm";

  // 自动归档策略（days），<=0 表示不自动归档
  public int AutoArchiveCompletedDays { get; set; } = 30;
}

public class Cfg_UserPreferences {
  public int UserId { get; set; }
  public bool ShowSecondsInTimer { get; set; } = false;
  public string Language { get; set; } = "en-US";
  public double FontScale { get; set; } = 1.0; // 用于 UI 缩放
}

public class State_UISettings {
  // 当前运行时使用的主题名/开关，通常从 Cfg_AppSettings 派生
  public string CurrentTheme { get; set; } = "default";
  public bool IsDarkMode { get; set; } = true;
  public bool SidebarCollapsed { get; set; } = false;
  public double FontScale { get; set; } = 1.0;
}

public class State_Session {
  public int? CurrentUserId { get; set; }
  public bool IsAuthenticated { get; set; }
  public DateTime SessionStartedAt { get; set; } = DateTime.UtcNow;
  public DateTime LastActivityAt { get; set; } = DateTime.UtcNow;
}

public class State_TimerSession {
  // 仅在运行时使用的计时器会话
  public int TimerId { get; set; }
  public DateTime? StartedAt { get; set; }
  public TimeSpan Accumulated { get; set; } = TimeSpan.Zero; // 累计已计时的时长
  public bool IsRunning { get; set; }
}

public class State_CalendarView {
  // 当前日历视图的运行时状态
  public DateOnly VisibleDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
  public string ViewMode { get; set; } = "month"; // day|week|month
  public int? SelectedPlanId { get; set; }
}

public class State_EditBuffer {
  // 通用的编辑缓冲区/临时对象，用于设置面板或编辑器未提交的数据保存
  public string EntityType { get; set; } = string.Empty; // e.g. "Plan", "PlanItem"
  public int? EntityId { get; set; }
  public string JsonPayload { get; set; } = string.Empty; // 可序列化的中间表示
  public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
}

public class Ui_SettingPageModel {
  // 聚合用于设置页的数据结构（非持久化）
  public Cfg_AppSettings AppSettings { get; set; } = new Cfg_AppSettings();
  public Cfg_UserPreferences UserPreferences { get; set; }
  public State_UISettings UIState { get; set; } = new State_UISettings();
}

// Tmp_ 前缀示例：用于短期计算或中间结果
public class Tmp_CalculationSummary {
  public string Key { get; set; } = string.Empty;
  public double Value { get; set; }
  public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
}
