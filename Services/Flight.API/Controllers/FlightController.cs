using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Flight.API.Infrastructure;
using Flight.API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Flight.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class FlightController : Controller
    {
        #region Constructor
        private readonly IFlightRepository _repository;

        public FlightController(IFlightRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        #endregion

        [HttpGet]
        [Route("flights")]
        [ProducesResponseType(typeof(IEnumerable<Model.Flight>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Flights()
        {
            var flights = await _repository.GetFlightsAsync();

            return Ok(flights.OrderBy(i => i.DepartureTime)
                .ThenBy(i => i.ArrivalCity)
                .ThenBy(i => i.ArrivalTime));
        }

        [HttpGet]
        [Route("flights/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Model.Flight), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetFlightById(int id)
        {
            return Ok(await _repository.GetFlightDetailsAsync(id));
        }
    }
}