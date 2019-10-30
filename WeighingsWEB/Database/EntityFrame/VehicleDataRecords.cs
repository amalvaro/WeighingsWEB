using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entities
{
    public class VehicleDataRecords
    {
        public long Id { get; set; }

        public string VehiclePlate { get; set; }

        public long? VehiclePlateStencilId { get; set; }

        public string Owner { get; set; }

        public string Rfid { get; set; }

        public string TrailerPlate { get; set; }

        public string AxlesFormula { get; set; }

        public bool IsDeleted { get; set; }

    }
}
