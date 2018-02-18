using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler_Sample.Export
{
    public class ExportOptionSetting : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(sender, e);
        }

        #region [  Json  ]
        

        private string _jsonFolderLocation;

        public string JsonFolderLocation
        {
            get => _jsonFolderLocation;
            set
            {
                _jsonFolderLocation = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("JsonFolderLocation"));
            }
        }

        private string _jsonFileName;

        public string JsonFileName
        {
            get => _jsonFileName;
            set
            {
                _jsonFileName = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("JsonFileName"));
            }
        }

        private int _jsonOverlapOption;

        public int JsonOverlapOption
        {
            get => _jsonOverlapOption;
            set
            {
                _jsonOverlapOption = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("JsonOverlapOption "));
            }
        }

        private bool _jsonSort;

        public bool JsonSort
        {
            get => _jsonSort;
            set
            {
                _jsonSort = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("JsonSort"));
            }
        }

        #endregion

        #region [  CSV  ]

        private string _csvFolderLocation;

        public string CSVFolderLocation
        {
            get => _csvFolderLocation;
            set
            {
                _csvFolderLocation = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("CSVFolderLocation"));
            }
        }

        private string _csvFileName;

        public string CSVFileName
        {
            get => _csvFileName;
            set
            {
                _csvFileName = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("CSVFileName"));
            }
        }

        private int _csvOverlapOption;

        public int CSVOverlapOption
        {
            get => _csvOverlapOption;
            set
            {
                _csvOverlapOption = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("CSVOverlapOption"));
            }
        }

        #endregion

        #region [  MySQL  ]

        private string _mySQLConnAddr;

        public string MySQLConnAddr
        {
            get => _mySQLConnAddr;
            set
            {
                _mySQLConnAddr = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("MySQLConnAddr"));
            }
        }

        private string _mySQLUserID;

        public string MySQLUserID
        {
            get => _mySQLUserID;
            set
            {
                _mySQLUserID = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("MySQLUserID"));
            }
        }

        private string _mySQLUserPassword;

        public string MySQLUserPassword
        {
            get => _mySQLUserPassword;
            set
            {
                _mySQLUserPassword = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("MySQLUserPassword"));
            }
        }

        private string _mySQLDatabaseName;

        public string MySQLDatabaseName
        {
            get => _mySQLDatabaseName;
            set
            {
                _mySQLDatabaseName = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("MySQLDatabaseName"));
            }
        }

        private string _mySQLConnString;

        public string MySQLConnString
        {
            get => _mySQLConnString;
            set
            {
                _mySQLConnString = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("MySQLConnString"));
            }
        }

        private bool _mySQLManualInput;

        public bool MySQLManualInput
        {
            get => _mySQLManualInput;
            set
            {
                _mySQLManualInput = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("MySQLManualInput"));
            }
        }

        #endregion
    }
}
    