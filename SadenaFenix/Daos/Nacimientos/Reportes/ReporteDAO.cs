using SadenaFenix.Commons.Utilerias;
using SadenaFenix.Excepcions;
using SadenaFenix.Models.Nacimientos.Consultas;
using SadenaFenix.Models.Nacimientos.Reportes;
using SadenaFenix.Persistence;
using SadenaFenix.Transport.Nacimientos.Reportes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Xml;

namespace SadenaFenix.Daos.Nacimientos.Reportes
{
    public class ReporteDAO : DataContext
    {
        #region Variables de instancia        
        private const string PRN_CONSULTA_SUBREGISTRO_NACIMIENTOS = "SDB.PRSSubregistroNacimientos";
        private const string PRN_CONSULTA_TOTALES_SUBREGISTRO_NACIMIENTOS = "SDB.PRSTotalesSubregistroNacimientos";
        private const string PRN_CONSULTA_REPORTE_XML_SUBREGISTRO_MUNICIPIOS = "SDB.PRSReporteXMLSubregistroMunicipios";
        private const string PRN_CONSULTA_REPORTE_TOTALES_MUNICIPIOS = "SDB.PRSReporteTotalesMunicipios";        
        private const string PRN_CONSULTA_REPORTE_SEXO_SUBREGISTRO_MUNICIPIOS = "SDB.PRSReporteSexoSubregistroMunicipios";
        private const string PRN_CONSULTA_REPORTE_ESCOLARIDAD_SUBREGISTRO_MUNICIPIOS = "SDB.PRSReporteEscolaridadSubregistroMunicipios";
        private const string PRN_CONSULTA_REPORTE_EDO_CIVIL_SUBREGISTRO_MUNICIPIOS = "SDB.PRSReporteEdoCivilSubregistroMunicipios";
        private const string PRN_CONSULTA_REPORTE_EDAD_SUBREGISTRO_MUNICIPIOS = "SDB.PRSReporteEdadSubregistroMunicipios";
        private const string PRN_CONSULTA_REPORTE_NUM_NAC_SUBREGISTRO_MUNICIPIOS = "SDB.PRSReporteNumNacSubregistroMunicipios";
        #endregion

        #region Métodos Públicos
        public TotalesSubregistroNacimientosRespuesta ConsultaTotalesSubregistroNacimientos(string anosUnion, string mesesUnion, string municipiosUnion)
        {
            TotalesSubregistroNacimientosRespuesta totalesSubregistroNacimientosRespuesta = new TotalesSubregistroNacimientosRespuesta();

            try
            {
                using (DataSet dataSet = new DataSet())
                {
                    dataSet.Locale = CultureInfo.InvariantCulture;

                    EjecutaProcedimiento(PRN_CONSULTA_TOTALES_SUBREGISTRO_NACIMIENTOS, CreaParametrosSubregistroNacimientos(anosUnion, mesesUnion, municipiosUnion), dataSet);

                    if (this.Codigo == 0 && ValidaDataSet(dataSet))
                    {
                        totalesSubregistroNacimientosRespuesta.ColTotales = new Collection<SubregistroTotal>();
                        
                        foreach (DataRow r in dataSet.Tables[0].Rows)
                        {
                            SubregistroTotal sub = new SubregistroTotal
                            {
                                IdGrupo = r.Field<int>("IdGrupo"),
                                NombreGrupo = r.Field<string>("NombreGrupo"),
                                Total = r.Field<int>("Total")
                            };

                            totalesSubregistroNacimientosRespuesta.ColTotales.Add(sub);
                        }

                    }
                    else
                    {
                        throw new EmptyDataException(this.Mensaje);
                    }
                }
            }
            catch (Exception de)
            {
                Bitacora.Error(de.Message);
                if (de is EmptyDataException)
                {
                    throw new DAOException(1, de.Message);
                }
                throw new DAOException(-1, de.Message);
            }

            return totalesSubregistroNacimientosRespuesta;
        }

        public SubregistroNacimientosRespuesta ConsultaDTSubregistroNacimientos(string anosUnion, string mesesUnion, string municipiosUnion)
        {
            SubregistroNacimientosRespuesta SubregistroNacimientosRespuesta = new SubregistroNacimientosRespuesta();
            SubregistroNacimientosRespuesta.ColDataTables = new Collection<DataTable>();

            try
            {
                using (DataSet dataSet = new DataSet())
                {
                    dataSet.Locale = CultureInfo.InvariantCulture;

                    EjecutaProcedimiento(PRN_CONSULTA_SUBREGISTRO_NACIMIENTOS, CreaParametrosSubregistroNacimientos(anosUnion, mesesUnion, municipiosUnion), dataSet);

                    if (this.Codigo == 0)
                    {
                        if (dataSet != null && dataSet.Tables.Count > 0)
                        {

                            SubregistroNacimientosRespuesta.ColTotales = new Collection<SubregistroTotal>();

                            foreach (DataRow r in dataSet.Tables[0].Rows)
                            {
                                SubregistroTotal sub = new SubregistroTotal();

                                sub.IdGrupo = r.Field<int>("IdGrupo");
                                sub.NombreGrupo = r.Field<string>("NombreGrupo");
                                sub.Total = r.Field<int>("Total");

                                SubregistroNacimientosRespuesta.ColTotales.Add(sub);
                            }
                        }

                        if (dataSet != null && dataSet.Tables.Count > 0)
                        {

                            SubregistroNacimientosRespuesta.ColSubregistros = new Collection<Subregistro>();

                            SubregistroNacimientosRespuesta.ColDataTables.Add(dataSet.Tables[1]);

                            foreach (DataRow r in dataSet.Tables[1].Rows)
                            {
                                Subregistro sub = new Subregistro();
                                sub.Folio = r.Field<string>("Folio");
                                sub.FechaNacimiento = r.Field<string>("FechaNacimiento");
                                sub.HoraNacimiento = r.Field<string>("HoraNacimiento");
                                sub.SexoId = r.Field<int>("SexoId");
                                sub.SexoDesc = r.Field<string>("SexoDesc");
                                sub.EdoId = r.Field<int>("EdoId");
                                sub.EdoDesc = r.Field<string>("EdoDesc");
                                sub.MpioId = r.Field<int>("MpioId");
                                sub.MpioDesc = r.Field<string>("MpioDesc");
                                sub.LocId = r.Field<int>("LocId");
                                sub.LocDesc = r.Field<string>("LocDesc");
                                sub.Calle = r.Field<string>("Calle");
                                sub.NoExt = r.Field<string>("NoExt");
                                sub.NoInt = r.Field<string>("NoInt");
                                sub.Edad = r.Field<int>("Edad");
                                sub.NumNacimiento = r.Field<int>("NumNacimiento");
                                sub.Ocupacion = r.Field<string>("Ocupacion");
                                sub.EdoCivilId = r.Field<int>("EdoCivilId");
                                sub.EdoCivilDesc = r.Field<string>("EdoCivilDesc");
                                sub.EscolId = r.Field<int>("EscolId");
                                sub.EscolDesc = r.Field<string>("EscolDesc");

                                SubregistroNacimientosRespuesta.ColSubregistros.Add(sub);
                            }
                        }

                        if (dataSet != null && dataSet.Tables.Count > 0)
                        {

                            SubregistroNacimientosRespuesta.ColOportunos = new Collection<Subregistro>();

                            SubregistroNacimientosRespuesta.ColDataTables.Add(dataSet.Tables[2]);

                            foreach (DataRow r in dataSet.Tables[2].Rows)
                            {
                                Subregistro sub = new Subregistro();
                                sub.Folio = r.Field<string>("Folio");
                                sub.FechaNacimiento = r.Field<string>("FechaNacimiento");
                                sub.HoraNacimiento = r.Field<string>("HoraNacimiento");
                                sub.SexoId = r.Field<int>("SexoId");
                                sub.SexoDesc = r.Field<string>("SexoDesc");
                                sub.EdoId = r.Field<int>("EdoId");
                                sub.EdoDesc = r.Field<string>("EdoDesc");
                                sub.MpioId = r.Field<int>("MpioId");
                                sub.MpioDesc = r.Field<string>("MpioDesc");
                                sub.LocId = r.Field<int>("LocId");
                                sub.LocDesc = r.Field<string>("LocDesc");
                                sub.Calle = r.Field<string>("Calle");
                                sub.NoExt = r.Field<string>("NoExt");
                                sub.NoInt = r.Field<string>("NoInt");
                                sub.Edad = r.Field<int>("Edad");
                                sub.NumNacimiento = r.Field<int>("NumNacimiento");
                                sub.Ocupacion = r.Field<string>("Ocupacion");
                                sub.EdoCivilId = r.Field<int>("EdoCivilId");
                                sub.EdoCivilDesc = r.Field<string>("EdoCivilDesc");
                                sub.EscolId = r.Field<int>("EscolId");
                                sub.EscolDesc = r.Field<string>("EscolDesc");

                                SubregistroNacimientosRespuesta.ColOportunos.Add(sub);
                            }
                        }

                        if (dataSet != null && dataSet.Tables.Count > 0)
                        {

                            SubregistroNacimientosRespuesta.ColExtemporaneos = new Collection<Subregistro>();

                            SubregistroNacimientosRespuesta.ColDataTables.Add(dataSet.Tables[3]);

                            foreach (DataRow r in dataSet.Tables[3].Rows)
                            {
                                Subregistro sub = new Subregistro();
                                sub.Folio = r.Field<string>("Folio");
                                sub.FechaNacimiento = r.Field<string>("FechaNacimiento");
                                sub.HoraNacimiento = r.Field<string>("HoraNacimiento");
                                sub.SexoId = r.Field<int>("SexoId");
                                sub.SexoDesc = r.Field<string>("SexoDesc");
                                sub.EdoId = r.Field<int>("EdoId");
                                sub.EdoDesc = r.Field<string>("EdoDesc");
                                sub.MpioId = r.Field<int>("MpioId");
                                sub.MpioDesc = r.Field<string>("MpioDesc");
                                sub.LocId = r.Field<int>("LocId");
                                sub.LocDesc = r.Field<string>("LocDesc");
                                sub.Calle = r.Field<string>("Calle");
                                sub.NoExt = r.Field<string>("NoExt");
                                sub.NoInt = r.Field<string>("NoInt");
                                sub.Edad = r.Field<int>("Edad");
                                sub.NumNacimiento = r.Field<int>("NumNacimiento");
                                sub.Ocupacion = r.Field<string>("Ocupacion");
                                sub.EdoCivilId = r.Field<int>("EdoCivilId");
                                sub.EdoCivilDesc = r.Field<string>("EdoCivilDesc");
                                sub.EscolId = r.Field<int>("EscolId");
                                sub.EscolDesc = r.Field<string>("EscolDesc");

                                SubregistroNacimientosRespuesta.ColExtemporaneos.Add(sub);
                            }
                        }

                    }
                    else
                    {
                        throw new EmptyDataException(this.Mensaje);
                    }
                }
            }
            catch (Exception de)
            {
                Bitacora.Error(de.Message);
                if (de is EmptyDataException)
                {
                    throw new DAOException(1, de.Message);
                }
                throw new DAOException(-1, de.Message);
            }

            return SubregistroNacimientosRespuesta;
        }


        public SubregistroNacimientosRespuesta ConsultaSubregistroNacimientos(string anosUnion, string mesesUnion, string municipiosUnion)
        {
            SubregistroNacimientosRespuesta SubregistroNacimientosRespuesta = new SubregistroNacimientosRespuesta();

            try
            {
                using (DataSet dataSet = new DataSet())
                {
                    dataSet.Locale = CultureInfo.InvariantCulture;

                    EjecutaProcedimiento(PRN_CONSULTA_SUBREGISTRO_NACIMIENTOS, CreaParametrosSubregistroNacimientos(anosUnion, mesesUnion, municipiosUnion), dataSet);

                    if (this.Codigo == 0)
                    {
                        //if (dataSet != null && dataSet.Tables.Count > 0
                        //    && dataSet.Tables[0].Rows.Count > 0)
                        //{

                        //    SubregistroNacimientosRespuesta.ColTotales = new Collection<SubregistroTotal>();

                        //    foreach (DataRow r in dataSet.Tables[0].Rows)
                        //    {
                        //        SubregistroTotal sub = new SubregistroTotal
                        //        {
                        //            IdGrupo = r.Field<int>("IdGrupo"),
                        //            NombreGrupo = r.Field<string>("NombreGrupo"),
                        //            Total = r.Field<int>("Total")
                        //        };

                        //        SubregistroNacimientosRespuesta.ColTotales.Add(sub);
                        //    }
                        //}

                        if (dataSet != null && dataSet.Tables.Count > 0
                            && dataSet.Tables[1].Rows.Count > 0)
                        {

                            SubregistroNacimientosRespuesta.ColSubregistros = new Collection<Subregistro>();

                            foreach (DataRow r in dataSet.Tables[1].Rows)
                            {
                                Subregistro sub = new Subregistro();
                                sub.Folio = r.Field<string>("Folio");
                                sub.FechaNacimiento = r.Field<string>("FechaNacimiento");
                                sub.HoraNacimiento = r.Field<string>("HoraNacimiento");
                                sub.SexoId = r.Field<int>("SexoId");
                                sub.SexoDesc = r.Field<string>("SexoDesc");
                                sub.EdoId = r.Field<int>("EdoId");
                                sub.EdoDesc = r.Field<string>("EdoDesc");
                                sub.MpioId = r.Field<int>("MpioId");
                                sub.MpioDesc = r.Field<string>("MpioDesc");
                                sub.LocId = r.Field<int>("LocId");
                                sub.LocDesc = r.Field<string>("LocDesc");
                                sub.Calle = r.Field<string>("Calle");
                                sub.NoExt = r.Field<string>("NoExt");
                                sub.NoInt = r.Field<string>("NoInt");                                
                                sub.Edad = r.Field<int>("Edad");
                                sub.NumNacimiento = r.Field<int>("NumNacimiento");
                                sub.Ocupacion = r.Field<string>("Ocupacion");
                                sub.EdoCivilId = r.Field<int>("EdoCivilId");
                                sub.EdoCivilDesc = r.Field<string>("EdoCivilDesc");
                                sub.EscolId = r.Field<int>("EscolId");
                                sub.EscolDesc = r.Field<string>("EscolDesc");

                                SubregistroNacimientosRespuesta.ColSubregistros.Add(sub);
                            }
                        }

                        if (dataSet != null && dataSet.Tables.Count > 0
                            && dataSet.Tables[2].Rows.Count > 0)
                        {

                            SubregistroNacimientosRespuesta.ColOportunos = new Collection<Subregistro>();

                            foreach (DataRow r in dataSet.Tables[2].Rows)
                            {
                                Subregistro sub = new Subregistro();
                                sub.Folio = r.Field<string>("Folio");
                                sub.FechaNacimiento = r.Field<string>("FechaNacimiento");
                                sub.HoraNacimiento = r.Field<string>("HoraNacimiento");
                                sub.SexoId = r.Field<int>("SexoId");
                                sub.SexoDesc = r.Field<string>("SexoDesc");
                                sub.EdoId = r.Field<int>("EdoId");
                                sub.EdoDesc = r.Field<string>("EdoDesc");
                                sub.MpioId = r.Field<int>("MpioId");
                                sub.MpioDesc = r.Field<string>("MpioDesc");
                                sub.LocId = r.Field<int>("LocId");
                                sub.LocDesc = r.Field<string>("LocDesc");
                                sub.Calle = r.Field<string>("Calle");
                                sub.NoExt = r.Field<string>("NoExt");
                                sub.NoInt = r.Field<string>("NoInt");
                                sub.Edad = r.Field<int>("Edad");
                                sub.NumNacimiento = r.Field<int>("NumNacimiento");
                                sub.Ocupacion = r.Field<string>("Ocupacion");
                                sub.EdoCivilId = r.Field<int>("EdoCivilId");
                                sub.EdoCivilDesc = r.Field<string>("EdoCivilDesc");
                                sub.EscolId = r.Field<int>("EscolId");
                                sub.EscolDesc = r.Field<string>("EscolDesc");

                                SubregistroNacimientosRespuesta.ColOportunos.Add(sub);
                            }
                        }

                        if (dataSet != null && dataSet.Tables.Count > 0
                            && dataSet.Tables[3].Rows.Count > 0)
                        {

                            SubregistroNacimientosRespuesta.ColExtemporaneos = new Collection<Subregistro>();

                            foreach (DataRow r in dataSet.Tables[3].Rows)
                            {
                                Subregistro sub = new Subregistro();
                                sub.Folio = r.Field<string>("Folio");
                                sub.FechaNacimiento = r.Field<string>("FechaNacimiento");
                                sub.HoraNacimiento = r.Field<string>("HoraNacimiento");
                                sub.SexoId = r.Field<int>("SexoId");
                                sub.SexoDesc = r.Field<string>("SexoDesc");
                                sub.EdoId = r.Field<int>("EdoId");
                                sub.EdoDesc = r.Field<string>("EdoDesc");
                                sub.MpioId = r.Field<int>("MpioId");
                                sub.MpioDesc = r.Field<string>("MpioDesc");
                                sub.LocId = r.Field<int>("LocId");
                                sub.LocDesc = r.Field<string>("LocDesc");
                                sub.Calle = r.Field<string>("Calle");
                                sub.NoExt = r.Field<string>("NoExt");
                                sub.NoInt = r.Field<string>("NoInt");
                                sub.Edad = r.Field<int>("Edad");
                                sub.NumNacimiento = r.Field<int>("NumNacimiento");
                                sub.Ocupacion = r.Field<string>("Ocupacion");
                                sub.EdoCivilId = r.Field<int>("EdoCivilId");
                                sub.EdoCivilDesc = r.Field<string>("EdoCivilDesc");
                                sub.EscolId = r.Field<int>("EscolId");
                                sub.EscolDesc = r.Field<string>("EscolDesc");

                                SubregistroNacimientosRespuesta.ColExtemporaneos.Add(sub);
                            }
                        }

                    }
                    else
                    {
                        throw new EmptyDataException(this.Mensaje);
                    }
                }
            }
            catch (Exception de)
            {
                Bitacora.Error(de.Message);
                if (de is EmptyDataException)
                {
                    throw new DAOException(1, de.Message);
                }
                throw new DAOException(-1, de.Message);
            }

            return SubregistroNacimientosRespuesta;
        }

        public XmlDocument ConsultarReporteXMLTotalesSubregistro(string anosUnion, string mesesUnion, string municipiosUnion)
        {
            XmlDocument xmlDocument = null;

            try
            {
                using (DataSet dataSet = new DataSet())
                {
                    dataSet.Locale = CultureInfo.InvariantCulture;

                    EjecutaProcedimiento(PRN_CONSULTA_REPORTE_XML_SUBREGISTRO_MUNICIPIOS, CreaParametrosSubregistroNacimientos(anosUnion, mesesUnion, municipiosUnion), dataSet);

                    if (this.Codigo == 0 && ValidaDataSet(dataSet))
                    {
                        xmlDocument = GetXml(dataSet);
                    }
                    else
                    {
                        throw new EmptyDataException(this.Mensaje);
                    }
                }
            }
            catch (Exception de)
            {
                Bitacora.Error(de.Message);
                if (de is EmptyDataException)
                {
                    throw new DAOException(1, de.Message);
                }
                throw new DAOException(-1, de.Message);
            }

            return xmlDocument;

        }

        public Collection<TotalesMunicipio> ConsultarReporteTotalesMunicipio(string anosUnion, string mesesUnion, string municipiosUnion)
        {
            Collection<TotalesMunicipio> col = null;

            try
            {
                using (DataSet dataSet = new DataSet())
                {
                    dataSet.Locale = CultureInfo.InvariantCulture;

                    EjecutaProcedimiento(PRN_CONSULTA_REPORTE_TOTALES_MUNICIPIOS, CreaParametrosSubregistroNacimientos(anosUnion, mesesUnion, municipiosUnion), dataSet);

                    if (this.Codigo == 0 && ValidaDataSet(dataSet))
                    {
                        col = new Collection<TotalesMunicipio>();
                        foreach (DataRow r in dataSet.Tables[0].Rows)
                        {
                            TotalesMunicipio mpio = new TotalesMunicipio
                            {
                                IdMunicipio = r.Field<int>("IdMunicipio"),
                                MpioDesc = r.Field<string>("MpioDesc"),
                                TotalSubregistro = r.Field<int>("TotalSubregistro"),
                                TotalRegistroOportuno = r.Field<int>("TotalRegistroOportuno"),
                                TotalRegistroExtemporaneo = r.Field<int>("TotalRegistroExtemporaneo")
                            };
                            col.Add(mpio);
                        }
                    }
                    else
                    {
                        throw new EmptyDataException(this.Mensaje);
                    }
                }
            }
            catch (Exception de)
            {
                Bitacora.Error(de.Message);
                if (de is EmptyDataException)
                {
                    throw new DAOException(1, de.Message);
                }
                throw new DAOException(-1, de.Message);
            }

            return col;

        }


        public Collection<DataTable> ConsultarReporteSexoSubregistro(string anosUnion, string mesesUnion, string municipiosUnion)
        {
            Collection<DataTable> dts = new Collection<DataTable>();

            try
            {
                using (DataSet dataSet = new DataSet())
                {
                    dataSet.Locale = CultureInfo.InvariantCulture;

                    EjecutaProcedimiento(PRN_CONSULTA_REPORTE_SEXO_SUBREGISTRO_MUNICIPIOS, CreaParametrosSubregistroNacimientos(anosUnion, mesesUnion, municipiosUnion), dataSet);

                    if (this.Codigo == 0 && (dataSet.Tables.Count > 0))
                    {

                        dts.Add(dataSet.Tables[0]);
                        dts.Add(dataSet.Tables[1]);
                        dts.Add(dataSet.Tables[2]);
                    }
                    else
                    {
                        throw new EmptyDataException(this.Mensaje);
                    }
                }
            }
            catch (Exception de)
            {
                Bitacora.Error(de.Message);
                if (de is EmptyDataException)
                {
                    throw new DAOException(1, de.Message);
                }
                throw new DAOException(-1, de.Message);
            }

            return dts;

        }       


        public Collection<DataTable> ConsultarReporteDSEdoCivilSubregistro(string anosUnion, string mesesUnion, string municipiosUnion)
        {
            Collection<DataTable> dts = new Collection<DataTable>();

            try
            {
                using (DataSet dataSet = new DataSet())
                {
                    dataSet.Locale = CultureInfo.InvariantCulture;

                    EjecutaProcedimiento(PRN_CONSULTA_REPORTE_EDO_CIVIL_SUBREGISTRO_MUNICIPIOS, CreaParametrosSubregistroNacimientos(anosUnion, mesesUnion, municipiosUnion), dataSet);

                    if (this.Codigo == 0 && (dataSet.Tables.Count > 0))
                    {

                        dts.Add(dataSet.Tables[0]);
                        dts.Add(dataSet.Tables[1]);
                        dts.Add(dataSet.Tables[2]);
                    }
                    else
                    {
                        throw new EmptyDataException(this.Mensaje);
                    }
                }
            }
            catch (Exception de)
            {
                Bitacora.Error(de.Message);
                if (de is EmptyDataException)
                {
                    throw new DAOException(1, de.Message);
                }
                throw new DAOException(-1, de.Message);
            }

            return dts;

        }

        public XmlDocument ConsultarReporteEdadSubregistro(string anosUnion, string mesesUnion, string municipiosUnion)
        {
            XmlDocument xmlDocument = null;

            try
            {
                using (DataSet dataSet = new DataSet())
                {
                    dataSet.Locale = CultureInfo.InvariantCulture;

                    EjecutaProcedimiento(PRN_CONSULTA_REPORTE_EDAD_SUBREGISTRO_MUNICIPIOS, CreaParametrosSubregistroNacimientos(anosUnion, mesesUnion, municipiosUnion), dataSet);

                    if (this.Codigo == 0 && (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0))
                    {

                        System.IO.StringWriter writer = new System.IO.StringWriter();
                        dataSet.Tables[0].WriteXml(writer, XmlWriteMode.WriteSchema, false);
                        string result = writer.ToString();

                        xmlDocument = new XmlDocument();
                        xmlDocument.LoadXml(result);
                    }
                    else
                    {
                        throw new EmptyDataException(this.Mensaje);
                    }
                }
            }
            catch (Exception de)
            {
                Bitacora.Error(de.Message);
                if (de is EmptyDataException)
                {
                    throw new DAOException(1, de.Message);
                }
                throw new DAOException(-1, de.Message);
            }

            return xmlDocument;
        }

        public Collection<DataTable> ConsultarReporteDSEdadSubregistro(string anosUnion, string mesesUnion, string municipiosUnion)
        {
            Collection<DataTable> dts = new Collection<DataTable>();

            try
            {
                using (DataSet dataSet = new DataSet())
                {
                    dataSet.Locale = CultureInfo.InvariantCulture;

                    EjecutaProcedimiento(PRN_CONSULTA_REPORTE_EDAD_SUBREGISTRO_MUNICIPIOS, CreaParametrosSubregistroNacimientos(anosUnion, mesesUnion, municipiosUnion), dataSet);

                    if (this.Codigo == 0 && (dataSet.Tables.Count > 0))
                    {

                        dts.Add(dataSet.Tables[0]);
                        dts.Add(dataSet.Tables[1]);
                        dts.Add(dataSet.Tables[2]);
                    }
                    else
                    {
                        throw new EmptyDataException(this.Mensaje);
                    }
                }
            }
            catch (Exception de)
            {
                Bitacora.Error(de.Message);
                if (de is EmptyDataException)
                {
                    throw new DAOException(1, de.Message);
                }
                throw new DAOException(-1, de.Message);
            }

            return dts;
        }

        public Collection<DataTable> ConsultarReporteEscolaridadSubregistro(string anosUnion, string mesesUnion, string municipiosUnion)
        {
            Collection<DataTable> dts = new Collection<DataTable>();

            try
            {
                using (DataSet dataSet = new DataSet())
                {
                    dataSet.Locale = CultureInfo.InvariantCulture;

                    EjecutaProcedimiento(PRN_CONSULTA_REPORTE_ESCOLARIDAD_SUBREGISTRO_MUNICIPIOS, CreaParametrosSubregistroNacimientos(anosUnion, mesesUnion, municipiosUnion), dataSet);

                    if (this.Codigo == 0 && (dataSet.Tables.Count > 0))
                    {

                        dts.Add(dataSet.Tables[0]);
                        dts.Add(dataSet.Tables[1]);
                        dts.Add(dataSet.Tables[2]);
                    }
                    else
                    {
                        throw new EmptyDataException(this.Mensaje);
                    }
                }
            }
            catch (Exception de)
            {
                Bitacora.Error(de.Message);
                if (de is EmptyDataException)
                {
                    throw new DAOException(1, de.Message);
                }
                throw new DAOException(-1, de.Message);
            }

            return dts;

        }

        public Collection<DataTable> ConsultarReporteNumNacSubregistro(string anosUnion, string mesesUnion, string municipiosUnion)
        {
            Collection<DataTable> dts = new Collection<DataTable>();

            try
            {
                using (DataSet dataSet = new DataSet())
                {
                    dataSet.Locale = CultureInfo.InvariantCulture;

                    EjecutaProcedimiento(PRN_CONSULTA_REPORTE_NUM_NAC_SUBREGISTRO_MUNICIPIOS, CreaParametrosSubregistroNacimientos(anosUnion, mesesUnion, municipiosUnion), dataSet);

                    if (this.Codigo == 0 && (dataSet.Tables.Count > 0))
                    {

                        dts.Add(dataSet.Tables[0]);
                        dts.Add(dataSet.Tables[1]);
                        dts.Add(dataSet.Tables[2]);
                    }
                    else
                    {
                        throw new EmptyDataException(this.Mensaje);
                    }
                }
            }
            catch (Exception de)
            {
                Bitacora.Error(de.Message);
                if (de is EmptyDataException)
                {
                    throw new DAOException(1, de.Message);
                }
                throw new DAOException(-1, de.Message);
            }

            return dts;

        }

        #endregion

        #region Métodos Privados
        public static XmlDocument GetXml(DataSet src, params string[] filterString)
        {
            string xmlDataSet = src.GetXml();

            foreach (var filter in filterString)
            {
                xmlDataSet = xmlDataSet.Replace(filter, "");
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlDataSet);

            XmlDocument xmlTable = new XmlDocument();
            xmlTable.LoadXml(xmlDoc.InnerText);

            return xmlTable;
        }

        private static Boolean ValidaDataSet(DataSet dataSet)
        {
            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        private Collection<SqlParameter> CreaParametrosSubregistroNacimientos(string anosUnion, string mesesUnion, string municipiosUnion)
        {
            Collection<SqlParameter> parametros = new Collection<SqlParameter>();
            SqlParameter parametro = null;

            parametro = new SqlParameter("@pc_anos", SqlDbType.NVarChar)
            {
                Size = 255,
                Value = (string.IsNullOrEmpty(anosUnion)) ? (object)DBNull.Value : anosUnion
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_meses", SqlDbType.NVarChar)
            {
                Size = 255,
                Value = (string.IsNullOrEmpty(mesesUnion)) ? (object)DBNull.Value : mesesUnion
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_municipios", SqlDbType.NVarChar)
            {
                Size = 255,
                Value = (string.IsNullOrEmpty(municipiosUnion)) ? (object)DBNull.Value : municipiosUnion
            };
            parametros.Add(parametro);

            CreaParametrosSalida(parametros);

            return parametros;
        }


        #endregion

    }
}
