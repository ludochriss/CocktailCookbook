﻿using System;
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
using Microsoft.AspNetCore.Authorization;

namespace CocktailCookbook.Controllers
{

    

   //[Authorize(Policy ="UserOnly")]
   //[Authorize(Roles ="Bar")]
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _um;
        private readonly SignInManager<IdentityUser> _sm;
       

        public PostsController(ApplicationDbContext context, UserManager<IdentityUser> um, SignInManager<IdentityUser> sm)
        {
            _context = context;
            _um = um;
                _sm =sm;
        }

        // GET: Posts

        //pass in id of user to check if the user is the same as the person who posted to edit posts
        //only person who created post can delete it 
        public async Task<IActionResult> Index()
        {
            //incase of bypassing with url, validates that 
            //TODO:remove edit and details button from view
            //TODO:remove delete post funtionality for all but admin
            if (_sm.IsSignedIn(User))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


                ViewBag.isAuthor = await _context.Staff.FirstOrDefaultAsync(s => s.UserId == userId);

                return View(await _context.Post.Include(p=>p.Author).ToListAsync());

            }
            else
            {
             
             return View("NotLoggedIn");
            }
           

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

            if (author == null)
            {
               var u = new User
                {
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    NickName = "Unknown",
                    Email = "Unknown"
                };
                author = u;
                _context.Add(u);
                _context.SaveChanges();
               
            }
            ViewBag.NickName = author.NickName;

            
               

            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content")] Post post)
        {
            var currentUser = await _um.GetUserAsync(User);
            post.Author = await _context.Staff.FirstOrDefaultAsync(u => u.UserId == currentUser.Id);
            post.TimeCreated = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        //takes a user ID and checks against current user to see if their Id Matches, returns bool
        public async Task<bool> IsCurrentUser(string id)
        {
            var currentUser = await _um.GetUserAsync(User);
            if (id == currentUser.Id)
            {
                return true;
            }
            else
            {
                return false;
            }
            

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
            //check if the ID is null return not found page if true
            if (id == null)
            {
                return NotFound();
            }
            //find the current users id
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            //var uid = _um.
            //match with database 
            var currentUser = await _context.Staff.FirstOrDefaultAsync(s=>s.UserId ==userId);
            if (currentUser == null)
            {
                
               
            }
            //bring the post from the database with author and comments
            var p = await _context.Post.Include(p=>p.Author).Include(p=>p.Comments).FirstOrDefaultAsync(p => p.Id == id);

            var c = new ReplyCommentViewModel
            {
                PostId = p.Id,
                PostTitle = p.Title,
                PostContent = p.Content,
                PostAuthor = p.Author.NickName,
                AuthorUserId = p.Author.UserId,
                Author = currentUser.UserId,
                Comments = p.Comments
                

            };           
            return View(c);

        }
        [HttpPost, ActionName("Reply")]
        public async Task<IActionResult> Reply([Bind("Id,Content,Author,PostId,PostTitle,PostContent,PostAuthor,AuthorUserId")]ReplyCommentViewModel cvm)
        {
                       
            if (ModelState.IsValid)
            {
                var comment = new Comment
                {
                    Time = DateTime.Now,
                    PostId = cvm.PostId,
                    AuthorId = cvm.AuthorUserId,
                    Content = cvm.Content

                };
                   _context.Add(comment);
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
            var p = await _context.Post.Where(p => p.Id == id)
                .Select(p=>p)
                .Include(p=>p.Author)
                .Include(p=>p.Comments)
                 .FirstOrDefaultAsync();

            //checks the current user against the post's author and will allow edit or delete functionality within the view
           
           

            var comments = await _context.Comment.
                Where(c=>c.PostId ==id)
                 .ToListAsync();
            //p.Comments = comments;

            return View(p);
        }
        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.Id == id);
        }
    }
}
