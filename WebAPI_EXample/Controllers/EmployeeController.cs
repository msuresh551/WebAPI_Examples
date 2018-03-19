using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ClassEmployee;

namespace WebAPI_EXample.Controllers
{
    public class EmployeeController : ApiController
    {
        public IEnumerable<Employee> GET()
        {
            using (EmployeeDBContext db = new EmployeeDBContext())
            {
                return db.Employees.ToList();
            }
        }
        //Employee
        public HttpResponseMessage GET(int id)
        {
            using (EmployeeDBContext db = new EmployeeDBContext())
            {
                var enitity = db.Employees.FirstOrDefault(e => e.EmpID == id);
                if(enitity!=null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, enitity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not found");
                }
            }

        }
        public HttpResponseMessage Post([FromBody]Employee emp)
        {
            try
            {
                using (EmployeeDBContext db = new EmployeeDBContext())
                {
                    db.Employees.Add(emp);
                    db.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, emp);
                    message.Headers.Location = new Uri(Request.RequestUri + emp.EmpID.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }



    }
}
