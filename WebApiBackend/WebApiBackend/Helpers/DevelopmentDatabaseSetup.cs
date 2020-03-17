﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiBackend.Model;

namespace WebApiBackend.Helpers
{
    public class DevelopmentDatabaseSetup
    {
        User yin, teresa, bryan;
        Payment payment1, payment2;
        // Payment[NAME]1 is for electricity
        // Payment[NAME]2 is for Rent due to many to many relationship
        UserPayment userPaymentYin1, userPaymentYin2, userPaymentBryan1,
            userPaymentBryan2, userPaymentTeresa1, userPaymentTeresa2;
        Schedule schedule1;
        Flat flat1,flat2;

        private readonly FlatManagementContext _database;

        public void InitialiseTestDataObjects()
        {
            var hasher = new PasswordHasher<User>();

            yin = new User
            {
                Id = 1,
                UserName = "YinWang",
                FirstName = "Yin",
                LastName = "Wang",
                DateOfBirth = new DateTime(1994, 12, 23),
                PhoneNumber = "0279284492",
                Email = "YinWang@qq.com",
                MedicalInformation = "N/A",
                BankAccount = "00-0000-0000000-000"
            };
            yin.HashedPassword = hasher.HashPassword(yin, "password");

            teresa = new User
            {
                Id = 2,
                UserName = "TreesAreGreen",
                FirstName = "Teresa",
                LastName = "Green",
                DateOfBirth = new DateTime(1996, 02, 12),
                PhoneNumber = "0228937228",
                Email = "GreenTrees@Yahoo.com",
                MedicalInformation = "Vegan, Gluten-Free, Lactose Intolerant",
                BankAccount = "12-3456-1234567-123"
            };
            teresa.HashedPassword = hasher.HashPassword(teresa, "password");

            bryan = new User
            {
                Id = 3,
                UserName = "BeboBryan",
                FirstName = "Bryan",
                LastName = "Ang",
                DateOfBirth = new DateTime(1984, 02, 09),
                PhoneNumber = "02243926392",
                Email = "BryanAng@Gmail.com",
                MedicalInformation = "N/A",
                BankAccount = "98-7654-3211234-210",
                FlatId = 2
                
            };
            bryan.HashedPassword = hasher.HashPassword(bryan, "password");

            payment1 = new Payment
            {
                Id = 1,
                PaymentType = PaymentType.Electricity,
                Amount = 175,
                Fixed = false,
                Frequency = Frequency.Monthly,
                StartDate = new DateTime(2020, 03, 07),
                EndDate = new DateTime(2020, 06, 07),
                Description = "electricity"
            };

            payment2 = new Payment
            {
                Id = 2,
                PaymentType = PaymentType.Rent,
                Amount = 1000,
                Fixed = true,
                Frequency = Frequency.Monthly,
                StartDate = new DateTime(2020, 03, 01),
                EndDate = new DateTime(2020, 10, 01),
                Description = "rent"
            };

            userPaymentBryan1 = new UserPayment
            {
                Payment = payment1,
                User = bryan,
                UserId = bryan.Id,
                PaymentId = payment1.Id
            };

            userPaymentBryan2 = new UserPayment
            {
                Payment = payment2,
                User = bryan,
                UserId = bryan.Id,
                PaymentId = payment2.Id
            };

            userPaymentYin1 = new UserPayment
            {
                Payment = payment1,
                User = yin,
                UserId = yin.Id,
                PaymentId = payment1.Id
            };

            userPaymentYin2 = new UserPayment
            {
                Payment = payment2,
                User = yin,
                UserId = yin.Id,
                PaymentId = payment2.Id
            };

            userPaymentTeresa1 = new UserPayment
            {
                Payment = payment1,
                User = teresa,
                UserId = teresa.Id,
                PaymentId = payment1.Id
            };

            userPaymentTeresa2 = new UserPayment
            {
                Payment = payment2,
                User = teresa,
                UserId = teresa.Id,
                PaymentId = payment2.Id
            };

            payment1.UserPayments = new List<UserPayment> { userPaymentBryan1, userPaymentTeresa1, userPaymentYin1 };
            payment2.UserPayments = new List<UserPayment> { userPaymentBryan2, userPaymentTeresa2, userPaymentYin2 };
            
            yin.UserPayments = new List<UserPayment> { userPaymentYin1, userPaymentYin2 };
            bryan.UserPayments = new List<UserPayment> { userPaymentBryan1, userPaymentBryan2 };
            teresa.UserPayments = new List<UserPayment> { userPaymentTeresa1, userPaymentTeresa2 };

            schedule1 = new Schedule
            {
                UserName = "BeboBryan",
                ScheduleType = ScheduleType.Away,
                StartDate = new DateTime(2020, 04, 01),
                EndDate = new DateTime(2020, 05, 01)
            };

            flat1 = new Flat
            {
                Id = 1,
                Address = "50 Symonds Street",
                Users = new List<User> { yin },
                Schedules = new List<Schedule> { schedule1 },
                Payments = new List<Payment> { payment1, payment2 }
            };
            flat2 = new Flat
            {
                Id = 2,
                Address = "51 Symonds Street",
                Users = new List<User> { bryan},
            };
        }

        public DevelopmentDatabaseSetup(FlatManagementContext database)
        {
            _database = database;
        }

        public void SetupDevelopmentDataSet()
        {
            // This function could also be called in the unit tests if not called here
            InitialiseTestDataObjects();

            _database.Add(yin);
            _database.Add(teresa);
            _database.Add(bryan);
            _database.Add(payment1);
            _database.Add(payment2);
            _database.Add(userPaymentBryan1);
            _database.Add(userPaymentBryan2);
            _database.Add(userPaymentTeresa1);
            _database.Add(userPaymentTeresa2);
            _database.Add(userPaymentYin1);
            _database.Add(userPaymentYin2);
            _database.Add(schedule1);
            _database.Add(flat1);
            _database.Add(flat2);

            _database.SaveChanges();
        }
    }
}
