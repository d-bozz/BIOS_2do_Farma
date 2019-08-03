using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_Compartidas;
using System.Data;
using System.Data.SqlClient;

namespace Persistencia
{
    internal class PersistenciaPedidosLinea
    {
        //Singleton*************************************
        private static PersistenciaPedidosLinea _instancia = null;

        private PersistenciaPedidosLinea()
        {
        }

        public static PersistenciaPedidosLinea GetInstance()
        {
            if (_instancia == null)
                _instancia = new PersistenciaPedidosLinea();
            return _instancia;
        }
        //************************************************

        internal List<PedidosLinea> LineasDeUnPedido(int pNumeroPedido)
        {
            int codigo_medicamento;
            int cantidad;
            string nombre_farma;
            Farmaceutica farmaceutica;
            Medicamento medicamento;
            List<PedidosLinea> listaDeMedicamentos = new List<PedidosLinea>();

            //aqui ingresar la id y pw del usuario pasado por parametro
            SqlConnection _cnn = new SqlConnection(Conexion.MiConexionTrusted);

            SqlCommand _comando = new SqlCommand("LineasDeUnPedido", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@numero", pNumeroPedido);

            try
            {
                //me conecto
                _cnn.Open();

                //ejecuto consulta
                SqlDataReader _lector = _comando.ExecuteReader();

                //verifico si hay lineas
                if (_lector.HasRows)
                {
                    while (_lector.Read())
                    {
                        codigo_medicamento = Convert.ToInt32(_lector["codigo_medicamento"]);
                        cantidad = Convert.ToInt32(_lector["cantidad"]);
                        nombre_farma = (string)_lector["nombre_farma"];


                        farmaceutica = PersistenciaFarmaceutica.GetInstance().BuscarFarmaceuticaTodas(nombre_farma);
                        if (farmaceutica == null)
                            throw new Exception("Ocurrio un error, intente nuevamente.");

                        medicamento = PersistenciaMedicamentos.GetInstance().BuscarMedicamentosTodos(farmaceutica, codigo_medicamento);
                        if (medicamento == null)
                            throw new Exception("Ocurrio un error, intente nuevamente.");

                        PedidosLinea linea = new PedidosLinea(medicamento, cantidad);
                        listaDeMedicamentos.Add(linea);
                    }
                }

                _lector.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _cnn.Close();
            }

            return listaDeMedicamentos;
        }

        internal void AltaLineas(PedidosLinea pLinea, int pNumeroPedido, SqlTransaction _pTransaccion)
        {
            SqlCommand cmd = new SqlCommand("AltaPedidoLineas", _pTransaccion.Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nombre_farma", pLinea.UnMedicamento.UnaFarmaceutica.NomFar);
            cmd.Parameters.AddWithValue("@codigo_medicamento", pLinea.UnMedicamento.Codigo);
            cmd.Parameters.AddWithValue("@numero", pNumeroPedido);
            cmd.Parameters.AddWithValue("@cantidad", pLinea.Cant);
            
            SqlParameter parmRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            parmRetorno.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(parmRetorno);

            try
            {
                cmd.Transaction = _pTransaccion;
                cmd.ExecuteNonQuery();

                int retorno = (int)parmRetorno.Value;

                if (retorno == -1)
                    throw new Exception("No existe un pedido con ese numero.");
                else if (retorno == -2)
                    throw new Exception("El medicamento " + pLinea.UnMedicamento.NomMed + " no existe.");
                else if (retorno == -3)
                    throw new Exception("No hay stock suficiente para hacer el pedido.");
                else if (retorno == -6)
                    throw new Exception("Ocurrio un error con la Base de Datos, contacte con el Administrador.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
