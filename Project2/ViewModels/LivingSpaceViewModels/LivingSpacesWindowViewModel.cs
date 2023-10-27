using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Avalonia.Controls;
using MySql.Data.MySqlClient;
using Project2.Models;
using Project2.Views.LivingSpaceViews.ResidentialComplexViews;
using ReactiveUI;

namespace Project2.ViewModels.LivingSpaceViewModels;

public class LivingSpacesWindowViewModel : ViewModelBase
{
    Type _type = typeof(LivingSpace);
    
    private readonly Window _parentWindow;
    
    private List<LivingSpace> _itemsFromDatabase;

    private List<LivingSpace> _itemsFilter;
    
    private ObservableCollection<LivingSpace> _itemsOnDataGrid;
    
    private string _searchQuery = "";
    
    private int _indexTake = 0;
    
    public LivingSpace CurrentItem { get; set; }
    
    private string _sql = $"select residential_complex.name as residential_complex, building.name as building, apartment.name as apartment, state.name as state " +
                          $"from residential_complex " +
                          $"join building " +
                          $"on residential_complex.id = building.residential_complex_id " +
                          $"join apartment " +
                          $"on building.id = apartment.building_id " +
                          $"join state " +
                          $"on building.state_id = state.id;";

    public ObservableCollection<LivingSpace> ItemsOnDataGrid
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
    
    public LivingSpacesWindowViewModel(Window window)
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
        _itemsFromDatabase = new List<LivingSpace>();

        using (Database db = new Database())
        {
            MySqlDataReader reader = db.GetData(_sql);
            
            while (reader.Read() && reader.HasRows)
            {
                var currentItem = new LivingSpace();

                PropertyInfo[] propertyInfos = _type.GetProperties();
                for (int i = 0; i < propertyInfos.Length; i++)
                {
                    propertyInfos[i].SetValue(currentItem, reader.GetValue(i));
                }

                _itemsFromDatabase.Add(currentItem);
            }

        }
    }
    
    public void ResidentialComplexButton()
    {
        var residentialComplex = new ResidentialComplexWindow();
        residentialComplex.ShowDialog(_parentWindow);
        UpdateItems();
    }
    
    public void BuildingButton()
    {
        
    }

    public void ApartmentButton()
    {
        
    }

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

        ItemsOnDataGrid = new ObservableCollection<LivingSpace>(_itemsFilter.GetRange(IndexTake, _itemsFilter.Count > 10 ? 10 : _itemsFilter.Count));
    }
}