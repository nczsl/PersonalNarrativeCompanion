/*
 依然采用 之前的缀模型它们是
 Lc_ : logic class
 R_ : relation
 Res_: resource
 Note_ : note
 Role_ : role
 Log_ : log
 State_ : state
 Cfg_ : config
一般的入库 （关系型数据库）的实体前缀有： Lc,R,Res,Note,Role,Log
State_ 不一定入库，因为它经常变化 ，但不排除会入内存库，State适合入内存库，或直接就是一个内存一般对象
Cfg_ ，不入库，一般映射配置文件实体
*/
namespace PNC.Domain.Models;

public class Note_StatusDefine{
  public int Id { get; set; }
  public string Tag { get; set; }
  public string Detail { get; set; }
}
///<summary> 用户状态切片</summary>
public class Note_Status {
  public int Id { get; set; }
  public int UserId { get; set; }
  public DateTime CreatedAt { get; set; }  
  public string Description { get; set; }
  // 评估分数
  public float Score { get; set; }
  public int StatusDefineId { get; set; }
  public Note_StatusDefine StatusDefine { get; set; }
}
public class Lc_Status {
  public int Id { get; set; }
  public string Tag { get; set; }
  public string Detail { get; set; }
}
public class Note_Plan {
  public int Id { get; set; }
  public string Name { get; set; }
  public bool IsActivitySet { get; set; }
  public DateOnly StartDate { get; set; }
  public DateOnly EndDate { get; set; }
  public string Summary { get; set; }
  public int StatusId { get; set; }
  public Lc_Status Status { get; set; }
  public int PlanItemId { get; set; }
  public ICollection<Note_PlanItem> PlanItems { get; set; }
}
public class Note_PlanItem {
  public int Id { get; set; }
  public string Title { get; set; }
  public bool IsActivity { get; set; }
  public TimeOnly StartTime { get; set; }
  public TimeOnly EndTime { get; set; }
  public string EndAnnotation { get; set; }
  public int StatusId { get; set; }
  public Lc_Status Status { get; set; }
  public int StateAnnotationId { get; set; }
  public ICollection<R_StatusAnnotation> StateAnnotations { get; set; }
  /*进度百分比，只要它是isactivity为false时有效，代表完成本项以后所达成的预期计划中的进度百分比*/
  public float ProgressPercent { get; set; }
}
public class R_StatusAnnotation {
  public int Id { get; set; }
  public int PlanItemId { get; set; }
  public Note_PlanItem PlanItem { get; set; }
  public string Annotation { get; set; }
}
public class R_StatusObservation {
  public int Id { get; set; }
  public int StatusId { get; set; }
  public Note_Status Status { get; set; }
  public int PlanId { get; set; }
  public Note_Plan Plan { get; set; }
  public int PlanItemId { get; set; }
  public Note_PlanItem PlanItem { get; set; }
}
public class Role_User {
  public int Id { get; set; }
  public string Name { get; set; }
  //email or phone
  public string Contact { get; set; }
  public string HashedPassword { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime LastLoginAt { get; set; }
}
