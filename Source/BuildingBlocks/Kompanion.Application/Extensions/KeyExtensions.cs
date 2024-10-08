﻿using System.Security.Cryptography;

namespace Kompanion.Application.Extensions;

public static class KeyExtensions
{
    public static int GenerateRandomKey(int size = 6)
    {
        int minValue = Convert.ToInt32(1.ToString().PadRight(size, '0'));
        int maxValue = Convert.ToInt32(9.ToString().PadRight(size, '9'));

        return RandomNumberGenerator.GetInt32(minValue, maxValue);
    }
}