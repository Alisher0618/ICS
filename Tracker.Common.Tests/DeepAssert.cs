// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using KellermanSoftware.CompareNetObjects;

namespace Tracker.Common.Tests;

public static class DeepAssert
{
    public static void Equal<T>(T? expected, T? actual, params string[] propertiesToIgnore)
    {
        CompareLogic compareLogic = new()
        {
            Config =
            {
                MembersToIgnore = new List<string>(),
                IgnoreCollectionOrder = true,
                IgnoreObjectTypes = true,
                CompareStaticProperties = false,
                CompareStaticFields = false
            }
        };

        foreach (var str in propertiesToIgnore)
            compareLogic.Config.MembersToIgnore.Add(str);

        var comparisonResult = compareLogic.Compare((object)expected!, (object)actual!);
        if (!comparisonResult.AreEqual)
            throw new ObjectEqualException((object)expected!, (object)actual!, comparisonResult.DifferencesString);
    }
}
