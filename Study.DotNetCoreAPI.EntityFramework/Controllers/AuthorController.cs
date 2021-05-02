using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Study.DotNetCoreAPI.EntityFramework.Models;

namespace Study.DotNetCoreAPI.EntityFramework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Author> Get()
        {
            using (var context = new BookStoresDBContext())
            {
                //Get all authors 
                //return context.Author.ToList();

                //Add author
                //Author author = new Author();
                //author.FirstName = "Kaustubh";
                //author.LastName = "Sonar";
                //author.EmailAddress = "k@mail.com";
                //context.Author.Add(author);
                //context.SaveChanges();

                //Update 
                //Author author1 = context.Author.Where(auth => auth.FirstName == "Kaustubh").FirstOrDefault();
                //author1.Phone = "123-131-131";
                //context.SaveChanges();

                //delete 
                //Author author2 = context.Author.Where(auth => auth.FirstName == "Kaustubh").FirstOrDefault();
                //context.Remove(author2);
                //context.SaveChanges();

                //Get by Id 
                return context.Authors.Where(auth=>auth.FirstName=="Kaustubh").ToList();
            }
        }
    }
}
