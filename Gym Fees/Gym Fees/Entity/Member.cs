using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

public class Member
{
    public Guid MemberId { get; set; }
    public string FullName { get; set; }
    public string NicNumber { get; set; }
    public string phoneNumber { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Userole { get; set; }
    public string Memberimg { get; set; }
    public DateOnly DateofRegistration { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public bool IsDeleted { get; set; }
}
