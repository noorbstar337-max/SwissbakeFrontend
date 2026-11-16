// Decompiled with JetBrains decompiler
// Type: prolib.Order
// Assembly: prolib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2FF12A11-4E83-40E7-A1E3-AE3629DFF068
// Assembly location: D:\CURRENT\PROJECTS\CLIENTS\SwissbakeBH\BACKUP\uat.swissbake.bh\bin\prolib.dll

using System;
using System.Data;
using System.Data.SqlClient;

namespace prolib
{
  public class Order
  {
    private string _spName = "mvc_Account";
    private string _spOrder = "mvc_Order";

    public int InsertOrder(
      int _Order_CustId,
      string _Order_ModeofPayment,
      double Order_FreightTaxPer,
      string _Po_No,
      string PODate,
      string Comment,
      string UploadPath)
    {
      try
      {
        return Convert.ToInt32(DBBridge.ExecuteScalar(this._spOrder, new SqlParameter[8]
        {
          new SqlParameter("@Mode", (object) "InsertFrontOrder"),
          new SqlParameter("@Order_CustId", (object) _Order_CustId),
          new SqlParameter("@Order_ModeofPayment", (object) _Order_ModeofPayment),
          new SqlParameter("@Order_PoNo", (object) _Po_No),
          new SqlParameter("@Order_FreightTaxPer", (object) Order_FreightTaxPer),
          new SqlParameter("@Order_PoDate", (object) PODate),
          new SqlParameter("@Order_Comment", (object) Comment),
          new SqlParameter("@Order_POAttachment", (object) UploadPath)
        }));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int OrderDetails(
      int _Order_ID,
      int _OrDetail_ProductId,
      int _OrDetail_Qty,
      double _OrDetail_Rate,
      double _OrDetail_Amount,
      double _OrDetail_Vat)
    {
      SqlParameter[] sqlParameterArray = new SqlParameter[8];
      try
      {
        sqlParameterArray[0] = new SqlParameter("@Mode", (object) "FrontOrder_Details");
        sqlParameterArray[1] = new SqlParameter("@Order_ID", (object) _Order_ID);
        sqlParameterArray[2] = new SqlParameter("@OrDetail_ProductId", (object) _OrDetail_ProductId);
        sqlParameterArray[3] = new SqlParameter("@OrDetail_Qty", (object) _OrDetail_Qty);
        sqlParameterArray[4] = new SqlParameter("@OrDetail_Rate", (object) _OrDetail_Rate);
        sqlParameterArray[5] = new SqlParameter("@OrDetail_Amount", (object) _OrDetail_Amount);
        sqlParameterArray[6] = new SqlParameter("@OrDetail_Vat", (object) _OrDetail_Vat);
        return DBBridge.ExecuteNonQuery(this._spOrder, sqlParameterArray);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int updatefreight(int _Order_ID, int _Order_CustId, double Order_FreightTaxPer)
    {
      SqlParameter[] sqlParameterArray = new SqlParameter[5];
      try
      {
        sqlParameterArray[0] = new SqlParameter("@Mode", (object) "UpdateFreight");
        sqlParameterArray[1] = new SqlParameter("@Order_ID", (object) _Order_ID);
        sqlParameterArray[2] = new SqlParameter("@Order_CustId", (object) _Order_CustId);
        sqlParameterArray[3] = new SqlParameter("@Order_FreightTaxPer", (object) Order_FreightTaxPer);
        return DBBridge.ExecuteNonQuery(this._spOrder, sqlParameterArray);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int InserOrderaddress(int id, int orderID)
    {
      SqlParameter[] sqlParameterArray = new SqlParameter[5];
      try
      {
        sqlParameterArray[0] = new SqlParameter("@Mode", (object) "InsertfrontOrderAddress");
        sqlParameterArray[1] = new SqlParameter("@Id", (object) id);
        sqlParameterArray[2] = new SqlParameter("@OrderAddr_OrderID", (object) orderID);
        return DBBridge.ExecuteNonQuery(this._spOrder, sqlParameterArray);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public DataSet getOrderDetails(string Mode, int _Order_ID)
    {
      SqlParameter[] sqlParameterArray = new SqlParameter[2];
      try
      {
        sqlParameterArray[0] = new SqlParameter("@Mode", (object) Mode);
        sqlParameterArray[1] = new SqlParameter("@Order_ID", (object) _Order_ID);
        return DBBridge.ExecuteDataset(this._spOrder, sqlParameterArray);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int UpdateTaxAcc(string Pan, string GST, int Acc_Id, int Addr_Id)
    {
      try
      {
        return DBBridge.ExecuteNonQuery(this._spName, new SqlParameter[5]
        {
          new SqlParameter("@Mode", (object) "updateShipTaxation"),
          new SqlParameter("@Cust_Pan", (object) Pan),
          new SqlParameter("@AccAddr_GST", (object) GST),
          new SqlParameter("@Acc_Id", (object) Acc_Id),
          new SqlParameter("@AccAddr_ID", (object) Addr_Id)
        });
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public DataSet ViewByID(int ID, string Mode)
    {
      SqlParameter[] sqlParameterArray = new SqlParameter[2];
      try
      {
        sqlParameterArray[0] = new SqlParameter("@Mode", (object) Mode);
        sqlParameterArray[1] = new SqlParameter("@ID", (object) ID);
        return DBBridge.ExecuteDataset("_ViewByID", sqlParameterArray);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public DataSet ViewAddressByID(string _Address_Type, int _Cust_ID)
    {
      SqlParameter[] sqlParameterArray = new SqlParameter[4];
      try
      {
        sqlParameterArray[0] = new SqlParameter("@Mode", (object) "FillBillAddress");
        sqlParameterArray[1] = new SqlParameter("@AccAddr_AccID", (object) _Cust_ID);
        sqlParameterArray[2] = new SqlParameter("@AccAddr_Type", (object) _Address_Type);
        return DBBridge.ExecuteDataset("_spAccountAddress", sqlParameterArray);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
