using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAPI.Services;

namespace WebApiUnitTests.Services
{
    [TestClass]
    public class TaskServiceTest
    {
        [TestMethod]
        public void CreateTaskTest()
        {
            var taskService = new TaskService();

            var task = taskService.CreateTask("testName", "description", "taskTheme", 1, "rdima.96@yandex.ru");

            Console.WriteLine(task.GetType().ToString());
        }
    }
}
