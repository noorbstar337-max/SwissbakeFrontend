// Decompiled with JetBrains decompiler
// Type: prolib.Feedback
// Assembly: prolib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2FF12A11-4E83-40E7-A1E3-AE3629DFF068
// Assembly location: D:\CURRENT\PROJECTS\CLIENTS\SwissbakeBH\BACKUP\uat.swissbake.bh\bin\prolib.dll

using System;
using System.Data.SqlClient;

namespace prolib
{
  public class Feedback
  {
    private string _spName = "_spActivity";

    public int Rating(
      int Rating_AgentId,
      string Rating_ActivityId,
      int Rating_Listening,
      int Rating_Knowledge,
      int Rating_Understandability,
      string Rating_Remark,
      string Rating_Status,
      int Rating_CreatedBy,
      string Rating_DeleteDate,
      string Rating_BadStatus)
    {
      try
      {
        return Convert.ToInt32(DBBridge.ExecuteScalar(this._spName, new SqlParameter[12]
        {
          new SqlParameter("@Mode", (object) "InsertRating"),
          new SqlParameter("@Rating_AgentId", (object) Rating_AgentId),
          new SqlParameter("@Rating_ActivityId", (object) Rating_ActivityId),
          new SqlParameter("@Rating_Listening", (object) Rating_Listening),
          new SqlParameter("@Rating_Knowledge", (object) Rating_Knowledge),
          new SqlParameter("@Rating_Understandability", (object) Rating_Understandability),
          new SqlParameter("@Rating_Remark", (object) Rating_Remark),
          new SqlParameter("@Rating_Status", (object) Rating_Status),
          new SqlParameter("@Rating_CreatedBy", (object) Rating_CreatedBy),
          new SqlParameter("@Rating_DeleteDate", (object) Rating_DeleteDate),
          new SqlParameter("@Rating_BadStatus", (object) Rating_BadStatus),
          null
        }));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
