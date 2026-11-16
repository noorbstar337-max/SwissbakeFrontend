// Decompiled with JetBrains decompiler
// Type: prolib.Complaint
// Assembly: prolib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2FF12A11-4E83-40E7-A1E3-AE3629DFF068
// Assembly location: D:\CURRENT\PROJECTS\CLIENTS\SwissbakeBH\BACKUP\uat.swissbake.bh\bin\prolib.dll

using System;
using System.Data.SqlClient;

namespace prolib
{
  public class Complaint
  {
    private string _spName = "mvc_Complaint";

    public int Insert(
      int _Complaint_DeptId,
      int _Complaint_CustId,
      string _Complaint_Subject,
      string _Complaint_Discription,
      string _Complaint_Attachment)
    {
      try
      {
        return Convert.ToInt32(DBBridge.ExecuteScalar(this._spName, new SqlParameter[6]
        {
          new SqlParameter("@Mode", (object) "InsertAccount"),
          new SqlParameter("@Complaint_DeptId", (object) _Complaint_DeptId),
          new SqlParameter("@Complaint_CustId", (object) _Complaint_CustId),
          new SqlParameter("@Complaint_Subject", (object) _Complaint_Subject),
          new SqlParameter("@Complaint_Discription", (object) _Complaint_Discription),
          new SqlParameter("@Complaint_Attachment", (object) _Complaint_Attachment)
        }));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int RInsert(
      int _Reply_CompaintId,
      int _Reply_TypeId,
      string _Reply_Type,
      string _Reply_Discription,
      string _Reply_Attachment)
    {
      try
      {
        return Convert.ToInt32(DBBridge.ExecuteScalar("_spReply", new SqlParameter[6]
        {
          new SqlParameter("@Mode", (object) "FrontInsert"),
          new SqlParameter("@Reply_CompaintId", (object) _Reply_CompaintId),
          new SqlParameter("@Reply_TypeId", (object) _Reply_TypeId),
          new SqlParameter("@Reply_Type", (object) _Reply_Type),
          new SqlParameter("@Reply_Discription", (object) _Reply_Discription),
          new SqlParameter("@Reply_Attachment", (object) _Reply_Attachment)
        }));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
