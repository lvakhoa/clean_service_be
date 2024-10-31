using System.ComponentModel.DataAnnotations;
using CleanService.Src.Models;

namespace CleanService.Src.Modules.Auth.Mapping.DTOs;

public class RegistrationDto
{
    public string Id { get; set; }
    
    public string Email { get; set; }
    
    public string Fullname { get; set; }
    
    public UserType UserType { get; set; }
}