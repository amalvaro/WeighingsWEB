using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;


namespace WeighingsWEB.Database.EntityWorker.Entities
{
	public class WeighingLogWorker
	{
		private EntityRepository<WeighingLog> repository;
		public WeighingLogWorker(EntityRepository<WeighingLog> entityRepository)
		{
			this.repository = entityRepository;
		}

		public IEnumerable<WeighingLog> GetShortLogList(int from, int count)
		{
			return repository
				.GetQueryable()
				.OrderByDescending(f => f.TimeStamp)
				.Select(t => new WeighingLog
				{
					Id = t.Id,
					VehiclePlate = t.VehiclePlate,
					TimeStamp = t.TimeStamp,
					Weight = t.Weight,
					Type = t.Type,
					WeighingImages = t.WeighingImages,
					Vehicle = new VehicleDataRecords
					{
						Owner = t.Vehicle.Owner
					}
				})
				.Skip(from)
				.Take(count)
				.ToList();
		}

		public IEnumerable<WeighingLog> GetLogList(int from, int count) {
			return repository
				.GetQueryable()
				.OrderByDescending(f=>f.TimeStamp)
				.Select(t => new WeighingLog {
					Id = t.Id,
					VehiclePlate = t.VehiclePlate,
					TrailerPlate = t.TrailerPlate,
					TimeStamp = t.TimeStamp,
					Operator = t.Operator,
					Weight = t.Weight,
					PreviousWeighingId = t.PreviousWeighingId,
					Type = t.Type,
					Flags = t.Flags,
					AxlesWeighingFlags = t.AxlesWeighingFlags,
					WeighingImages = t.WeighingImages,
					Vehicle = new VehicleDataRecords {
						Owner = t.Vehicle.Owner
					},
					PreviousWeighing = new WeighingLog {
						TimeStamp = t.PreviousWeighing.TimeStamp,
						Weight = t.PreviousWeighing.Weight
					}
				})
				.Skip(from)
				.Take(count)
				.ToList();
		}

		public void RemoveLog(WeighingLog t)
		{
			repository.Remove(t);
		}


	}
}
