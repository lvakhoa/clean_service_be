using System.Net;
using CleanService.Src.Modules.Booking.Mapping.DTOs;
using CleanService.Src.Modules.Booking.Services;
using CleanService.Src.Utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Pagination.EntityFrameworkCore.Extensions;

namespace CleanService.Src.Modules.Manage;

[Route("[controller]")]
public class ManageController : Controller
{

    public ManageController()
    {
        
    }
    
}