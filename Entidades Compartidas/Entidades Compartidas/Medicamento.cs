using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Runtime.Serialization;
using System.ServiceModel;

namespace Entidades_Compartidas
{
    [DataContract]
    public class Medicamento
    {

        //Atributos
        private Farmaceutica _unaFarmaceutica;
        private int _codigo;
        private string _nomMed;
        private string _desc;
        private int _precio;
        private string _tipo;
        private int _stock;



        //Propiedades
        [DataMember]
        public Farmaceutica UnaFarmaceutica
        {
            get { return _unaFarmaceutica; }
            set 
            {
                if (value != null)
                {
                    _unaFarmaceutica = value;
                }
                else
                {
                    throw new Exception("La farmaceutica no puede ser vacia");
                }
            }
        }

        [DataMember]
        public int Codigo
        {
            get { return _codigo; }
            set
            {
                if (value > 0 && value.ToString().All(char.IsNumber))
                {
                    _codigo = value;
                }
                else
                {
                    throw new Exception("El codigo ingresado no es valido");
                }
            }
        }

        [DataMember]
        public string NomMed
        {
            get { return _nomMed; }
            set
            {
                if (value.Length != 0)
                {
                    if (value.Length <= 20)
                    { _nomMed = value; }
                    else
                    { throw new Exception("El nombre del medicamento no puede tener mas de 20 caracteres."); }
                }
                else
                {
                    throw new Exception("El nombre del medicamento no puede ser vacio.");
                }
            }
        }

        [DataMember]
        public string Desc
        {
            get { return _desc; }
            set
            {
                if (value.Length != 0)
                {
                    if (value.Length <= 50)
                    { _desc = value; }
                    else
                    { throw new Exception("La descripcion no puede tener mas de 50 caracteres."); }
                }
                else
                {
                    throw new Exception("La decripcion no puede ser vacia.");
                }
            }
        }

        [DataMember]
        public int Precio
        {
            get { return _precio; }
            set
            {
                if (value > 0 && value.ToString().All(char.IsNumber))
                {
                    _precio = value;
                }
                else
                {
                    throw new Exception("El precio ingresado no es valido");
                }
            }
        }

        [DataMember]
        public string Tipo
        {
            get { return _tipo; }
            set
            {
                if (value.Length != 0)
                {
                        if (value.ToLower() == "cardiologico" || value.ToLower() == "diabeticos" || value.ToLower() == "otros")
                            _tipo = value.ToLower();
                        else
                            throw new Exception("El tipo debe ser cardiologico, diabeticos u otros."); 
                }
                else
                {
                    throw new Exception("El tipo no puede ser vacio.");
                }
            }
        }

        [DataMember]
        public int Stock
        {
            get { return _stock; }
            set
            {
                if (value.ToString().All(char.IsNumber))
                {
                    _stock = value;
                }
                else
                {
                    throw new Exception("El numero de stock no es valido");
                }
            }
        }

        //Constructor completo
        public Medicamento(Farmaceutica pUnaFarmaceutica, int pCodigo, string pNomMed, string pDesc, int pPrecio, string pTipo, int pStock)
        {
            UnaFarmaceutica = pUnaFarmaceutica;
            Codigo = pCodigo;
            NomMed = pNomMed;
            Desc = pDesc;
            Precio = pPrecio;
            Tipo = pTipo;
            Stock = pStock;
        }

        //Constructor por defecto
        public Medicamento()
        {

        }

    }
}
