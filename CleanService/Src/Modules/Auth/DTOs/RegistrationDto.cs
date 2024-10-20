using System.ComponentModel.DataAnnotations;
using CleanService.Src.Models;

namespace CleanService.Src.Modules.Auth.DTOs;

public class RegistrationDto
{
    [Required]
    public string Id { get; set; }
    
    [EmailAddress]
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string Fullname { get; set; }
    
    [Required]
    public UserType UserType { get; set; }
}