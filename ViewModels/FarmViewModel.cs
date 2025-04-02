using FarmAnalogyApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FarmAnalogyApp.ViewModels
{
    public class FarmViewModel : INotifyPropertyChanged
    {
        private FarmModel _farmModel;

        // Properties for farm dimensions
        private int _length;
        public int Length
        {
            get => _length;
            set
            {
                _length = value;
                OnPropertyChanged();
            }
        }

        private int _width;
        public int Width
        {
            get => _width;
            set
            {
                _width = value;
                OnPropertyChanged();
            }
        }

        private int _maxCows;
        public int MaxCows
        {
            get => _maxCows;
            set
            {
                _maxCows = value;
                OnPropertyChanged();
            }
        }

        private int _cowsPerPaddock;
        public int CowsPerPaddock
        {
            get => _cowsPerPaddock;
            set
            {
                _cowsPerPaddock = value;
                OnPropertyChanged();
            }
        }

        private int _numberofPaddocks;
        public int NumberofPaddocks
        {
            get => _numberofPaddocks;
            set
            {
                _numberofPaddocks = value;
                OnPropertyChanged();
            }
        }
        private string _paddockDimensions;
        public string PaddockDimensions
        {
            get => _paddockDimensions;
            set
            {
                _paddockDimensions = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<FarmModel.Paddock> Paddocks { get; } = new ObservableCollection<FarmModel.Paddock>();

        public RelayCommand GenerateFarmCommand { get; private set; }
        public RelayCommand GeneratePaddocksCommand { get; private set; }
        public RelayCommand<object> MoveCowsCommand { get; private set; }
        public RelayCommand<object> SellCowsCommand { get; private set; }
        public RelayCommand<object> RestockCowsCommand { get; private set; }

        public FarmViewModel()
        {
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            GenerateFarmCommand = new RelayCommand(GenerateFarm, CanGenerateFarm);
            GeneratePaddocksCommand = new RelayCommand(GeneratePaddocks, CanGeneratePaddocks);
            MoveCowsCommand = new RelayCommand<object>(MoveCows, CanMoveCows);
            SellCowsCommand = new RelayCommand<object>(SellCows, CanSellCows);
            RestockCowsCommand = new RelayCommand<object>(RestockCows, CanRestockCows);
        }

        private void GenerateFarm()
        {
            _farmModel = new FarmModel(Length, Width);
            MaxCows = _farmModel.CalculateMaxCows();
        }

        private bool CanGenerateFarm()
        {
            return Length >= 210 && Width >= 300;
        }

        private void GeneratePaddocks()
        {
            if (_farmModel == null) return;

             (int noOfPaddocks, string dimensions) = _farmModel.GeneratePaddocks(CowsPerPaddock);

            Paddocks.Clear();
            foreach (var paddock in _farmModel.Paddocks)
            {
                Paddocks.Add(paddock);
            }
            PaddockDimensions = dimensions;
            NumberofPaddocks = noOfPaddocks;
            OnPropertyChanged(nameof(Paddocks));
        }

        private bool CanGeneratePaddocks()
        {
            return _farmModel != null && CowsPerPaddock > 0;
        }

        private void MoveCows(object parameters)
        {
            
        }

        private bool CanMoveCows(object parameters)
        {
            return true;
        }

        private void SellCows(object parameters)
        {
        }

        private bool CanSellCows(object parameters)
        {
            return true;
        }

        private void RestockCows(object parameters)
        {
        }

        private bool CanRestockCows(object parameters)
        {
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // Helper class for commands
    public class RelayCommand : System.Windows.Input.ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) =>
            _canExecute == null || _canExecute();

        public void Execute(object parameter) => _execute();

        // Method to trigger CanExecuteChanged event manually
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public class RelayCommand<T> : System.Windows.Input.ICommand
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) =>
            _canExecute == null || _canExecute((T)parameter);

        public void Execute(object parameter) => _execute((T)parameter);

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
