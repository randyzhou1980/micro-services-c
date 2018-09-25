using FlightCentre.API.Config;
using FlightCentre.API.Model;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightCentre.API.Services
{
    public class FlightService : BaseService, IFlightService
    {
        #region Constructor
        private readonly UrlsConfig _urls;
        public FlightService(IOptionsSnapshot<UrlsConfig> config) : base()
        {
            _urls = config.Value;
        }
        #endregion

        public async Task<IEnumerable<Flight>> GetFLightsAsync(SearchBookingRequest request)
        {
            var flights = await GetFLightsAsync();
            if (request == null)
            {
                return flights;
            }

            return flights.Where(f => (string.IsNullOrWhiteSpace(request.FlightNo) || request.FlightNo == f.Name)
                    && (string.IsNullOrWhiteSpace(request.DepartureCity) || request.DepartureCity == f.DepartureCity)
                    && (string.IsNullOrWhiteSpace(request.ArrivalCity) || request.ArrivalCity == f.ArrivalCity));
        }

        public async Task<IEnumerable<Flight>> GetFLightsAsync(SearchFlightRequest request)
        {
            var flights = await GetFLightsAsync();
            if (request == null)
            {
                return flights;
            }

            return flights.Where(f => f.FlightModel.Capacity >= request.PassengerNo);
        }

        private async Task<IEnumerable<Flight>> GetFLightsAsync()
        {
            var data = await GetStringAsync(_urls.Flight + UrlsConfig.FlightOperations.GetFlights());

            return !string.IsNullOrEmpty(data) ? JsonConvert.DeserializeObject<IEnumerable<Flight>>(data) : null;
        }
    }
}
