# Personal Narrative Companion

Personal Narrative Companion（个人叙事伴侣）是一款面向自我管理与反思的桌面应用。

简介
- 将“计时”和“事件记录”结合，通过把相关事件或完成的计划归档为“个人故事”（Story），帮助用户理解时间使用、评估价值并改进行为。

主要功能
- 计时（Timer）：支持手动计时与可选预期结束时间；支持超时阈值与恢复策略。
- 计划与事件：平级实体模型，支持引用、分组、评分与颜色标注。
- 调度（Schedule）：支持周期（RRULE）、间隔与按次列表触发，触发时生成内部消息与弹窗。
- Inbox（内部消息/伪邮件）：未读通知角标、消息列表、快速跳转。
- Story（个人故事）：归档单个事件或事件集合或完成计划，用于长期反思与统计。

技术栈建议
- .NET 9 + Avalonia UI
- SQLite（Microsoft.Data.Sqlite）+ Dapper/EF Core
- Ical.Net（RRULE 支持）
- NodaTime（可选，处理时区）

项目结构（建议）
- docs/        - 设计规格
- src/         - 源代码（后续创建）
- sql/         - SQL 脚本与 schema

后续我可以：
- A) 在 `apps/PersonalNarrativeCompanion` 初始化项目骨架并添加 `sql/schema.sql` 与基础模型。
- B) 实现 scheduling POC（manual-list -> messages -> badge）。
- C) 输出更详细的技术规格文档（Markdown）。

请选择 A/B/C 继续。
