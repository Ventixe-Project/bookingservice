using Presentation.Models;

namespace Presentation.Interfaces
{
    public interface IBookingService
    {
        Task<BookingResult> CreateBookingAsync(CreateBookingRequest request);
        Task<BookingResult<BookingModel?>> GetBookingByIdAsync(string id);
    }
}
