using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataLayer.classes
{
    class FilePathSingleton
    {
        //-------------------------------------------Instance variables-------------------------------------------
        //This string will find the file by going out 3 directories first (Debug->bin->PresentationLayer)
        //Then go in two directories (->DataLayer->Data);
        public string path = @"..\..\..\DataLayer\Data\data.csv";
    }
}
