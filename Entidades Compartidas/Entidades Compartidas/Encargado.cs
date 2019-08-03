using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.ServiceModel;
using System.Runtime.Serialization;

namespace Entidades_Compartidas
{
    [DataContract]
    public class Encargado : Usuario
    {
        //Atributos
       
        private int _telefono;

        //Propiedades
        [DataMember]
        public int Telefono
        {
            get { return _telefono; }
            set
            {
                if (value > 0 && value.ToString().All(char.IsNumber))
                {
                    _telefono = value;
                }
                else
                {
                    throw new Exception("El numero de telefono no es valido");
                }
            }
        }

        //Constructor Completo
        public Encargado(int pCi, string pNomUser, string pContra, string pNomCom, int pTelefono)
            : base(pCi, pNomUser, pContra, pNomCom)
        {
            Telefono = pTelefono;            
        }

        //Constructor por defecto 
        public Encargado()
            :base()
        { 
        
        }

    }
}
