﻿using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityModels
{
    public class FashionEntity : TableEntity
    {
        public string FashionCategory { get; set; }

        public string ProviderId { get; set; }

        public string Title { get; set; }

        public string Provider { get; set; }

        public string ImageHref { get; set; }

        public string ItemHref { get; set; }

        public float OriginalPrice { get; set; }

        public float SalePrice { get; set; }

        public int RatingsCount { get; set; }

        public int Ratings { get; set; }
    }
}