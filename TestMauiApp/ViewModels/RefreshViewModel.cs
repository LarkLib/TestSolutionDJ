﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;


namespace TestMauiApp.ViewModels
{
    class RefreshViewModel : INotifyPropertyChanged
    {
        const int RefreshDuration = 2;
        readonly Random random;
        bool isRefreshing;

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set
            {
                isRefreshing = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Item> Items { get; private set; }

        public ICommand RefreshCommand => new Command(async () => await RefreshItemsAsync());

        public RefreshViewModel()
        {
            random = new Random();
            Items = new ObservableCollection<Item>();
            AddItems();
        }

        void AddItems()
        {
            int itemNumber = 1;
            for (int i = 0; i < 50; i++)
            {
                Items.Add(new Item
                {
                    Color = Color.FromRgb((byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255)),
                    Name = $"Item {itemNumber++}"
                });
            }
        }

        async Task RefreshItemsAsync()
        {
            IsRefreshing = true;
            await Task.Delay(TimeSpan.FromSeconds(RefreshDuration));
            Items.Clear();
            AddItems();
            IsRefreshing = false;
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion    
    }

    public class Item
    {
        public string Name { get; set; }
        public Color Color { get; set; }
    }
}
