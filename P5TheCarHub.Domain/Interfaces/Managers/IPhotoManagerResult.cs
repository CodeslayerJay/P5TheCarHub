using System;
using System.Collections.Generic;
using System.Text;

namespace P5TheCarHub.Core.Interfaces.Managers
{
    public interface IPhotoManagerResult
    {
        bool IsValidImage { get; set; }
        string ImageFullPath { get; set; }
        string ImageUrl { get; set; }
        bool Success { get; set; }
    }
}
