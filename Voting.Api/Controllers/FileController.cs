using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Voting.Service.Interfaces;
using Voting.Service.Models;
using Voting.Shared;

namespace IR.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        private readonly ILogger<FileController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FileController(IHttpContextAccessor httpContextAccessor, ILogger<FileController> logger)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        [AllowAnonymous]
        [HttpGet("download/{filename}")]
        public async Task<IActionResult> Download(string filename)
        {
            try
            {
                if (filename == null)
                    return Content("filename not present");

                var path = FileHelper.GetFilePath(filename);

                var memory = new MemoryStream();
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                return File(memory, FileHelper.GetContentType(path), Path.GetFileName(path));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + Environment.NewLine + ex.StackTrace);
            }
            

        }

        [HttpPost("uploadfile")]
        [Authorize(Policy = Policies.Admin)]
        public IActionResult UploadFile([FromForm] FileDto model)
        {
            string userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(
             c => c.Type == ClaimTypes.Name)?.Value;

            if (string.IsNullOrWhiteSpace(userId))
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            //var user = _userService.GetUser(long.Parse(userId));
            //if (user == null)
            //    return BadRequest(new { message = "Username or password is incorrect" });

            //if (!string.IsNullOrWhiteSpace(user.CompanyCode) && user.CompanyCode != model.CompanyCode)
            //    return Ok(false);

            var fileUrl = "";
            //upload file first
            if (model.file != null)
            {
                if (model.file.Length > 0)
                {
                    if (FileHelper.ValidFileExtension(model.file.FileName))
                    {
                        fileUrl = model.file.FileName;

                        var filePath = FileHelper.GetFilePath(fileUrl);

                        using (var stream = System.IO.File.Create(filePath))
                        {
                            model.file.CopyTo(stream);
                        }
                    }
                    else
                        return BadRequest();

                }
            }
            return Ok();
        }


    }
}