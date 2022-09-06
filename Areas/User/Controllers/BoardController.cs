using System;
using System.Collections.Generic;
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
                return View("ViewBoard", board);
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                                       .Where(y => y.Count > 0)
                                       .ToList();
            }
            return View(board);
        }

        //GET ViewBoard Action Method
        public IActionResult ViewBoard(Board board)
        {
            var boards = _db.Boards.Include(b => b.Lists).Where(b => b.Id == board.Id);
            return View(boards);
        }

        //POST CreateList Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBoardList(string id, string listName)
        {
            try
            {
                if (id != null)
                {
                    var boardId = Int32.Parse(id);
                    var board = _db.Boards.Include(b=>b.Lists).FirstOrDefault(b => b.Id == boardId);
                    var boardList = new BoardList()
                    {
                        BoardId = boardId,
                        Name = listName
                    };
                    board.Lists.Add(boardList);
                    await _db.SaveChangesAsync();
                    return View("ViewBoard", board);

                }
                return View("ViewBoard");
            }
            catch
            {
                return NotFound();
            }
        }

        //POST CreateCard Action Method
        public async Task<IActionResult> CreateCard(string id, string cardName)
        {

            return View("ViewBoard");
        }
    }
}
