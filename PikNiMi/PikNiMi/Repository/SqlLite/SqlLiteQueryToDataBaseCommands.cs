﻿using System;

namespace PikNiMi.Repository.SqlLite
{
    public static class SqlLiteQueryToDataBaseCommands
    {
        private const string FullProductInfoTable = "FullProductInfoTable FPIT";
        private const string FullProductInfoTableShort = "FPIT";
        private static readonly string ProductTypeNone = "Pasirinkti tipą...";

        public static string GetAllOfFullProductInfoCommand()
        {
            string getExistingServiceQuery =
                $@"
                    SELECT * FROM {FullProductInfoTable}
                ";

            return getExistingServiceQuery;
        }

        public static string SearchFullProductInfoBySearchRequest(string search, string productType)
        {
            string searchQuery;

            if (productType == ProductTypeNone)
            {
                searchQuery = 
                    $@"
                        SELECT * FROM {FullProductInfoTable}
                        WHERE * LIKE '%{search}%'
                        ORDER BY {FullProductInfoTableShort}.ProductId DESC;
                    ";
            }

            searchQuery = string.Empty;

            return searchQuery;
        }
    }
}