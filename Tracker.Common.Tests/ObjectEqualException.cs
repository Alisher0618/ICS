// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Xunit.Sdk;

namespace Tracker.Common.Tests;

public class ObjectEqualException : AssertActualExpectedException
{
    public ObjectEqualException(object? expected, object? actual, string message)
        : base(expected, actual, "Assert.Equal() Failure")
    {
        Message = message;
    }

    public override string Message { get; }
}
