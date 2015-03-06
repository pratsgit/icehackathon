using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EntityModels;

namespace EntityExtractor
{
    public static class Extensions
    {
        /// <summary>
        /// Epoc start time
        /// </summary>
        private static readonly DateTime EpocStartTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Convert to Epoc time seconds
        /// </summary>
        /// <param name="date">date from which epoc time is calculated</param>
        /// <returns>time from epoc</returns>
        public static long ToEpocTime(this DateTime date)
        {
            return Convert.ToInt64(
                (date - EpocStartTime).TotalSeconds);
        }
    }
}
