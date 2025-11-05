using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.page.entity
{
    public class UserInfo : INotifyPropertyChanged
    {
        
        private string _userId;
        private string _name;
        private string _gender;
        private DateTime _birthday;
        private string _phoneNumber;

        public string UserId
        {
            get { return _userId; }
            set { _userId = value; OnPropertyChanged(nameof(UserId)); }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        public string Gender
        {
            get { return _gender; }
            set { _gender = value; OnPropertyChanged(nameof(Gender)); }
        }

        public DateTime Birthday
        {
            get { return _birthday; }
            set { _birthday = value; OnPropertyChanged(nameof(Birthday)); }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; OnPropertyChanged(nameof(PhoneNumber)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
