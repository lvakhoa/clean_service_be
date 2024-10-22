using CleanService.Src.Models;

namespace CleanService.Src.Modules.Auth.DTOs;


public class BookingReturnDto
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string HelperId { get; set; }
    public DateTime BookingDate { get; set; }
    public string BookingTime { get; set; }
    public BookingStatus BookingStatus { get; set; }
}