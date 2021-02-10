namespace Anorisoft.WinUI.Commands.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Input.ICommand" />
    public interface ICommand : System.Windows.Input.ICommand
    {
        bool CanExecute();

        void Execute();
    }
}