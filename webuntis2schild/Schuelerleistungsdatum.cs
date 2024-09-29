public class Schuelerleistungsdatum
{
    internal object schuelerleistungsdatum;
    internal string allgBildenderAbschluss;
    internal string berufsbezAbschluss;

    public Schuelerleistungsdatum()
    {
    }

    public string Nachname { get; internal set; }
    public string Vorname { get; internal set; }
    public string Geburtsdatum { get; internal set; }
    public int Jahr { get; internal set; }
    public int Abschnitt { get; internal set; }
    public string Fach { get; internal set; }
    public string Fachlehrer { get; internal set; }
    public string Kursart { get; internal set; }
    public string Kurs { get; internal set; }
    public string Note { get; internal set; }
    public string Abiturfach { get; internal set; }
    public int Wochenstd { get; internal set; }
    public string ExterneSchulnr { get; internal set; }
    public string Zusatzkraft { get; internal set; }
    public string WochenstdZK { get; internal set; }
    public string Jahrgang { get; internal set; }
    public string Jahrgänge { get; internal set; }
    public int Fehlstd { get; internal set; }
    public int UnentschFehlstd { get; internal set; }    
}