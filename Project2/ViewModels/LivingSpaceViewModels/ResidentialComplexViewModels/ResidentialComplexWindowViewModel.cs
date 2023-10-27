using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Avalonia.Controls;
using MySql.Data.MySqlClient;
using Project2.Models;
using ReactiveUI;

namespace Project2.ViewModels.LivingSpaceViewModels.ResidentialComplexViewModels;

public class ResidentialComplexWindowViewModel : ViewModelBase
{
    Type _type = typeof(ResidentialComplex);
    
    private readonly Window _parentWindow;
    
    private List<ResidentialComplex> _itemsFromDatabase;

    private List<ResidentialComplex> _itemsFilter;
    
    private ObservableCollection<ResidentialComplex> _itemsOnDataGrid;
    
    private string _searchQuery = "";
    
    private int _indexTake = 0;
    
    public ResidentialComplex CurrentItem { get; set; }
    
    private string _sql = $"select residential_complex.id, state.id as state_id, state.name as state_name, residential_complex.name as name " +
                          $"from residential_complex " +
                          $"join state " +
                          $"on residential_complex.state_id = state.id;";

    public ObservableCollection<ResidentialComplex> ItemsOnDataGrid
    {
        get => _itemsOnDataGrid;
        set
        {
            _itemsOnDataGrid = value;
            this.RaisePropertyChanged();
        }
    }
    
    public string SearchQuery
    {
        get => _searchQuery;
        set
        {
            _searchQuery = value;
            UpdateItems();
        }
    }
    
    private int IndexTake
    {
        get => _indexTake;
        set
        {
            _indexTake = value;
            
            if (_indexTake > _itemsFilter.Count - 10)
            {
                _indexTake = _itemsFilter.Count - 10;
            }
            
            if (_indexTake < 0)
            {
                _indexTake = 0;
            } 
        }
    }
    
    public ResidentialComplexWindowViewModel(Window window)
    {
        _parentWindow = window;
        
        UpdateItems();
        
        PropertyChanged += OnSearchQueryChanged;
    }
    
    
    private void OnSearchQueryChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != nameof(SearchQuery)) return;
        Search();
    }

    private void Search()
    {
        if (SearchQuery == "")
        {
            _itemsFilter = new(_itemsFromDatabase);
        }

        _itemsFilter = new(_itemsFromDatabase.Where(it =>
        {
            PropertyInfo[] propertyInfos = _type.GetProperties();
            foreach (PropertyInfo f in propertyInfos)
            {
                if (f.GetValue(it).ToString().ToLower().Contains(SearchQuery.ToLower()))
                    return true;
            }

            return false;
        }));
    }

    public void UpdateItems()
    {
        GetDataFromDatabase();
        Search();
        TakeItems(TakeItemsEnum.FirstItems);
    }
    
    private void GetDataFromDatabase()
    {
        _itemsFromDatabase = new List<ResidentialComplex>();

        using (Database db = new Database())
        {
            MySqlDataReader reader = db.GetData(_sql);
            
            while (reader.Read() && reader.HasRows)
            {
                var currentItem = new ResidentialComplex();

                PropertyInfo[] propertyInfos = _type.GetProperties();
                for (int i = 0; i < propertyInfos.Length; i++)
                {
                    propertyInfos[i].SetValue(currentItem, reader.GetValue(i));
                }

                _itemsFromDatabase.Add(currentItem);
            }

        }
    }
    
    // public void AddItem()
    // {
    //     var itemAddWindowView = new ChangeItemView(UpdateItems);
    //     itemAddWindowView.ShowDialog(_parentWindow);
    //     UpdateItems();
    // }
    //
    // public void EditItem()
    // {
    //     if (CurrentItem == null) return;
    //     var itemEditWindowView = new ChangeItemView(UpdateItems, CurrentItem);
    //     itemEditWindowView.ShowDialog(_parentWindow);
    // }

    public void TakeItems(TakeItemsEnum takeItems)
    {
        switch (takeItems)
        {
            case TakeItemsEnum.FirstItems:
                IndexTake = 0;
                break;
            case TakeItemsEnum.LastItems:
                IndexTake = _itemsFilter.Count - 10;
                break;
            case TakeItemsEnum.NextItems:
                IndexTake += 10;
                break;
            case TakeItemsEnum.PreviousItems:
                IndexTake -= 10;
                break;
        }

        ItemsOnDataGrid = new ObservableCollection<ResidentialComplex>(_itemsFilter.GetRange(IndexTake, _itemsFilter.Count > 10 ? 10 : _itemsFilter.Count));
    }
}