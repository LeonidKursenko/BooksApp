using BooksApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApp.ViewModels
{
    public class BooksViewModel : NotifyPropertyChanged
    {
        private Models.AppContext _db;
        private RelayCommand _editCommand;
        private RelayCommand _removeCommand;
        private IEnumerable<Book> _books;

        public IEnumerable<Book> Books
        {
            get => _books;
            set
            {
                _books = value;
                OnPropertyChanged("Books");
            }
        }

        public BooksViewModel()
        {
            _db = new Models.AppContext();
            _db.Books.Load();
            Books = _db.Books.Local.ToBindingList();
        }

        public RelayCommand EditBook
        {
            get
            {
                return _editCommand ??
                    (_editCommand = new RelayCommand(obj => _db.SaveChanges()));
            }
        }

        public RelayCommand RemoveBook
        {
            get
            {
                return _removeCommand ??
                    (_removeCommand = new RelayCommand(obj =>
                    {
                        if(obj is Book book)
                        {
                            _db.Books.Remove(book);
                            _db.SaveChanges();
                        }
                    }, 
                    obj => obj != null)); 
            }
        }
    }
}
