using GoFileClient.Entities;
using GoFileHelper.Common;
using SQLite;

namespace GoFileClient.Database
{

	public class DbConnection
	{
		private SQLiteAsyncConnection _connection;
		private static DbConnection _dbConnection;
		private string _dbPath;
		private readonly static object lockObject = new object();
		private List<UploadHeaderEntity> uploadHeadersCache = new List<UploadHeaderEntity>();
		private List<UploadLineEntity> uploadLinessCache = new List<UploadLineEntity>();
		public static event EventHandler LoadingComplete;
		private static bool isLoadingComplete = false;
		private DbConnection()
		{
			string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Database");

			Utils.MakeSureDirectoryExists(folderPath);

			_dbPath = Path.Combine(folderPath, "GoFileClientDB.db3");

			_connection = new SQLiteAsyncConnection(_dbPath);

			Initialize();
		}

		private async void Initialize()
		{
			await CreateTable();

			await LoadDataToCache();

			isLoadingComplete = true;
			LoadingComplete?.Invoke(this, EventArgs.Empty);
		}

		public static bool IsLoadingComplete()
		{
			return isLoadingComplete;
		}

		public static DbConnection GetDbConnection()
		{
			lock (lockObject)
			{
				if (_dbConnection == null)
				{
					_dbConnection = new DbConnection();
				}
			}

			return _dbConnection;
		}

		private async Task CreateTable()
		{
			await _connection.CreateTableAsync<UploadHeaderEntity>();
			await _connection.CreateTableAsync<UploadLineEntity>();
		}

		public async Task InsertRecord<E>(E item) where E : Entity
		{
			item.CreatedDate = DateTime.Now;
			await _connection.InsertAsync(item);
			AddToCache(item);
		}

		public async Task UpdateRecord<E>(E item) where E : Entity
		{
			item.ModifiedDate = DateTime.Now;
			await _connection.UpdateAsync(item);
			UpdateCache(item);
		}

		public async Task DeleteRecord<E>(E item) where E : Entity
		{
			await _connection.DeleteAsync(item);
			DeleteFromCache(item);
		}
		public async Task InsertRecord<E>(List<E> items) where E : Entity
		{
			items.ForEach(x => x.CreatedDate = DateTime.Now);
			await _connection.InsertAllAsync(items);
			AddToCache(items);
		}

		public async Task UpdateRecord<E>(List<E> items) where E : Entity
		{
			items.ForEach(x => x.ModifiedDate = DateTime.Now);
			await _connection.UpdateAllAsync(items);
			UpdateCache(items);
		}

		public async Task DeleteRecord<E>(List<E> items) where E : Entity
		{
			foreach (E item in items)
			{
				await DeleteRecord<E>(item);
			}
			DeleteFromCache(items);
		}

		public async Task<List<UploadHeaderEntity>> GetUploadHeaders()
		{
			return uploadHeadersCache;
		}

		public async Task<List<UploadLineEntity>> GetUploadLines()
		{
			return uploadLinessCache;
		}

		public string GetDBPath()
		{
			return _dbPath;
		}

		#region Cache data

		private async Task LoadDataToCache()
		{
			uploadHeadersCache.Clear();
			uploadLinessCache.Clear();
			uploadHeadersCache.AddRange(await _connection.Table<UploadHeaderEntity>().ToListAsync());
			uploadLinessCache.AddRange(await _connection.Table<UploadLineEntity>().ToListAsync());
		}

		private void AddToCache<E>(E item) where E : Entity
		{
			lock (lockObject)
			{
				if (item is UploadHeaderEntity)
				{
					uploadHeadersCache.Add(item as UploadHeaderEntity);
				}
				else if (item is UploadLineEntity)
				{
					uploadLinessCache.Add(item as UploadLineEntity);
				}
			}
		}
		private void AddToCache<E>(List<E> items) where E : Entity
		{
			items.ForEach(x => AddToCache(x));
		}

		private void UpdateCache<E>(E item) where E : Entity
		{
			//if (item is UploadHeaderEntity)
			//{
			//    uploadHeadersCache.Add(item as UploadHeaderEntity);
			//}
			//else if (item is UploadLineEntity)
			//{
			//    uploadLinessCache.Add(item as UploadLineEntity);
			//}
		}

		private void UpdateCache<E>(List<E> items) where E : Entity
		{

		}

		private void DeleteFromCache<E>(E item) where E : Entity
		{
			lock (lockObject)
			{
				if (item is UploadHeaderEntity)
				{
					uploadHeadersCache.Remove(item as UploadHeaderEntity);
				}
				else if (item is UploadLineEntity)
				{
					uploadLinessCache.Remove(item as UploadLineEntity);
				}
			}
		}

		private void DeleteFromCache<E>(List<E> items) where E : Entity
		{
			items.ForEach(x => DeleteFromCache(x));
		}


		#endregion
	}
}
