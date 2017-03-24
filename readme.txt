CodeFirst:
Replace the connection string in the App.config file of the Sitemap.DAL project
1. PM> enable-migrations
2. PM> add-migration -c SitemapDBContext
3. PM> update-database
