// Decompiled with JetBrains decompiler
// Type: prolib.Category
// Assembly: prolib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2FF12A11-4E83-40E7-A1E3-AE3629DFF068
// Assembly location: D:\CURRENT\PROJECTS\CLIENTS\SwissbakeBH\BACKUP\uat.swissbake.bh\bin\prolib.dll

using System;
using System.Data;
using System.Data.SqlClient;

namespace prolib
{
  public class Category
  {
    private string _spName = "mvc_Category";

    public DataSet GetList()
    {
      try
      {
        return DBBridge.ExecuteDataset(this._spName, new SqlParameter[1]
        {
          new SqlParameter("@Mode", (object) "MainProductCat")
        });
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public DataSet GetRecentCat(string mode, int id)
    {
      try
      {
        return DBBridge.ExecuteDataset(this._spName, new SqlParameter[2]
        {
          new SqlParameter("@Mode", (object) mode),
          new SqlParameter("@id", (object) id)
        });
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public DataSet GetList6()
    {
      try
      {
        return DBBridge.ExecuteDataset(this._spName, new SqlParameter[1]
        {
          new SqlParameter("@Mode", (object) "Home6ProductCat")
        });
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
