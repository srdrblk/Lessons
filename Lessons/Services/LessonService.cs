using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using LessonServer;
using Microsoft.Extensions.Logging;


namespace Lessons
{
    public class LessonService : Lesson.LessonBase 
    {
        private readonly ILogger<LessonService> _logger;

        private List<LessonModel> lessons ;
        public LessonService(ILogger<LessonService> logger)
        {
            _logger = logger;
            lessons = new List<LessonModel>() { 
            new LessonModel(){Id= Guid.NewGuid().ToString(), Name= "math",Credit=2},
             new LessonModel(){Id= Guid.NewGuid().ToString(), Name= "physic",Credit=3},
              new LessonModel(){Id= Guid.NewGuid().ToString(), Name= "history",Credit=4},
               new LessonModel(){Id= Guid.NewGuid().ToString(),  Name= "geoprhy",Credit=5}

            };

            for (int i = 1; i < 1000; i++)
            {
                lessons.Add(new LessonModel() { Id = Guid.NewGuid().ToString(), Name = "geoprhy" + i, Credit = i });
            }
        }



        public override Task<LessonModel> AddLesson(LessonModel request, ServerCallContext context)
        {
            request.Id = Guid.NewGuid().ToString();
            lessons.Add(request);
            return Task.FromResult(request);
        }
        public override Task<GetAllReply> GetLessons(GetAllRequest request, ServerCallContext context)
        {
            GetAllReply getAllRepl = new GetAllReply();
            getAllRepl.Lessons.AddRange(lessons);

            return Task.FromResult(getAllRepl);
        }

        public override Task<ResponseMessage> DeleteLesson(DeleteLessonRequest request, ServerCallContext context)
        {
            ResponseMessage response = new ResponseMessage();
            var lesson = lessons.FirstOrDefault(l => l.Id == request.LessonId);
            try
            {
                lessons.Remove(lesson);
                response.Message = "Deleted";
            }
            catch (Exception ex)
            {

                response.Message = ex.Message;
            }

            return Task.FromResult(response);
        }
    }
}
