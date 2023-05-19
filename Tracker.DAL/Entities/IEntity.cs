// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

namespace Tracker.DAL.Entities;

public interface IEntity
{
    Guid Id { get; set; }
}

