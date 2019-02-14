using Newtonsoft.Json;
using SadenaFenix.Commons.Utilerias;
using SadenaFenix.Excepcions;
using SadenaFenix.Models.Catalogos.Geografia;
using SadenaFenix.Models.Catalogos.Socieconomica;
using SadenaFenix.Models.Catalogos.Socioeconomica;
using SadenaFenix.Models.Catalogos.Tiempo;
using SadenaFenix.Persistence;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace SadenaFenix.Daos.Catalogos
{
    public class CatalogosDAO : DataContext
    {
        #region Variables de instancia
        private const string PRS_CT_ESTATUS_REGISTRO = "SDB.PRSCTEstatusRegistro";
        private const string PRS_CT_SEXO = "SDB.PRSCTSexo";
        private const string PRS_CT_ESTADO_CIVIL = "SDB.PRSCTEdoCivil";
        private const string PRS_CT_ESCOLARIDAD = "SDB.PRSCTEscolaridad";
        private const string PRS_CT_MUNICIPIO = "SDB.PRSCTMunicipio";
        private const string PRS_CT_LOCALIDAD = "SDB.PRSCTLocalidad";
        private const string PRS_CT_LOCALIDAD_COAHUILA = "SDB.PRSCTLocalidadCoahuila";
        private const string PRS_CONSULTA_MESES_X_ANIO = "SDB.PRSConsultaMesesXAnio";


        #endregion

        #region Métodos Públicos
        public Collection<EstatusRegistro> ConsultaCatEstatusRegistro()
        {

            Collection<EstatusRegistro> colEstatusRegistro = null;

            try
            {
                using (DataSet dataSet = new DataSet())
                {
                    dataSet.Locale = CultureInfo.InvariantCulture;

                    Collection<SqlParameter> parametrosCatEstatusRegistro = new Collection<SqlParameter>();
                    CreaParametrosSalida(parametrosCatEstatusRegistro);

                    EjecutaProcedimiento(PRS_CT_ESTATUS_REGISTRO, parametrosCatEstatusRegistro, dataSet);

                    if (this.Codigo == 0 && validaDataSet(dataSet))
                    {
                        colEstatusRegistro = new Collection<EstatusRegistro>();
                        foreach (DataRow r in dataSet.Tables[0].Rows)
                        {
                            EstatusRegistro statusRegistro = new EstatusRegistro
                            {
                                EstatusRegistroId = r.Field<int>("EstatusRegistroId"),
                                EstatusRegistroDesc = r.Field<string>("EstatusRegistroDesc")
                            };
                            colEstatusRegistro.Add(statusRegistro);
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

            return colEstatusRegistro;
        }

        public Collection<Mes> ConsultarMesesXAnio(string anio)
        {
            Collection<Mes> colMeses = null;

            try
            {
                using (DataSet dataSet = new DataSet())
                {
                    dataSet.Locale = CultureInfo.InvariantCulture;

                    EjecutaProcedimiento(PRS_CONSULTA_MESES_X_ANIO, CreaParametrosMesesXAnio(anio), dataSet);

                    if (this.Codigo == 0 && validaDataSet(dataSet))
                    {
                        colMeses = new Collection<Mes>();
                        foreach (DataRow r in dataSet.Tables[0].Rows)
                        {
                            Mes mes = new Mes
                            {
                                MesId = r.Field<int>("MesId"),
                                MesDesc = r.Field<string>("MesDesc")
                            };
                            colMeses.Add(mes);
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

            return colMeses;
        }

        public Collection<Sexo> ConsultaCatSexo()
        {

            Collection<Sexo> colSexo = null;

            try
            {
                using (DataSet dataSet = new DataSet())
                {
                    dataSet.Locale = CultureInfo.InvariantCulture;

                    Collection<SqlParameter> parametrosCatSexo = new Collection<SqlParameter>();
                    CreaParametrosSalida(parametrosCatSexo);

                    EjecutaProcedimiento(PRS_CT_SEXO, parametrosCatSexo, dataSet);

                    if (this.Codigo == 0 && validaDataSet(dataSet))
                    {
                        colSexo = new Collection<Sexo>();
                        foreach (DataRow r in dataSet.Tables[0].Rows)
                        {
                            Sexo sexo = new Sexo
                            {
                                SexoId = r.Field<int>("SexoId"),
                                SexoDesc = r.Field<string>("SexoDesc")
                            };
                            colSexo.Add(sexo);
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

            return colSexo;
        }

        public Collection<EdoCivil> ConsultaCatEdoCivil()
        {

            Collection<EdoCivil> colEdoCivil = null;

            try
            {
                using (DataSet dataSet = new DataSet())
                {
                    dataSet.Locale = CultureInfo.InvariantCulture;

                    Collection<SqlParameter> parametrosCatEdoCivil = new Collection<SqlParameter>();
                    CreaParametrosSalida(parametrosCatEdoCivil);

                    EjecutaProcedimiento(PRS_CT_ESTADO_CIVIL, parametrosCatEdoCivil, dataSet);

                    if (this.Codigo == 0 && validaDataSet(dataSet))
                    {
                        colEdoCivil = new Collection<EdoCivil>();
                        foreach (DataRow r in dataSet.Tables[0].Rows)
                        {
                            EdoCivil edoCivil = new EdoCivil
                            {
                                EdoCivilId = r.Field<int>("EdoCivilId"),
                                EdoCivilDesc = r.Field<string>("EdoCivilDesc")
                            };
                            colEdoCivil.Add(edoCivil);
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

            return colEdoCivil;
        }

        public Collection<Escolaridad> ConsultaCatEscolaridad()
        {

            Collection<Escolaridad> colEscolaridad = null;

            try
            {
                using (DataSet dataSet = new DataSet())
                {
                    dataSet.Locale = CultureInfo.InvariantCulture;

                    Collection<SqlParameter> parametrosCatEscolaridad = new Collection<SqlParameter>();
                    CreaParametrosSalida(parametrosCatEscolaridad);

                    EjecutaProcedimiento(PRS_CT_ESCOLARIDAD, parametrosCatEscolaridad, dataSet);

                    if (this.Codigo == 0 && validaDataSet(dataSet))
                    {
                        colEscolaridad = new Collection<Escolaridad>();
                        foreach (DataRow r in dataSet.Tables[0].Rows)
                        {
                            Escolaridad escol = new Escolaridad
                            {
                                EscolaridadId = r.Field<int>("EscolId"),
                                EscolaridadDesc = r.Field<string>("EscolDesc")
                            };
                            colEscolaridad.Add(escol);
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

            return colEscolaridad;
        }

        public Collection<Municipio> ConsultaCatMunicipio()
        {

            Collection<Municipio> colMunicipio = null;

            try
            {
                using (DataSet dataSet = new DataSet())
                {
                    dataSet.Locale = CultureInfo.InvariantCulture;

                    Collection<SqlParameter> parametrosCatMunicipio = new Collection<SqlParameter>();
                    CreaParametrosSalida(parametrosCatMunicipio);

                    EjecutaProcedimiento(PRS_CT_MUNICIPIO, parametrosCatMunicipio, dataSet);

                    if (this.Codigo == 0 && validaDataSet(dataSet))
                    {
                        colMunicipio = new Collection<Municipio>();
                        foreach (DataRow r in dataSet.Tables[0].Rows)
                        {
                            Municipio mpio = new Municipio
                            {
                                MpioId = r.Field<int>("MpioId"),
                                MpioDesc = r.Field<string>("MpioDesc")
                            };
                            colMunicipio.Add(mpio);
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

            return colMunicipio;
        }

        public Collection<Municipio> ConsultaCatPoligonoMunicipio()
        {

            Collection<Municipio> colMunicipio = null;

            try
            {
                using (DataSet dataSet = new DataSet())
                {
                    dataSet.Locale = CultureInfo.InvariantCulture;

                    Collection<SqlParameter> parametrosCatMunicipio = new Collection<SqlParameter>();
                    CreaParametrosSalida(parametrosCatMunicipio);

                    EjecutaProcedimiento(PRS_CT_MUNICIPIO, parametrosCatMunicipio, dataSet);

                    if (this.Codigo == 0 && validaDataSet(dataSet))
                    {
                        colMunicipio = new Collection<Municipio>();
                        foreach (DataRow r in dataSet.Tables[0].Rows)
                        {

                            Collection<Geopunto> geopuntos = new Collection<Geopunto>();
                            string poligono = r.Field<string>("Poligono");

                            //SqlGeography geoPoligono = SqlGeography.Parse(poligono);

                            //for (int i = 1; i <= geoPoligono.STNumPoints(); i++)
                            //{
                            //    SqlGeography point = geoPoligono.STPointN(i);
                            //    Geopunto geopunto = new Geopunto
                            //    {
                            //        Latitud = (double)point.Lat,
                            //        Longitud = (double)point.Long
                            //    };
                            //    geopuntos.Add(geopunto);
                            //    //poligono += point.Long + "," + point.Lat + " ";
                            //}

                            Municipio mpio = new Municipio
                            {
                                MpioId = r.Field<int>("MpioId"),
                                MpioDesc = r.Field<string>("MpioDesc"),
                                Longitud = r.Field<string>("Longitud"),
                                Latitud = r.Field<string>("Latitud"),
                                //JsonPoligono = JsonConvert.SerializeObject(geopuntos)
                            };

                            colMunicipio.Add(mpio);
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

            return colMunicipio;
        }

        public Collection<Localidad> ConsultaCatLocalidad()
        {

            Collection<Localidad> colLocalidad = null;

            try
            {
                using (DataSet dataSet = new DataSet())
                {
                    dataSet.Locale = CultureInfo.InvariantCulture;

                    Collection<SqlParameter> parametrosCatLocalidad = new Collection<SqlParameter>();
                    CreaParametrosSalida(parametrosCatLocalidad);

                    EjecutaProcedimiento(PRS_CT_LOCALIDAD, parametrosCatLocalidad, dataSet);

                    if (this.Codigo == 0 && validaDataSet(dataSet))
                    {
                        colLocalidad = new Collection<Localidad>();
                        foreach (DataRow r in dataSet.Tables[0].Rows)
                        {
                            Localidad loc = new Localidad
                            {
                                LocEdoId = r.Field<int>("LocEdoId"),
                                LocEdoDesc = r.Field<string>("LocEdoDesc"),
                                LocMpioId = r.Field<int>("LocMpioId"),
                                LocMpioDesc = r.Field<string>("LocMpioDesc"),
                                LocId = r.Field<int>("LocId"),
                                LocDesc = r.Field<string>("LocDesc")
                            };
                            colLocalidad.Add(loc);
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

            return colLocalidad;
        }

        public Collection<Localidad> ConsultaCatLocalidadCoahuila()
        {

            Collection<Localidad> colLocalidad = null;

            try
            {
                using (DataSet dataSet = new DataSet())
                {
                    dataSet.Locale = CultureInfo.InvariantCulture;

                    Collection<SqlParameter> parametrosCatLocalidad = new Collection<SqlParameter>();
                    CreaParametrosSalida(parametrosCatLocalidad);

                    EjecutaProcedimiento(PRS_CT_LOCALIDAD_COAHUILA, parametrosCatLocalidad, dataSet);

                    if (this.Codigo == 0 && validaDataSet(dataSet))
                    {
                        colLocalidad = new Collection<Localidad>();
                        foreach (DataRow r in dataSet.Tables[0].Rows)
                        {
                            Localidad loc = new Localidad
                            {
                                LocEdoId = r.Field<int>("LocEdoId"),
                                LocEdoDesc = r.Field<string>("LocEdoDesc"),
                                LocMpioId = r.Field<int>("LocMpioId"),
                                LocMpioDesc = r.Field<string>("LocMpioDesc"),
                                LocId = r.Field<int>("LocId"),
                                LocDesc = r.Field<string>("LocDesc")
                            };
                            colLocalidad.Add(loc);
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

            return colLocalidad;
        }

        private static Boolean validaDataSet(DataSet dataSet)
        {
            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }



        #endregion

        #region Métodos Privados

        private static Collection<SqlParameter> CreaParametrosMesesXAnio(string anio)
        {
            Collection<SqlParameter> parametros = new Collection<SqlParameter>();
            SqlParameter parametro = new SqlParameter("@pc_ano", SqlDbType.NVarChar)
            {
                Size = 4,
                Value = anio
            };
            parametros.Add(parametro);

            CreaParametrosSalida(parametros);

            return parametros;
        }

        #endregion

    }
}