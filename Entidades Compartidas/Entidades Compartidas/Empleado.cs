using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.ServiceModel;
using System.Runtime.Serialization;

namespace Entidades_Compartidas
{
    [DataContract]
    public class Empleado : Usuario
    {
        //Atributos
        private string _inicio;
        private string _fin;

        //Propiedades
        [DataMember]
        public string Inicio
        {
            get { return _inicio; }
            set
            {
                try
                {
                    DateTime timeTest;
                    if (!DateTime.TryParse(value, out timeTest))
                        throw new Exception("La hora de inicio no es valida.");

                    _inicio = value;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        [DataMember]
        public string Fin
        {
            get { return _fin; }
            set
            {
                DateTime timeTest;
                if (!DateTime.TryParse(value, out timeTest))
                    throw new Exception("La hora de inicio no es valida.");

                _fin = value;
            }
        }

        //Constuctor Compelto
        public Empleado(int pCi, string pNomUser, string pContra, string pNomCom, string pInicio, string pFin)
            : base(pCi, pNomUser, pContra, pNomCom)
        {
            Inicio = pInicio;
            Fin = pFin;
        }

        //Constructor por defecto
        public Empleado()
            : base()
        {

        }

    }
}
