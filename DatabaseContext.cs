// Decompiled with JetBrains decompiler
// Type: prolib.DatabaseContext
// Assembly: prolib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2FF12A11-4E83-40E7-A1E3-AE3629DFF068
// Assembly location: D:\CURRENT\PROJECTS\CLIENTS\SwissbakeBH\BACKUP\uat.swissbake.bh\bin\prolib.dll

using System.Data.Entity;

namespace prolib
{
  public class DatabaseContext : DbContext
  {
    public DatabaseContext()
      : base("ConnectionString")
    {
    }
  }
}
