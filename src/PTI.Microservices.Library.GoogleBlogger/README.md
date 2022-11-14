# PTI.Microservices.Library.GoogleBlogger

This is part of PTI.Microservices.Library set of packages

The purpose of this package is to facilitate the calls to Google Blogger APIs, while maintaining a consistent usage pattern among the different services in the group

**Examples:**

## List Posts
    GoogleBloggerService googleBloggerService =
        new GoogleBloggerService(null, base.GoogleBloggerConfiguration);
    var result = await googleBloggerService.ListPostsAsync();