using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ECCryptoLib.PasswordGenerator
{
    public class PasswordGeneratorSettings : INotifyPropertyChanged
    {
        private bool _useLowerCaseLetters = true;
        public bool UseLowerCaseLetters
        {
            get
            {
                return _useLowerCaseLetters;
            }
            set
            {
                if (_useLowerCaseLetters != value)
                {
                    _useLowerCaseLetters = value;
                    OnPropertyChanged("UseLowerCaseLetters");
                }
            }
        }

        private bool _useUpperCaseLetters = true;
        public bool UseUpperCaseLetters
        {
            get
            {
                return _useUpperCaseLetters;
            }
            set
            {
                if (_useUpperCaseLetters != value)
                {
                    _useUpperCaseLetters = value;
                    OnPropertyChanged("UseUpperCaseLetters");
                }
            }
        }

        private bool _useNumbers = true;
        public bool UseNumbers
        {
            get
            {
                return _useNumbers;
            }
            set
            {
                if (_useNumbers != value)
                {
                    _useNumbers = value;
                    OnPropertyChanged("UseNumbers");
                }
            }
        }


        private bool _useSpecialCharacters = true;
        public bool UseSpecialCharacters
        {
            get
            {
                return _useSpecialCharacters;
            }
            set
            {
                if (_useSpecialCharacters != value)
                {
                    _useSpecialCharacters = value;
                    OnPropertyChanged("UseSpecialCharacters");
                }
            }
        }


        private int _numberOfSpecialCharacters = 2;
        public int NumberOfSpecialCharacters
        {
            get
            {
                return _numberOfSpecialCharacters;
            }
            set
            {
                if (_numberOfSpecialCharacters != value)
                {
                    if(_numberOfNumbers + _numberOfSpecialCharacters <= _numberOfCharacters)
                    {
                        _numberOfSpecialCharacters = value;
                        OnPropertyChanged("NumberOfSpecialCharacters");
                    }
                }
            }
        }


        private int _numberOfNumbers = 2;
        public int NumberOfNumbers
        {
            get
            {
                return _numberOfNumbers;
            }
            set
            {
                if (_numberOfNumbers != value)
                {
                    if (_numberOfNumbers + _numberOfSpecialCharacters <= _numberOfCharacters)
                    {
                        _numberOfNumbers = value;
                        OnPropertyChanged("NumberOfNumbers");
                    }
                }
            }
        }

        private int _numberOfCharacters = 10;
        public int NumberOfCharacters
        {
            get
            {
                return _numberOfCharacters;
            }
            set
            {
                if (_numberOfCharacters != value)
                {
                    if (_numberOfNumbers + _numberOfSpecialCharacters <= _numberOfCharacters)
                    {
                        _numberOfCharacters = value;
                        OnPropertyChanged("NumberOfCharacters");
                    }
                }
            }
        }

        public PasswordGeneratorSettings()
        {
            this.NumberOfCharacters = 10;
            this.NumberOfSpecialCharacters = 2;
            this.NumberOfNumbers = 2;
            this.UseLowerCaseLetters = true;
            this.UseUpperCaseLetters = true;
            this.UseSpecialCharacters = true;
            this.UseNumbers = true;
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion INotifyPropertyChanged
    }
}
