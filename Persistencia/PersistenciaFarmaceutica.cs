using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_Compartidas;
using System.Data;
using System.Data.SqlClient;

namespace Persistencia
{
    internal class PersistenciaFarmaceutica : IPersistenciaFarmaceutica
    {
        #region Singleton
        private static PersistenciaFarmaceutica _instancia = null;

        private PersistenciaFarmaceutica()
        {
        }

        public static PersistenciaFarmaceutica GetInstance()
        {
            if (_instancia == null)
                _instancia = new PersistenciaFarmaceutica();

            return _instancia;
        }
        #endregion

        public void AltaFarmaceutica(Farmaceutica unaFarmaceutica, Encargado unEncargado)
        {
            SqlConnection cnn = new SqlConnection(Conexion.MiConexion(unEncargado));
            try
            {
                SqlCommand cmd = new SqlCommand("AltaFarmaceutica", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre", unaFarmaceutica.NomFar);
                cmd.Parameters.AddWithValue("@direccion_fiscal", unaFarmaceutica.DirFisc);
                cmd.Parameters.AddWithValue("@telefono", unaFarmaceutica.Telefono);
                cmd.Parameters.AddWithValue("@correo", unaFarmaceutica.Correo);
                SqlParameter parmRetorno = new SqlParameter();
                parmRetorno.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(parmRetorno);

                cnn.Open();
                cmd.ExecuteNonQuery();

                int retorno = (int)parmRetorno.Value;

                if (retorno == -1)
                    throw new Exception("La Farmaceutica " + unaFarmaceutica.NomFar + " ya existe.");
                else if (retorno == -6)
                    throw new Exception("Ocurrio un error con la Base de Datos, contacte con el Administrador.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }

        public void EliminarFarmaceutica(Farmaceutica unaFarmaceutica, Encargado unEncargado)
        {
            SqlConnection cnn = new SqlConnection(Conexion.MiConexion(unEncargado));
            try
            {
                SqlCommand cmd = new SqlCommand("EliminarFarmaceutica", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre", unaFarmaceutica.NomFar);
                SqlParameter parmRetorno = new SqlParameter();
                parmRetorno.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(parmRetorno);

                cnn.Open();
                cmd.ExecuteNonQuery();

                int retorno = (int)parmRetorno.Value;

                if (retorno == -1)
                    throw new Exception("La Farmaceutica " + unaFarmaceutica.NomFar + " no existe.");
                else if (retorno == -6)
                    throw new Exception("Ocurrio un error con la Base de Datos, contacte con el Administrador.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }

        public void ModificarFarmaceutica(Farmaceutica unaFarmaceutica, Encargado unEncargado)
        {
            SqlConnection cnn = new SqlConnection(Conexion.MiConexion(unEncargado));
            try
            {
                SqlCommand cmd = new SqlCommand("ModificarFarmaceutica", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre", unaFarmaceutica.NomFar);
                cmd.Parameters.AddWithValue("@direccion_fiscal", unaFarmaceutica.DirFisc);
                cmd.Parameters.AddWithValue("@telefono", unaFarmaceutica.Telefono);
                cmd.Parameters.AddWithValue("@correo", unaFarmaceutica.Correo);
                SqlParameter parmRetorno = new SqlParameter();
                parmRetorno.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(parmRetorno);

                cnn.Open();
                cmd.ExecuteNonQuery();

                int retorno = (int)parmRetorno.Value;

                if (retorno == -1)
                    throw new Exception("La Farmaceutica " + unaFarmaceutica.NomFar + " no existe.");
                else if (retorno == -6)
                    throw new Exception("Ocurrio un error con la Base de Datos, contacte con el Administrador.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }

        public Farmaceutica BuscarFarmaceuticaActiva(string pnombre, Usuario unUsuario)
        {
            SqlConnection cnn = new SqlConnection(Conexion.MiConexion(unUsuario));

            SqlCommand _Comando = new SqlCommand("BuscarFarmaceuticaActiva", cnn);
            _Comando.CommandType = CommandType.StoredProcedure;
            _Comando.Parameters.AddWithValue("@nombre", pnombre);
            SqlDataReader lector;
            Farmaceutica unaFarma = null;
            try
            {
                cnn.Open();
                lector = _Comando.ExecuteReader();
                if (lector.HasRows)
                {
                    lector.Read();
                    string nombre = (string)lector["nombre"];
                    string direccion_fiscal = (string)lector["direccion_fiscal"];
                    int telefono = (int)lector["telefono"];
                    string correo = (string)lector["correo"];

                    unaFarma = new Farmaceutica(nombre, direccion_fiscal, telefono, correo);
                    lector.Close();
                }

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Problemas con la base de datos:" + ex.Message);
            }

            finally
            {
                cnn.Close();
            }
            return unaFarma;
        }

        //Le saco el usuario para iniciar sesion, porque se usa desde la WEB.
        internal Farmaceutica BuscarFarmaceuticaTodas(string pnombre)
        {
            SqlConnection cnn = new SqlConnection(Conexion.MiConexionTrusted);

            SqlCommand _Comando = new SqlCommand("BuscarFarmaceuticaTodas", cnn);
            _Comando.CommandType = CommandType.StoredProcedure;
            _Comando.Parameters.AddWithValue("@nombre", pnombre);
            SqlDataReader lector;
            Farmaceutica unaFarma = null;
            try
            {
                cnn.Open();
                lector = _Comando.ExecuteReader();
                if (lector.HasRows)
                {
                    lector.Read();
                    string nombre = (string)lector["nombre"];
                    string direccion_fiscal = (string)lector["direccion_fiscal"];
                    int telefono = (int)lector["telefono"];
                    string correo = (string)lector["correo"];

                    unaFarma = new Farmaceutica(nombre, direccion_fiscal, telefono, correo);
                    lector.Close();
                }

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Problemas con la base de datos:" + ex.Message);
            }

            finally
            {
                cnn.Close();
            }
            return unaFarma;
        }

        public List<Farmaceutica> ListarFarmaceuticasActivas(Encargado unEncargado)
        {
            List<Farmaceutica> farma = new List<Farmaceutica>();
            Farmaceutica unaFarma = null;

            SqlConnection cnn = new SqlConnection(Conexion.MiConexion(unEncargado));
            SqlCommand _Comando = new SqlCommand("ListarFarmaceuticasActivas", cnn);
            _Comando.CommandType = CommandType.StoredProcedure;

            SqlDataReader lector;
            try
            {
                cnn.Open();
                lector = _Comando.ExecuteReader();
                while (lector.Read())
                {
                    unaFarma = new Farmaceutica((string)lector["nombre"], (string)lector["direccion_fiscal"], (int)lector["telefono"], (string)lector["correo"]);
                    farma.Add(unaFarma);
                }
            }
            catch (Exception ex)
            { throw ex; }
            finally
            { cnn.Close(); }

            return farma;
        }

        internal List<Farmaceutica> ListarFarmaceuticas(Encargado unEncargado)
        {
            List<Farmaceutica> farma = new List<Farmaceutica>();
            Farmaceutica unaFarma = null;

            SqlConnection cnn = new SqlConnection(Conexion.MiConexion(unEncargado));
            SqlCommand _Comando = new SqlCommand("ListarFarmaceuticas", cnn);
            _Comando.CommandType = CommandType.StoredProcedure;

            SqlDataReader lector;
            try
            {
                cnn.Open();
                lector = _Comando.ExecuteReader();
                while (lector.Read())
                {
                    unaFarma = new Farmaceutica((string)lector["nombre"], (string)lector["direccion_fiscal"], (int)lector["telefono"], (string)lector["correo"]);
                    farma.Add(unaFarma);
                }
            }
            catch (Exception ex)
            { throw ex; }
            finally
            { cnn.Close(); }

            return farma;
        }
    
    }
}