using System;
using System.Collections.Generic;
using System.Linq;
using BusquedasUI.Core.Base;
using BusquedasUI.Core.Xml;

namespace BusquedasUI.Core.Search
{
    public class HabitacionesOcupadas
    {
        public static List<Habitacion> GetHabitacionesOcupadas( int piso)
        {
            List<Habitacion> habitacionesSinFiltrar = GetHabitacionesOcupadas();

            return piso == 0 
                ? habitacionesSinFiltrar 
                : GetHabitacionesFiltradasPorPiso(habitacionesSinFiltrar, piso);
        }

        private static List<Habitacion> GetHabitacionesFiltradasPorPiso(List<Habitacion> habitacionesSinFiltrar, int piso)
        {
            return habitacionesSinFiltrar.Where(habitacion => habitacion.GetNumPiso() == piso).ToList();
        }

        private static List<Habitacion> GetHabitacionesOcupadas()
        {
            HabitacionXmlReader xmlReader = new HabitacionXmlReader();
            List<Habitacion> habitaciones = xmlReader.GetHabitaciones();
            List<Habitacion> habitacionesVacias = new List<Habitacion>();

            var today = DateTime.Today;
            foreach (var habitacion in habitaciones)
            {
                var fechaUltimaReserva = DateTime.Parse(habitacion.FechaUltimaReserva);
                if (DateTime.Compare(fechaUltimaReserva, today) == 0)
                {
                    habitacionesVacias.Add(habitacion);
                }
            }
            return habitacionesVacias;
        }
    }
}