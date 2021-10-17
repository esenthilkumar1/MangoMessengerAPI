﻿using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace MangoAPI.BusinessLogic.ApiCommands.Users
{
    public record UpdateUserAccountInfoRequest
    {
        [JsonConstructor]
        public UpdateUserAccountInfoRequest(
            string phoneNumber,
            DateTime? birthdayDate,
            string email,
            string website,
            string username,
            string bio,
            string address,
            string displayName)
        {
            PhoneNumber = phoneNumber;
            BirthdayDate = birthdayDate;
            Email = email;
            Website = website;
            Username = username;
            Bio = bio;
            Address = address;
            DisplayName = displayName;
        }

        [DefaultValue("Test User")]
        public string DisplayName { get; }

        [DefaultValue("54763198")]
        public string PhoneNumber { get; }

        [DefaultValue("1995-04-07T00:00:00")]
        public DateTime? BirthdayDate { get; }

        [DefaultValue("test@gmail.com")]
        public string Email { get; }

        [DefaultValue("test.com")]
        public string Website { get; }

        [DefaultValue("TestUser")]
        public string Username { get; }

        [DefaultValue("Test user from $'{cityName}'")]
        public string Bio { get; }

        [DefaultValue("Finland, Helsinki")]
        public string Address { get; }
    }
}