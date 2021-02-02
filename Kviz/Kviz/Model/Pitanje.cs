using SQLite;

namespace Kviz.Model
{
    public class Pitanje
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Tekst { get; set; }

        public override string ToString()
        {
            return $"{this.Id} - {this.Tekst}";
        }
    }
}
