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

public class ProjectFacadeTests : FacadeTestsBase
{
    private readonly IProjectFacade _facadeSUT;

    public ProjectFacadeTests(ITestOutputHelper output) : base(output)
    {
        _facadeSUT = new ProjectFacade(UnitOfWorkFactory, ProjectModelMapper);
    }

    [Fact]
    public async Task Create_WithWithoutActivity_DoesNotThrowAndEqualsCreated()
    {
        //Arrange
        var model = new ProjectDetailModel()
        {
            Name = "Jozko",
            Activities = new System.Collections.ObjectModel.ObservableCollection<ActivityListModel>(),
            Users = new System.Collections.ObjectModel.ObservableCollection<UserListModel>()
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
        var detailModel = ProjectModelMapper.MapToDetailModel(ProjectSeeds.Project0);

        //Act
        var returnedModel = await _facadeSUT.GetAsync(detailModel.Id);

        //Assert
        DeepAssert.Equal(detailModel, returnedModel);
    }

    [Fact]
    public async Task GetAll_FromSeeded_DoesNotThrowAndContainsSeeded()
    {
        //Arrange
        var listModel = ProjectModelMapper.MapToListModel(ProjectSeeds.Project0);

        //Act
        var returnedModel = await _facadeSUT.GetAsync();

        //Assert
        Assert.Contains(listModel, returnedModel);
    }

    [Fact]
    public async Task Update_FromSeeded_DoesNotThrow()
    {
        //Arrange
        var detailModel = ProjectModelMapper.MapToDetailModel(ProjectSeeds.Project0);
        detailModel.Name = "Zmeneny Projekt";

        //Act & Assert
        await _facadeSUT.SaveAsync(detailModel);
    }

    [Fact]
    public async Task Update_Name_FromSeeded_CheckUpdated()
    {
        //Arrange
        var detailModel = ProjectModelMapper.MapToDetailModel(ProjectSeeds.Project0);
        detailModel.Name = "Zmeneny Projekt 1";

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
        var detailModel = ProjectModelMapper.MapToDetailModel(ProjectSeeds.Project0);
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
        var ActivitiyProject = new ActivityProjectEntity()
        {
            ProjectId = ProjectSeeds.Project1.Id,
            Project = ProjectSeeds.Project1,
            ActivityId = ActivitySeeds.Testing.Id,
            Activity = ActivitySeeds.Testing,
            Id = Guid.NewGuid()
        };

        //Arrange
        var detailModel = ProjectModelMapper.MapToDetailModel(ProjectSeeds.Project1 with { Activities = new List<ActivityProjectEntity>() { ActivitiyProject } });
        detailModel.Activities.Remove(detailModel.Activities.First());

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
        await _facadeSUT.DeleteAsync(ProjectSeeds.Project0.Id);
    }

    private static void FixIds(ProjectDetailModel expectedModel, ProjectDetailModel returnedModel)
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
        foreach (var UserListModel in returnedModel.Users)
        {
            var UserDetailModel = expectedModel.Users.FirstOrDefault(i =>
                i.Name == UserListModel.Name
                && i.Surname == UserListModel.Surname
                && i.ImgUrl == UserListModel.ImgUrl);

            if (UserDetailModel != null)
            {
                UserListModel.Id = UserDetailModel.Id;
            }
        }
    }
}
