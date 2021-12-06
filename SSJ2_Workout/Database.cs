﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace SSJ2_Workout
{
    public class Database
    {
        private readonly SQLiteAsyncConnection _database;
        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Product>();
        }

        public Task<List<Product>> GetProductAsync()
        {
            return _database.Table<Product>().ToListAsync();
        }

        public Task<int> SaveProductAsync(Product product)
        {
            return _database.InsertAsync(product);
        }
        public Task<int> DeleteProductAsync(Product product)
        {
            return _database.DeleteAsync(product);
        }
        public Task<int> ChangeCheck(Product product)
        {
            return _database.DeleteAsync(product);
        }

        public Task<Product> GetProduct(int id)
        {
            return _database.FindAsync<Product>(id);
        }
        public Task<int> UpdateProduct(Product product)
        {
            return _database.UpdateAsync(new Product
            {
                Id = product.Id,
                Name = product.Name,
                Calories = product.Calories,
                Eat = product.Eat
            }); ; ;

        }

        //_sqLiteConnection.Update(new YourModelClassName
        //{
        //   Id=1,
        //   Name="Updated name"
        //});



        //public ObservableCollection<Product> GetProductAsync2()
        //{
        //    List<Product> list = _database.Table<Product>().ToListAsync().Result;
        //    ObservableCollection<Product> result = new ObservableCollection<Product>(list);

        //    return result;
        //}

    }
}