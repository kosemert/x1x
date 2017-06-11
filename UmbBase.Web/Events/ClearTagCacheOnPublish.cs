using Umbraco.Core;
using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Core.Publishing;
using Umbraco.Core.Services;

namespace UmbBase.Web.Events
{
    public class ClearTagCacheOnPublish : ApplicationEventHandler
    {
        private ApplicationContext context;

        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            ContentService.Published += ContentServicePublished;
            context = applicationContext;
        }

        private void ContentServicePublished(IPublishingStrategy sender, PublishEventArgs<IContent> args)
        {
            foreach (var node in args.PublishedEntities)
            {
                if (node.ContentType.Alias == "modelName")
                {
                    context.ApplicationCache.RuntimeCache.ClearCacheItem("CacheKey");
                }
            }
        }
    }
}