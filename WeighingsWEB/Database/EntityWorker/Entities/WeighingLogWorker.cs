using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using WeighingsWEB.Controllers.Response;

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

		public IEnumerable<WeighingLog> GetLogList(int from, int count, SearchParams searchParams) {

			IQueryable<WeighingLog> log;

			log = 
				repository.GetQueryable()
				.OrderByDescending(f => f.TimeStamp)
				.Select(t => new WeighingLog
				{
					Id = t.Id,
					VehiclePlate = t.VehiclePlate,
					TrailerPlate = t.TrailerPlate,
					TimeStamp = t.TimeStamp,
					Operator = t.Operator,
					Weight = t.Weight,
					PreviousWeighingId = t.PreviousWeighingId,
					Type = t.Type,
					WeighingImages = t.WeighingImages,
					IsDeleted = t.IsDeleted,
					Vehicle = new VehicleDataRecords
					{
						Owner = t.Vehicle.Owner
					},
					PreviousWeighing = new WeighingLog
					{
						TimeStamp = t.PreviousWeighing.TimeStamp,
						Weight = t.PreviousWeighing.Weight
					}
				});

			log = ApplySearchParams(log, searchParams);

			log = log
				.Skip(from)
				.Take(count);


			return log.ToList();
		}

		private IQueryable<WeighingLog> ApplySearchParams(IQueryable<WeighingLog> weighingLog, SearchParams searchParams)
		{
			if(searchParams != null)
			{

				if(searchParams.date.from != null && DateTime.MinValue != searchParams.date.from) {
					weighingLog = weighingLog.Where(e => e.TimeStamp >= searchParams.date.from);
				}

				if(searchParams.date.to != null && DateTime.MinValue != searchParams.date.to) {
					weighingLog = weighingLog.Where(e => e.TimeStamp <= searchParams.date.to);
				}

				if(searchParams.carNumber.carNumber != null)
				{
					var carNumber = searchParams.carNumber.carNumber.Trim();
					if(carNumber.Length != 0) {
						if(!searchParams.carNumber.fullContain) {
							weighingLog = weighingLog.Where(e => e.VehiclePlate.Contains(carNumber));
						} else {
							weighingLog = weighingLog.Where(e => e.VehiclePlate == carNumber);
						}
					}
				}

				if(searchParams.typeAndStatus.firstSelection != null) {
					weighingLog = weighingLog.Where(e => e.Type == searchParams.typeAndStatus.firstSelection);
				}

				if(searchParams.typeAndStatus.secondSelection != null) {
					bool bIsDeletedRow = (searchParams.typeAndStatus.secondSelection == 1);
					weighingLog = weighingLog.Where(e => e.IsDeleted == bIsDeletedRow);
				}

			}

			return weighingLog;
		}

		public void RemoveLog(WeighingLog t)
		{
			repository.Remove(t);
		}


	}
}
