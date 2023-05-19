// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Tracker.DAL.Entities;
using Tracker.DAL.Mappers;
using Tracker.DAL.Repositories;

namespace Tracker.DAL.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IRepository<TEntity> GetRepository<TEntity, TEntityMapper>() where TEntity : class, IEntity where TEntityMapper : IEntityMapper<TEntity>, new();
        Task CommitAsync();
    }
}
