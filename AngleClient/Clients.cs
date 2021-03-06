﻿using System;

namespace AngleClient
{
    internal class Clients
    {
        private static string[] clientCodes = new[]
        {
            "BGO",
            "NUOL",
            "NTC",
            "CQT",
            "WYXP",
            "LXHA",
            "LOE",
            "MWWH",
            "SXZ"
        };

        private static Random random = new Random();
        
        public static string GetClientCode()
        {
            return clientCodes[random.Next(0, 9)];
        }
    }
}