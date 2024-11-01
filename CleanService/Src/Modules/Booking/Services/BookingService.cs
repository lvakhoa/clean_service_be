using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Booking.Mapping.DTOs;
using CleanService.Src.Modules.Booking.Repositories;
using CleanService.Src.Modules.Contract.DTOs;
using CleanService.Src.Modules.Contract.Services;
using CleanService.Src.Modules.Service.Services;

namespace CleanService.Src.Modules.Booking.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IServiceTypeService _serviceTypeService;
    private readonly IContractService _contractService;
    private readonly IMapper _mapper;

    public BookingService(IBookingRepository bookingRepository, IServiceTypeService serviceTypeService,
        IContractService contractService, IMapper mapper)
    {
        _bookingRepository = bookingRepository;
        _serviceTypeService = serviceTypeService;
        _contractService = contractService;
        _mapper = mapper;
    }

    public async Task<BookingReturnDto> CreateBooking(CreateBookingDto createBookingDto)
    {
        // var price = _serviceService.GetPriceById(createBookingDto.ServiceTypeId);
        var booking = _mapper.Map<Bookings>(createBookingDto);
        var bookingEntity = await _bookingRepository.CreateBooking(booking);
        var bookingDto = _mapper.Map<BookingReturnDto>(bookingEntity);

        await _contractService.CreateContract(new CreateContractDto
        {
            BookingId = booking.Id,
            Content = "Clean at " + booking.Location
        });
        return bookingDto;
    }

    public async Task<BookingReturnDto?> UpdateBooking(Guid id, UpdateBookingDto updateBookingDto)
    {
        var booking = await _bookingRepository.GetBookingById(id);
        if (booking == null)
            throw new KeyNotFoundException("Booking not found");
        
        var updateBooking = _mapper.Map<PartialBookings>(updateBookingDto);
        var bookingEntity = await _bookingRepository.UpdateBooking(id, updateBooking);
        var bookingDto = _mapper.Map<BookingReturnDto>(bookingEntity);
        
        return bookingDto;
    }

    public async Task<BookingReturnDto[]> GetAllBookings()
    {
        var bookings = await _bookingRepository.GetAllBookings();
        var bookingsDto = _mapper.Map<BookingReturnDto[]>(bookings);
        return bookingsDto;
    }

    public async Task<BookingReturnDto?> GetBookingById(Guid id)
    {
        var booking = await _bookingRepository.GetBookingById(id);
        var bookingDto = _mapper.Map<BookingReturnDto>(booking);
        return bookingDto;
    }
}