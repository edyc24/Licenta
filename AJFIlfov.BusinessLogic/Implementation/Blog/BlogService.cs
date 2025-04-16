using AJFIlfov.BusinessLogic.Implementation.Blog.Models;
using AJFIlfov.DataAccess;
using AJFIlfov.Entities.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using AJFIlfov.BusinessLogic.Base;
using AJFIlfov.Common.DTOs;
using AJFIlfov.BusinessLogic.Implementation.Audituri;
using PdfSharp.Logging;

namespace AJFIlfov.BusinessLogic.Implementation.Blog
{
    public class BlogService : BaseService
    {
        private readonly AuditService _auditService;

        public BlogService(ServiceDependencies dependencies, AuditService auditService)
            : base(dependencies)
        {
            this._auditService = auditService;
        }

        public List<BlogPostViewModel> GetAllPosts()
        {
            var posts = UnitOfWork.BlogPosts.Get();
            return Mapper.Map<List<BlogPostViewModel>>(posts);
        }

        public BlogPostViewModel GetPostById(int id)
        {
            var post = UnitOfWork.BlogPosts.Get().FirstOrDefault(p => p.Id == id);
            return Mapper.Map<BlogPostViewModel>(post);
        }

        public bool CreatePost(BlogPostViewModel postModel)
        {
            var post = Mapper.Map<BlogPost>(postModel);
            post.CreatedAt = DateTime.Now;
            post.PublishedBy = CurrentUser.FirstName + " " + CurrentUser.LastName;
            UnitOfWork.BlogPosts.Insert(post);
            _auditService.LogAction(CurrentUser.FirstName + " " + CurrentUser.LastName, "Created a new blog post!");
            UnitOfWork.SaveChanges();
            return true;
        }

        public void UpdatePost(BlogPostViewModel postModel)
        {
            var post = UnitOfWork.BlogPosts.Get().FirstOrDefault(p => p.Id == postModel.Id);
            if (post == null)
            {
                throw new Exception("Blog post not found.");
            }

            post.Title = postModel.Title;
            post.Content = postModel.Content;
            UnitOfWork.SaveChanges();
        }

        public void DeletePost(int id)
        {
            var post = UnitOfWork.BlogPosts.Get().FirstOrDefault(p => p.Id == id);
            if (post != null)
            {
                UnitOfWork.BlogPosts.Delete(post);
                _auditService.LogAction(CurrentUser.FirstName + " " + CurrentUser.LastName, "Deleted a blog post!");
                UnitOfWork.SaveChanges();
            }
        }
    }
}
