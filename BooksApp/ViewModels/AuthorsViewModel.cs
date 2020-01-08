using BooksApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BooksApp.ViewModels
{
    public class AuthorsViewModel : NotifyPropertyChanged
    {
        private Models.AppContext _db;
        private RelayCommand _editCommand;
        private RelayCommand _removeCommand;
        private IEnumerable<Author> _authors;

        public IEnumerable<Author> Authors
        {
            get => _authors;
            set
            {
                _authors = value;
                OnPropertyChanged("Authors");
            }
        }

        public AuthorsViewModel()
        {
            _db = new Models.AppContext();
            _db.Authors.Load();
            Authors = _db.Authors.Local.ToBindingList();
        }

        public RelayCommand EditAuthor
        {
            get
            {
                return _editCommand ??
                    (_editCommand = new RelayCommand(obj => _db.SaveChanges()));
            }
        }

        public RelayCommand RemoveAuthor
        {
            get
            {
                return _removeCommand ??
                    (_removeCommand = new RelayCommand(obj =>
                    {
                        if (obj is Author author)
                        {
                            if(author.Books == null || author.Books.Count > 0)
                            {
                                MessageBox.Show($"Author:{author.Name} {author.Surname} can`t be removed.");
                                return;
                            }
                            _db.Authors.Remove(author);
                            _db.SaveChanges();
                        }
                    },
                    obj => obj != null));
            }
        }
    }
}
