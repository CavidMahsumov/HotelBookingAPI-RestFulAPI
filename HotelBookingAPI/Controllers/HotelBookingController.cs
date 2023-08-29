using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HotelBookingAPI.Models;
using HotelBookingAPI.Data;

namespace HotelBookingAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HotelBookingController : ControllerBase
    {
        private readonly ApiContext _context;
        public HotelBookingController(ApiContext context)
        {
            _context = context;
        }
        [HttpPost]
        public JsonResult CreateEdit(HotelBooking booking)
        {
            if (booking.Id == 0)
            {
                _context.Bookings.Add(booking);
            }
            else
            {
                var bookinginDb = _context.Bookings.Find(booking.Id);
                if (bookinginDb == null)
                {
                    return new JsonResult(NotFound());
                }
                bookinginDb = booking;   
            } 
            _context.SaveChanges();
            return new JsonResult(Ok(booking));
        }
        [HttpGet]
        public JsonResult Get(int id)
        {
            var booking= _context.Bookings.Find(id);
            if (booking != null)
            {
                return new JsonResult(Ok(booking));
            }
            else
            {
                return new JsonResult(NotFound());
            }
        }
        [HttpDelete]
        public JsonResult Delete(int id) { 
            var result=_context.Bookings.Find(id);
            if (result == null)
            {
                return new JsonResult(NotFound());
            }
            _context.Bookings.Remove(result);
            _context.SaveChanges();
            return new JsonResult(NoContent());

        }
        [HttpGet]
        public JsonResult GetAll()
        {
            var result = _context.Bookings.ToList();
            if (result == null)
            {
                return new JsonResult(NotFound());
            }
            return new JsonResult(Ok(result));
        }
    }
}
