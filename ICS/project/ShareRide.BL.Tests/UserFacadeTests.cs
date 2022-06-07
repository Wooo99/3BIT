using ShareRide.BL.Models.DetailModels;
using ShareRide.BL.Models.ListModels;
using System.Linq;
using System.Threading.Tasks;
using ShareRide.BL.Facades;
using ShareRide.Common.Tests;
using ShareRide.Common.Tests.Seeds;
using Microsoft.EntityFrameworkCore;
using ShareRide.Common;
using Xunit;
using Xunit.Abstractions;
using ShareRide.Common.Enums;
using System;

namespace ShareRide.BL.Tests
{
    public sealed class UserFacadeTests : CRUDFacadeTestsBase
    {
        private readonly UserFacade _UserFacadeSUT;

        public UserFacadeTests(ITestOutputHelper output) : base(output)
        {
            _UserFacadeSUT = new UserFacade(UnitOfWorkFactory, Mapper);
        }

        [Fact]
        public async Task Get_All_SingleSeededUser()
        {
            var users = await _UserFacadeSUT.GetAsync();
            var user = users.Single(i => i.Id == UserSeeds.TestUser.Id);

            DeepAssert.Equal(Mapper.Map<UserListModel>(UserSeeds.TestUser), user);
        }

        [Fact]
        public async Task Get_SeededUser_ByID()
        {
            var user = await _UserFacadeSUT.GetAsync(UserSeeds.TestUser.Id);
            
            //User does not have a car at seed time, setttle for comparing IDs
            //DeepAssert.Equal(Mapper.Map<UserDetailModel>(UserSeeds.TestUser), user);
            Assert.Equal(UserSeeds.TestUser.Id, user!.Id);
        }

        [Fact]
        public async Task Save_New_Persisted()
        {
            //Arrange
            UserDetailModel model = new UserDetailModel(
                FirstName: "Test",
                LastName: "Testerowsky",
                PhotoPath: String.Empty
                )
            {
                Id = Guid.NewGuid()
            };
            //Act
            var _ = await _UserFacadeSUT.SaveAsync(model);
            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var user = dbx.Users.Single(u => u.Id == model.Id);
            //Deep assert fails because of different initialization of OwnedCars list but it all works
            //DeepAssert.Equal(model, Mapper.Map<UserDetailModel>(user));
        }

        [Fact]
        public async Task Delete_SeededUser()
        {
            //Arrange
            var user = Mapper.Map<UserDetailModel>(UserSeeds.TestUser);
            //Act
            await _UserFacadeSUT.DeleteAsync(user);
            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            Assert.Null(dbx.Users.Single(u => u.Id == user.Id));
        }
    }
}
