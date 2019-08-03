using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text.RegularExpressions;

namespace Entidades_Compartidas
{
    [DataContract]
    public class Farmaceutica
    {
        //Atributos

        private string _nomFar;
        private string _dirFisc;
        private int _telefono;
        private string _correo;

        //Propiedades

        [DataMember]
        public string Correo
        {
            get { return _correo; }
            set
            {
                if (value.Length != 0)
                {
                    if (value.Length <= 50)
                    {
                        String sFormato;
                        sFormato = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
                        if (Regex.IsMatch(value, sFormato))
                            _correo = value;
                        else
                            throw new Exception("El correo no cumple con el formato.");
                    }
                    else
                    { throw new Exception("El correo no puede tener mas de 50 caracteres."); }
                }
                else
                {
                    throw new Exception("El correo no puede ser vacio.");
                }
            }
        }

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

        [DataMember]
        public string DirFisc
        {
            get { return _dirFisc; }
            set
            {
                if (value.Length != 0)
                {
                    if (value.Length <= 50)
                    { _dirFisc = value; }
                    else
                    { throw new Exception("La direccion no puede tener mas de 50 caracteres."); }
                }
                else
                {
                    throw new Exception("La direccion no puede ser vacia.");
                }
            }
        }

        [DataMember]
        public string NomFar
        {
            get { return _nomFar; }
            set
            {
                if (value.Length != 0)
                {
                    if (value.Length <= 20)
                    { _nomFar = value; }
                    else
                    { throw new Exception("El nombre de  no puede contener mas de 20 caracteres."); }
                }
                else
                {
                    throw new Exception("El nombre no puede ser vacio.");
                }
            }
        }


        //Constructor completo

        public Farmaceutica(string pNomFar, string pDirFisc, int pTelefono, string pCorreo)
        {
            NomFar = pNomFar;
            DirFisc = pDirFisc;
            Telefono = pTelefono;
            Correo = pCorreo;
        }

        //Constructor por defecto

        public Farmaceutica()
        {
        }

    }
}
