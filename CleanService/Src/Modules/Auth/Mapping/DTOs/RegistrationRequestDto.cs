using System.ComponentModel.DataAnnotations;

using CleanService.Src.Models;
using CleanService.Src.Models.Enums;

namespace CleanService.Src.Modules.Auth.Mapping.DTOs;

public class RegistrationRequestDto
{
    public string Id { get; set; }

    public string Email { get; set; }

    public string Fullname { get; set; }

    public UserType UserType { get; set; }
}
