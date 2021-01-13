// -----------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anorisoft.WinUI.Commands.GUITest
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    using Anorisoft.WinUI.Commands.GUITest.Thiriet;

    using JetBrains.Annotations;

    using AsyncCommand = Anorisoft.WinUI.Commands.AsyncCommand;

    internal class MainViewModel : INotifyPropertyChanged
    {
        private readonly MainWindow window;

        private string text;

        private ThirietViewModel thirietViewModel;

        private bool throwException;

        public MainViewModel(MainWindow window)
        {
            this.ThirietViewModel = new ThirietViewModel();
            this.window = window;
            this.DirectCommand = new DirectCommand(this.Execute, this.CanExecute);
            this.DirectCommand.RaiseCanExecuteChanged();

            this.RelayCommand = new RelayCommand(this.Execute, this.CanExecute);

            this.AsyncCommand = new AsyncCommand(
                async () => await this.ExecuteAsync().ConfigureAwait(false),
                this.CanExecute);

            this.ConcurrencyRelayCommand = new ConcurrencyRelayCommand(this.ExecuteWithToken, this.CanExecute);
        }

        public ThirietViewModel ThirietViewModel
        {
            get => this.thirietViewModel;
            set
            {
                if (Equals(value, this.thirietViewModel))
                {
                    return;
                }

                this.thirietViewModel = value;
                this.OnPropertyChanged();
            }
        }

        public RelayCommand RelayCommand { get; }

        public AsyncCommand AsyncCommand { get; }

        public ConcurrencyRelayCommand ConcurrencyRelayCommand { get; }

        public DirectCommand DirectCommand { get; }

        public string Text
        {
            get => this.text;
            set
            {
                if (value == this.text)
                {
                    return;
                }

                this.text = value;
                this.OnPropertyChanged();
            }
        }

        public bool Toggle { get; set; }

        public bool ThrowException
        {
            get => this.throwException;
            set
            {
                if (value == this.throwException)
                {
                    return;
                }
                this.throwException = value;
                this.OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void ExecuteWithToken(CancellationToken token)
        {
            if (ThrowException)
            {
                throw new Exception("Test Exception");
            }

            var cancelled = token.WaitHandle.WaitOne(TimeSpan.FromSeconds(5));
            if (cancelled)
            {
                token.ThrowIfCancellationRequested();
            }

            //throw new Exception("Test Ex");
            //Thread.Sleep(TimeSpan.FromSeconds(5));
        }

        private async Task ExecuteAsync()
        {
            await Task.Yield();
            this.Execute();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool CanExecute()
        {
            //this.Toggle = !this.Toggle;
            //Debug.WriteLine("CanExecute");
            //return this.Toggle;
            return true;
        }

        private void Execute()
        {
            Thread.Sleep(TimeSpan.FromSeconds(5));
        }
    }
}