using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Entities
{
    public class WeighingLog
    {
        public long Id { get; set; }

        public long? VehicleId { get; set; }

        public string VehiclePlate { get; set; }

        public long? VehiclePlateStencilId { get; set; }

        public long? TrailerId { get; set; }

        public string TrailerPlate { get; set; }

        public long? TrailerPlateStencilId { get; set; }

        public DateTime TimeStamp { get; set; }

        public string ScalesId { get; set; }

        public string Operator { get; set; }

        public double Weight { get; set; }

        public long? PreviousWeighingId { get; set; }

       

        public byte Type { get; set; }

        public int Flags { get; set; }

        public int? AxlesWeighingFlags { get; set; }

        public bool IsDeleted { get; set; }

        public long? DeletedById { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string DeletionReason { get; set; }


		/* Связанные таблицы */

		public VehicleDataRecords Vehicle { get; set; }
		public WeighingLog PreviousWeighing { get; set; }
		[ForeignKey("WeighingId")]
		public List<WeighingImages> WeighingImages { get; set; }

	}


}
