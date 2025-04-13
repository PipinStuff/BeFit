namespace BeFit.Models
{
    public class Statystyki
    {
        public string TypĆwiczenia { get; set; }
        public int LiczbaWykonań { get; set; }
        public int ŁącznaLiczbaPowtórzeń { get; set; }
        public double ŚrednieObciążenie { get; set; }
        public double MaksymalneObciążenie { get; set; }
    }
}
