using AJFIlfov.BusinessLogic.Base;
using AJFIlfov.BusinessLogic.Implementation.Blog;
using AJFIlfov.BusinessLogic.Implementation.Blog.Models;
using AJFIlfov.Code.Base;
using AJFIlfov.Entities.Entities;
using AJFIlfov.WebApp.Code.Base;
using Microsoft.AspNetCore.Mvc;
using PdfSharp.Logging;

namespace AJFIlfovWebsite.Controllers
{
    public class BlogController : BaseController
    {
        private readonly BlogService _blogService;

        public BlogController(ControllerDependencies dependencies,BlogService blogService) : base(dependencies)
        {
            _blogService = blogService;
        }

        public IActionResult Index()
        {
            var posts = _blogService.GetAllPosts();
            var model = posts.Select(p => new BlogPostViewModel
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                CreatedAt = p.CreatedAt
            }).ToList();
            return View(model);
        }

        public IActionResult Details(int id)
        {
            var post = _blogService.GetPostById(id);
            if (post == null)
            {
                return NotFound();
            }
            var model = new BlogPostViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                CreatedAt = post.CreatedAt
            };
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BlogPostViewModel model)
        {
            if (ModelState.IsValid)
            {
                var post = new BlogPostViewModel
                {
                    Title = model.Title,
                    Content = model.Content
                };
                _blogService.CreatePost(post);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var post = _blogService.GetPostById(id);
            if (post == null)
            {
                return NotFound();
            }
            var model = new BlogPostViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(BlogPostViewModel model)
        {
            if (ModelState.IsValid)
            {
                var post = new BlogPostViewModel
                {
                    Id = model.Id,
                    Title = model.Title,
                    Content = model.Content
                };
                _blogService.UpdatePost(post);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var post = _blogService.GetPostById(id);
            if (post == null)
            {
                return NotFound();
            }
            var model = new BlogPostViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content
            };
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _blogService.DeletePost(id);
            return RedirectToAction("Index");
        }
    }

}
