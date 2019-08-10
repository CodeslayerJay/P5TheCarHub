using System;
using System.Collections.Generic;
using System.Text;

namespace P5TheCarHub.UnitTests.ServicesTests
{
    public class PhotoServiceTests
    {
        private readonly PhotoService _photoService;

        public PhotoServiceTests()
        {
            var photoRepo = new PhotoRepositoryMock();
            _photoService = new PhotoService(photoRepo);
        }
    }
}
