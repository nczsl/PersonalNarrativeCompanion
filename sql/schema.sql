-- 数据库 Schema for Personal Narrative Companion

-- 核心表：事件/计划 (Events)
-- 这是最核心的表，记录用户的计划、想法或已发生的事件。
CREATE TABLE Events (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Title TEXT NOT NULL,
    Description TEXT,
    -- 'Plan', 'Event', 'Thought', etc.
    EventType TEXT NOT NULL DEFAULT 'Event',
    -- 用于分组或分类
    GroupId INTEGER,
    -- 优先级或评分
    Rating INTEGER,
    -- 颜色标注
    ColorTag TEXT,
    -- 引用其他事件
    ReferenceId INTEGER,
    -- 创建时间
    CreatedAt TEXT NOT NULL DEFAULT (datetime('now', 'localtime')),
    -- 计划开始时间
    ScheduledStart TEXT,
    -- 计划结束时间
    ScheduledEnd TEXT,
    -- 实际开始时间
    ActualStart TEXT,
    -- 实际结束时间
    ActualEnd TEXT,
    -- 'Pending', 'InProgress', 'Completed', 'Archived'
    Status TEXT NOT NULL DEFAULT 'Pending'
);

-- 故事表 (Stories)
-- 用于归档和组织相关的事件集合。
CREATE TABLE Stories (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Title TEXT NOT NULL,
    Narrative TEXT, -- 对这个故事的描述或反思
    CreatedAt TEXT NOT NULL DEFAULT (datetime('now', 'localtime'))
);

-- 事件与故事的关联表 (Event_Story_Links)
-- 一个事件可以属于一个故事，一个故事可以包含多个事件。
CREATE TABLE Event_Story_Links (
    EventId INTEGER NOT NULL,
    StoryId INTEGER NOT NULL,
    PRIMARY KEY (EventId, StoryId),
    FOREIGN KEY (EventId) REFERENCES Events(Id) ON DELETE CASCADE,
    FOREIGN KEY (StoryId) REFERENCES Stories(Id) ON DELETE CASCADE
);

-- 调度规则表 (Schedules)
-- 存储周期性任务的规则。
CREATE TABLE Schedules (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    EventId INTEGER NOT NULL, -- 关联的事件模板
    RRule TEXT NOT NULL, -- iCalendar RRULE string
    -- 下一次触发时间，用于优化查询
    NextRunTime TEXT NOT NULL,
    IsEnabled BOOLEAN NOT NULL DEFAULT 1,
    FOREIGN KEY (EventId) REFERENCES Events(Id) ON DELETE CASCADE
);

-- 内部消息/收件箱 (Inbox)
-- 存储由调度器或其他内部逻辑生成的通知。
CREATE TABLE InboxMessages (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Title TEXT NOT NULL,
    Content TEXT,
    -- 'ScheduleTrigger', 'Reminder', 'System'
    MessageType TEXT NOT NULL,
    -- 关联的实体，例如一个事件ID
    RelatedEntityId INTEGER,
    IsRead BOOLEAN NOT NULL DEFAULT 0,
    CreatedAt TEXT NOT NULL DEFAULT (datetime('now', 'localtime'))
);

-- 创建索引以提高查询性能
CREATE INDEX idx_events_status ON Events(Status);
CREATE INDEX idx_events_scheduled_start ON Events(ScheduledStart);
CREATE INDEX idx_schedules_next_run_time ON Schedules(NextRunTime);
CREATE INDEX idx_inbox_is_read ON InboxMessages(IsRead);
