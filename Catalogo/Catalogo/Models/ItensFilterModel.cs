using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Catalogo.Models
{
    public class ItensFilterModel : INotifyPropertyChanged
    {
        public ObservableCollection<Filter> ItensFilter { get; set; }

        public ItensFilterModel(List<Filter> ListFilters)
        {
            ItensFilter = new ObservableCollection<Filter>();

            foreach (var item in ListFilters)
            {
                ItensFilter.Add(new Filter
                {
                    id = item.id,
                    name = item.name
                });
            }
            ItensFilter.CollectionChanged += ItensFilter_CollectionChanged;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void ItensFilter_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}

