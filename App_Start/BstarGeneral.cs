using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using prolib;

namespace SwissbakeIn.App_Start
{

    public class BstarGeneral : ActionFilterAttribute
    {

        public static double GetDoubleVal(object objParam)
        {
            double gval = 0;

            try
            {
                gval = Convert.ToDouble(objParam);
            }
            catch (Exception ex)
            {

            }

            return gval;
        }

        public static int GetIntVal(object objParam)
        {
            int gval = 0;

            try
            {
                gval = Convert.ToInt32(objParam);
            }
            catch (Exception ex)
            {

            }

            return gval;
        }

        public static string GetStringVal(object objParam)
        {
            string gval = "";

            try
            {
                gval = Convert.ToString(objParam);
            }
            catch (Exception ex)
            {

            }

            return gval;
        }
    }
}
