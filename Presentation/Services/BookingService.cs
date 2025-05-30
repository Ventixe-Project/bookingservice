using Presentation.Data.Entities;
using Presentation.Interfaces;
using Presentation.Models;

namespace Presentation.Services
{
    public class BookingService(IBookingRepository bookingRepository) : IBookingService
    {
        private readonly IBookingRepository _bookingRepository = bookingRepository;

        public async Task<BookingResult> CreateBookingAsync(CreateBookingRequest request)
        {
            var bookingEntity = new BookingEntity
            {
                EventId = request.EventId,
                PackageId = request.PackageId,
                BookingDate = DateTime.Now,
                TicketQuantity = request.TicketQuantity,
                BookingOwner = new BookingOwnerEntity
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Address = new BookingAddressEntity
                    {
                        Street = request.Street,
                        City = request.City,
                        PostalCode = request.PostalCode,
                    },
                },
            };

            var result = await _bookingRepository.AddAsync(bookingEntity);
            return result.Success
                ? new BookingResult { Success = true, BookingId = bookingEntity.Id }
                : new BookingResult { Success = false, Error = result.Error };
        }

        public async Task<BookingResult<BookingModel?>> GetBookingByIdAsync(string id)
        {
            var result = await _bookingRepository.GetAsync(x => x.Id == id);

            if (!result.Success || result.Result == null)
                return new BookingResult<BookingModel?>
                {
                    Success = false,
                    Error = "Booking not found",
                };

            var booking = result.Result;
            var dto = new BookingModel
            {
                Id = booking.Id,
                EventId = booking.EventId,
                PackageId = booking.PackageId,
                TicketQuantity = booking.TicketQuantity,
                FirstName = booking.BookingOwner?.FirstName ?? "",
                LastName = booking.BookingOwner?.LastName ?? "",
                Email = booking.BookingOwner?.Email ?? "",
                Street = booking.BookingOwner?.Address?.Street ?? "",
                City = booking.BookingOwner?.Address?.City ?? "",
                PostalCode = booking.BookingOwner?.Address?.PostalCode ?? "",
            };

            return new BookingResult<BookingModel?> { Success = true, Result = dto };
        }
    }
}
