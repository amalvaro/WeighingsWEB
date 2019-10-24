using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entities
{
    public class WeighingImages
    {
        public long Id { get; set; }

        public string Type { get; set; }

        public string CameraId { get; set; }

        public Guid ImageKey { get; set; }

        public byte[] ImageData { get; set; }

        public string Format { get; set; }

        public short PlateX { get; set; }

        public short PlateY { get; set; }

        public short PlateWidth { get; set; }

        public short PlateHeight { get; set; }

        public long WeighingId { get; set; }

    }

}
