using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MyFriends.Models;
using System.Diagnostics;
using MyFriends.DAL;
namespace MyFriends.Controllers;
using Microsoft.EntityFrameworkCore;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    //פונקצית הוספת תמונה לחבר קיים במערכת
    [HttpPost,ValidateAntiForgeryToken]
    public IActionResult AddImage(Friend friend)
    {
        Data.Get.Friends.FirstOrDefault(f => f.ID == friend.ID).AddImage(friend.Images.First().myImage);
        Data.Get.SaveChanges();
        return RedirectToAction("Details", new {id = friend.ID});
    }
    // פונקציה להצגת פרטי חבר אחד כולל תמונות
    public IActionResult Details(int? ID)
    {
        if (ID == null)
            return RedirectToAction("Index");
        Friend friend = Data.Get.Friends.Include(f=> f.Images).FirstOrDefault(x => x.ID == ID); // Include comes from Microsoft.EntityFrameworkCore and includes the images now
        //Data.Get.Friends.ToList().Find(f=>f.ID == id); another way
        if (friend == null)
            return RedirectToAction("Index");
        return View(friend);
    }
    public IActionResult Delete(int? ID)
    {
        if(ID == null)
            return RedirectToAction("Index");
        Friend findFriend = Data.Get.Friends.FirstOrDefault(x => x.ID == ID);
        if (findFriend != null)
        {
            foreach(Image image in Data.Get.Images)
            {
                if(image.friend == findFriend)
                    Data.Get.Images.Remove(image);
            }
            Data.Get.Friends.Remove(findFriend);
            Data.Get.SaveChanges();
        }
        return RedirectToAction("Index");
    }

    //פונקציה לעריכת פרטי חבר  
    public IActionResult Edit(int? ID)
    {
        if (ID == null)
            return RedirectToAction("Index");
        Friend friend = Data.Get.Friends.FirstOrDefault(x => x.ID == ID);
        //Data.Get.Friends.ToList().Find(f=>f.ID == id); another way
        if (friend == null)
            return RedirectToAction("Index");
        return View(friend);
    }
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Edit(Friend friend)
    {
        if (friend == null)
            return RedirectToAction("Index");
        Friend refriend = Data.Get.Friends.FirstOrDefault(x => x.ID == friend.ID);
        if (refriend == null)
            return RedirectToAction("Index");
            refriend.PhoneNumber = friend.PhoneNumber;
            refriend.FirstName = friend.FirstName;
        refriend.LastName = friend.LastName;
        refriend.Email = friend.Email;
        Data.Get.SaveChanges();
        return RedirectToAction("Index");

    }
    //פונקציה להוספת חבר
    public IActionResult CreateFriend()
    {
        return View(new Friend());
    }
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult CreateFriend(Friend friend)
    {
        Data.Get.Friends.Add(friend);
        Data.Get.SaveChanges();
        return RedirectToAction("Index");
    }
    public IActionResult Index() // show all friends
    {
        List<Friend> list = Data.Get.Friends.ToList();
        return View(list);
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
