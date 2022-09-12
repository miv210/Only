using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;
using Only_test.Models;

namespace Only_test.Controllers
{
    public class ImportUsersController : Controller
    {
        IWebHostEnvironment _appEnviroment;
        Only_tContext db;
        List<User> usersList = new List<User>();
        string path;
        public ImportUsersController(Only_tContext context, IWebHostEnvironment appEnviroment)
        {
            _appEnviroment = appEnviroment;
            db = context;
        }
        public IActionResult Import()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile uploadedFile)
        {
            if (ModelState.IsValid)
            {
                if (uploadedFile != null)
                {
                    path = "/ExcelFiles/" + uploadedFile.FileName;
                    using (var fileStream = new FileStream(_appEnviroment.WebRootPath + path, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(fileStream);
                    }
                }

                using (var wb = new XLWorkbook(_appEnviroment.WebRootPath + path))
                {
                    var ws = wb.Worksheet(1);

                        
                        foreach (var row in ws.RowsUsed().Skip(1))
                        {

                            try
                            {
                                User user = new User
                                {
                                    FullName = row.Cell(4).Value.ToString(),
                                    Department = row.Cell(1).Value.ToString(),
                                    ParentDepartment = row.Cell(2).Value.ToString(),
                                    Position = row.Cell(3).Value.ToString()
                                };
                                usersList.Add(user);
                                
                            }
                            catch (Exception ex)
                            {
                                
                            }
                        }
                    db.Users.AddRangeAsync(usersList);
                    await db.SaveChangesAsync();
                }
            }
            
            return RedirectToAction("Index","Home");
        }
    }
}
