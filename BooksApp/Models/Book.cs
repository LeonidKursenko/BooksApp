using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BooksApp.Models
{
    public class Book : NotifyPropertyChanged
    {
        private string _title;
        private int _price;

        public int Id { get; set; }
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        public int Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged("Surname");
            }
        }

        public virtual ObservableCollection<Author> Authors { get; set; }

    }
}
