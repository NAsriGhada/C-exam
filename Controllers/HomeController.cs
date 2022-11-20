using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using csharpexam.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace csharpexam.Controllers;

public class HomeController : Controller
{
    private MyContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

     //Register
    [HttpPost("/register")]
    public IActionResult Register(User newUser)
    {
        if (ModelState.IsValid)
        {
            if (_context.Users.Any(u => u.Email == newUser.Email))
            {
                ModelState.AddModelError("Email", "Email is already in use! ");
                return View("Index");
            }
            else
            {
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                _context.Add(newUser);
                _context.SaveChanges();
                HttpContext.Session.SetInt32("userId", newUser.UserId);
                return RedirectToAction("Dashboard");
            }
        }
        return View("Index");
    }

    // LOGIN
    [HttpPost("/login")]
    public IActionResult LoginUser(LoginUser loginUser)
    {
        if (ModelState.IsValid)
        {
            var UserExist = _context.Users.FirstOrDefault(u => u.Email == loginUser.LoginEmail);
            if (UserExist == null)
            {
                ModelState.AddModelError("LoginEmail", "Invalid Email/Password");
                return View("Index");
            }
            PasswordHasher<LoginUser>? hasher = new PasswordHasher<LoginUser>();
            var result = hasher.VerifyHashedPassword(loginUser, UserExist.Password, loginUser.LoginPassword);
            if (result == 0)
            {
                ModelState.AddModelError("LoginPassword", "Invalid Email/Password");
                return View("Index");
            }
            HttpContext.Session.SetInt32("userId", UserExist.UserId);
            return RedirectToAction("Dashboard");
        }
        return View("Index");
    }

    // DASHBOARD
    public IActionResult Dashboard()
    {
        if (HttpContext.Session.GetInt32("userId") == null)
        {
            return RedirectToAction("Index");
        }
        User? LoggedUser = _context.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("userId"));
        ViewBag.LoggedUser = LoggedUser;

        List<MeetUp> AllMeetings = _context.MeetUps.Include(p => p.ParticipantsList).ThenInclude(p => p.User).Where(p => p.Date >= DateTime.Now).OrderBy(g => g.Date).ToList();
        ViewBag.AllMeetings = AllMeetings;

        List<User> userCreators = _context.Users.ToList();
            ViewBag.Creators = userCreators;

        User LoggedUserOne = _context.Users.Include(u=> u.Participants).FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("userID"));
        return View("Dashboard");
    }


 // READ AND CREATE A MEETUP PLAN
    [HttpGet("/meetups/new")]
    public IActionResult NewActivity()
    {
        User? LoggedUser = _context.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("userId"));
        ViewBag.LoggedUser = LoggedUser;
        return View();
    }

    [HttpPost("/newMeetUp")]
    public IActionResult AddNewMeeting(MeetUp newMeeting)
    {
        User? LoggedUser = _context.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("userId"));
        ViewBag.LoggedUser = LoggedUser;
        if (ModelState.IsValid)
        {
            newMeeting.UserId = (int)HttpContext.Session.GetInt32("userId");
            _context.Add(newMeeting);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        return View("NewActivity");
    }

    
    // GET THE WEDDING DETAILS
    [HttpGet("/meetups/{meetUpId}")]
    public IActionResult MeetUpDetails(int meetUpId)
    {

        User? LoggedUser = _context.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("userId"));
        ViewBag.LoggedUser = LoggedUser;
        MeetUp? OneMeetUp = _context.MeetUps.Include(g => g.ParticipantsList).ThenInclude(u => u.User).FirstOrDefault(w => w.MeetUpId == meetUpId);
        ViewBag.OneMeetUp = OneMeetUp;
        return View();
    }


    // delete a meet up
    [HttpGet("/delete/{MeetUpId}")]
    public IActionResult DeleteMeeting(int MeetUpId)
    {
       MeetUp RetrievedMeeting = _context.MeetUps.SingleOrDefault(m => m.MeetUpId == MeetUpId);

        _context.MeetUps.Remove(RetrievedMeeting);

        _context.SaveChanges();
        return RedirectToAction("Dashboard");
    }


    [HttpGet("/leave/{MeetUpId}")]
        public IActionResult NoActivity(int MeetUpId)
        {
            Participation participation = _context.Participations.FirstOrDefault(a => a.ParticipationId == MeetUpId);
            _context.Participations.Remove(participation);
            _context.SaveChanges();
            return RedirectToAction("Home");
        }
    
    // LOGOUT
    [HttpGet("/logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

    // join a meeting
    [HttpPost("/reserve")]
    public IActionResult Reserve(Participation newParticipation)
    {

        newParticipation.UserId = (int)HttpContext.Session.GetInt32("userId");
        _context.Add(newParticipation);
        _context.SaveChanges();
        return RedirectToAction("Dashboard");

    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
