// Decompiled with JetBrains decompiler
// Type: prolib.Dropdown
// Assembly: prolib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2FF12A11-4E83-40E7-A1E3-AE3629DFF068
// Assembly location: D:\CURRENT\PROJECTS\CLIENTS\SwissbakeBH\BACKUP\uat.swissbake.bh\bin\prolib.dll

using System;
using System.Data;
using System.Data.SqlClient;

namespace prolib
{
  public class Dropdown
  {
    private string _spName = "mvc_Dropdown";

    public DataSet FillYearById(int Id, string Mode)
    {
      try
      {
        return DBBridge.ExecuteDataset(this._spName, new SqlParameter[2]
        {
          new SqlParameter("@Mode", (object) Mode),
          new SqlParameter("@ID", (object) Id)
        });
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public DataSet FillDrop(int Id, string Mode)
    {
      try
      {
        return DBBridge.ExecuteDataset(this._spName, new SqlParameter[2]
        {
          new SqlParameter("@Mode", (object) Mode),
          new SqlParameter("@ID", (object) Id)
        });
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
