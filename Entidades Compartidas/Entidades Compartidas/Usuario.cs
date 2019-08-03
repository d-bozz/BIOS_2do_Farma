using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.ServiceModel;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace Entidades_Compartidas
{
    [KnownType(typeof(Empleado))]
    [KnownType(typeof(Encargado))]
    [DataContract]
    public abstract class Usuario
    {
        //Atributos

        private int _ci;
        private string _nomUser;
        private string _contra;
        private string _nomCom;

        //Propiedades

        [DataMember]
        public int Ci
        {
            get { return _ci; }
            set
            {
                if (value.ToString().Length != 8)
                    throw new Exception("El largo de la cedula de identidad no es correcto.");

                if (value.ToString().All(char.IsNumber) && value > 0)
                {
                    _ci = value;
                }

                else
                {
                    throw new Exception("La ci ingresada no es valida");
                }
            }
        }

        [DataMember]
        public string NomUser
        {
            get { return _nomUser; }
            set
            {
                if (value.Length != 0)
                {
                    if (value.Length <= 20)
                    { _nomUser = value; }
                    else
                    { throw new Exception("El nombre de usuario no puede contener mas de 20 caracteres."); }
                }
                else
                {
                    throw new Exception("El nombre de usuario no puede ser vacio.");
                }
            }
        }

        [DataMember]
        public string Contra
        {
            get { return _contra; }
            set
            {
                if (value.ToString().Trim().Length != 0)
                {
                    if (value.ToString().Trim().Length == 7)
                    {
                        string letras = value;
                        letras = letras.Replace("0", "");
                        letras = letras.Replace("1", "");
                        letras = letras.Replace("2", "");
                        letras = letras.Replace("3", "");
                        letras = letras.Replace("4", "");
                        letras = letras.Replace("5", "");
                        letras = letras.Replace("6", "");
                        letras = letras.Replace("7", "");
                        letras = letras.Replace("8", "");
                        letras = letras.Replace("9", "");

                        if(letras.Length != 5)
                            throw new Exception("La contrasena debe ser con el formato abcde12");

                        string numeros = value.Substring(5, 2);
                        int salida;
                        if (!Int32.TryParse(numeros, out salida))
                            throw new Exception("La contrasena debe ser con el formato abcde12");
                        _contra = value;
                    }
                    else
                    { throw new Exception("La contraseña debe tener 7 caracteres."); }
                }
                else
                {
                    throw new Exception("La contraseña no puede ser vacia.");
                }
            }
        }

        [DataMember]
        public string NomCom
        {
            get { return _nomCom; }
            set
            {
                if (value.Length != 0)
                {
                    if (value.Length <= 50)
                    { _nomCom = value; }
                    else
                    { throw new Exception("El nombre completo no puede contener mas de 50 caracteres."); }
                }
                else
                {
                    throw new Exception("El nombre completo no puede ser vacio.");
                }
            }
        }

        //Constructor Completo
        public Usuario(int pCi, string pNomUser, string pContra, string pNomCom)
        {
            Ci = pCi;
            NomUser = pNomUser;
            Contra = pContra;
            NomCom = pNomCom;
        }

        //Constructor por defecto
        public Usuario()
        {
        }

    }
}
