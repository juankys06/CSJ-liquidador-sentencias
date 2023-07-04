using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using liquidador_web.Models;
using Microsoft.Extensions.Configuration;

namespace liquidador_web.Data
{
    public class ConexionDB
    {
        private static IConfiguration con = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        private readonly string cadenaConexion = con["ConnectionStrings:PortalConnection"];
        private readonly string cadenaConexionLiq = con["ConnectionStrings:DefaultConnection"];

        private static string _SQL = string.Empty;
        public List<EntidadDespacho> ListarEntidad() 
        {
            List<EntidadDespacho> entidades = new List<EntidadDespacho>();


            _SQL = "select case when len(codigo)>1 then convert(varchar,CODIGO) else '0'+convert(varchar,CODIGO) end codigo, nombre  from adm_entidad where estado = 1 order by nombre asc";

            using (SqlConnection connection = new SqlConnection(cadenaConexion)) {
                SqlCommand comando = new SqlCommand(_SQL, connection);
                comando.CommandType = System.Data.CommandType.Text;
                try
                {
                    connection.Open();

                    SqlDataReader reader = comando.ExecuteReader();

                    if (reader != null)
                    {
                        while (reader.Read()) 
                        {
                            EntidadDespacho entidad = new EntidadDespacho();
                            entidad.codigo = reader.GetString(0);
                            entidad.nombre = reader.GetString(1);
                            entidades.Add(entidad);
                        }
                        
                    }
                    connection.Close();
                }
                catch (Exception)
                {}
            }
                return entidades;
        }

        public List<DepartamentoDespacho> ListarDepartamentos()
        {
            List<DepartamentoDespacho> departamentos = new List<DepartamentoDespacho>();


            _SQL = "SELECT COD_DANE codigo, NOMBRE nombre FROM ADM_LOCALIZACION where COD_DANE = COD_DANE_DEPARTAMENTO order by nombre asc";

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand(_SQL, connection);
                comando.CommandType = System.Data.CommandType.Text;
                try
                {
                    connection.Open();

                    SqlDataReader reader = comando.ExecuteReader();

                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            DepartamentoDespacho departamento = new DepartamentoDespacho();
                            departamento.codigo = reader.GetString(0);
                            departamento.nombre = reader.GetString(1);
                            departamentos.Add(departamento);
                        }

                    }
                    connection.Close();
                }
                catch (Exception)
                { }
            }
            return departamentos;
        }

        public List<MunicipioDespacho> ListarMunicipios(String departamento)
        {
            List<MunicipioDespacho> municipios = new List<MunicipioDespacho>();


            _SQL = " SELECT COD_DANE codigo, NOMBRE nombre FROM ADM_LOCALIZACION where COD_DANE_DEPARTAMENTO = @departamento and COD_DANE != COD_DANE_DEPARTAMENTO order by nombre asc";

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand(_SQL, connection);
                comando.CommandType = System.Data.CommandType.Text;
                comando.Parameters.AddWithValue("@departamento", departamento);
                try
                {
                    connection.Open();

                    SqlDataReader reader = comando.ExecuteReader();

                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            MunicipioDespacho municipio = new MunicipioDespacho();
                            municipio.codigo = reader.GetString(0);
                            municipio.nombre = reader.GetString(1);
                            municipios.Add(municipio);
                        }

                    }
                    connection.Close();
                }
                catch (Exception)
                { }
            }
            return municipios;
        }

        public List<EspecialidadDespacho> ListarEspecialidad()
        {
            List<EspecialidadDespacho> especialidades = new List<EspecialidadDespacho>();


            _SQL = " SELECT case when len(codigo)>1 then convert(varchar,CODIGO) else '0'+convert(varchar,CODIGO) end codigo, NOMBRE nombre FROM ADM_ESPECIALIDAD where estado = 1 order by nombre asc";

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand(_SQL, connection);
                comando.CommandType = System.Data.CommandType.Text;
                try
                {
                    connection.Open();

                    SqlDataReader reader = comando.ExecuteReader();

                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            EspecialidadDespacho especialidad = new EspecialidadDespacho();
                            especialidad.codigo = reader.GetString(0);
                            especialidad.nombre = reader.GetString(1);
                            especialidades.Add(especialidad);
                        }

                    }
                    connection.Close();
                }
                catch (Exception)
                { }
            }
            return especialidades;
        }

        public List<Despacho> ListarDespachos(string departamento,string municipio, string entidad, string especialidad)
        {
            List<Despacho> despachos = new List<Despacho>();


            _SQL = " SELECT CODIGO codigo, NOMBRE nombre FROM FUN_DESPACHO_INFO where FK_ESTADO_TO_DESPACHO=1";

            if (departamento != null)
                _SQL += " and substring(codigo,1,2) = '"+departamento+"'";
            if (municipio != null)
                _SQL += " and substring(codigo,3,3) = '" + municipio + "'";
            if (entidad != null)
                _SQL += " and substring(codigo,6,2) = '" + entidad + "'";
            if (especialidad != null)
                _SQL += " and substring(codigo,8,2) = '" + especialidad + "'";

            _SQL += " order by nombre asc";

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand(_SQL, connection);
                comando.CommandType = System.Data.CommandType.Text;
                try
                {
                    connection.Open();

                    SqlDataReader reader = comando.ExecuteReader();

                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            Despacho despacho = new Despacho();
                            despacho.codigo = reader.GetString(0);
                            despacho.nombre = reader.GetString(1);
                            despachos.Add(despacho);
                        }

                    }
                    connection.Close();
                }
                catch (Exception)
                { }
            }
            return despachos;
        }

        public string NombreDepartamento(String departamento)
        {
            string respuesta = "";


            _SQL = "select NOMBRE from ADM_LOCALIZACION where COD_DANE = @departamento and COD_DANE_DEPARTAMENTO = @departamento";

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand(_SQL, connection);
                comando.CommandType = System.Data.CommandType.Text;
                comando.Parameters.AddWithValue("@departamento", departamento);
                try
                {
                    connection.Open();

                    SqlDataReader reader = comando.ExecuteReader();

                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            respuesta = reader.GetString(0);
                        }

                    }
                    connection.Close();
                }
                catch (Exception)
                { }
            }
            return respuesta;
        }

        public string NombreMunicipio(String municipio)
        {
            string respuesta = "";


            _SQL = "select NOMBRE from ADM_LOCALIZACION where COD_DANE_DEPARTAMENTO+COD_DANE = @municipio";

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand(_SQL, connection);
                comando.CommandType = System.Data.CommandType.Text;
                comando.Parameters.AddWithValue("@municipio", municipio);
                try
                {
                    connection.Open();

                    SqlDataReader reader = comando.ExecuteReader();

                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            respuesta = reader.GetString(0);
                        }

                    }
                    connection.Close();
                }
                catch (Exception)
                { }
            }
            return respuesta;
        }

        public string NombreEntidad(String entidad)
        {
            string respuesta = "";


            _SQL = "select NOMBRE from ADM_ENTIDAD where CODIGO = @entidad";

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand(_SQL, connection);
                comando.CommandType = System.Data.CommandType.Text;
                comando.Parameters.AddWithValue("@entidad", entidad);
                try
                {
                    connection.Open();

                    SqlDataReader reader = comando.ExecuteReader();

                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            respuesta = reader.GetString(0);
                        }

                    }
                    connection.Close();
                }
                catch (Exception)
                { }
            }
            return respuesta;
        }

        public string NombreEspecialidad(String especialidad)
        {
            string respuesta = "";


            _SQL = "select NOMBRE from ADM_ESPECIALIDAD where CODIGO = @especialidad";

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand(_SQL, connection);
                comando.CommandType = System.Data.CommandType.Text;
                comando.Parameters.AddWithValue("@especialidad", especialidad);
                try
                {
                    connection.Open();

                    SqlDataReader reader = comando.ExecuteReader();

                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            respuesta = reader.GetString(0);
                        }

                    }
                    connection.Close();
                }
                catch (Exception)
                { }
            }
            return respuesta;
        }

        public string NombreDespacho(String codigo)
        {
            string respuesta = "";


            _SQL = "select NOMBRE from FUN_DESPACHO_INFO where CODIGO = @codigo and FK_ESTADO_TO_DESPACHO=1";

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand(_SQL, connection);
                comando.CommandType = System.Data.CommandType.Text;
                comando.Parameters.AddWithValue("@codigo", codigo);
                try
                {
                    connection.Open();

                    SqlDataReader reader = comando.ExecuteReader();

                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            respuesta = reader.GetString(0);
                        }

                    }
                    connection.Close();
                }
                catch (Exception)
                { }
            }
            return respuesta;
        }

        public List<string> ListarDistritos()
        {
            List<string> lista = new List<string>();


            _SQL = "select distinct(NOMBRE) from ADM_DISTRITO where ESTADO = 1 order by NOMBRE asc";

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand(_SQL, connection);
                comando.CommandType = System.Data.CommandType.Text;
                try
                {
                    connection.Open();

                    SqlDataReader reader = comando.ExecuteReader();

                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            lista.Add( reader.GetString(0) );
                        }

                    }
                    connection.Close();
                }
                catch (Exception)
                { }
            }
            return lista;
        }

        public string ObtenerDistrito(string cod)
        {
            string respuesta = "";


            _SQL = "select distri.NOMBRE "+
                    "from ADM_ENTIDAD enti "+
                    "join ADM_MAPA_JUDICIAL mapa on (mapa.FK_DANEMUNI_TO_MAPA = SUBSTRING(@codDespacho, 3, 3) and mapa.FK_DANEDEPAR_TO_MAPA = SUBSTRING(@codDespacho, 1, 2) and mapa.FK_JURIDICCION_TO_MAPA = enti.FK_ENTIDAD_TO_JURIDICCION) " +
                    "join ADM_CIRCUITO circu on(circu.id= mapa.FK_CIRCUITO_TO_MAPA) "+
                    "join ADM_DISTRITO distri on(circu.FK_DISTRITO_TO_CIRCUITO= distri.id) "+
                    "where enti.CODIGO = SUBSTRING(@codDespacho, 6, 2)";

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand(_SQL, connection);
                comando.CommandType = System.Data.CommandType.Text;
                comando.Parameters.AddWithValue("@codDespacho", cod);
                try
                {
                    connection.Open();

                    SqlDataReader reader = comando.ExecuteReader();

                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            respuesta = reader.GetString(0);
                        }

                    }
                    connection.Close();
                }
                catch (Exception)
                { }
            }
            return respuesta;
        }

        public List<incrementoInteranualTasas> ObtenerIncremento(string tipoTasa)
        {
            
            List<incrementoInteranualTasas> lista = new List<incrementoInteranualTasas>();
            tipoTasa = "I" + tipoTasa;

            _SQL = "SELECT convert(int,YEAR(A921VigenteDesde)),A921TipoTasa,A921ValorTasa FROM T921DATASAINTE WHERE A921TipoTasa = @tipoTasa order by 1 asc";

            //using (SqlConnection connection = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=civilcto;Trusted_Connection=True;MultipleActiveResultSets=true; User Id=sa; Password=liquidador"))
            using (SqlConnection connection = new SqlConnection(cadenaConexionLiq))
            {
                SqlCommand comando = new SqlCommand(_SQL, connection);
                comando.CommandType = System.Data.CommandType.Text;
                comando.Parameters.AddWithValue("@tipoTasa", tipoTasa);
                try
                {
                    connection.Open();

                    SqlDataReader reader = comando.ExecuteReader();

                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            incrementoInteranualTasas v = new incrementoInteranualTasas();
                            v.anhio = reader.GetInt32(0);
                            v.tipoTasa = reader.GetString(1);
                            v.incremento = reader.GetDouble(2);
                            lista.Add(v);
                        }

                    }
                    connection.Close();
                }
                catch (Exception)
                { }
            }
            return lista;
        }

    }
}