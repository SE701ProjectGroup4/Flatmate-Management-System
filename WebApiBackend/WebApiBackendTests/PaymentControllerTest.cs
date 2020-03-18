﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using WebApiBackend.Controllers;
using WebApiBackend.Dto;
using WebApiBackend.EF;
using WebApiBackend.Helpers;
using WebApiBackend.Model;
using WebApiBackendTests.Helper;

namespace WebApiBackendTests
{
    class PaymentControllerTest
    {
        private DatabaseSetUpHelper _dbSetUpHelper;
        private FlatManagementContext _context;

        private MapperHelper _mapperHelper;

        private PaymentsRepository _paymentsRepository;
        private UserPaymentsRepository _userPaymentsRepository;
        private FlatRepository _flatRepository;
        private UserRepository _userRepository;

        private PaymentsController _paymentsController;

        [SetUp]
        public void Setup()
        {
            _dbSetUpHelper = new DatabaseSetUpHelper();
            _context = _dbSetUpHelper.GetContext();

            _paymentsRepository = new PaymentsRepository(_context);
            _userPaymentsRepository = new UserPaymentsRepository(_context);
            _flatRepository = new FlatRepository(_context);
            _userRepository = new UserRepository(_context);

            _mapperHelper = new MapperHelper();
            var mockMapper = _mapperHelper.GetMapper();

            DefaultHttpContext httpContext = new DefaultHttpContext();
            GenericIdentity MyIdentity = new GenericIdentity("User");
            ClaimsIdentity objClaim = new ClaimsIdentity(new List<Claim> { new Claim(ClaimTypes.NameIdentifier, "1") });

            _paymentsController = new PaymentsController(_paymentsRepository, _userPaymentsRepository, _flatRepository, _userRepository, mockMapper)
            {
                ControllerContext = new ControllerContext()
            };
            _paymentsController.ControllerContext.HttpContext = httpContext;
            httpContext.User = new ClaimsPrincipal(objClaim);
        }

        [Test]
        public async Task TestAddUserToExistingPaymentAsync()
        {
            // Arrange
            var paymentId = 1;
            var userId = 4;

            // Act
            var response = await _paymentsController.AddUserToExistingPayment(paymentId, userId);

            // Assert
            Assert.IsNotNull(response);
        }

        [Test]
        public async Task TestCreatePaymentForFlatAsync()
        {
            //// Arrange
            //var flatId = 1;
            //var payment = new PaymentDTO
            //{
            //    Amount = 99,
            //    PaymentType = PaymentType.Other,
            //    Frequency = Frequency.Weekly,
            //    StartDate = new DateTime(2020, 04, 04),
            //    EndDate = new DateTime(2020, 05, 05),
            //    Fixed = false,
            //    Description = "food",
            //};
            //var userIds = new List<int>{ 1, 2 };

            //// Act
            //var response = await _paymentsController.CreatePaymentForFlat(flatId, payment, userIds);

            //// Assert
            //Assert.IsInstanceOf<OkObjectResult>(response);
            Assert.Pass();
        }

        [Test]
        public void TestDeletePaymentForFlat()
        {
            Assert.Pass();
        }

        [Test]
        public void TestDeleteUserFromPayment()
        {
            Assert.Pass();
        }

        [Test]
        public void TestEditPayment()
        {
            Assert.Pass();
        }

        [Test]
        public async Task TestGetPaymentsForFlatAsync()
        {
            // Arrange
            var flatId = 1;

            // Act
            var response = await _paymentsController.GetPaymentsForFlat(flatId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(response);
        }

        [Test]
        public async Task TestGetAllPaymentsForUserAsync()
        {
            // Arrange

            // Act
            var response = await _paymentsController.GetAllPaymentsForUser();

            // Assert
            Assert.IsNotNull(response);
        }
    }
}
