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

		public int Count(SearchParams searchParams = null) {

			if(searchParams == null) 
			{
				return repository
					.GetQueryable()
					.Count();
			}
			else {
				var log = repository
					.GetQueryable();

				log = ApplySearchParams(log, searchParams);
				return log.Count();
			}

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
					},
					WeighingReferences = t.WeighingReferences
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
				if(searchParams.values.selection != null) {
					switch (searchParams.values.selection)
					{
						case 0: {

							if(searchParams.values.fullContain == true) {
								weighingLog = weighingLog.Where(e => e.Operator == searchParams.values.value);
							} else {
								weighingLog = weighingLog.Where(e => e.Operator.Contains(searchParams.values.value));
							}

							break;
						}
						case 1: {
							
							if(searchParams.values.fullContain == true) {
								weighingLog = weighingLog.Where(e => e.TrailerPlate == searchParams.values.value);
							} else {
								weighingLog = weighingLog.Where(e => e.TrailerPlate.Contains(searchParams.values.value));
							}

							break;
						}
					}
				}
				if(searchParams.directory.selection != null) {
					List<int?> directoryState = searchParams.directory.selection;
					if(searchParams.directory.selection.Count != 0) {						
						for (int i = 0; i < directoryState.Count; i++)
						{
							if(directoryState[i] != null) {
								var state = directoryState[i];
								weighingLog = weighingLog.Where(e=>e.WeighingReferences.Any(f=>f.RecordId==state));
							}
						}
					}
				}
				if(searchParams.date.enable) {
					if(searchParams.date.from != null && DateTime.MinValue != searchParams.date.from) {
						weighingLog = weighingLog.Where(e => e.TimeStamp >= searchParams.date.from);
					}
					if(searchParams.date.to != null && DateTime.MinValue != searchParams.date.to) {
						weighingLog = weighingLog.Where(e => e.TimeStamp <= searchParams.date.to);
					}
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
