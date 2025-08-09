using System;
using System.Collections.Generic;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using FluentAvalonia.UI.Input;

namespace TodoCal;
public class TodoTask {
    public string Title { get; set; }
    public bool IsCompleted { get; set; }
    public ICommand StarCommand { get; set; }
    public TodoTask(string title)
    {
        Title = title;
        StarCommand = new PrintSomething();
    }
}

public class PrintSomething : ICommand
{
    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        Console.WriteLine("Printing something");
    }

    public event EventHandler? CanExecuteChanged;
}


public partial class MainWindow : Window
{
    public List<TodoTask> Tasks { set; get; } = new();
    private static TodoTask fakeNull = new TodoTask("");
    public TodoTask Selected { get; set; } = fakeNull;
    public MainWindow()
    {
        DataContext = this;
        Tasks.Add(new TodoTask("aaa"));
        Tasks.Add(new TodoTask("bbb"));
        InitializeComponent();
        
    }

    
    public void ToggleComplete(object sender,RoutedEventArgs args)
    {
        if (Selected == null)
        {
            return;
        }
        if (Selected.IsCompleted)
        {
            Selected.IsCompleted = false;
        }
        else
        {
            Selected.IsCompleted = true;
        }
    }

    private void SetCurrentTask(object? sender, PointerPressedEventArgs e)
    {
        if (sender is IDataContextProvider dataContextProvider)
        {
            var dataContext = dataContextProvider.DataContext;
            if (dataContext is TodoTask task)
            {
                Selected = task;
                TaskDetail.DataContext = task;
                TaskDetail.IsVisible = true;
                return;
            }
        }
        TaskDetail.DataContext = fakeNull;
        TaskDetail.IsVisible = false;
    }

    private void ClearCurrentTask(object? sender, PointerPressedEventArgs e)
    {
        TaskDetail.DataContext = fakeNull;
        TaskDetail.IsVisible = false;
    }
}