using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SadenaFenix.Commons.Utilerias
{
    public static class Bitacora
    {
        //private static log4net.ILog Log { get; set; }

        static Bitacora()
        {
            //Log = Log
        }

        public static void Error(object msg)
        {
            //Log.Error(msg);
        }

        public static void Error(object msg, Exception ex)
        {
            //Log.Error(msg, ex);
        }

        public static void Error(Exception ex)
        {
            if (ex == null)
                return;

            //Log.Error(ex.Message, ex);
        }

        public static void Info(object msg)
        {
            //Log.Info(msg);
        }
    }
}
