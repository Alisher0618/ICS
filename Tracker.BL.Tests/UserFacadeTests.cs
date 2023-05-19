// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Tracker.BL.Facades;
using Tracker.BL.Facades.Interfaces;
using Tracker.BL.Models;
using Tracker.Common.Tests;
using Tracker.Common.Tests.Seeds;
using Tracker.DAL.Entities;
using Xunit;
using Xunit.Abstractions;

namespace Tracker.BL.Tests;

public class UserFacadeTests : FacadeTestsBase
{
    private readonly IUserFacade _facadeSUT;

    public UserFacadeTests(ITestOutputHelper output) : base(output)
    {
        _facadeSUT = new UserFacade(UnitOfWorkFactory, UserModelMapper);
    }

    [Fact]
    public async Task Create_WithWithoutActivity_DoesNotThrowAndEqualsCreated()
    {
        //Arrange
        var model = new UserDetailModel()
        {
            Name = "Jozko",
            Surname = "Vajda",
            ImgUrl = null,
            Activities = new System.Collections.ObjectModel.ObservableCollection<ActivityListModel>()
        };

        //Act
        var returnedModel = await _facadeSUT.SaveAsync(model);

        //Assert
        FixIds(model, returnedModel);
        DeepAssert.Equal(model, returnedModel);
    }

    [Fact]
    public async Task GetById_FromSeeded_DoesNotThrowAndEqualsSeeded()
    {
        //Arrange
        var detailModel = UserModelMapper.MapToDetailModel(UserSeeds.UserEntity);

        //Act
        var returnedModel = await _facadeSUT.GetAsync(detailModel.Id);

        //Assert
        DeepAssert.Equal(detailModel, returnedModel);
    }

    [Fact]
    public async Task GetAll_FromSeeded_DoesNotThrowAndContainsSeeded()
    {
        //Arrange
        var listModel = UserModelMapper.MapToListModel(UserSeeds.UserEntity);

        //Act
        var returnedModel = await _facadeSUT.GetAsync();

        //Assert
        Assert.Contains(listModel, returnedModel);
    }

    [Fact]
    public async Task Update_FromSeeded_DoesNotThrow()
    {
        //Arrange
        var detailModel = UserModelMapper.MapToDetailModel(UserSeeds.UserEntity);
        detailModel.Name = "Zmeneny Jozko";

        //Act & Assert
        await _facadeSUT.SaveAsync(detailModel);
    }

    [Fact]
    public async Task Update_Name_FromSeeded_CheckUpdated()
    {
        //Arrange
        var detailModel = UserModelMapper.MapToDetailModel(UserSeeds.UserEntity);
        detailModel.Name = "Zmeneny Jozko 1";

        //Act
        await _facadeSUT.SaveAsync(detailModel);

        //Assert
        var returnedModel = await _facadeSUT.GetAsync(detailModel.Id);
        DeepAssert.Equal(detailModel, returnedModel);
    }

    [Fact]
    public async Task Update_RemoveProjects_FromSeeded_CheckUpdated()
    {
        //Arrange
        var detailModel = UserModelMapper.MapToDetailModel(UserSeeds.UserEntity);
        detailModel.Activities.Clear();

        //Act
        await _facadeSUT.SaveAsync(detailModel);

        //Assert
        var returnedModel = await _facadeSUT.GetAsync(detailModel.Id);
        DeepAssert.Equal(detailModel, returnedModel);
    }

    [Fact]
    public async Task Update_RemoveOneOfProjects_FromSeeded_CheckUpdated()
    {
        //Arrange
        var detailModel = UserModelMapper.MapToDetailModel(UserSeeds.UserEntity with { Activities = new List<ActivityEntity>() { ActivitySeeds.Testing } });
        detailModel.Activities.Remove(detailModel.Activities.FirstOrDefault());

        //Act
        await _facadeSUT.SaveAsync(detailModel);

        //Assert
        var returnedModel = await _facadeSUT.GetAsync(detailModel.Id);
        DeepAssert.Equal(detailModel, returnedModel);
    }

    [Fact]
    public async Task DeleteById_FromSeeded_DoesNotThrow()
    {
        //Arrange & Act & Assert
        await _facadeSUT.DeleteAsync(UserSeeds.UserEntity.Id);
    }

    private static void FixIds(UserDetailModel expectedModel, UserDetailModel returnedModel)
    {
        returnedModel.Id = expectedModel.Id;

        foreach (var ActivityListModel in returnedModel.Activities)
        {
            var ActivityDetailModel = expectedModel.Activities.FirstOrDefault(i =>
                i.Start == ActivityListModel.Start
                && i.End == ActivityListModel.End
                && i.Name == ActivityListModel.Name);

            if (ActivityDetailModel != null)
            {
                ActivityListModel.Id = ActivityDetailModel.Id;
            }
        }
    }
}
