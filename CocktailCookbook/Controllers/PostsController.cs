using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CocktailCookbook.Data;
using CocktailCookbook.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CocktailCookbook.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _um;
       

        public PostsController(ApplicationDbContext context, UserManager<IdentityUser> um)
        {
            _context = context;
            _um = um;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Post.ToListAsync());
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            //var p = new Post { Author = User.Identity.GetUserId() };
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var author =  _context.Staff.FirstOrDefault(s => s.UserId == userId);
            ViewBag.NickName = author.NickName;

            
               

            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content,Author")] Post post)
        {
           
             
            post.TimeCreated = DateTime.Now;
           


            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,TimeCreated")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Post.FindAsync(id);
            _context.Post.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet, ActionName("Reply")]
        public async Task<IActionResult> Reply(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var author = _context.Staff.FirstOrDefault(s => s.UserId == userId);


            var p = await _context.Post.FirstOrDefaultAsync(p => p.Id == id);
            ViewBag.OriginalAuthor = p.Author;
            var c = new Comment
            {
                Author = author.NickName,
                PostId = p.Id
               
            };

            return View(c);
        }
        [HttpPost, ActionName("Reply")]
        public async Task<IActionResult> Reply([Bind("Content,Author,PostId")]Comment comment)
        {


            if (ModelState.IsValid)
            {
                comment.Time = DateTime.Now;
                var comments  = _context.Comment.ToList();
                int length = comments.Count();
                comment.Id = comment.PostId +"-" + length.ToString();

                 _context.Comment.Add(comment);
              await _context.SaveChangesAsync();

                 }
            else
            {
                return View(NotFound());
            }


            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ViewThread(int? id)
        {

            var post = await _context.Post.FirstOrDefaultAsync(p => p.Id == id);


            var comments = await _context.Comment.
                Where(c=>c.PostId ==id)
                 .ToListAsync();
            post.Comments = comments;

            return View(post);
        }
        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.Id == id);
        }
    }
}
