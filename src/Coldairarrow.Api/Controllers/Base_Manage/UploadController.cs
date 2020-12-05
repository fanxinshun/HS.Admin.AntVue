using Coldairarrow.Business.MiniPrograms;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.Base_Manage
{
    [Route("/Base_Manage/[controller]/[action]")]
    public class UploadController : BaseApiController
    {
        readonly IConfiguration _configuration;
        public UploadController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult UploadFileByForm()
        {
            var file = Request.Form.Files.FirstOrDefault();
            if (file == null)
                return JsonContent(new { status = "error" }.ToJson());

            string path = $"/Upload/{Guid.NewGuid().ToString("N")}/{file.FileName}";
            string physicPath = GetAbsolutePath($"~{path}");
            string dir = Path.GetDirectoryName(physicPath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            using (FileStream fs = new FileStream(physicPath, FileMode.Create))
            {
                file.CopyTo(fs);
            }

            string url = $"{_configuration["WebRootUrl"]}{path}";
            var res = new
            {
                name = file.FileName,
                status = "done",
                thumbUrl = url,
                url = url
            };

            return JsonContent(res.ToJson());
        }

        /// <summary>
        /// 文件上传到FastDFS服务器
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UploadFile()
        {
            var file = Request.Form.Files.FirstOrDefault();
            if (file == null)
                return JsonContent(new { status = "error" }.ToJson());

            string fileNmae = file.FileName;
            Stream stream = file.OpenReadStream();
            if (stream == null || string.IsNullOrEmpty(fileNmae))
            {
                return JsonContent(new { status = "error" }.ToJson());
            }
            string file_extension = Path.GetExtension(fileNmae);
            string image_url = await FastDFSHelper.UpdateFile(stream, file_extension);

            var res = new
            {
                name = file.FileName,
                status = "done",
                thumbUrl = image_url,
                url = image_url
            };

            return JsonContent(res.ToJson());
        }
    }
}
