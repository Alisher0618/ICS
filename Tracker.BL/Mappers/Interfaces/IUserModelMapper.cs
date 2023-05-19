// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Tracker.BL.Models;
using Tracker.DAL.Entities;

namespace Tracker.BL.Mappers.Interfaces
{
    public interface IUserModelMapper : IModelMapper<UserEntity, UserListModel, UserDetailModel>
    {
    }
}
