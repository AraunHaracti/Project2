using System;
using System.Collections.Generic;
using System.Reflection;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;
using Project2.Models;

namespace Project2.Views.LivingSpaceViews.ResidentialComplexViews;

public partial class ChangeResidentialComplex : Window
{
    private Action _action;
    
    private ResidentialComplex _item = new();

    private bool isEdit = false;
    
    private List<State> items = new List<State>();
    
    private ComboBox comboBoxItems;
    public ChangeResidentialComplex(Action action)
    {
        InitializeComponent();

        DataContext = _item;

        _action += action;
        
        isEdit = false;
    }
    
    public ChangeResidentialComplex(Action action, ResidentialComplex item)
    {
        InitializeComponent();

        _item = item;
        
        DataContext = _item;

        comboBoxItems.SelectedIndex = (int)_item.StateId;

        _action += action;

        isEdit = true;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
        
        comboBoxItems = this.FindControl<ComboBox>("ComboBoxItems");
        
        using (Database db = new Database())
        {
            MySqlDataReader reader = db.GetData("select * from state");
            
            while (reader.Read() && reader.HasRows)
            {
                var currentItem = new State();

                PropertyInfo[] propertyInfos = typeof(State).GetProperties();
                for (int i = 0; i < propertyInfos.Length; i++)
                {
                    propertyInfos[i].SetValue(currentItem, reader.GetValue(i));
                }

                items.Add(currentItem);
            }
        }
        
        

        List<string?> itemsName = new List<string?>();
        
        foreach (var item in items)
        {
            itemsName.Add(item.Name);
        }
        
        comboBoxItems.ItemsSource = itemsName;
    }

    private void Cancel_OnClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }

    private void Change_OnClick(object? sender, RoutedEventArgs e)
    {
        if (isEdit)
        {
            EditItem();    
        }
        else
        {
            AddItem();    
        }
    }
    
    private void AddItem()
    {
        string sql = $"insert into residential_complex (Name, State_Id) " +
                     $"values ({_item.Name}', '{_item.StateId}')";

        using (Database db = new Database())
        {
            db.SetData(sql);
        }
        
        _action.Invoke();
        
        Close();
    }

    private void EditItem()
    {
        string sql = $"update residential_complex as t set " +
                     $"t.Name = '{_item.Name}', " +
                     $"t.State_Id = '{_item.StateId}' " +
                     $"where t.Id = '{_item.Id}'";
        
        using (Database db = new Database())
        {
            db.SetData(sql);
        }
        
        _action.Invoke();

        Close();
    }

    private void ComboBoxItems_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        ComboBox comboBoxItems = this.FindControl<ComboBox>("ComboBoxItems");
        _item.StateId = comboBoxItems.SelectedIndex;
    }
}