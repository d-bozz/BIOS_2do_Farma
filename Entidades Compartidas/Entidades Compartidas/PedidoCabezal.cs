    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.Runtime.Serialization;

namespace Entidades_Compartidas
{
    [DataContract]
    public class PedidoCabezal
    {
        //Atributos
        private int _numero;
        private DateTime _fecReal;
        private Empleado _unEmp;
        private string _estado;
        private string _dirEtga;
        private List<PedidosLinea> _lineas;

        //Propiedades
        [DataMember]
        public int Numero
        {
            get { return _numero; }
            set
            {
                if (Convert.ToInt32(value) > 0 && value.ToString().All(char.IsNumber))
                {
                    _numero = value;
                }
                else
                {
                    throw new Exception("El numero ingresado no es valido");
                }
            }
        }

        [DataMember]
        public DateTime FecReal
        {
            get { return _fecReal; }
            set 
            {  
                _fecReal = DateTime.Today; 
            }
        }

        [DataMember]
        public string DirEtga
        {
            get { return _dirEtga; }
            set
            {
                if (value.ToString().Length != 0)
                    if (value.ToString().Length <= 50)
                        _dirEtga = value;
                    else
                        throw new Exception("La direccion de entrega no puede tener mas de 50 caracteres.");
                else
                {
                    throw new Exception("La direccion de entrega no puede ser vacio.");
                }
            }
        }

        [DataMember]
        public string Estado
        {
            get { return _estado; }
            set
            {
                if (value.ToString().Length != 0)
                    if (value.ToString().Length <= 10)
                        if (value.ToUpper() == "GENERADO" || value.ToUpper() == "ENVIADO" || value.ToUpper() == "ENTREGADO")
                            _estado = value.ToUpper();
                    else
                        throw new Exception("El estado de entrega no puede tener mas de 10 caracteres.");
                else
                {
                    throw new Exception("El estado de entrega no puede ser vacio.");
                }
            }
        }

        [DataMember]
        public Empleado UnEmp
        {
            get { return _unEmp; }
            set
            {
                if (value != null)
                {
                    _unEmp = value;
                }
                else
                {
                    throw new Exception("El empleado no puede ser vacio");
                }
            }
        }

        [DataMember]
        public List<PedidosLinea> Lineas
        {
            get {return _lineas;}
            set
            {
                if (value == null || value.Count == 0)
                    throw new Exception("El pedido debe contener Medicamentos.");
                _lineas = value; 
            }
        }

        //Constructor completo
        public PedidoCabezal(int pNumero, DateTime pFecReal, string pDirEtga, string pEstado, Empleado pUnEmp, List<PedidosLinea> pLineas)
        {
            Numero = pNumero;
            FecReal = pFecReal;
            DirEtga = pDirEtga;
            Estado = pEstado;
            UnEmp = pUnEmp;
            Lineas = pLineas;
        }

        //Constructor por defecto
        public PedidoCabezal()
        {

        }

        //Operaciones
        public void AgregarLinea(PedidosLinea pPedidosLinea)
        {
            int i = 0;
            if (pPedidosLinea == null)
                throw new Exception("La linea no puede ser nula");
            while (i < Lineas.Count)
            {
                if (Lineas[i].UnMedicamento.Codigo == pPedidosLinea.UnMedicamento.Codigo)
                    throw new Exception("Ya existe una linea con el medicamento " + pPedidosLinea.UnMedicamento.NomMed +
                                        ", codigo " + pPedidosLinea.UnMedicamento.Codigo +" .");
                i++;
            }
            Lineas.Add(pPedidosLinea);
        }

        public void EliminarTodasLasLineas()
        {
            Lineas.Clear();
        }
    }
}
