using SQLite;

namespace App1
{
    public class Person
    {
        
        public Person(int id, string name, bool isAdmin, string Password)
        {
            Id = id;
            Name = name;
            IsAdmin = isAdmin;
            this.Password = Password;
        }

        public Person()
        {
        }
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }

        public string Password { get; set; }
        
    }

    
}