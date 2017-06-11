using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UmbBase.Web.Business
{
    public class ApplicationConstants
    {
        public static int BlogPageSize = 50;
        public static string LatestBlogsCacheName = "LatestBlogs";
        public static string LocalizedSiteDocumentAlias = "clientHome";
        public static string GalleryItemDocumentAlias = "galleryItemDocType";
        public static string VideoItemDocumentAlias = "video";
        public static string ServicePageDocumentAlias = "servicesPage";
        public static string ContentFolderDocumentAlias = "contentFolder";
        public static string ContentPageDocumentAlias = "clientContentPage";
        public static string BlogFolderDocumentAlias = "blogFolder";
        public static string BlogDocumentAlias = "blogPage";
        public static string ProjectDocumentAlias = "project";
        public static string ContentPagePublishDateFieldAlias = "pagePublishDate";
        public static int TrCategoriesRootId = 8237;
        public static int EnCategoriesRootId = 8238;
        public static int TrProjectsRootId = 8350;
        public static int EnProjectsRootId = 8462;
    }
}