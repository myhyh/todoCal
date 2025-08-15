using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using FluentAvalonia.UI.Input;
using TodoCal.Db;
using TodoCal.utils;

namespace TodoCal;
public class TodoTask
{
    public TwoWayBindable<string> Title { get; set; } = new();
    public TwoWayBindable<bool> IsCompleted { get; set; } = new();
    public ICommand StarCommand { get; set; }
    public TodoTask(string title)
    {
        Title.Value = title;
        StarCommand = new PrintSomething();
    }

    public void ToggleComplete()
    {
        IsCompleted.Value = !IsCompleted.Value;
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
    private static TodoTask fakeNull = new("");
    private TodoTask Selected { get; set; } = fakeNull;

    public TwoWayBindable<GridLength> 任务列表的列表栏宽度 { get; set; } = new();
    public TwoWayBindable<GridLength> 任务列表栏宽度 { get; set; } = new();
    public TwoWayBindable<GridLength> 任务详情栏宽度 { get; set; } = new();
    public MainWindow()
    {
        DataContext = this;
        Tasks.Add(new TodoTask("aaa"));
        Tasks.Add(new TodoTask("bbb"));
        任务列表的列表栏宽度.Value = GridLength.Parse("1*");
        任务列表栏宽度.Value = GridLength.Parse("3*");
        任务详情栏宽度.Value = GridLength.Parse("0*");
        InitializeComponent();
    }

    private void loadData()
    {
        using (var db = new LocalSqlite())
        {
            var todoLists = db.TodoLists.Where(l => !l.IsDeleted).ToList();
            
        }
    }

    
    public void ToggleComplete(object sender,RoutedEventArgs args)
    {
        if (Selected == fakeNull)
        {
            return;
        }
        Selected.ToggleComplete();
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
                showTaskDetail();
                return;
            }
        }
        TaskDetail.DataContext = fakeNull;
        TaskDetail.IsVisible = false;
        hideTaskDetail();
    }

    private void showTaskDetail()
    {
        TaskDetail.IsVisible = true;
        任务详情栏宽度.Value = GridLength.Parse("1.5*");
        任务列表栏宽度.Value = GridLength.Parse("1.5*");
    }

    private void hideTaskDetail()
    {
        TaskDetail.IsVisible = false;
        任务详情栏宽度.Value = GridLength.Parse("0*");
        任务列表栏宽度.Value = GridLength.Parse("3*");
    }

    private void ClearCurrentTask(object? sender, PointerPressedEventArgs e)
    {
        TaskDetail.DataContext = fakeNull;
        hideTaskDetail();
    }
}