using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_Compartidas;
using System.Data;
using System.Data.SqlClient;


namespace Persistencia
{
    internal class PersistenciaPedidoCabezal : IPersistenciaPedidoCabezal
    {
        //Singleton*************************************
        private static PersistenciaPedidoCabezal _instancia = null;

        private PersistenciaPedidoCabezal()
        {
        }

        public static PersistenciaPedidoCabezal GetInstance()
        {
            if (_instancia == null)
                _instancia = new PersistenciaPedidoCabezal();
            return _instancia;
        }
        //************************************************

        //Faltaria listado anio en curso

        public List<PedidoCabezal> ListaPedidosEsteAno(Encargado pEncargado)
        {
            List<PedidoCabezal> listaPedidos = new List<PedidoCabezal>();
            List<PedidosLinea> _lasLineas = null;
            PedidoCabezal _unPedido = null;
            Empleado _unEmpleado = null;
            SqlConnection _Conexion = new SqlConnection(Conexion.MiConexion(pEncargado));
            SqlCommand _Comando = new SqlCommand("ListaPedidosEsteAno", _Conexion);
            _Comando.CommandType = CommandType.StoredProcedure;
            SqlDataReader lector;

            try
            {
                _Conexion.Open();
                lector = _Comando.ExecuteReader();
                while (lector.Read())
                {
                    int numero = Convert.ToInt32(lector["numero"]);
                    DateTime fechaRealizado = Convert.ToDateTime(lector["fecha_realizado"]);
                    string direccion_entrega = (string)lector["direccion_entrega"];
                    string estado = (string)lector["estado"];
                    int ci_empleado = Convert.ToInt32(lector["ci_empleado"]);

                    _unEmpleado = PersistenciaEmpleado.GetInstance().BuscarEmpleadosTodos(ci_empleado);

                    if (_unEmpleado == null)
                        throw new Exception("Ocurrio un error, intente nuevamente.");

                    _lasLineas = PersistenciaPedidosLinea.GetInstance().LineasDeUnPedido(numero);

                    if (_lasLineas.Count == 0)
                        throw new Exception("Un pedido debe tener al menos un medicamento.");

                    _unPedido = new PedidoCabezal(numero, fechaRealizado, direccion_entrega, estado, _unEmpleado, _lasLineas);
                    
                    listaPedidos.Add(_unPedido);
                }

                lector.Close();

                return listaPedidos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la base de datos: " + ex.Message);
            }
            finally
            {
                _Conexion.Close();
            }
        }

        public List<PedidoCabezal> ListaPedidosGeneradoEnviado(Encargado pEncargado)
        {
            List<PedidoCabezal> listaPedidos = new List<PedidoCabezal>();
            List<PedidosLinea> _lasLineas = new List<PedidosLinea>();
            PedidoCabezal _unPedido = null;
            Empleado _unEmpleado = null;
            SqlConnection _Conexion = new SqlConnection(Conexion.MiConexion(pEncargado));
            SqlCommand _Comando = new SqlCommand("ListaPedidosGeneradoEnviado", _Conexion);
            _Comando.CommandType = CommandType.StoredProcedure;
            SqlDataReader lector;

            try
            {
                _Conexion.Open();
                lector = _Comando.ExecuteReader();
                while (lector.Read())
                {
                    int numero = Convert.ToInt32(lector["numero"]);
                    DateTime fechaRealizado = Convert.ToDateTime(lector["fecha_realizado"]);
                    string direccion_entrega = (string)lector["direccion_entrega"];
                    string estado = (string)lector["estado"];
                    int ci_empleado = Convert.ToInt32(lector["ci_empleado"]);

                    _unEmpleado = PersistenciaEmpleado.GetInstance().BuscarEmpleadosTodos(ci_empleado);

                    if (_unEmpleado == null)
                        throw new Exception("Ocurrio un error, intente nuevamente.");

                    _lasLineas = PersistenciaPedidosLinea.GetInstance().LineasDeUnPedido(numero);

                    if (_lasLineas.Count == 0)
                        throw new Exception("Un pedido debe tener al menos un medicamento.");

                    _unPedido = new PedidoCabezal(numero, fechaRealizado, direccion_entrega, estado, _unEmpleado, _lasLineas);
                    
                    listaPedidos.Add(_unPedido);
                }

                lector.Close();

                return listaPedidos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la base de datos: " + ex.Message);
            }
            finally
            {
                _Conexion.Close();
            }
        }

        public PedidoCabezal BuscarPedido(int pCodigo)
        {
            PedidoCabezal _unPedido = null;
            Empleado _unEmpleado = null;
            List<PedidosLinea> _lasLineas = new List<PedidosLinea>();
            SqlConnection _Conexion = new SqlConnection(Conexion.MiConexionTrusted);
            SqlCommand _Comando = new SqlCommand("BuscarPedido", _Conexion);
            _Comando.CommandType = CommandType.StoredProcedure;
            _Comando.Parameters.AddWithValue("@numero", pCodigo);
            SqlDataReader lector;

            try
            {
                _Conexion.Open();
                lector = _Comando.ExecuteReader();
                if (lector.HasRows)
                {
                    lector.Read();
                    int numero = Convert.ToInt32(lector["numero"]);
                    DateTime fechaRealizado = Convert.ToDateTime(lector["fecha_realizado"]);
                    string direccion_entrega = (string)lector["direccion_entrega"];
                    string estado = (string)lector["estado"];
                    int ci_empleado = Convert.ToInt32(lector["ci_empleado"]);

                    _unEmpleado = PersistenciaEmpleado.GetInstance().BuscarEmpleadosTodos(ci_empleado);

                    if (_unEmpleado == null)
                        throw new Exception("Ocurrio un error, intente nuevamente.");

                    lector.Close();

                    _lasLineas = PersistenciaPedidosLinea.GetInstance().LineasDeUnPedido(numero);

                    if (_lasLineas.Count == 0)
                        throw new Exception("Un pedido debe tener al menos un medicamento.");

                    _unPedido = new PedidoCabezal(numero, fechaRealizado, direccion_entrega, estado, _unEmpleado, _lasLineas);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la base de datos: " + ex.Message);
            }
            finally
            { _Conexion.Close(); }

            return _unPedido;
        }

        //Aca no paso un Empleado porque ya esta adentro del Pedido
        public void AltaPedido(PedidoCabezal pPedido)
        {
            SqlConnection cnn = new SqlConnection(Conexion.MiConexion(pPedido.UnEmp));
            SqlTransaction _miTransaccion = null;
            try
            {
                SqlCommand cmd = new SqlCommand("AltaPedidoCabezal", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@direccion_entrega", pPedido.DirEtga);
                cmd.Parameters.AddWithValue("@ci_empleado", pPedido.UnEmp.Ci);
                SqlParameter parmRetorno = new SqlParameter();
                parmRetorno.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(parmRetorno);

                cnn.Open();
                _miTransaccion = cnn.BeginTransaction();
                cmd.Transaction = _miTransaccion;
                cmd.ExecuteNonQuery();

                int retorno = (int)parmRetorno.Value;

                if (retorno == -1)
                    throw new Exception("El empleado " + pPedido.UnEmp.NomCom + " no existe.");
                else if (retorno == -6)
                    throw new Exception("Ocurrio un error con la Base de Datos, contacte con el Administrador.");

                //Si el insert del cabezal salio bien, el retorno deberia contener el numero de pedido asignado.
                //Se lo paso a las lineas para hacer el alta.
                foreach (PedidosLinea linea in pPedido.Lineas)
                    PersistenciaPedidosLinea.GetInstance().AltaLineas(linea, retorno, _miTransaccion);

                //Si llego aca está todo OK y hago el Commit de todo.
                _miTransaccion.Commit();
            }
            catch (Exception ex)
            { throw ex; }
            finally
            { cnn.Close(); }
        }

        public void CambioDeEstado(PedidoCabezal pPedido, string pEstadoNuevo, Encargado E)
        {
            SqlConnection cnn = new SqlConnection(Conexion.MiConexion(E));
            try
            {
                SqlCommand cmd = new SqlCommand("CambioEstado", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@numero", pPedido.Numero);
                cmd.Parameters.AddWithValue("@estadoNuevo", pEstadoNuevo);
                SqlParameter parmRetorno = new SqlParameter();
                parmRetorno.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(parmRetorno);

                cnn.Open();
                cmd.ExecuteNonQuery();

                int retorno = (int)parmRetorno.Value;

                if (retorno == -1)
                    throw new Exception("No existe un pedido con numero " + pPedido.Numero + ".");
                else if (retorno == -2)
                    throw new Exception("El estado luego de GENERADO deberia ser ENVIADO.");
                else if (retorno == -3)
                    throw new Exception("El estado luego de ENVIADO deberia ser ENTREGADO.");
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
    }
}
