using System;
namespace EasyNetQ.Customers.API.Models
{
	public class CustomerInputModel
	{
		public int Id { get; set; }
        public string FullName { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
    }
}

