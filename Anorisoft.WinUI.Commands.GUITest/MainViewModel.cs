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
 
            this.window = window;
            this.DirectCommand = new DirectCommand(() => this.OpenObservablePropertyTest1());
            this.DirectCommand.RaiseCanExecuteChanged();
            PropertyObservableNullReferenceCommand = new DirectCommand(() => new PropertyObservableNullReferenceTest().ShowDialog());

        }

        private void OpenObservablePropertyTest1()
        {
            new PropertyObservableTest().ShowDialog();
        }


        public DirectCommand DirectCommand { get; }

        public DirectCommand PropertyObservableNullReferenceCommand { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}