using Raunstrup.Service.Contract.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Raunstrup.UI.MVC.Models.ProfessionModelView
{
    public class ProfessionMapper
    {
        public static Profession Map(ProfessionDTO dto)
        {
            if (dto == null)
            { return null; }

            return new Profession
            {
                ID = dto.ID,
                Type = dto.Type,
                HourPrice = dto.HourPrice
            };
        }
        public static IEnumerable<Profession> Map(IEnumerable<ProfessionDTO> dtos)
        {
            return dtos.Select(x => Map(x)).AsEnumerable();
        }

        public static ProfessionDTO Map(Profession view)
        {
            if (view == null)
            { return null; }

            return new ProfessionDTO
            {
                ID = view.ID,
                Type = view.Type,
                HourPrice = view.HourPrice
            };
        }
    }
}
