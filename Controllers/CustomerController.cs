using System;
using Microsoft.AspNetCore.Mvc;

namespace TrainWreck.Controllers
{
	[ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        public Repository r;

		[HttpGet("names")]
		public string GetCustomerNames()
		{
			try
			{
				return this.GetFirstNames();
			}
			catch(Exception e)
			{
				throw e;
			}
		}

		[HttpPost]
		public void NewCustomer(Customer customer)
		{
			try
			{
				var sql = $"INSERT INTO CUSTOMER VALUES FirstName={customer.FirstName}, LastName={customer.LastName}, UserName={customer.UserName}, Password={customer.Password}, Age={customer.Age}";
				r = new Repository("Driver={SQL Server Native Client 10.0};Server=den1.mssql#.gear.host;Database=DBName;Uid=DBUser;Pwd=myPassword;");
				r.RunQuery(sql);
			}
			catch(Exception e)
			{
			}
		}

		[HttpPost("uploadpassport")]
		public ActionResult UploadPassport(string customerName)
		{
			var file = System.IO.File.Create($"c:\\passports\\{customerName}");
			Request.Body.CopyTo(file);

			return Ok();
		}

		public string GetFirstNames()
		{
			string names;
			r = new Repository("Driver={SQL Server Native Client 10.0};Server=den1.mssql#.gear.host;Database=DBName;Uid=DBUser;Pwd=myPassword;");

			var result = r.RunQuery("SELECT TOP 100 FirstName FROM Customer;");

			for(int i = 1; i <= 100; i++)
			{
				names += ", " + result[i];
			}

			return names;
		}
    }
}
