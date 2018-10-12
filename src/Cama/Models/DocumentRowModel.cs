﻿using System.ComponentModel;
using Cama.Service;

namespace Cama.Models
{
    public class DocumentRowModel : INotifyPropertyChanged
    {
#pragma warning disable 067
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 067

        public FileMutationsModel MFile { get; set; }

        public int MutationCount => MFile.MutationDocuments.Count;
    }
}