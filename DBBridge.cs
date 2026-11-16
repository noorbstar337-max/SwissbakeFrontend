// Decompiled with JetBrains decompiler
// Type: prolib.DBBridge
// Assembly: prolib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2FF12A11-4E83-40E7-A1E3-AE3629DFF068
// Assembly location: D:\CURRENT\PROJECTS\CLIENTS\SwissbakeBH\BACKUP\uat.swissbake.bh\bin\prolib.dll

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace prolib
{
  public class DBBridge
  {
    public static string DBConnection()
    {
      try
      {
        return ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
      }
      catch (Exception ex)
      {
        throw new ApplicationException("Unable to get DB Connection string from Config File. Contact Administrator" + Convert.ToString((object) ex));
      }
    }

    public static int ExecuteNonQuerywithTrans(string storedProcedure, SqlParameter[] param)
    {
      SqlConnection sqlConnection = new SqlConnection(DBBridge.DBConnection());
      sqlConnection.Open();
      SqlTransaction transaction = sqlConnection.BeginTransaction();
      try
      {
        int num = SQLHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, storedProcedure, param);
        transaction.Commit();
        return num;
      }
      catch (Exception ex)
      {
        transaction.Rollback();
        throw ex;
      }
      finally
      {
        sqlConnection.Close();
      }
    }

    public static int ExecuteNonQuery(string storedProcedure, SqlParameter[] param)
    {
      try
      {
        return SQLHelper.ExecuteNonQuery(DBBridge.DBConnection(), CommandType.StoredProcedure, storedProcedure, param);
      }
      catch (SqlException ex)
      {
        throw ex;
      }
    }

    public static int ExecuteNonQuerySQL(string sqlquery)
    {
      try
      {
        return SQLHelper.ExecuteNonQuery(DBBridge.DBConnection(), CommandType.Text, sqlquery);
      }
      catch (SqlException ex)
      {
        throw ex;
      }
    }

    public static int ExecuteNonQuerywithTrans(string storedProcedure)
    {
      SqlConnection sqlConnection = new SqlConnection(DBBridge.DBConnection());
      sqlConnection.Open();
      SqlTransaction transaction = sqlConnection.BeginTransaction();
      try
      {
        int num = SQLHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, storedProcedure);
        transaction.Commit();
        return num;
      }
      catch (Exception ex)
      {
        transaction.Rollback();
        throw ex;
      }
      finally
      {
        sqlConnection.Close();
      }
    }

    public static int ExecuteNonQuerywithTransfromFrontEnd(
      SqlTransaction sqlTrans,
      string storedProcedure,
      SqlParameter[] param)
    {
      try
      {
        return SQLHelper.ExecuteNonQuery(sqlTrans, CommandType.StoredProcedure, storedProcedure, param);
      }
      catch (SqlException ex)
      {
        sqlTrans.Rollback();
        throw ex;
      }
    }

    public static DataSet ExecuteDataset(string storedProcedure, SqlParameter[] param)
    {
      try
      {
        return SQLHelper.ExecuteDataset(DBBridge.DBConnection(), CommandType.StoredProcedure, storedProcedure, param);
      }
      catch (SqlException ex)
      {
        throw ex;
      }
    }

    public static DataSet ExecuteDatasetSQL(string storedProcedure)
    {
      try
      {
        return SQLHelper.ExecuteDataset(DBBridge.DBConnection(), CommandType.Text, storedProcedure);
      }
      catch (SqlException ex)
      {
        throw ex;
      }
    }

    public static DataSet ExecuteDataset(string storedProcedure)
    {
      try
      {
        return SQLHelper.ExecuteDataset(DBBridge.DBConnection(), CommandType.StoredProcedure, storedProcedure);
      }
      catch (SqlException ex)
      {
        throw ex;
      }
    }

    public static object ExecuteScalar(string storedProcedure, SqlParameter[] param)
    {
      try
      {
        return SQLHelper.ExecuteScalar(DBBridge.DBConnection(), CommandType.StoredProcedure, storedProcedure, param);
      }
      catch (SqlException ex)
      {
        throw ex;
      }
    }

    public static object ExecuteScalar(
      SqlTransaction sqlTrans,
      string storedProcedure,
      SqlParameter[] param)
    {
      try
      {
        return SQLHelper.ExecuteScalar(sqlTrans, CommandType.StoredProcedure, storedProcedure, param);
      }
      catch (SqlException ex)
      {
        throw ex;
      }
    }

    public static SqlDataReader ExecuteReader(string storedProcedure, SqlParameter[] param)
    {
      try
      {
        return SQLHelper.ExecuteReader(DBBridge.DBConnection(), CommandType.StoredProcedure, storedProcedure, param);
      }
      catch (SqlException ex)
      {
        throw ex;
      }
    }

    public static SqlDataReader ExecuteReader(string storedProcedure)
    {
      try
      {
        return SQLHelper.ExecuteReader(DBBridge.DBConnection(), CommandType.StoredProcedure, storedProcedure);
      }
      catch (SqlException ex)
      {
        throw ex;
      }
    }

    public static SqlDataReader ExecuteReaderSQL(string sqlquery)
    {
      try
      {
        return SQLHelper.ExecuteReader(DBBridge.DBConnection(), CommandType.Text, sqlquery);
      }
      catch (SqlException ex)
      {
        throw ex;
      }
    }

    public static int ExecuteNonQuerywithMultipleTrans(
      SqlTransaction sqlTrans,
      string storedProcedure)
    {
      try
      {
        return SQLHelper.ExecuteNonQuery(sqlTrans, CommandType.StoredProcedure, storedProcedure);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static int ExecuteNonQuerywithMultipleTrans(
      SqlTransaction sqlTrans,
      string storedProcedure,
      SqlParameter[] param)
    {
      try
      {
        return SQLHelper.ExecuteNonQuery(sqlTrans, CommandType.StoredProcedure, storedProcedure, param);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
