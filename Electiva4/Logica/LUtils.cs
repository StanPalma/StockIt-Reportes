using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Electiva4.Logica
{
    public class LUtils
    {
        WSStockIt.WebServiceSI WS = new WSStockIt.WebServiceSI();

        public string fechaAAAAMMDD()
        {
            try
            {
                return WS.fechaAAAAMMDD();
            }
            catch (Exception)
            {
                return "";
            }
        }

        public string fechaDDMMAAAA()
        {
            try
            {
                return WS.fechaDDMMAAAA();
            }
            catch (Exception)
            {
                return "";
            }
        }

        public string fechaEntregaDDMMAAAA()
        {
            try
            {
                return WS.fechaEntregaDDMMAAAA();
            }
            catch (Exception)
            {
                return "";
            }
        }

        public string fechaHoraActual()
        {
            try
            {
                return WS.fechaHoraActual();
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}