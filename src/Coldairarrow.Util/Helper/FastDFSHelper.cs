using FastDFS.Client;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Coldairarrow.Util
{
    public class FastDFSHelper
    {
        static FastDFSHelper()
        {
            string[] trackers = ConfigHelper.GetValue("FastDFS:Host")?.Split(new char[','], StringSplitOptions.RemoveEmptyEntries);
            int.TryParse(ConfigHelper.GetValue("FastDFS:Port"), out int port);

            var trackerIPs = new List<IPEndPoint>();
            foreach (var tracker in trackers)
            {
                trackerIPs.Add(new IPEndPoint(IPAddress.Parse(tracker), port));
            }
            ConnectionManager.Initialize(trackerIPs);
        }

        public static async Task<string> UpdateFile(Byte[] content, string fileExt = "jpg", string groupName = "group1")
        {
            if (content == null) throw new ArgumentNullException(nameof(content));

            var node = await FastDFSClient.GetStorageNodeAsync(groupName);
            return await FastDFSClient.UploadFileAsync(node, content, fileExt);
        }

        public static async Task<string> UpdateFile(Stream fileStream, string fileExt = "jpg", string groupName = "group1")
        {
            if (fileStream == null) throw new ArgumentNullException(nameof(fileStream));

            byte[] content = new byte[fileStream.Length];

            using (BinaryReader reader = new BinaryReader(fileStream))
            {
                content = reader.ReadBytes((int)fileStream.Length);
            }

            var node = await FastDFSClient.GetStorageNodeAsync(groupName);
            return await FastDFSClient.UploadFileAsync(node, content, fileExt);
        }
    }
}
