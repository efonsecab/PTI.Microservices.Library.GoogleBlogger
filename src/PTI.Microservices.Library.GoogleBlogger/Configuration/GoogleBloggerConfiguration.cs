using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTI.Microservices.Library.Configuration
{
    /// <summary>
    /// Configuration class for GoogleBloggerService
    /// </summary>
    public class GoogleBloggerConfiguration
    {
        /// <summary>
        /// Google Blogger APi Key
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// Google Blogger Application Name
        /// </summary>
        public string ApplicationName { get; set; }
        /// <summary>
        /// Id of the Blog
        /// </summary>
        public string BlogId { get; set; }
    }
}
