using EmployeeDemo.Models;
using EmployeeDemo.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace EmployeeUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var employee = new Employees() { EmployeeCode = "234", Name = "Mohit", Id = 1, Salary = 35000 };

            var mockRepo = new Mock<IEmployeeService>();
            mockRepo.Setup(x => x.InsertEmployee(employee)).Returns(Task.FromResult(true));            
        }
    }
}
