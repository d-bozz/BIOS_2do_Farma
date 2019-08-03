using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_Compartidas;
using System.Data;
using System.Data.SqlClient;

namespace Persistencia
{
    internal class PersistenciaMedicamentos : IPersistenciaMedicamentos
    {
        //Singleton*************************************
        private static PersistenciaMedicamentos _instancia = null;

        private PersistenciaMedicamentos()
        {
        }

        public static PersistenciaMedicamentos GetInstance()
        {
            if (_instancia == null)
                _instancia = new PersistenciaMedicamentos();
            return _instancia;
        }
        //************************************************

        public void AgregarMedicamentos(Medicamento M, Encargado E)
        {
            SqlConnection cnn = new SqlConnection(Conexion.MiConexion(E));
            try
            {
                SqlCommand cmd = new SqlCommand("AltaMedicamentos", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre_farma", M.UnaFarmaceutica.NomFar);
                cmd.Parameters.AddWithValue("@codigo", M.Codigo);
                cmd.Parameters.AddWithValue("@nombre", M.NomMed);
                cmd.Parameters.AddWithValue("@descripcion", M.Desc);
                cmd.Parameters.AddWithValue("@precio", M.Precio);
                cmd.Parameters.AddWithValue("@tipo", M.Tipo);
                cmd.Parameters.AddWithValue("@stock", M.Stock);
                SqlParameter parmRetorno = new SqlParameter();
                parmRetorno.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(parmRetorno);

                cnn.Open();
                cmd.ExecuteNonQuery();

                int retorno = (int)parmRetorno.Value;

                if (retorno == -1)
                    throw new Exception("El medicamento " + M.NomMed + " ya existe en la Base de datos.");
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

        public void EliminarMedicamentos(Medicamento M, Encargado E)
        { 
             SqlConnection cnn = new SqlConnection(Conexion.MiConexion(E));
             try
             {
                 SqlCommand cmd = new SqlCommand("EliminarMedicamentos", cnn);
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.Parameters.AddWithValue("@nombre_farma", M.UnaFarmaceutica.NomFar);
                 cmd.Parameters.AddWithValue("@codigo", M.Codigo);
                 SqlParameter parmRetorno = new SqlParameter();
                 parmRetorno.Direction = ParameterDirection.ReturnValue;
                 cmd.Parameters.Add(parmRetorno);

                 cnn.Open();
                 cmd.ExecuteNonQuery();

                 int retorno = (int)parmRetorno.Value;

                 if (retorno == -1)
                     throw new Exception("El medicamento " + M.NomMed + " no existe.");
                 else if (retorno == -6)
                     throw new Exception("Ocurrio un error con la Base de Datos, contacte con el Administrador.");
             }
             catch (Exception ex)
             { throw ex; }
             finally
             { cnn.Close(); }
        }

        public void ModificarMedicamentos(Medicamento M, Encargado E)
        {
            SqlConnection cnn = new SqlConnection(Conexion.MiConexion(E));
            try
            {
                SqlCommand cmd = new SqlCommand("ModificarMedicamentos", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre_farma", M.UnaFarmaceutica.NomFar);
                cmd.Parameters.AddWithValue("@codigo", M.Codigo);
                cmd.Parameters.AddWithValue("@nombre", M.NomMed);
                cmd.Parameters.AddWithValue("@descripcion", M.Desc);
                cmd.Parameters.AddWithValue("@precio", M.Precio);
                cmd.Parameters.AddWithValue("@tipo", M.Tipo);

                SqlParameter parmRetorno = new SqlParameter();
                parmRetorno.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(parmRetorno);

                cnn.Open();
                cmd.ExecuteNonQuery();

                int retorno = (int)parmRetorno.Value;

                if (retorno == -1)
                    throw new Exception("El medicamento " + M.NomMed + " no existe.");
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

        public List<Medicamento> MedicamentosStock()
        {
            List<Medicamento> medicamento = new List<Medicamento>();
            Medicamento unMedi = null;
            Farmaceutica unaFarmaceutica;

            SqlConnection cnn = new SqlConnection(Conexion.MiConexionTrusted);
            SqlCommand _Comando = new SqlCommand("MedicamentosEnStock", cnn);
            _Comando.CommandType = CommandType.StoredProcedure;

            SqlDataReader lector;
            try
            {
                cnn.Open();
                lector = _Comando.ExecuteReader();
                while (lector.Read())
                {                    
                    unaFarmaceutica = PersistenciaFarmaceutica.GetInstance().BuscarFarmaceuticaTodas((string)lector["nombre_farma"]);
                    unMedi = new Medicamento(unaFarmaceutica, (int)lector["codigo"], (string)lector["nombre"], (string)lector["descripcion"], (int)lector["precio"], (string)lector["tipo"], (int)lector["stock"]);
                    medicamento.Add(unMedi);
                }
            }
            catch (Exception ex)
            { throw ex; }
            finally
            { cnn.Close(); }

            return medicamento;
        }

        public Medicamento BuscarMedicamentosActivos(Farmaceutica pnombreF, int pcodigo, Usuario unUsuario)
        {
            SqlConnection cnn = new SqlConnection(Conexion.MiConexion(unUsuario));

            SqlCommand _Comando = new SqlCommand("BuscarMedicamentosActivos", cnn);
            _Comando.CommandType = CommandType.StoredProcedure;
            _Comando.Parameters.AddWithValue("@nombre_farma", pnombreF.NomFar);
            _Comando.Parameters.AddWithValue("@codigo", pcodigo);

            SqlDataReader lector;
            Medicamento unMedi = null;
            try
            {
                cnn.Open();
                lector = _Comando.ExecuteReader();
                if (lector.HasRows)
                {
                    lector.Read();
                    string nombre_farma = (string)lector["nombre_farma"];
                    Farmaceutica unaFarmaceutica = PersistenciaFarmaceutica.GetInstance().BuscarFarmaceuticaTodas(nombre_farma);

                    int codigo = (int)lector["codigo"];
                    string nombre = (string)lector["nombre"];
                    string descripcion = (string)lector["descripcion"];
                    int precio = (int)lector["precio"];
                    string tipo = (string)lector["tipo"];
                    int stock = (int)lector["stock"];

                    unMedi = new Medicamento(unaFarmaceutica, codigo, nombre, descripcion, precio, tipo, stock);
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
            return unMedi;
        }

        internal Medicamento BuscarMedicamentosTodos(Farmaceutica pnombreF, int pcodigo)
        {
            SqlConnection cnn = new SqlConnection(Conexion.MiConexionTrusted);

            SqlCommand _Comando = new SqlCommand("BuscarMedicamentosTodos", cnn);
            _Comando.CommandType = CommandType.StoredProcedure;
            _Comando.Parameters.AddWithValue("@nombre_farma", pnombreF.NomFar);
            _Comando.Parameters.AddWithValue("@codigo", pcodigo);

            SqlDataReader lector;
            Medicamento unMedi = null;
            try
            {
                cnn.Open();
                lector = _Comando.ExecuteReader();
                if (lector.HasRows)
                {
                    lector.Read();
                    string nombre_farma = (string)lector["nombre_farma"];
                    Farmaceutica unaFarmaceutica = PersistenciaFarmaceutica.GetInstance().BuscarFarmaceuticaTodas(nombre_farma);

                    int codigo = (int)lector["codigo"];
                    string nombre = (string)lector["nombre"];
                    string descripcion = (string)lector["descripcion"];
                    int precio = (int)lector["precio"];
                    string tipo = (string)lector["tipo"];
                    int stock = (int)lector["stock"];

                    unMedi = new Medicamento(unaFarmaceutica, codigo, nombre, descripcion, precio, tipo, stock);
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
            return unMedi;
        }

        public List<Medicamento> ListarMedicamentosActivos(Encargado unEncargado)
        {
            List<Medicamento> medicamento = new List<Medicamento>();
            Medicamento unMedi = null;
            Farmaceutica unaFarmaceutica;

            SqlConnection cnn = new SqlConnection(Conexion.MiConexion(unEncargado));
            SqlCommand _Comando = new SqlCommand("ListarMedicamentosActivos", cnn);
            _Comando.CommandType = CommandType.StoredProcedure;

            SqlDataReader lector;
            try
            {
                cnn.Open();
                lector = _Comando.ExecuteReader();
                while (lector.Read())
                {
                    unaFarmaceutica = PersistenciaFarmaceutica.GetInstance().BuscarFarmaceuticaTodas((string)lector["nombre_farma"]);

                    if (unaFarmaceutica == null)
                        throw new Exception("Ocurrio un error, intente nuevamente.");

                    unMedi = new Medicamento(unaFarmaceutica, (int)lector["codigo"], (string)lector["nombre"], (string)lector["descripcion"], (int)lector["precio"], (string)lector["tipo"], (int)lector["stock"]);

                    medicamento.Add(unMedi);
                }
            }
            catch (Exception ex)
            { throw ex; }
            finally
            { cnn.Close(); }

            return medicamento;
        }

        internal List<Medicamento> ListarMedicamentos(Encargado unEncargado)
        {
            List<Medicamento> medicamento = new List<Medicamento>();
            Medicamento unMedi = null;
            Farmaceutica unaFarmaceutica;

            SqlConnection cnn = new SqlConnection(Conexion.MiConexion(unEncargado));
            SqlCommand _Comando = new SqlCommand("ListarMedicamentos", cnn);
            _Comando.CommandType = CommandType.StoredProcedure;

            SqlDataReader lector;
            try
            {
                cnn.Open();
                lector = _Comando.ExecuteReader();
                while (lector.Read())
                {
                    unaFarmaceutica = PersistenciaFarmaceutica.GetInstance().BuscarFarmaceuticaTodas((string)lector["nombre_farma"]);
                    
                    if (unaFarmaceutica == null)
                        throw new Exception("Ocurrio un error, intente nuevamente.");

                    unMedi = new Medicamento(unaFarmaceutica, (int)lector["codigo"], (string)lector["nombre"], (string)lector["descripcion"], (int)lector["precio"], (string)lector["tipo"], (int)lector["stock"]);
                    medicamento.Add(unMedi);
                }
            }
            catch (Exception ex)
            { throw ex; }
            finally
            { cnn.Close(); }

            return medicamento;
        }

    }
}
