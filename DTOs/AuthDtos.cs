 
namespace Tasky.DTOs;

public record RegisterRequest(string Username, string Password);
public record LoginRequest(string Username, string Password);
public record AuthResponse(int UserId, string Username, string Token);
