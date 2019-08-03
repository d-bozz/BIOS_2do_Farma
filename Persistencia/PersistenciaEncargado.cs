using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Entidades_Compartidas;

namespace Persistencia
{
    internal class PersistenciaEncargado : IPersistenciaEncargado
    {
        #region Singleton
        private static PersistenciaEncargado _instancia = null;

        private PersistenciaEncargado()
        {
        }

        public static PersistenciaEncargado GetInstance()
        {
            if (_instancia == null)
                _instancia = new PersistenciaEncargado();

            return _instancia;
        }
        #endregion

        public Encargado Login(string pNombreUsuario, string pContrasena)
        {
            Encargado _unEncargado = null;
            SqlConnection _Conexion = new SqlConnection(Conexion.MiConexionTrusted);
            SqlCommand _Comando = new SqlCommand("LoginEncargado", _Conexion);
            _Comando.CommandType = CommandType.StoredProcedure;
            _Comando.Parameters.AddWithValue("@nombre_usuario", pNombreUsuario);
            _Comando.Parameters.AddWithValue("@contrasena", pContrasena);
            SqlDataReader lector;
            try
            {
                _Conexion.Open();
                lector = _Comando.ExecuteReader();
                if (lector.HasRows)
                {
                    lector.Read();
                    int ci = Convert.ToInt32(lector["ci"]);
                    string contrasena = (string)lector["contrasena"];
                    string nombre_usuario = (string)lector["nombre_usuario"];
                    string nombre_completo = (string)lector["nombre_completo"];
                    int telefono = Convert.ToInt32(lector["telefono"]);
                    _unEncargado = new Encargado(ci, nombre_usuario, contrasena, nombre_completo, telefono);
                    lector.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la base de datos: " + ex.Message);
            }
            finally
            { _Conexion.Close(); }
            return _unEncargado;
        }

        public void AltaEncargado(Encargado nuevoEncargado, Encargado encargadoAutorizante)
        {
            SqlConnection cnn = new SqlConnection(Conexion.MiConexion(encargadoAutorizante));
            try
            {
                SqlCommand cmd = new SqlCommand("AltaEncargado", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ci", nuevoEncargado.Ci);
                cmd.Parameters.AddWithValue("@nombre_usuario", nuevoEncargado.NomUser);
                cmd.Parameters.AddWithValue("@contrasena", nuevoEncargado.Contra);
                cmd.Parameters.AddWithValue("@nombre_completo", nuevoEncargado.NomCom);
                cmd.Parameters.AddWithValue("@telefono", nuevoEncargado.Telefono);
                SqlParameter parmRetorno = new SqlParameter();
                parmRetorno.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(parmRetorno);

                cnn.Open();
                cmd.ExecuteNonQuery();

                int retorno = (int)parmRetorno.Value;

                if (retorno == -1)
                    throw new Exception("El Encargado " + nuevoEncargado.NomCom+ " ya existe.");
                else if(retorno == -2)
                    throw new Exception("Error Crear usuario de Logueo.");
                else if (retorno == -3)
                    throw new Exception("Error Crear usuario de Base de Datos.");
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

        //Deberia ir buscar en Encargados? y en caso de que si, deberia ir public o internal?
        public Encargado BuscarEncargados(int pci, Encargado unEncargado)
        {
            SqlConnection cnn = new SqlConnection(Conexion.MiConexion(unEncargado));

            SqlCommand _Comando = new SqlCommand("BuscarEncargados", cnn);
            _Comando.CommandType = CommandType.StoredProcedure;
            _Comando.Parameters.AddWithValue("@ci", pci);

            SqlDataReader lector;
            Encargado unEnca = null;
            try
            {
                cnn.Open();
                lector = _Comando.ExecuteReader();
                if (lector.HasRows)
                {
                    lector.Read();
                    int ci = (int)lector["ci"];
                    int telefono = (int)lector["telefono"];
                    string nombre_usuario = (string)lector["nombre_usuario"];
                    string contrasena = (string)lector["contrasena"];
                    string nombre_completo = (string)lector["nombre_completo"];


                    unEnca = new Encargado(ci, nombre_usuario, contrasena, nombre_completo, telefono);
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
            return unEnca;
        }

        //public List<Encargado> ListarEncargados(Encargado unEncargado)
        //{
        //    List<Encargado> encargado = new List<Encargado>();
        //    Encargado unEnca = null;

        //    SqlConnection cnn = new SqlConnection(Conexion.MiConexion(unEncargado));
        //    SqlCommand _Comando = new SqlCommand("ListarEncargados", cnn);
        //    _Comando.CommandType = CommandType.StoredProcedure;

        //    SqlDataReader lector;
        //    try
        //    {
        //        cnn.Open();
        //        lector = _Comando.ExecuteReader();
        //        while (lector.Read())
        //        {
        //            unEnca = new Encargado((int)lector["ci"], (string)lector["nombre_usuario"], (string)lector["contrasena"], (string)lector["nombre_completo"], (int)lector["telefono"]);
        //            encargado.Add(unEnca);
        //        }
        //    }
        //    catch (Exception ex)
        //    { throw ex; }
        //    finally
        //    { cnn.Close(); }

        //    return encargado;
        //}

    }
}
