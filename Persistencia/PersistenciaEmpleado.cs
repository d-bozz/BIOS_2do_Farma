using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_Compartidas;
using System.Data;
using System.Data.SqlClient;

namespace Persistencia
{
    internal class PersistenciaEmpleado : IPersistenciaEmpleado
    {
        #region Singleton
        private static PersistenciaEmpleado _instancia = null;

        private PersistenciaEmpleado()
        {
        }

        public static PersistenciaEmpleado GetInstance()
        {
            if (_instancia == null)
                _instancia = new PersistenciaEmpleado();

            return _instancia;
        }
        #endregion

        public Empleado Login(string pNombreUsuario, string pContrasena)
        {
            Empleado _unEmpleado = null;
            SqlConnection _Conexion = new SqlConnection(Conexion.MiConexionTrusted);
            SqlCommand _Comando = new SqlCommand("LoginEmpleado", _Conexion);
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

                    string horario_inicio = Convert.ToString(lector["horario_inicio"]);
                    string horario_fin = Convert.ToString(lector["horario_fin"]);

                    _unEmpleado = new Empleado(ci,nombre_usuario, contrasena, nombre_completo, horario_inicio, horario_fin);
                    lector.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { _Conexion.Close(); }
            return _unEmpleado;
        }  

        public void AltaEmpleado(Empleado nuevoEmpleado, Encargado encargadoAutorizante)
        {
            SqlConnection cnn = new SqlConnection(Conexion.MiConexion(encargadoAutorizante));
            try
            {
                SqlCommand cmd = new SqlCommand("AltaEmpleado", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ci", nuevoEmpleado.Ci);
                cmd.Parameters.AddWithValue("@nombre_usuario", nuevoEmpleado.NomUser);
                cmd.Parameters.AddWithValue("@contrasena", nuevoEmpleado.Contra);
                cmd.Parameters.AddWithValue("@nombre_completo", nuevoEmpleado.NomCom);
                cmd.Parameters.AddWithValue("@horario_inicio", nuevoEmpleado.Inicio);
                cmd.Parameters.AddWithValue("@horario_fin", nuevoEmpleado.Fin);
                SqlParameter parmRetorno = new SqlParameter();
                parmRetorno.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(parmRetorno);

                cnn.Open();
                cmd.ExecuteNonQuery();

                int retorno = (int)parmRetorno.Value;

                if (retorno == -1)
                    throw new Exception("El Empleado " + nuevoEmpleado.NomCom + " ya existe.");
                else if (retorno == -2)
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

        public void ModificarEmpleado(Empleado nuevoEmpleado, Encargado encargadoAutorizante)
        {
            SqlConnection cnn = new SqlConnection(Conexion.MiConexion(encargadoAutorizante));
            try
            {
                SqlCommand cmd = new SqlCommand("ModificarEmpleado", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ci", nuevoEmpleado.Ci);
                cmd.Parameters.AddWithValue("@nombre_usuario", nuevoEmpleado.NomUser);
                cmd.Parameters.AddWithValue("@contrasena", nuevoEmpleado.Contra);
                cmd.Parameters.AddWithValue("@nombre_completo", nuevoEmpleado.NomCom);
                cmd.Parameters.AddWithValue("@horario_inicio", nuevoEmpleado.Inicio);
                cmd.Parameters.AddWithValue("@horario_fin", nuevoEmpleado.Fin);
                SqlParameter parmRetorno = new SqlParameter();
                parmRetorno.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(parmRetorno);

                cnn.Open();
                cmd.ExecuteNonQuery();

                int retorno = (int)parmRetorno.Value;

                if (retorno == -1)
                    throw new Exception("El Empleado " + nuevoEmpleado.NomCom + " no existe.");
                else if (retorno == -2)
                    throw new Exception("Error al Actualizar el inicio de sesion.");
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

        public void EliminarEmpleado(Empleado nuevoEmpleado, Encargado encargadoAutorizante)
        {
            SqlConnection cnn = new SqlConnection(Conexion.MiConexion(encargadoAutorizante));
            try
            {
                SqlCommand cmd = new SqlCommand("EliminarEmpleado", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ci", nuevoEmpleado.Ci);
                cmd.Parameters.AddWithValue("@nombre_usuario", nuevoEmpleado.NomUser);
                SqlParameter parmRetorno = new SqlParameter();
                parmRetorno.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(parmRetorno);

                cnn.Open();
                cmd.ExecuteNonQuery();

                int retorno = (int)parmRetorno.Value;

                if (retorno == -1)
                    throw new Exception("El Empleado " + nuevoEmpleado.NomCom + " no existe.");
                else if (retorno == -2)
                    throw new Exception("Error al borrar usuario de BD.");
                else if (retorno == -3)
                    throw new Exception("Error al borrar usuario de Logueo.");
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

        internal Empleado BuscarEmpleadosTodos(int pci)
        {
            SqlConnection cnn = new SqlConnection(Conexion.MiConexionTrusted);

            SqlCommand _Comando = new SqlCommand("BuscarEmpleadosTodos", cnn);
            _Comando.CommandType = CommandType.StoredProcedure;
            _Comando.Parameters.AddWithValue("@ci", pci);

            SqlDataReader lector;
            Empleado unEmp = null;
            try
            {
                cnn.Open();
                lector = _Comando.ExecuteReader();
                if (lector.HasRows)
                {
                    lector.Read();
                    int ci = (int)lector["ci"];
                    string horario_inicio = Convert.ToString(lector["horario_inicio"]);
                    string horario_fin = Convert.ToString(lector["horario_fin"]);

                    string nombre_completo = (string)lector["nombre_completo"];
                    string nombre_usuario = (string)lector["nombre_usuario"];
                    string contrasena = (string)lector["contrasena"];




                    unEmp = new Empleado(ci, nombre_usuario, contrasena, nombre_completo, horario_inicio, horario_fin);
                    lector.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                cnn.Close();
            }
            return unEmp;
        }

        public Empleado BuscarEmpleadosActivos(int pci, Encargado encargadoAutorizante)
        {
            SqlConnection cnn = new SqlConnection(Conexion.MiConexion(encargadoAutorizante));

            SqlCommand _Comando = new SqlCommand("BuscarEmpleadosActivos", cnn);
            _Comando.CommandType = CommandType.StoredProcedure;
            _Comando.Parameters.AddWithValue("@ci", pci);

            SqlDataReader lector;
            Empleado unEmp = null;
            try
            {
                cnn.Open();
                lector = _Comando.ExecuteReader();
                if (lector.HasRows)
                {
                    lector.Read();
                    int ci = (int)lector["ci"];
                    string horario_inicio = Convert.ToString(lector["horario_inicio"]);
                    string horario_fin = Convert.ToString(lector["horario_fin"]);
                    string nombre_completo = (string)lector["nombre_completo"];
                    string nombre_usuario = (string)lector["nombre_usuario"];
                    string contrasena = (string)lector["contrasena"];




                    unEmp = new Empleado(ci, nombre_usuario, contrasena, nombre_completo, horario_inicio, horario_fin);
                    lector.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                cnn.Close();
            }
            return unEmp;
        }

        public void AgregaHorasExtras(int pCi, DateTime pFecha, int pMinutos)
        {
            SqlConnection cnn = new SqlConnection(Conexion.MiConexionTrusted);
            try
            {
                SqlCommand cmd = new SqlCommand("AgregaHorasExtras", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ci", pCi);
                cmd.Parameters.AddWithValue("@fecha", pFecha);
                cmd.Parameters.AddWithValue("@minutos", pMinutos);
                SqlParameter parmRetorno = new SqlParameter();
                parmRetorno.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(parmRetorno);

                cnn.Open();
                cmd.ExecuteNonQuery();

                int retorno = (int)parmRetorno.Value;

                if (retorno == -6)
                    throw new Exception("El empleado no existe.");
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

        //public List<Empleado> ListadoEmpleados(Encargado encargadoAutorizante)
        //{
        //    List<Empleado> empleado = new List<Empleado>();
        //    Empleado unEmp = null;

        //    SqlConnection cnn = new SqlConnection(Conexion.MiConexion(encargadoAutorizante));
        //    SqlCommand _Comando = new SqlCommand("ListadoEmpleados", cnn);
        //    _Comando.CommandType = CommandType.StoredProcedure;

        //    SqlDataReader lector;
        //    try
        //    {
        //        cnn.Open();
        //        lector = _Comando.ExecuteReader();
        //        while (lector.Read())
        //        {
        //            unEmp = new Empleado((int)lector["ci"], (string)lector["nombre_usuario"], (string)lector["contrasena"], (string)lector["nombre_completo"], (string)lector["horario_inicio"], (string)lector["horario_fin"]);
        //            empleado.Add(unEmp);
        //        }
        //    }
        //    catch (Exception ex)
        //    { throw ex; }
        //    finally
        //    { cnn.Close(); }

        //    return empleado;
        //}

        //public List<Empleado> ListadoEmpleadosActivos(Encargado encargadoAutorizante)
        //{
        //    List<Empleado> empleado = new List<Empleado>();
        //    Empleado unEmp = null;

        //    SqlConnection cnn = new SqlConnection(Conexion.MiConexion(encargadoAutorizante));
        //    SqlCommand _Comando = new SqlCommand("ListadoEmpleadosActivos", cnn);
        //    _Comando.CommandType = CommandType.StoredProcedure;

        //    SqlDataReader lector;
        //    try
        //    {
        //        cnn.Open();
        //        lector = _Comando.ExecuteReader();
        //        while (lector.Read())
        //        {
        //            unEmp = new Empleado((int)lector["ci"], (string)lector["nombre_usuario"], (string)lector["contrasena"], (string)lector["nombre_completo"], (string)lector["horario_inicio"], (string)lector["horario_fin"]);
        //            empleado.Add(unEmp);
        //        }
        //    }
        //    catch (Exception ex)
        //    { throw ex; }
        //    finally
        //    { cnn.Close(); }

        //    return empleado;
        //}

    }
}
