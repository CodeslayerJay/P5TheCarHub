using P5TheCarHub.Core.Interfaces.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace P5TheCarHub.UI.Models.Managers
{
    public class PhotoManagerResult : IPhotoManagerResult
    {
        public bool IsValidImage { get; set; }
        public string ImageFullPath { get; set; }
        public string ImageUrl { get; set; }
        public bool Success { get; set; }
        public Exception Error { get; set; }
    }
}
