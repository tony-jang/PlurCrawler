using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Common
{
    public class BasePair : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        internal void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    /// <summary>
    /// 2개의 타입을 가지고 있는 값의 한 쌍입니다.
    /// </summary>
    /// <typeparam name="T1">첫번째 형식입니다.</typeparam>
    /// <typeparam name="T2">두번째 형식입니다.</typeparam>
    public class Pair<T1,T2> : BasePair
    {
        private T1 _item1;

        public T1 Item1
        {
            get => _item1;
            set
            {
                _item1 = value;
                OnPropertyChanged("Item1");
            }
        }

        private T2 _item2;

        public T2 Item2
        {
            get => _item2;
            set
            {
                _item2 = value;
                OnPropertyChanged("Item2");
            }
        }
    }

    /// <summary>
    /// 3개의 타입을 가지고 있는 값의 한 쌍입니다.
    /// </summary>
    /// <typeparam name="T1">첫번째 형식입니다.</typeparam>
    /// <typeparam name="T2">두번째 형식입니다.</typeparam>
    /// <typeparam name="T3">세번째 형식입니다.</typeparam>
    public class Pair<T1, T2,T3> : BasePair
    {
        private T1 _item1;

        public T1 Item1
        {
            get => _item1;
            set
            {
                _item1 = value;
                OnPropertyChanged("Item1");
            }
        }

        private T2 _item2;

        public T2 Item2
        {
            get => _item2;
            set
            {
                _item2 = value;
                OnPropertyChanged("Item2");
            }
        }
        
        private T3 _item3;

        public T3 Item3
        {
            get => _item3;
            set
            {
                _item3 = value;
                OnPropertyChanged("Item3");
            }
        }
    }

    /// <summary>
    /// 4개의 타입을 가지고 있는 값의 한 쌍입니다.
    /// </summary>
    /// <typeparam name="T1">첫번째 형식입니다.</typeparam>
    /// <typeparam name="T2">두번째 형식입니다.</typeparam>
    /// <typeparam name="T3">세번째 형식입니다.</typeparam>
    /// <typeparam name="T4">네번째 형식입니다.</typeparam>
    public class Pair<T1, T2, T3, T4> : BasePair
    {
        private T1 _item1;

        public T1 Item1
        {
            get => _item1;
            set
            {
                _item1 = value;
                OnPropertyChanged("Item1");
            }
        }

        private T2 _item2;

        public T2 Item2
        {
            get => _item2;
            set
            {
                _item2 = value;
                OnPropertyChanged("Item2");
            }
        }

        private T3 _item3;

        public T3 Item3
        {
            get => _item3;
            set
            {
                _item3 = value;
                OnPropertyChanged("Item3");
            }
        }

        private T4 _item4;

        public T4 Item4
        {
            get => _item4;
            set
            {
                _item4 = value;
                OnPropertyChanged("Item4");
            }
        }

    }

    /// <summary>
    /// 5개의 타입을 가지고 있는 값의 한 쌍입니다.
    /// </summary>
    /// <typeparam name="T1">첫번째 형식입니다.</typeparam>
    /// <typeparam name="T2">두번째 형식입니다.</typeparam>
    /// <typeparam name="T3">세번째 형식입니다.</typeparam>
    /// <typeparam name="T4">네번째 형식입니다.</typeparam>
    /// <typeparam name="T5">다섯번째 형식입니다.</typeparam>
    public class Pair<T1, T2, T3, T4, T5> : BasePair
    {
        private T1 _item1;

        public T1 Item1
        {
            get => _item1;
            set
            {
                _item1 = value;
                OnPropertyChanged("Item1");
            }
        }

        private T2 _item2;

        public T2 Item2
        {
            get => _item2;
            set
            {
                _item2 = value;
                OnPropertyChanged("Item2");
            }
        }

        private T3 _item3;

        public T3 Item3
        {
            get => _item3;
            set
            {
                _item3 = value;
                OnPropertyChanged("Item3");
            }
        }

        private T4 _item4;

        public T4 Item4
        {
            get => _item4;
            set
            {
                _item4 = value;
                OnPropertyChanged("Item4");
            }
        }
        
        private T5 _item5;

        public T5 Item5
        {
            get => _item5;
            set
            {
                _item5 = value;
                OnPropertyChanged("Item5");
            }
        }
    }

    /// <summary>
    /// 6개의 타입을 가지고 있는 값의 한 쌍입니다.
    /// </summary>
    /// <typeparam name="T1">첫번째 형식입니다.</typeparam>
    /// <typeparam name="T2">두번째 형식입니다.</typeparam>
    /// <typeparam name="T3">세번째 형식입니다.</typeparam>
    /// <typeparam name="T4">네번째 형식입니다.</typeparam>
    /// <typeparam name="T5">다섯번째 형식입니다.</typeparam>
    /// <typeparam name="T6">여섯번째 형식입니다.</typeparam>
    public class Pair<T1, T2, T3, T4, T5, T6> : BasePair
    {
        private T1 _item1;

        public T1 Item1
        {
            get => _item1;
            set
            {
                _item1 = value;
                OnPropertyChanged("Item1");
            }
        }

        private T2 _item2;

        public T2 Item2
        {
            get => _item2;
            set
            {
                _item2 = value;
                OnPropertyChanged("Item2");
            }
        }

        private T3 _item3;

        public T3 Item3
        {
            get => _item3;
            set
            {
                _item3 = value;
                OnPropertyChanged("Item3");
            }
        }

        private T4 _item4;

        public T4 Item4
        {
            get => _item4;
            set
            {
                _item4 = value;
                OnPropertyChanged("Item4");
            }
        }

        private T5 _item5;

        public T5 Item5
        {
            get => _item5;
            set
            {
                _item5 = value;
                OnPropertyChanged("Item5");
            }
        }

        private T6 _item6;

        public T6 Item6
        {
            get => _item6;
            set
            {
                _item6 = value;
                OnPropertyChanged("Item6");
            }
        }
    }
}
