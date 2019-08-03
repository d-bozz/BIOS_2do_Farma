using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_Compartidas;
using System.Data;
using System.Data.SqlClient;

namespace Persistencia
{
    internal class PersistenciaUsuario : IPersistenciaUsuario
    {
        #region Singleton
        private static PersistenciaUsuario _instancia = null;

        private PersistenciaUsuario()
        {
        }

        public static PersistenciaUsuario GetInstance()
        {
            if (_instancia == null)
                _instancia = new PersistenciaUsuario();

            return _instancia;
        }
        #endregion

        public void CambioContrasena(Usuario pUsuario, string newContrasena)
        {
            SqlConnection cnn = new SqlConnection(Conexion.MiConexion(pUsuario));
            try
            {
                SqlCommand cmd = new SqlCommand("CambioContrasena", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre_usuario", pUsuario.NomUser);
                cmd.Parameters.AddWithValue("@contrasena", pUsuario.Contra);
                cmd.Parameters.AddWithValue("@new_contrasena", newContrasena);
                SqlParameter parmRetorno = new SqlParameter();
                parmRetorno.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(parmRetorno);

                cnn.Open();
                cmd.ExecuteNonQuery();

                int retorno = (int)parmRetorno.Value;

                if (retorno == -1)
                    throw new Exception("El Usuario " + pUsuario.NomCom + " no existe.");
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
