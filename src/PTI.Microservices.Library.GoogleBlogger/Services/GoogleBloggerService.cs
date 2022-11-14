using Google.Apis.Blogger.v3;
using Google.Apis.Blogger.v3.Data;
using Google.Apis.Http;
using Microsoft.Extensions.Logging;
using PTI.Microservices.Library.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PTI.Microservices.Library.Services
{
    /// <summary>
    /// Service in charge of exposing access to Google Blogger functionality
    /// </summary>
    public class GoogleBloggerService
    {
        private ILogger<GoogleBloggerService> Logger { get; }
        private GoogleBloggerConfiguration GoogleBloggerConfiguration { get; }
        private BloggerService BloggerService { get; }

        /// <summary>
        /// Creates a new instance of <see cref="GoogleBloggerService"/>
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="googleBloggerConfiguration"></param>
        public GoogleBloggerService(ILogger<GoogleBloggerService> logger, 
            GoogleBloggerConfiguration googleBloggerConfiguration)
        {
            this.Logger = logger;
            this.GoogleBloggerConfiguration = googleBloggerConfiguration;
            this.BloggerService =
                new BloggerService(
                    new Google.Apis.Services.BaseClientService.Initializer()
                    {
                        ApiKey = googleBloggerConfiguration.Key,
                        ApplicationName = googleBloggerConfiguration.ApplicationName,
                    });
        }

        /// <summary>
        /// Retrieves a list of posts
        /// </summary>
        /// <param name="pageToken"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IList<Post>> ListPostsAsync(string pageToken=null,
            CancellationToken cancellationToken=default)
        {
            try
            {
                var request = this.BloggerService.Posts.List(this.GoogleBloggerConfiguration.BlogId);
                if (!String.IsNullOrWhiteSpace(pageToken))
                    request.PageToken = pageToken;
                var response = await request.ExecuteAsync(cancellationToken);
                return response.Items;
            }
            catch (Exception ex)
            {
                this.Logger?.LogError(ex.Message, ex);
                throw;
            }
        }

        /// <summary>
        /// Creates a new post
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="labels"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<Post> CreatePostAsync(string title, 
            string content, IList<string> labels,
            CancellationToken cancellationToken=default)
        {
            try
            {
                Post model = new Post()
                {
                    Content = content,
                    Labels = labels,
                    Title = title,
                };
                var request = this.BloggerService.Posts.Insert(model, this.GoogleBloggerConfiguration.BlogId);
                return await request.ExecuteAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                this.Logger?.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
