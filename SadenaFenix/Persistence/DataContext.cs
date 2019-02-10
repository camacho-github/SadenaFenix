using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace SadenaFenix.Persistence
{
    public class DataContext
    {
        #region Variables de instancia
        private const string keyConnection = "SqlConection";
        private string connectionString;
        private const string keyExcelConnection = "ExcelConection";
        private string excelConnectionString;
        private const string keyAccessConnection = "AccessConection";
        private string accessConnectionString;
        #endregion

        #region Propiedades
        protected string Mensaje { get; set; }
        protected int Codigo { get; set; }
        #endregion

        #region Constructor
        public DataContext()
        {
            connectionString = ObtieneCadenaConexion();
            excelConnectionString = ObtieneExcelCadenaConexion();
            accessConnectionString = ObtieneAccessCadenaConexion();
        }
        #endregion

        #region Métodos Protegidos
        protected void EjecutaProcedimiento(string storedProcedure, Collection<SqlParameter> parametros, DataSet dataSet)
        {
            if (string.IsNullOrEmpty(storedProcedure) || parametros == null)
                throw new DataException("El query o los parámetros no han sido inicializados.");

            LimpiaPropiedadesControl();

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();

                using (SqlCommand cmd = new SqlCommand(storedProcedure, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 120;

                    foreach (SqlParameter parametro in parametros)
                    {
                        cmd.Parameters.Add(parametro);
                    }

                    using (SqlDataAdapter objAdapter = new SqlDataAdapter(cmd))
                    {
                        objAdapter.Fill(dataSet);

                        Codigo = Convert.ToInt32(cmd.Parameters["@po_msg_code"].Value, CultureInfo.InvariantCulture);
                        Mensaje = cmd.Parameters["@po_msg"].Value.ToString();
                    }
                }
            }
        }

       
        protected void EjecutaProcedimiento(string storedProcedure, Collection<SqlParameter> parametros)
        {
            if (string.IsNullOrEmpty(storedProcedure) || parametros == null)
                throw new DataException("El query o los parámetros no han sido inicializados.");

            LimpiaPropiedadesControl();

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();

                using (SqlCommand cmd = new SqlCommand(storedProcedure, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 120;

                    AgregaParametros(cmd, parametros);

                    cmd.ExecuteNonQuery();

                    Codigo = ObtieneCodigoRespuesta(cmd.Parameters);
                    Mensaje = ObtieneMensajeRespuesta(cmd.Parameters);
                }
            }
        }

        protected static void CreaParametrosSalida(Collection<SqlParameter> parametros)
        {
            SqlParameter parametro = null;

            parametro = new SqlParameter("@po_msg_code", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output,
                Value = 0
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@po_msg", SqlDbType.NVarChar)
            {
                Size = 255,
                Direction = ParameterDirection.Output,
                Value = string.Empty
            };
            parametros.Add(parametro);
        }

        private string ObtenerNombreHoja(OleDbConnection connection)
        {
            string nombre = "";

            using (DataTable dt = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null))
            {
                if (dt == null || dt.Rows.Count < 1)
                {
                    throw new DataException("El archivo no contiene información");
                }
                else if (dt.Rows.Count == 1)
                {
                    nombre = dt.Rows[0].Field<string>("TABLE_NAME");
                }
                else if (dt.Rows.Count > 1)
                {
                    nombre = dt.Rows[1].Field<string>("TABLE_NAME");
                }
            }

            nombre = nombre.Replace("'", "");
            nombre = nombre.Replace("$", "");

            return nombre;

        }

        protected void GuardarExcelEnBaseDatos(string nombreArchivo, string consultaFuente, string nombreTabla)
        {
            //String strConnection = "Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\Users\\Hemant\\documents\\visual studio 2010\\Projects\\CRMdata\\CRMdata\\App_Data\\Database1.mdf';Integrated Security=True;User Instance=True";

            //string excelConnString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0\"", nombreArchivo);
            String excelConnString = String.Format(excelConnectionString, nombreArchivo);
            //Create Connection to Excel work book 

            //string rango = GetRange("'Exportar Hoja de Trabajo$'", excelConnString);
            using (OleDbConnection excelConnection = new OleDbConnection(excelConnString))
            {
                excelConnection.Open();

                string nombreHoja = ObtenerNombreHoja(excelConnection);
                consultaFuente = String.Format(consultaFuente, nombreHoja, 250000);

                //Create OleDbCommand to fetch data from Excel 
                using (OleDbCommand cmd = new OleDbCommand(consultaFuente, excelConnection))
                {

                    using (OleDbDataReader dReader = cmd.ExecuteReader())
                    {
                        using (SqlBulkCopy sqlBulk = new SqlBulkCopy(connectionString))
                        {
                            //Give your Destination table name 
                            sqlBulk.DestinationTableName = nombreTabla;
                            sqlBulk.WriteToServer(dReader);
                        }
                    }
                }
            }
        }



        protected void GuardarAccessEnBaseDatos(string nombreArchivo, string consultaFuente, string nombreTabla)
        {
            //String strConnection = "Data Source=.\\SQLEXPRESS;AttachDbFilename='C:\\Users\\Hemant\\documents\\visual studio 2010\\Projects\\CRMdata\\CRMdata\\App_Data\\Database1.mdf';Integrated Security=True;User Instance=True";

            //String excelConnString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0\"", nombreArchivo);
            String oleConnString = String.Format(accessConnectionString, nombreArchivo);
            //Create Connection to Excel work book 
            using (OleDbConnection accessConnection = new OleDbConnection(oleConnString))
            {
                //Create OleDbCommand to fetch data from Access 
                using (OleDbCommand cmd = new OleDbCommand(consultaFuente, accessConnection))
                {
                    accessConnection.Open();
                    using (OleDbDataReader dReader = cmd.ExecuteReader())
                    {
                        using (SqlBulkCopy sqlBulk = new SqlBulkCopy(connectionString))
                        {
                            //Give your Destination table name 
                            sqlBulk.BulkCopyTimeout = 0;
                            sqlBulk.DestinationTableName = nombreTabla;
                            sqlBulk.WriteToServer(dReader);
                        }
                    }
                }
            }
        }

        #endregion

        #region Métodos Privados
        private string ObtieneCadenaConexion()
        {
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings[keyConnection].ConnectionString.ToString();
            }
            catch (ConfigurationErrorsException cee)
            {
                throw new DataException(cee.Message);
            }
            return connectionString;
        }

        private string ObtieneExcelCadenaConexion()
        {
            try
            {
                excelConnectionString = ConfigurationManager.ConnectionStrings[keyExcelConnection].ConnectionString.ToString();
            }
            catch (ConfigurationErrorsException cee)
            {
                throw new DataException(cee.Message);
            }
            return excelConnectionString;
        }

        private string ObtieneAccessCadenaConexion()
        {
            try
            {
                accessConnectionString = ConfigurationManager.ConnectionStrings[keyAccessConnection].ConnectionString.ToString();
            }
            catch (ConfigurationErrorsException cee)
            {
                throw new DataException(cee.Message);
            }
            return accessConnectionString;
        }

        private void LimpiaPropiedadesControl()
        {
            Codigo = 0;
            Mensaje = string.Empty;
        }

        private static void AgregaParametros(SqlCommand cmd, Collection<SqlParameter> parametros)
        {
            try
            {
                foreach (SqlParameter parametro in parametros)
                {
                    cmd.Parameters.Add(parametro);
                }
            }
            catch (ArgumentException ae)
            {
                throw new DataException(ae.Message);
            }
            catch (InvalidCastException ie)
            {
                throw new DataException(ie.Message);
            }
        }

        private static int ObtieneCodigoRespuesta(SqlParameterCollection parametros)
        {
            int respuesta = -1;

            try
            {
                respuesta = Convert.ToInt32(parametros["@po_msg_code"].Value, CultureInfo.InvariantCulture);
            }
            catch (FormatException fe)
            {
                throw new DataException(fe.Message);
            }
            catch (InvalidCastException ice)
            {
                throw new DataException(ice.Message);
            }
            catch (OverflowException ofe)
            {
                throw new DataException(ofe.Message);
            }
            return respuesta;
        }

        private static string ObtieneMensajeRespuesta(SqlParameterCollection parametros)
        {
            return parametros["@po_msg"].Value.ToString();
        }


        private string GetRange(string SheetName, string excelConnectionString)
        {
            string rangeInput = "", rangeColName = "";
            int columnsCount = 0;
            int rowStartRange = 0;

            columnsCount = GetNumberOfColumnsInSheet(SheetName, excelConnectionString);
            rowStartRange = GetStartRowRange(SheetName, excelConnectionString); // This is optional if you want always A1. just assign 1 here 
            while (columnsCount > 0)
            {
                columnsCount--;
                rangeColName = (char)('A' + columnsCount % 26) + rangeColName;
                columnsCount /= 26;
            }

            rangeInput = "A" + rowStartRange + ":" + rangeColName + "0";



            return rangeInput;
        }



        // Get Sheet Name assuming only one sheet for workbook and no hidden sheets
        private string GetSheetName(string filepath)
        {
            string sheetname = "";
            String connect = ExcelConn(filepath);
            OleDbConnection con = new OleDbConnection(connect);
            con.Open();

            DataTable tables = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

            foreach (DataRow row in tables.Rows)
            {
                sheetname = row[2].ToString();
                if (!sheetname.EndsWith("$"))
                    continue;

            }

            con.Close();
            return sheetname;
        }


        // Get number of columns in a given sheet
        private int GetNumberOfColumnsInSheet(string SheetName, string excelConnectionString)
        {
            int columnsCount = 0;

            //If a valid excel file
            if (!string.IsNullOrEmpty(excelConnectionString))
            {
                using (OleDbConnection conn = new OleDbConnection(excelConnectionString))
                {
                    conn.Open();
                    DataTable dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, null);
                    if (dt.Rows.Count > 0)
                        columnsCount = dt.AsEnumerable().Where(a => a["TABLE_NAME"].ToString() == SheetName).Count();
                    conn.Close();
                }
            }
            return columnsCount;
        }


        // Get the first row count in sheet contains some keyword . This method call is optional if you always want A1. Here I need to check some keyword exist and from there only I have to start something like A4


        private int GetStartRowRange(string SheetName, string excelConnectionString)
        {
            int rowStartRange = 1;

            //If a valid excel file
            if (!string.IsNullOrEmpty(excelConnectionString))
            {
                using (OleDbConnection conn = new OleDbConnection(excelConnectionString))
                {
                    string colValue;
                    conn.Open();
                    string cmdstr = "select * from [" + SheetName + "]";

                    OleDbCommand com = new OleDbCommand(cmdstr, conn);
                    DataTable dt = new DataTable();
                    OleDbDataAdapter da = new OleDbDataAdapter(com);
                    da.Fill(dt);



                    // get first row data where it started

                    foreach (DataRow dataRow in dt.Rows)
                    {

                        colValue = dataRow[0].ToString();


                        if ((colValue.Contains("Value1") || colValue.Contains("Value2") || colValue.Contains("Value3")) && (string.IsNullOrEmpty(dataRow[1].ToString()) == false))
                        {
                            rowStartRange = rowStartRange + 1;
                            break;
                        }
                        else
                        {
                            rowStartRange = rowStartRange + 1;
                        }

                    }

                    conn.Close();


                }

            }
            return rowStartRange;
        }


        // Connection to excel document
        private string ExcelConn(string FilePath)
        {
            string constr = "";
            string extension = System.IO.Path.GetExtension(FilePath);

            //Checking for the extentions, if XLS connect using Jet OleDB
            if (extension.Equals(".xls", StringComparison.CurrentCultureIgnoreCase))
            {
                constr = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0};Extended Properties=\"Excel 12.0;IMEX=1;HDR=YES\"", FilePath);
            }
            //Use ACE OleDb if xlsx extention
            else if (extension.Equals(".xlsx", StringComparison.CurrentCultureIgnoreCase))
            {
                constr = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 8.0;IMEX=1;HDR=YES\"", FilePath);
            }


            return constr;

        }
        #endregion
    }
}