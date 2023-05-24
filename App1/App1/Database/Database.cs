using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
namespace App1
{
    public class Database
    {
        private readonly SQLiteAsyncConnection _database;
        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            if (_database.Table<Person>().CountAsync().Result == 0 || _database.Table<Person>()
                .FirstAsync(_ => _.Name == "Admin").Result == null)
            {
                _database.InsertAsync(new Person(1, "Admin", true, "1234"));
            }
            _database.CreateTableAsync<Person>();
        }
        public Task<List<Person>> GetPeopleAsync()
        {
            return _database.Table<Person>().ToListAsync();
        }
        public Task<int> SavePersonAsync(Person person)
        {
            return _database.InsertAsync(person);
        }
        public Task<int> UpdatePersonAsync(Person person)
        {
            return _database.UpdateAsync(person);
        }
        public Task<int> DeletePersonAsync(Person person)
        {
            return _database.DeleteAsync(person);
        }
        public Task<List<Person>> QueryIsAdminAsync()
        {
            return _database.QueryAsync<Person>
                ("SELECT * FROM Person WHERE IsAdmin = true");
            
        }

        public Task<List<Person>> LinqNoAdminAsync()
        {
            return  _database.Table<Person>().Where(p => p.IsAdmin == false).ToListAsync();
        }
        
        public Task<List<Person>> LinqShowAllAsync()
        {
            return  _database.Table<Person>().ToListAsync();
        }


    }
}