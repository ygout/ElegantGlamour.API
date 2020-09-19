﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ElegantGlamour.Core.Models;
using ElegantGlamour.Data;
using ElegantGlamour.Api.Controllers;
using Xunit;
using ElegantGlamour.Services;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Microsoft.AspNetCore.Mvc;
using AutoWrapper.Wrappers;
using ElegantGlamour.Api.Mapping;
using ElegantGlamour.Core.Dtos;
using System.Threading.Tasks;
using ElegantGlamour.Core.Error;
using ElegantGlamour.API.Controllers;

namespace ElegantGlamour.Tests.UnitTests
{
    public class PrestationsController_Test
    {
        /// <summary>
        /// Test the Get a prestation by an id and return OK
        /// </summary>
        [Fact]
        public async Task GetPrestationById_Return_Ok()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(GetPrestationById_Return_Ok));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockCategoryService = new CategoryService(mockUnitOfWork);
            var mockPrestationService = new PrestationService(mockUnitOfWork, mockCategoryService);

            var mockLogger = Mock.Of<ILogger<PrestationsController>>();

            var controller = new PrestationsController(mockPrestationService, mapper, mockLogger);
            var id = 1;
            #endregion

            #region Act
            var response = await controller.GetPrestationById(id);
            dbContext.Dispose();

            #endregion

            #region Assert
            Assert.IsType<GetPrestationDto>(response);
            #endregion
        }

        /// <summary>
        /// Test the Get a prestation by an id and return NOT_FOUND
        /// </summary>
        [Fact]
        public async Task GetPrestationById_Return_Not_Found()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(GetPrestationById_Return_Not_Found));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockCategoryService = new CategoryService(mockUnitOfWork);
            var mockPrestationService = new PrestationService(mockUnitOfWork, mockCategoryService);

            var mockLogger = Mock.Of<ILogger<PrestationsController>>();

            var controller = new PrestationsController(mockPrestationService, mapper, mockLogger);
            var id = 300;
            #endregion
            #region Act
            var apiException = await Assert.ThrowsAsync<ApiException>(() => controller.GetPrestationById(id));

            dbContext.Dispose();
            #endregion

            #region Assert
            Assert.Equal(404, apiException.StatusCode);
            #endregion

        }

        /// <summary>
        /// Test return all prestations response 200 OK
        /// </summary>
        [Fact]
        public async void GetPrestations_Return_Ok()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(GetPrestations_Return_Ok));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockCategoryService = new CategoryService(mockUnitOfWork);
            var mockPrestationService = new PrestationService(mockUnitOfWork, mockCategoryService);

            var mockLogger = Mock.Of<ILogger<PrestationsController>>();

            var controller = new PrestationsController(mockPrestationService, mapper, mockLogger);
            #endregion

            #region Act
            var response = await controller.GetPrestations();

            dbContext.Dispose();
            #endregion

            #region Assert
            Assert.IsAssignableFrom<IEnumerable<GetPrestationDto>>(response);
            #endregion
        }

        /// <summary>
        /// Create a prestation response OK 201
        /// </summary>
        [Fact]
        public async Task POST_Create_Prestation_Return_Ok()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(POST_Create_Prestation_Return_Ok));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockCateogryService = new CategoryService(mockUnitOfWork);
            var mockPrestationService = new PrestationService(mockUnitOfWork, mockCateogryService);

            var mockLogger = Mock.Of<ILogger<PrestationsController>>();

            var controller = new PrestationsController(mockPrestationService, mapper, mockLogger);
            #endregion
            var addPrestation = new AddPrestationDto()
            {
                Title = "TEST_PRESTATION",
                Description = "Ceci est un test prestation",
                Price = 10,
                Duration = 60,
                CategoryId = 1
            };
            #region Act
            var response = await controller.CreatePrestation(addPrestation);

            dbContext.Dispose();
            #endregion

            #region Assert
            Assert.IsType<ApiResponse>(response);
            Assert.Equal(201, response.StatusCode);
            #endregion
        }

        /// <summary>
        /// Create a prestation response OK 201
        /// </summary>
        [Fact]
        public async Task POST_Create_Prestation_Return_Error_Category_Does_Not_Exist()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(POST_Create_Prestation_Return_Error_Category_Does_Not_Exist));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockCateogryService = new CategoryService(mockUnitOfWork);
            var mockPrestationService = new PrestationService(mockUnitOfWork, mockCateogryService);

            var mockLogger = Mock.Of<ILogger<PrestationsController>>();

            var controller = new PrestationsController(mockPrestationService, mapper, mockLogger);
            #endregion
            var addPrestation = new AddPrestationDto()
            {
                Title = "TEST_PRESTATION",
                Description = "Ceci est un test prestation",
                Price = 10,
                Duration = 60,
                CategoryId = 89
            };
            #region Act

            var apiException = await Assert.ThrowsAsync<ApiException>(() => controller.CreatePrestation(addPrestation));

            dbContext.Dispose();
            #endregion

            #region Assert
            Assert.Equal(400, apiException.StatusCode);
            Assert.Contains(ErrorMessage.Err_Category_Does_Not_Exist, apiException.CustomError.ToString());

            #endregion
        }

        /// <summary>
        /// Create a prestation with error title can't be empty
        /// </summary>
        [Fact]
        public async Task POST_Create_Prestation_Return_Error_Title_Empty()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(POST_Create_Prestation_Return_Error_Title_Empty));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockCateogryService = new CategoryService(mockUnitOfWork);
            var mockPrestationService = new PrestationService(mockUnitOfWork, mockCateogryService);

            var mockLogger = Mock.Of<ILogger<PrestationsController>>();

            var controller = new PrestationsController(mockPrestationService, mapper, mockLogger);
            #endregion
            var addPrestation = new AddPrestationDto()
            {
                Title = "",
                Description = "Test",
                Price = 10,
                Duration = 60,
                CategoryId = 89
            };
            #region Act

            var apiException = await Assert.ThrowsAsync<ApiException>(() => controller.CreatePrestation(addPrestation));

            dbContext.Dispose();
            #endregion

            #region Assert
            Assert.Equal(400, apiException.StatusCode);
            Assert.Contains(ErrorMessage.Err_Prestation_Title_Not_Empty, apiException.CustomError.ToString());

            #endregion
        }
        /// <summary>
        /// Create a prestation with error description can't be empty
        /// </summary>
        [Fact]
        public async Task POST_Create_Prestation_Return_Error_Description_Empty()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(POST_Create_Prestation_Return_Error_Description_Empty));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockCateogryService = new CategoryService(mockUnitOfWork);
            var mockPrestationService = new PrestationService(mockUnitOfWork, mockCateogryService);

            var mockLogger = Mock.Of<ILogger<PrestationsController>>();

            var controller = new PrestationsController(mockPrestationService, mapper, mockLogger);
            #endregion
            var addPrestation = new AddPrestationDto()
            {
                Title = "Test",
                Description = "",
                Price = 10,
                Duration = 60,
                CategoryId = 89
            };
            #region Act

            var apiException = await Assert.ThrowsAsync<ApiException>(() => controller.CreatePrestation(addPrestation));

            dbContext.Dispose();
            #endregion

            #region Assert
            Assert.Equal(400, apiException.StatusCode);
            Assert.Contains(ErrorMessage.Err_Prestation_Description_Not_Empty, apiException.CustomError.ToString());

            #endregion
        }

        /// <summary>
        /// Create a prestation with error price can't be at null
        /// </summary>
        [Fact]
        public async Task POST_Create_Prestation_Return_Error_Price_Empty()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(POST_Create_Prestation_Return_Error_Price_Empty));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockCateogryService = new CategoryService(mockUnitOfWork);
            var mockPrestationService = new PrestationService(mockUnitOfWork, mockCateogryService);

            var mockLogger = Mock.Of<ILogger<PrestationsController>>();

            var controller = new PrestationsController(mockPrestationService, mapper, mockLogger);
            #endregion
            var addPrestation = new AddPrestationDto()
            {
                Title = "Test",
                Description = "Test",
                Duration = 60,
                CategoryId = 89
            };
            #region Act

            var apiException = await Assert.ThrowsAsync<ApiException>(() => controller.CreatePrestation(addPrestation));

            dbContext.Dispose();
            #endregion

            #region Assert
            Assert.Equal(400, apiException.StatusCode);
            Assert.Contains(ErrorMessage.Err_Prestation_Price_Not_Empty, apiException.CustomError.ToString());

            #endregion
        }

        /// <summary>
        /// Create a prestation with error duration can't be equal to 0
        /// </summary>
        [Fact]
        public async Task POST_Create_Prestation_Return_Error_Duration_Equal_Zero()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(POST_Create_Prestation_Return_Error_Duration_Equal_Zero));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockCateogryService = new CategoryService(mockUnitOfWork);
            var mockPrestationService = new PrestationService(mockUnitOfWork, mockCateogryService);

            var mockLogger = Mock.Of<ILogger<PrestationsController>>();

            var controller = new PrestationsController(mockPrestationService, mapper, mockLogger);
            #endregion
            var addPrestation = new AddPrestationDto()
            {
                Title = "Test",
                Description = "Test",
                Duration = 0,
                CategoryId = 89,
                Price = 50
            };
            #region Act

            var apiException = await Assert.ThrowsAsync<ApiException>(() => controller.CreatePrestation(addPrestation));

            dbContext.Dispose();
            #endregion

            #region Assert
            Assert.Equal(400, apiException.StatusCode);
            Assert.Contains(ErrorMessage.Err_Prestation_Duration_Not_Equal_To_0, apiException.CustomError.ToString());

            #endregion
        }

        /// <summary>
        /// Create a prestation with error duration can't be empty
        /// </summary>
        [Fact]
        public async Task POST_Create_Prestation_Return_Error_Duration_Empty()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(POST_Create_Prestation_Return_Error_Duration_Empty));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockCateogryService = new CategoryService(mockUnitOfWork);
            var mockPrestationService = new PrestationService(mockUnitOfWork, mockCateogryService);

            var mockLogger = Mock.Of<ILogger<PrestationsController>>();

            var controller = new PrestationsController(mockPrestationService, mapper, mockLogger);
            #endregion
            var addPrestation = new AddPrestationDto()
            {
                Title = "Test",
                Description = "Test",
                CategoryId = 89,
                Price = 50
            };
            #region Act

            var apiException = await Assert.ThrowsAsync<ApiException>(() => controller.CreatePrestation(addPrestation));

            dbContext.Dispose();
            #endregion

            #region Assert
            Assert.Equal(400, apiException.StatusCode);
            Assert.Contains(ErrorMessage.Err_Prestation_Duration_Not_Empty, apiException.CustomError.ToString());

            #endregion
        }
        /// <summary>
        /// Create a prestation with error category id can't be empty
        /// </summary>
        [Fact]
        public async Task POST_Create_Prestation_Return_Error_Category_Empty()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(POST_Create_Prestation_Return_Error_Category_Empty));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockCateogryService = new CategoryService(mockUnitOfWork);
            var mockPrestationService = new PrestationService(mockUnitOfWork, mockCateogryService);

            var mockLogger = Mock.Of<ILogger<PrestationsController>>();

            var controller = new PrestationsController(mockPrestationService, mapper, mockLogger);
            #endregion
            var addPrestation = new AddPrestationDto()
            {
                Title = "Test",
                Description = "Test",
                Price = 50,
                Duration = 50
            };
            #region Act

            var apiException = await Assert.ThrowsAsync<ApiException>(() => controller.CreatePrestation(addPrestation));

            dbContext.Dispose();
            #endregion

            #region Assert
            Assert.Equal(400, apiException.StatusCode);
            Assert.Contains(ErrorMessage.Err_Category_Not_Empty, apiException.CustomError.ToString());

            #endregion
        }

    }
}
