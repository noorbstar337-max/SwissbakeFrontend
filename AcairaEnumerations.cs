// Decompiled with JetBrains decompiler
// Type: AcairaEnumerations
// Assembly: prolib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2FF12A11-4E83-40E7-A1E3-AE3629DFF068
// Assembly location: D:\CURRENT\PROJECTS\CLIENTS\SwissbakeBH\BACKUP\uat.swissbake.bh\bin\prolib.dll

using System;

public class AcairaEnumerations
{
  public static object FixNullFromDB(object objInput) => Convert.ToString(objInput) == "" ? (object) "" : objInput;

  public enum DML_OPERATION
  {
    INSERT = 1,
    UPDATE = 2,
    DELETE = 3,
  }
}
