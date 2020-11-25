using System.Diagnostics;
using System.Threading.Tasks;
using Grpc.Net.Client;
using LessonServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mvc.Models;

namespace mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            GrpcChannel greetChannel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Lesson.LessonClient(greetChannel);
            GetAllRequest getAllRequest = new GetAllRequest();
            var reply = client.GetLessons(getAllRequest);

            return View(reply);
        }
        [HttpPost]
        public JsonResult Create(string Name, int Credit)
        {
            GrpcChannel greetChannel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Lesson.LessonClient(greetChannel);
            LessonModel lesson = new LessonModel();
            lesson.Name = Name;
            lesson.Credit = Credit;
            var response =  client.AddLesson(lesson);

            return Json(response);
        }

        [HttpDelete]
        public async Task<JsonResult> Delete(string id)
        {
            GrpcChannel greetChannel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Lesson.LessonClient(greetChannel);
            DeleteLessonRequest lesson = new DeleteLessonRequest();
            lesson.LessonId = id;
           
            var response = await client.DeleteLessonAsync(lesson);

            return Json(response);
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
}
