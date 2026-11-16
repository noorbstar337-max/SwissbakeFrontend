// Decompiled with JetBrains decompiler
// Type: prolib.Home
// Assembly: prolib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2FF12A11-4E83-40E7-A1E3-AE3629DFF068
// Assembly location: D:\CURRENT\PROJECTS\CLIENTS\SwissbakeBH\BACKUP\swissbake.bh\bin\prolib.dll

using System;
using System.Data;
using System.Data.SqlClient;

namespace prolib
{
  public class Home
  {
    private string _spName = "mvc_Banner";
    private string _spView = "mvc_ViewByID";
    private string _spEnq = "mvc_Enquiry";
    private string _spCarr = "mvc_Career";

    public DataSet GetList()
    {
      try
      {
        return DBBridge.ExecuteDataset(this._spName, new SqlParameter[1]
        {
          new SqlParameter("@Mode", (object) "getDualbanner")
        });
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public DataSet GetBlogCatList(string mode)
    {
      try
      {
        return DBBridge.ExecuteDataset(this._spView, new SqlParameter[1]
        {
          new SqlParameter("@Mode", (object) mode)
        });
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public DataSet GetBlogCat(string SeoUrl, string mode)
    {
      try
      {
        return DBBridge.ExecuteDataset(this._spView, new SqlParameter[2]
        {
          new SqlParameter("@Mode", (object) mode),
          new SqlParameter("@Value", (object) SeoUrl)
        });
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public DataSet GetSolution3(string mode)
    {
      try
      {
        return DBBridge.ExecuteDataset(this._spView, new SqlParameter[2]
        {
          new SqlParameter("@Mode", (object) mode),
          null
        });
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public DataSet GetFaq()
    {
      try
      {
        return DBBridge.ExecuteDataset(this._spView, new SqlParameter[1]
        {
          new SqlParameter("@Mode", (object) "ViewFaq")
        });
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int InsertEnq(
      string Name,
      string Email,
      string Number,
      string Org,
      string Mess,
      string Type,
      string Product,
      int State,
      string City,
      int Country,
      string Pin,
      string mode)
    {
      try
      {
        return Convert.ToInt32(DBBridge.ExecuteScalar(this._spEnq, new SqlParameter[12]
        {
          new SqlParameter("@Mode", (object) mode),
          new SqlParameter("@Enq_Name", (object) Name),
          new SqlParameter("@Enq_EmailId", (object) Email),
          new SqlParameter("@Enq_Organstn", (object) Org),
          new SqlParameter("@Enq_Phone", (object) Number),
          new SqlParameter("@Enq_Messg", (object) Mess),
          new SqlParameter("@Enq_Type", (object) Type),
          new SqlParameter("@Enq_Product", (object) Product),
          new SqlParameter("@Enq_Country", (object) Country),
          new SqlParameter("@Enq_State", (object) State),
          new SqlParameter("@Enq_City", (object) City),
          new SqlParameter("@Enq_ZipCode", (object) Pin)
        }));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int InsertRequest(
      string type,
      string Name,
      string Number,
      string Email,
      string Org,
      string address,
      string City,
      string Pin,
      string Mess,
      int Pid,
      string mode)
    {
      try
      {
        return Convert.ToInt32(DBBridge.ExecuteScalar(this._spEnq, new SqlParameter[12]
        {
          new SqlParameter("@Mode", (object) mode),
          new SqlParameter("@Enq_Name", (object) Name),
          new SqlParameter("@Enq_EmailId", (object) Email),
          new SqlParameter("@Enq_Organstn", (object) Org),
          new SqlParameter("@Enq_Phone", (object) Number),
          new SqlParameter("@Enq_Messg", (object) Mess),
          new SqlParameter("@Enq_Type", (object) type),
          new SqlParameter("@Pid", (object) Pid),
          new SqlParameter("@Enq_City", (object) City),
          new SqlParameter("@Enq_ZipCode", (object) Pin),
          new SqlParameter("@Address", (object) address),
          null
        }));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public string InsertCarr(
      string Name,
      string Email,
      string Nationallity,
      string Mobile,
      string CurrDesg,
      string Emp,
      string CarrCtc,
      string ExpCtc,
      string Upload,
      string Experience,
      string Pos,
      string Loc,
      string Com,
      string Appfor,
      string Appno,
      string mode)
    {
      try
      {
        return Convert.ToString(DBBridge.ExecuteScalar(this._spCarr, new SqlParameter[16]
        {
          new SqlParameter("@Mode", (object) mode),
          new SqlParameter("@Carr_Name", (object) Name),
          new SqlParameter("@Carr_Email", (object) Email),
          new SqlParameter("@Carr_Nationallity", (object) Nationallity),
          new SqlParameter("@Carr_Mobile", (object) Mobile),
          new SqlParameter("@Carr_CurrentDesgntn", (object) CurrDesg),
          new SqlParameter("@Carr_Employer", (object) Emp),
          new SqlParameter("@Carr_Ctc", (object) CarrCtc),
          new SqlParameter("@Carr_ExpctCtc", (object) ExpCtc),
          new SqlParameter("@Carr_Upload", (object) Upload),
          new SqlParameter("@Carr_Experience", (object) Experience),
          new SqlParameter("@Carr_Position", (object) Pos),
          new SqlParameter("@Carr_Location", (object) Loc),
          new SqlParameter("@Carr_Comment", (object) Com),
          new SqlParameter("@Carr_AppFor", (object) Appfor),
          new SqlParameter("@Carr_AppNo", (object) Appno)
        }));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public string InsertBakSup(
      int BakerSup_ID,
      string BakerSup_Name,
      string BakerSup_EmailId,
      string BakerSup_Company,
      string BakerSup_Phone,
      string BakerSup_Messg,
      string BakerSup_Country,
      bool BakerSup_Read,
      bool BakerSup_Tip,
      bool BakerSup_Recipe,
      bool BakerSup_Solution,
      string BakerSup_Value1,
      string BakerSup_Value2,
      string BakerSup_Value3,
      string mode)
    {
      try
      {
        return Convert.ToString(DBBridge.ExecuteScalar("_spBakerSupport", new SqlParameter[17]
        {
          new SqlParameter("@Mode", (object) mode),
          new SqlParameter("@BakerSup_ID", (object) BakerSup_ID),
          new SqlParameter("@BakerSup_Name", (object) BakerSup_Name),
          new SqlParameter("@BakerSup_EmailId", (object) BakerSup_EmailId),
          new SqlParameter("@BakerSup_Company", (object) BakerSup_Company),
          new SqlParameter("@BakerSup_Phone", (object) BakerSup_Phone),
          new SqlParameter("@BakerSup_Messg", (object) BakerSup_Messg),
          new SqlParameter("@BakerSup_Country", (object) BakerSup_Country),
          new SqlParameter("@BakerSup_Read", (object) BakerSup_Read),
          new SqlParameter("@BakerSup_Tip", (object) BakerSup_Tip),
          new SqlParameter("@BakerSup_Recipe", (object) BakerSup_Recipe),
          new SqlParameter("@BakerSup_Solution", (object) BakerSup_Solution),
          new SqlParameter("@BakerSup_Value1", (object) BakerSup_Value1),
          new SqlParameter("@BakerSup_Value2", (object) BakerSup_Value2),
          new SqlParameter("@BakerSup_Value3", (object) BakerSup_Value3),
          null,
          null
        }));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

        public string InsertExport(
          string Enq_Organstn,
          string Enq_Type,
          string Enq_Turnover,
          string Enq_Name,
          string Enq_Desig,
          string Enq_Phone,
          string Enq_EmailId,
          string Enq_Country,
          string Enq_City,
          string Enq_ZipCode,
          string Enq_Product,
          string Enq_Messg)
        {
            try
            {
                return Convert.ToString(DBBridge.ExecuteScalar("MVC_Enquiry", new SqlParameter[17]
                {
          new SqlParameter("@Mode", (object) "InsertExportEnq"),
          new SqlParameter("@Enq_Organstn", (object) Enq_Organstn),
          new SqlParameter("@Enq_Type", (object) Enq_Type),
          new SqlParameter("@Enq_Turnover", (object) Enq_Turnover),
          new SqlParameter("@Enq_Name", (object) Enq_Name),
          new SqlParameter("@Enq_Desig", (object) Enq_Desig),
          new SqlParameter("@Enq_Phone", (object) Enq_Phone),
          new SqlParameter("@Enq_EmailId", (object) Enq_EmailId),
          new SqlParameter("@Enq_Countryn", (object) Enq_Country),
          new SqlParameter("@Enq_City", (object) Enq_City),
          new SqlParameter("@Enq_ZipCode", (object) Enq_ZipCode),
          new SqlParameter("@Enq_Product", (object) Enq_Product),
          new SqlParameter("@Enq_Messg", (object) Enq_Messg),
          null,
          null,
          null,
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
