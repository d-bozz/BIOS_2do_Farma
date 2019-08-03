using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.ServiceModel;
using System.Runtime.Serialization;

namespace Entidades_Compartidas
{
    [DataContract]
    public class PedidosLinea
    {
        //Atributos
        private Medicamento _unMedicamento;
        private int _cant;

        //Propiedades
        [DataMember]
        public Medicamento UnMedicamento
        {
            get { return _unMedicamento; }
            set
            {
                if (value != null)
                {
                    _unMedicamento = value;
                }
                else
                {
                    throw new Exception("El medicamento no puede ser vacio");
                }
            }

        }

        [DataMember]
        public int Cant
        {
            get { return _cant; }
            set
            {
                if (Convert.ToInt32(value) > 0 && value.ToString().All(char.IsNumber))
                {
                    _cant = value;
                }
                else
                {
                    throw new Exception("El numero ingresado no es valido");
                }
            }
        }

        //Constructor Completo
        public PedidosLinea(Medicamento pUnMedicamento, int pCant)
        {
            UnMedicamento = pUnMedicamento;
            Cant = pCant;
        }

        //Constructor por defecto
        public PedidosLinea()
        {
        }
    }
}
