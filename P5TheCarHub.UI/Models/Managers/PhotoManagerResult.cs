using P5TheCarHub.Core.Interfaces.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace P5TheCarHub.UI.Models.Managers
{
    public class PhotoManagerResult : IPhotoManagerResult
    {
        public bool IsValidImage { get; set; }
        public string ImagePath { get; set; }
        public bool Success { get; set; }
    }
}
