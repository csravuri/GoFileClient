﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GoFileClient.Entities
{
    public class FileDetailsEntity : Entity
    {
        public string FileName { get; set; }
        public string Size { get; set; }
        public string FullPath { get; set; }
        public FileInfo OriginalFileInfo { get; set; }
    }
}
