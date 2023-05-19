// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Tracker.BL.Mappers.Interfaces;
using Tracker.BL.Models;
using Tracker.DAL.Entities;

namespace Tracker.BL.Mappers
{
    public abstract class
        ModelMapperBase<TEntity, TListModel, TDetailModel> : IModelMapper<TEntity, TListModel, TDetailModel>
        where TEntity : IEntity
        where TListModel : IModel
        where TDetailModel : IModel
    {
        public abstract TListModel MapToListModel(TEntity? entity);

        public IEnumerable<TListModel> MapToListModel(IEnumerable<TEntity> entities)
            => entities.Select(MapToListModel);

        public abstract TDetailModel MapToDetailModel(TEntity? entity);
        public abstract TEntity MapToEntity(TDetailModel model);
    }
}
