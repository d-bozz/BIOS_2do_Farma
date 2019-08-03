using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_Compartidas;

namespace Logica
{
    public interface ILogicaFarmaceuticas
    {
        void AltaFarmaceutica(Farmaceutica unaFarmaceutica, Encargado unEncargado);
        void EliminarFarmaceutica(Farmaceutica unaFarmaceutica, Encargado unEncargado);
        void ModificarFarmaceutica(Farmaceutica unaFarmaceutica, Encargado unEncargado);
        Farmaceutica BuscarFarmaceuticaActiva(string pnombre, Usuario unUsuario);
        List<Farmaceutica> ListarFarmaceuticasActivas(Encargado unEncargado);
    }
}
