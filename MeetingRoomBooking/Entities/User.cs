﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace MeetingRoomBooking.Entities

{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public long TelegramId { get; set; }

        public string Login { get; set; }

        public bool Admin { get; set; } = false;

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}
