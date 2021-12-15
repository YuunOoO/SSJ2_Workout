using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace SSJ2_Workout
{
    
    public class Database <T> where T : new()
    {
        private readonly SQLiteAsyncConnection _database;
        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Product>();
            _database.CreateTableAsync<Exercise>();
            _database.CreateTableAsync<StoreData>();
        }

        public Task<List<T>> GetProductAsync()
        {
           return _database.Table<T>().ToListAsync();
        }

        public Task<int> SaveProductAsync(T product)
        {
            return _database.InsertAsync(product);
        }
        public Task<int> DeleteProductAsync(T product)
        {
            return _database.DeleteAsync(product);
        }
        public Task<int> ChangeCheck(T product)
        {
            return _database.DeleteAsync(product);
        }

        public Task<T> GetProduct(int id)
        {
            return _database.FindAsync<T>(id);
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

        public Task<int> UpdateProduct(Exercise exercise)
        {
            return _database.UpdateAsync(new Exercise
            {
                Id = exercise.Id,
                Name = exercise.Name,
                Calories = exercise.Calories,
                Did = exercise.Did
            }); ; ;
        }

        public Task<int> UpdateProduct(StoreData storedata)
        {
            return _database.UpdateAsync(new StoreData
            {
                Id = storedata.Id,
                Day = storedata.Day,
                Total_burned = storedata.Total_burned,
                Total_calories = storedata.Total_calories,
                Total_delivered = storedata.Total_delivered,
                Total_steps = storedata.Total_steps
            }) ; ; ;

        }

    }
}
