//----------------------------------------------------------------------
// <copyright file="MigrationConfigurationSettings.cs" company="Microsoft">
//     Copyright (c) 2013 Microsoft Corporation.  All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace EntityExtractor
{
    using System.Configuration;
    using Microsoft.WindowsAzure.Storage;

    /// <summary>
    /// Configuration settings specific to Cms Migration
    /// </summary>
    public class ConfigurationSettings
    {
        public static CloudStorageAccount EntityStorageAccount
        {
            get
            {
                string connectionString = ConfigurationManager.AppSettings["EntityExtractorStorageKey"];

                if (!string.IsNullOrEmpty(connectionString))
                {
                    return CloudStorageAccount.Parse(connectionString);
                }

                return null;
            }
        }
    }
}
