﻿namespace HotelBookingApp.DTO
{
    public class UserDTO
    {
        public string UserId { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Firstname { get; set; } = string.Empty;  
        public string Lastname { get; set; } = string.Empty;  
        
     //   public string Password {  get; set; } = string.Empty;
     //   public string ConfirmPassword { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}

