using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Booking.API.Infrastructure;
using Booking.API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Booking.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class BookingController : Controller
    {
        #region Constructor
        private readonly IBookingRepository _bookingRepository;

        public BookingController(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
        }
        #endregion

        [HttpGet]
        [Route("bookings")]
        [ProducesResponseType(typeof(IEnumerable<Model.Booking>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Bookings()
        {
            var bookings = await _bookingRepository.GetBookingsAsync();

            return Ok(bookings);
        }

        [HttpGet]
        [Route("bookings/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Model.Booking), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBookingById(int id)
        {
            return Ok(await _bookingRepository.GetBookingDetailsAsync(id));
        }

        [HttpPost]
        [Route("bookings")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> MakeBooking([FromBody]NewBookingInfo newBooking)
        {
            var newBookingId = await _bookingRepository.MakeBookingAsync(newBooking);

            if(!newBookingId.HasValue)
                return BadRequest();

            return CreatedAtAction(nameof(MakeBooking), new { id = newBookingId.Value });
        }
    }
}