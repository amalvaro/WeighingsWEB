using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppConfiguration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WeighingsWEB.Controllers
{
	[ApiController]
	[Route("[controller]")]

	public class InterfaceController : ControllerBase
    {
		private Manager manager { get; set; }
		public InterfaceController(Manager manager)
		{
			this.manager = manager;
		}


		[HttpGet]
		public List<WeighingTable> Get()
		{
			return manager.configuration
				.Database
				.WeighingTables
				.WeighingTable;
		}


	}
}