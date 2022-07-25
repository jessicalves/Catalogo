using System;
using System.ComponentModel;

namespace Catalogo.Model
{
    public class Policy : INotifyPropertyChanged
    {
        public int Min { get; set; }
        public double Discount { get; set; }

        public Policy()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

