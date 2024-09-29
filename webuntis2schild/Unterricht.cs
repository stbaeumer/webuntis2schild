public class Unterricht(string klassen, string fachlehrer, string fach, string kursart, string kurs, int periods, string note)
{
    public string Klassen { get; private set; } = klassen;
    public string Fachlehrer { get; private set; } = fachlehrer;
    public string Fach { get; private set; } = fach;
    public string Kursart { get; } = kursart;
    public string Kurs { get; } = kurs;
    public int Wochenstd { get; } = periods;
    public string Note { get; } = note;
}