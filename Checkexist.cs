// Decompiled with JetBrains decompiler
// Type: prolib.Checkexist
// Assembly: prolib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2FF12A11-4E83-40E7-A1E3-AE3629DFF068
// Assembly location: D:\CURRENT\PROJECTS\CLIENTS\SwissbakeBH\BACKUP\uat.swissbake.bh\bin\prolib.dll

using System;
using System.Data;
using System.Data.SqlClient;

namespace prolib
{
  public class Checkexist
  {
    private string _spName = "mvc_CheckExist";

    public int CheckExist(string Value, string Mode)
    {
      try
      {
        return Convert.ToString(DBBridge.ExecuteScalar(this._spName, new SqlParameter[2]
        {
          new SqlParameter("@Mode", (object) Mode),
          new SqlParameter("@Value", (object) Value)
        })) == "Already Exist" ? 1 : 0;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public string CheckRegStep(string Id)
    {
      try
      {
        return Convert.ToString(DBBridge.ExecuteScalar(this._spName, new SqlParameter[2]
        {
          new SqlParameter("@Mode", (object) "CheckRegisterSteps"),
          new SqlParameter("@ID", (object) Id)
        }));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public DataSet GetRegStepDet(int id, string value)
    {
      try
      {
        return DBBridge.ExecuteDataset(this._spName, new SqlParameter[3]
        {
          new SqlParameter("@Mode", (object) nameof (GetRegStepDet)),
          new SqlParameter("@ID", (object) id),
          new SqlParameter("@Value", (object) value)
        });
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
