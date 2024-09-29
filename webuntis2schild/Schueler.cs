public class Schueler
{
    public Schueler()
    {
        Unterrichte = [];
    }

    public int Zeile { get; internal set; }
    public string MailPrefix { get; internal set; }
    public string Nachname { get; internal set; }
    public string Vorname { get; internal set; }
    public string Geschlecht { get; internal set; }
    public DateTime Geburtsdatum { get; internal set; }
    public string Klasse { get; internal set; }
    public DateTime Eintrittsdatum { get; internal set; }
    public DateTime Austrittsdatum { get; internal set; }
    public string Mail { get; internal set; }
    public string Mobil { get; internal set; }
    public string Straße { get; internal set; }
    public string Wohnort { get; internal set; }
    public string Plz { get; internal set; }
    public int WebuntisId { get; internal set; }
    public Unterrichte Unterrichte { get; internal set; }
    public int Fehlstd { get; internal set; }
    public int UnentschFehlstd { get; internal set; }
    public int Jahrgang { get; private set; }

    internal void GetJahrgang(List<int> aktSj)
    {
        for (int i = 0; i < 4; i++)
        {
            if (Klasse.Contains((aktSj[0] - 2000 - i).ToString()))
            {
                Jahrgang = i + 1;
            }
        }
    }

    public void GetFehlstd(Datei absencesPerStudent, List<int> aktSj, int abschnitt)
    {
        try
        {
            Fehlstd = (from a in absencesPerStudent.Zeilen
                       where a[Array.IndexOf(absencesPerStudent.Kopfzeile, "Schüler*innen")].Contains(Nachname)
                       where a[Array.IndexOf(absencesPerStudent.Kopfzeile, "Schüler*innen")].Contains(Vorname)               
                       select Convert.ToInt32(a[Array.IndexOf(absencesPerStudent.Kopfzeile, "Fehlstd.")])).Sum();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            Console.ReadKey(); 
        }
    }

    public void GetUnentFehlstd(Datei absencesPerStudent, List<int> aktSj, int abschnitt)
    {
        try
        {
            UnentschFehlstd = (from a in absencesPerStudent.Zeilen
                               where a[Array.IndexOf(absencesPerStudent.Kopfzeile, "Schüler*innen")].Contains(Nachname)
                               where a[Array.IndexOf(absencesPerStudent.Kopfzeile, "Schüler*innen")].Contains(Vorname)
                               where a[Array.IndexOf(absencesPerStudent.Kopfzeile, "Status")].Contains("nicht entsch.")
                               select Convert.ToInt32(a[Array.IndexOf(absencesPerStudent.Kopfzeile, "Fehlstd.")])).Sum();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            Console.ReadKey();
        }
    }

    internal string GetNote(int index, Datei marksPerLesson)
    {
        try
        {
            var note = (from zeile in marksPerLesson.Zeilen
                        where zeile[Array.IndexOf(marksPerLesson.Kopfzeile, "Name")].Contains(Vorname)
                        where zeile[Array.IndexOf(marksPerLesson.Kopfzeile, "Name")].Contains(Nachname)
                        where zeile[Array.IndexOf(marksPerLesson.Kopfzeile, "Klasse")].Contains(Klasse)
                        select zeile[Array.IndexOf(marksPerLesson.Kopfzeile, "Gesamtnote")]).Distinct().ToList();

            if (note.Count > 1)
            {
                Console.WriteLine("Mehr als eine Note");
                Console.ReadKey();
            }
            if (note.Count == 0)
            {
                return "";
            }
            return note[0];
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            Console.ReadKey();
            return "";
        }
    }
}