using FirstApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;


namespace FirstApplication.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private IWebHostEnvironment _environment;
        public UserController(IWebHostEnvironment environment) 
        {
            _environment = environment;
        }
      
        [HttpGet]
        public DataTable GetAllUser()
        {
            var user = SQLDatabase.GetDataTable("select * from user");

            return user;
        }
        [HttpPost]
        public ActionResult InsertUser([FromBody] User user) {
            if (ModelState.IsValid)
            {
                SQLDatabase.ExecNonQuery($"INSERT INTO `user`( `name`, `email`, `password`) VALUES ('{user.Name}','{user.Email}','{user.Password}')");
                return Ok(user);
            }
            else
            {
                return BadRequest(ModelState.ToList());
            }
        }


        [HttpGet]
        public ActionResult GetById(int id)
        {

            var user = SQLDatabase.GetDataTable($"select * from `user` where id={id}");

           
            if (user != null)
            return Ok(user);
            else 
                return NotFound("User not Found"); 
        }
        [HttpGet]
        public DataTable GetByEmail(string email)
        {
            var user = SQLDatabase.GetDataTable($"select * from `user` where `email`='{email}'");


            return user;
        }
        [HttpPut("{Id}")]
        public ActionResult UpdateData(int Id,UserUpdateModel request)
        {
           int a= SQLDatabase.ExecNonQuery($"UPDATE  `user` SET `name`='{request.Name}',`email`='{request.Email}',`password`='{request.Password}' where id={Id}");
            if (a>1)
                return BadRequest();
            return Ok($"{a}");
           
        }
        [HttpDelete("{Id}")]
        public ActionResult Delete(int Id)
        {
            int a = SQLDatabase.ExecNonQuery($"delete from `user` where id={Id}");
            if (a > 1)
                return BadRequest();
            return Ok($"{a}");
        }
        [HttpPost]
        public async Task<ActionResult> UploadImage([FromForm]ImageModel model)
        {
            string BasePath = Path.Combine(_environment.WebRootPath,"ProductImages");
            if (!Directory.Exists(BasePath))
            {
                Directory.CreateDirectory(BasePath);
            }

            string UniqueName = Guid.NewGuid().ToString();
            string FileExtension = Path.GetExtension(model.ImageFile.FileName);

            string NewFilePath = Path.Combine(BasePath, UniqueName + FileExtension);

            using (var fileStream = new FileStream(NewFilePath, FileMode.Create))
            {
                await model.ImageFile.CopyToAsync(fileStream);
            }

            string ReturnPath = "ProductImages/" + UniqueName + FileExtension;
            return Ok(ReturnPath);
        }

    }
}
