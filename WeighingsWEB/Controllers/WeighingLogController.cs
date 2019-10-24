using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WeighingsWEB.Controllers
{


    [ApiController]
    [Route("[controller]")]
    public class WeighingLogController : ControllerBase
    {
        public WeighingLogController(IDbService dbService)
        {
            //TODO : разорабться, как регистрировать зависимости в этом приложении
            // Зарегистрировать IDbService как single instance

            // Поискать легкую простую библиотеку, которая позволяет
            // проверять аргументы на Null, Empty
            // что-то в стиле lib for make sure argument is not null C#

            DatabaseService = dbService ?? throw new ArgumentNullException();
        }

        private IDbService DatabaseService { get; }

        /* 23.10.2019. Контроллер для отображения журнала взвешиваний. */
        [HttpGet]
        public IEnumerable<WeighingLog> Get()
        {
            using(var ctx = DatabaseService.CreateContext())
            {
                //ctx.Set;
            }

            Context Database = new Context();
            List<WeighingLog> weighingLogs = new List<WeighingLog>();
            /* 
             
            getWeightLog()
             
             */
            var data = Database.WeighingLog
                .OrderByDescending(f => f.TimeStamp)
                .Select(f => new WeighingLog {                                  
                    VehicleId = f.VehicleId,
                    VehiclePlate = f.VehiclePlate,
                    VehiclePlateStencilId = f.VehiclePlateStencilId,
                    TrailerId = f.TrailerId,
                    TrailerPlate = f.TrailerPlate,
                    TrailerPlateStencilId = f.TrailerPlateStencilId,
                    TimeStamp = f.TimeStamp,
                    ScalesId = f.ScalesId,
                    Operator = f.Operator,
                    Weight = f.Weight,
                    PreviousWeighingId = f.PreviousWeighingId,
                    PreviousWeighing = f.PreviousWeighing,
                    Type = f.Type,
                    Flags = f.Flags,
                    AxlesWeighingFlags = f.AxlesWeighingFlags,
                    IsDeleted = f.IsDeleted,
                    DeletedById = f.DeletedById,
                    DeletedOn = f.DeletedOn,
                    DeletionReason = f.DeletionReason,
                    Vehicle = new VehicleDataRecords {
                        Owner = f.Vehicle.Owner
                    }}).ToList();

            weighingLogs.AddRange(data);

            /* Database.WeighingLog.OrderByDescending( f => f.TimeStamp ) */

            return weighingLogs;
        }


    }
}
