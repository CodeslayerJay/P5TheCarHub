using System;
using System.Collections.Generic;
using System.Text;

namespace P5TheCarHub.Core.Interfaces.Managers
{
    public interface IPhotoManagerResult
    {
        bool IsValidImage { get; set; }
        string ImagePath { get; set; }
    }
}
