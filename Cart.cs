// Decompiled with JetBrains decompiler
// Type: prolib.Cart
// Assembly: prolib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2FF12A11-4E83-40E7-A1E3-AE3629DFF068
// Assembly location: D:\CURRENT\PROJECTS\CLIENTS\SwissbakeBH\BACKUP\uat.swissbake.bh\bin\prolib.dll

using System;
using System.Data;
using System.Data.SqlClient;

namespace prolib
{
  public class Cart
  {
    private string _spname = "mvc_cart";
    private string _spOrder = "mvc_Order";
    private string _spcredit = "MVC_ApplyCredit";

    public DataSet CheckCartName(string Cart_ProdId, string Cart_CustID, string Mode)
    {
      SqlParameter[] sqlParameterArray = new SqlParameter[6];
      try
      {
        sqlParameterArray[0] = new SqlParameter("@Mode", (object) Mode);
        sqlParameterArray[1] = new SqlParameter("@Cart_ProdId", (object) Cart_ProdId);
        sqlParameterArray[2] = new SqlParameter("@Cart_CustID", (object) Cart_CustID);
        return DBBridge.ExecuteDataset("mvc_CheckExist", sqlParameterArray);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int Insert(string Cart_CustId, int Cart_ProductId, int Cart_Qty, string Cart_CreatedId)
    {
      SqlParameter[] sqlParameterArray = new SqlParameter[8];
      try
      {
        sqlParameterArray[0] = new SqlParameter("@Mode", (object) nameof (Insert));
        sqlParameterArray[1] = new SqlParameter("@Cart_CustId", (object) Cart_CustId);
        sqlParameterArray[2] = new SqlParameter("@Cart_ProductId", (object) Cart_ProductId);
        sqlParameterArray[3] = new SqlParameter("@Cart_Qty", (object) Cart_Qty);
        sqlParameterArray[4] = new SqlParameter("@Cart_CreatedId", (object) Cart_CreatedId);
        return DBBridge.ExecuteNonQuery(this._spname, sqlParameterArray);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int Update(
      string Cart_CustId,
      int Cart_ProductId,
      int Cart_Qty,
      string Cart_ModifiedId,
      int Cart_Id)
    {
      SqlParameter[] sqlParameterArray = new SqlParameter[8];
      try
      {
        sqlParameterArray[0] = new SqlParameter("@Mode", (object) nameof (Update));
        sqlParameterArray[1] = new SqlParameter("@Cart_CustId", (object) Cart_CustId);
        sqlParameterArray[2] = new SqlParameter("@Cart_ProductId", (object) Cart_ProductId);
        sqlParameterArray[3] = new SqlParameter("@Cart_Qty", (object) Cart_Qty);
        sqlParameterArray[4] = new SqlParameter("@Cart_ModifiedId", (object) Cart_ModifiedId);
        sqlParameterArray[5] = new SqlParameter("@Cart_Id", (object) Cart_Id);
        return DBBridge.ExecuteNonQuery(this._spname, sqlParameterArray);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int Deletefrontcart(string Mode, string Cart_CustId, int Cart_ProductId)
    {
      SqlParameter[] sqlParameterArray = new SqlParameter[4];
      try
      {
        sqlParameterArray[0] = new SqlParameter("@Mode", (object) Mode);
        sqlParameterArray[1] = new SqlParameter("@Cart_CustId", (object) Cart_CustId);
        sqlParameterArray[2] = new SqlParameter("@Cart_ProductId", (object) Cart_ProductId);
        return DBBridge.ExecuteNonQuery(this._spname, sqlParameterArray);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public DataSet buildcart(string Mode, string ID, string CookieID)
    {
      SqlParameter[] sqlParameterArray = new SqlParameter[3];
      try
      {
        sqlParameterArray[0] = new SqlParameter("@Mode", (object) Mode);
        sqlParameterArray[1] = new SqlParameter("@qID", (object) ID);
        sqlParameterArray[2] = new SqlParameter("@qCookieID", (object) CookieID);
        return DBBridge.ExecuteDataset(this._spname, sqlParameterArray);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int ApplyCredit(string Mode, int AppCredit_AccID, string AppCredit_Message)
    {
      SqlParameter[] sqlParameterArray = new SqlParameter[4];
      try
      {
        sqlParameterArray[0] = new SqlParameter("@Mode", (object) Mode);
        sqlParameterArray[1] = new SqlParameter("@AppCredit_AccID", (object) AppCredit_AccID);
        sqlParameterArray[2] = new SqlParameter("@AppCredit_Message", (object) AppCredit_Message);
        return DBBridge.ExecuteNonQuery(this._spcredit, sqlParameterArray);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public DataSet b2bGetbothstock(int prdId)
    {
      SqlParameter[] sqlParameterArray = new SqlParameter[3];
      try
      {
        sqlParameterArray[0] = new SqlParameter("@Mode", (object) nameof (b2bGetbothstock));
        sqlParameterArray[2] = new SqlParameter("@Product_Id", (object) prdId);
        return DBBridge.ExecuteDataset("_spRealTimeStock", sqlParameterArray);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
