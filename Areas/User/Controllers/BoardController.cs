using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectPlanningApp.Areas.User.Models;
using ProjectPlanningApp.Data;

namespace ProjectPlanningApp.Areas.User.Controllers
{
    [Area("User")]
    public class BoardController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public BoardController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            ViewBag.UserId = _userManager.GetUserId(HttpContext.User);
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }

        //POST Create Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Board board)
        {
            if (ModelState.IsValid)
            {
                _db.Boards.Add(board);
                await _db.SaveChangesAsync();
                return this.RedirectToAction("ViewBoard", "Board", new { id = board.Id });
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                                       .Where(y => y.Count > 0)
                                       .ToList();
            }
            return NotFound();
        }

        //GET ViewBoard Action Method
        [HttpGet]
        public IActionResult ViewBoard(Board board)
        {
            var boards = _db.Boards.Include(b => b.Lists).ThenInclude(c=>c.Cards).FirstOrDefault(b => b.Id == board.Id);
            return View(boards);
        }

        //POST CreateList Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBoardList(string id, string listName)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (id != null)
                    {
                        int boardId = int.Parse(id);
                        Board board = _db.Boards.Include(b => b.Lists).FirstOrDefault(b => b.Id == boardId);
                        BoardList boardList = new BoardList()
                        {
                            BoardId = boardId,
                            Name = listName
                        };
                        board.Lists.Add(boardList);
                        await _db.SaveChangesAsync();
                        return this.RedirectToAction("ViewBoard", "Board", new { id = board.Id });
                    }
                    //If we get to any of this returns, something failed.
                    return View("ViewBoard");
                }
                return View("ViewBoard");
            }
            catch
            {
                return NotFound();
            }
        }

        //POST CreateCard Action Method
        public async Task<IActionResult> CreateCard(string listIdCard, string cardName, string cardColour)
        {
            if (listIdCard != null || cardName != null)
            {
                if (ModelState.IsValid)
                    try
                    {
                        int listId = int.Parse(listIdCard);
                        BoardList list = _db.BoardLists.Include(l=>l.Cards).FirstOrDefault(l => l.Id == listId);
                        Card card = new Card()
                        {
                            Name = cardName,
                            CoverColour = cardColour
                        };
                        list.Cards.Add(card);
                        Board board = _db.Boards.FirstOrDefault(b => b.Id == list.BoardId);
                        await _db.SaveChangesAsync();
                        return RedirectToAction("ViewBoard", "Board", new { id = board.Id });
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("ERROR: " + ex);
                        return View("ViewBoard");
                    }

            }
            return View("ViewBoard");
        }

    }
}
