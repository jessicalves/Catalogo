using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalogo.Model;
using SQLite;

namespace Catalogo.Services
{
    public class ItemDatabase
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<ItemDatabase> Instance = new AsyncLazy<ItemDatabase>(async () =>
        {
            var instance = new ItemDatabase();
            CreateTableResult result = await Database.CreateTableAsync<Produto>();
            return instance;
        });

        public ItemDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        public Task<List<Produto>> GetItemsAsync()
        {
            return Database.Table<Produto>().ToListAsync();
        }

        public Task<List<Produto>> GetItemsNotDoneAsync()
        {
            return Database.QueryAsync<Produto>("SELECT * FROM Produto WHERE [Done] = 0");
        }

        public Task<Produto> GetItemAsync(int id)
        {
            return Database.Table<Produto>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Produto item)
        {
            if (item.Id != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(Produto item)
        {
            return Database.DeleteAsync(item);
        }

    }
}

