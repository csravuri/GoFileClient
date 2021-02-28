using GoFileClient.Common;
using GoFileClient.Entities;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace GoFileClient.Database
{

    public class DbConnection
    {
        private SQLiteAsyncConnection _connection;
        private static DbConnection _dbConnection;
        private string _dbPath;
        private DbConnection()
        {
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Database");

            Utils.MakeSureDirectoryExists(folderPath);

            _dbPath = Path.Combine(folderPath, "GoFileClientDB.db3");

            _connection = new SQLiteAsyncConnection(_dbPath);

            CreateTable();

        }

        public static DbConnection GetDbConnection()
        {
            if (_dbConnection == null)
            {
                _dbConnection = new DbConnection();
            }

            return _dbConnection;
        }

        private async void CreateTable()
        {
            await _connection.CreateTableAsync<UploadHeaderEntity>();
            await _connection.CreateTableAsync<UploadLineEntity>();
        }

        public async Task InsertRecord<E>(E item) where E : Entity
        {
            item.CreatedDate = DateTime.Now;
            await _connection.InsertAsync(item);
        }

        public async Task UpdateRecord<E>(E item) where E : Entity
        {
            item.ModifiedDate = DateTime.Now;
            await _connection.UpdateAsync(item);
        }

        public async Task DeleteRecord<E>(E item) where E : Entity
        {
            await _connection.DeleteAsync(item);
        }
        public async Task InsertRecord<E>(List<E> items) where E : Entity
        {
            items.ForEach(x => x.CreatedDate = DateTime.Now);
            await _connection.InsertAllAsync(items);
        }

        public async Task UpdateRecord<E>(List<E> items) where E : Entity
        {
            items.ForEach(x => x.ModifiedDate = DateTime.Now);
            await _connection.UpdateAllAsync(items);
        }

        public async Task DeleteRecord<E>(List<E> items) where E : Entity
        {
            foreach (E item in items)
            {
                await DeleteRecord<E>(item);
            }
        }

        public async Task<List<UploadHeaderEntity>> GetUploadHeaders()
        {
            return await _connection.Table<UploadHeaderEntity>().ToListAsync();
        }

        public async Task<List<UploadLineEntity>> GetUploadLines()
        {
            return await _connection.Table<UploadLineEntity>().ToListAsync();
        }

        public string GetDBPath()
        {
            return _dbPath;
        }
    }
}
