using SQLite;

namespace Kviz.Model
{
    public class Odgovor
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string TekstOdg { get; set; }
        public bool IsTacan { get; set; }
        public int PitanjeID { get; set; }

        public override string ToString()
        {
            return $"{Id} - {TekstOdg} | {IsTacan} | {PitanjeID}";
        }

    }
}
