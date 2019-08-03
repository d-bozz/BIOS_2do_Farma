using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_Compartidas;

namespace Persistencia
{
    public interface IPersistenciaMedicamentos 
    {
        void AgregarMedicamentos(Medicamento M, Encargado E);
        void EliminarMedicamentos(Medicamento M, Encargado E);
        void ModificarMedicamentos(Medicamento M, Encargado E);
        List<Medicamento> MedicamentosStock();
        Medicamento BuscarMedicamentosActivos(Farmaceutica pnombreF, int pcodigo, Usuario unUsuario);
        List<Medicamento> ListarMedicamentosActivos(Encargado unEncargado);
    }
}
