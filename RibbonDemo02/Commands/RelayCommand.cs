using System;
using System.Windows.Input;

namespace RibbonDemo02
{
    // This class implements ICommand to support command-based interaction
    public class RelayCommand : ICommand
    {
        // Delegate representing the method to be executed when the command is invoked
        private readonly Action<object> _execute;

        // Delegate representing the method that determines whether the command can execute
        private readonly Predicate<object> _canExecute;

        // Constructor for RelayCommand: requires an execute action and an optional canExecute predicate
        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            // If execute is null, throw an ArgumentNullException to ensure a valid action is provided
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        // CanExecute method checks whether the command can be executed based on the _canExecute predicate
        public bool CanExecute(object parameter)
        {
            // If no _canExecute predicate is provided, return true, meaning the command can always execute
            // If _canExecute is provided, invoke it with the given parameter and return the result
            return _canExecute?.Invoke(parameter) ?? true;
        }

        // Execute method invokes the action provided at initialization
        public void Execute(object parameter)
        {
            _execute(parameter);  // Calls the delegate (the action passed when the command was created)
        }

        // Event to notify the CommandManager that the command's CanExecute state may have changed
        // When triggered, UI elements using this command can update their state to reflect whether the command can be executed
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;   // Subscribe to requery suggestions when CanExecute changes
            remove => CommandManager.RequerySuggested -= value; // Unsubscribe from requery suggestions when CanExecute changes
        }
    }
}
