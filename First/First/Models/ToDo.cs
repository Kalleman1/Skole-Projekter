using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First.Models
{
    public class ToDo : INotifyPropertyChanged
    {
        int _id;
        public int Id
        {
            get => _id;
            set 
            {
                if (_id == value)
                    return;

                _id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
            }
        }
        string _name;
        public string ToDoName {
            get => _name; 
            set
            {
                if (_name == value)
                    return;

                _name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ToDoName)));
            }
        }
        string _description;
        public string Description {
            get => _description;
            set
            {
                if (_description == value)
                    return;

                _description = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
