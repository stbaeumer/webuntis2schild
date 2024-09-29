using System.Linq;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class Schülers : List<Schueler>
{
    public List<int> AktSj { get; }
    public int Jahrgang { get; private set; }
    public int Abschnitt { get; set; }
    public List<string> InteressierendeKlassen { get; private set; }
    public Exception Fehler { get; private set; }
    public Schülers InteressierendeSchuelers { get; set; }

    public Schülers()
    {
        InteressierendeKlassen = [];
        Abschnitt = (DateTime.Now.Month > 2 && DateTime.Now.Month <= 9) ? 2 : 1;

        AktSj = new List<int>() {
            (DateTime.Now.Month >= 7 ? DateTime.Now.Year : DateTime.Now.Year - 1),
            (DateTime.Now.Month >= 7 ? DateTime.Now.Year + 1 : DateTime.Now.Year)
            };
    }

    public Schülers(Datei datei)
    {
        try
        {
            InteressierendeKlassen = [];
            Abschnitt = (DateTime.Now.Month > 2 && DateTime.Now.Month <= 9) ? 2 : 1;

            AktSj = new List<int>() {
            (DateTime.Now.Month >= 7 ? DateTime.Now.Year : DateTime.Now.Year - 1),
            (DateTime.Now.Month >= 7 ? DateTime.Now.Year + 1 : DateTime.Now.Year)
            };

            foreach (var zeile in datei.Zeilen)
            {
                var schueler = new Schueler();
                schueler.MailPrefix = zeile[Array.IndexOf(datei.Kopfzeile, "name")];
                schueler.Nachname = zeile[Array.IndexOf(datei.Kopfzeile, "longName")];
                schueler.Vorname = zeile[Array.IndexOf(datei.Kopfzeile, "foreName")];
                schueler.Geschlecht = zeile[Array.IndexOf(datei.Kopfzeile, "gender")];
                schueler.Geburtsdatum = zeile[Array.IndexOf(datei.Kopfzeile, "birthDate")] == "" ? new DateTime().Date : DateTime.ParseExact(zeile[Array.IndexOf(datei.Kopfzeile, "birthDate")], "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
                schueler.Klasse = zeile[Array.IndexOf(datei.Kopfzeile, "klasse.name")];
                schueler.Eintrittsdatum = zeile[Array.IndexOf(datei.Kopfzeile, "entryDate")] == "" ? new DateTime() : DateTime.ParseExact(zeile[Array.IndexOf(datei.Kopfzeile, "entryDate")], "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
                schueler.Austrittsdatum = zeile[Array.IndexOf(datei.Kopfzeile, "exitDate")] == "" ? new DateTime() : DateTime.ParseExact(zeile[Array.IndexOf(datei.Kopfzeile, "exitDate")], "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
                schueler.WebuntisId = Convert.ToInt32(zeile[Array.IndexOf(datei.Kopfzeile, "id")]);
                schueler.Mail = zeile[Array.IndexOf(datei.Kopfzeile, "address.email")];
                schueler.Mobil = zeile[Array.IndexOf(datei.Kopfzeile, "address.mobile")];
                schueler.Wohnort = zeile[Array.IndexOf(datei.Kopfzeile, "address.city")];
                schueler.Plz = zeile[Array.IndexOf(datei.Kopfzeile, "address.postCode")];
                schueler.Straße = zeile[Array.IndexOf(datei.Kopfzeile, "address.street")];
                schueler.GetJahrgang(AktSj);

                this.Add(schueler);

                if ((from t in this where t.Geburtsdatum.Date == new DateTime().Date select t).Any())
                {
                    Console.WriteLine("");
                    Console.WriteLine("Es wurden Schüler ohne Geburtsdatum in Webuntis gefunden. Das muss zuerst korrigiert werden:");
                    Console.WriteLine(schueler.Nachname + ", " + schueler.Vorname);
                    Console.ReadKey();
                    //Environment.Exit(0);
                }
            }
        }
        catch (Exception ex)
        {
            Fehler = ex;
        }
        finally
        {
            Global.ZeileSchreiben(2, "Schüler*innen", "importiert", Fehler);
        }
    }

    internal Datei Leistungsdaten(Datei exportLessons, Datei studentgroupStudents, Datei absencePerStudent, Datei marksPerLesson, string[] kopfzeile, string dateiPfad)
    {
        Datei schuelerleistungsdaten = new Datei();
        schuelerleistungsdaten.DateiPfad = dateiPfad;
        schuelerleistungsdaten.Kopfzeile = kopfzeile;

        try
        {
            foreach (var schueler in this)
            {
                schueler.GetFehlstd(absencePerStudent, AktSj, Abschnitt);
                schueler.GetUnentFehlstd(absencePerStudent, AktSj, Abschnitt);

                foreach (var zeile in (from zeile in exportLessons.Zeilen
                                       where zeile[Array.IndexOf(exportLessons.Kopfzeile, "klassen")].Split('~').Contains(schueler.Klasse)
                                       select zeile).ToList())
                {
                    string note = schueler.GetNote(Array.IndexOf(marksPerLesson.Kopfzeile, "Gesamtnote"), marksPerLesson);

                    if (zeile[Array.IndexOf(exportLessons.Kopfzeile, "klassen")].Contains(schueler.Klasse))
                    {
                        if (zeile[Array.IndexOf(exportLessons.Kopfzeile, "studentgroup")] == "") // Klassenunterricht werden immer hinzugefügt
                        {
                            schuelerleistungsdaten.Zeilen.Add(new List<string>
                            {
                                schueler.Nachname,
                                schueler.Vorname,
                                schueler.Geburtsdatum.ToString("dd.MM.yyyy"),
                                AktSj[0].ToString(),
                                Abschnitt.ToString(),
                                zeile[Array.IndexOf(exportLessons.Kopfzeile, "subject")],
                                zeile[Array.IndexOf(exportLessons.Kopfzeile, "teacher")],
                                "PUK", // Pflichtunterricht im Klassenverband
                                "", // Kein Kursname
                                note,
                                zeile[Array.IndexOf(exportLessons.Kopfzeile, "periods")],
                                "", // ExterneSchulnr
                                "", // Zusatzkraft
                                "", // WochenstdZK
                                "", // Jahrgang
                                "", // Jahrgänge
                                schueler.Fehlstd == 0 ? "" : schueler.Fehlstd.ToString(), // Fehlstd
                                schueler.UnentschFehlstd == 0 ? "" : schueler.Fehlstd.ToString() // UnentschFehlstd
                            });
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            Console.ReadKey();
        }
        return schuelerleistungsdaten;
    }

    public List<Schueler> GetIntessierendeKlasse()
    {
        InteressierendeSchuelers = new Schülers();
        var linkeSeite = "Sie haben diese Klassen gewählt:";
        var rechteSeite = "keine";

        try
        {
            Console.WriteLine("");
            Console.WriteLine("Bitte die interessierende Klasse oder die interessierenden Klassen kommasepariert eingeben.");
            Console.Write(" Sie können auch nur den oder die Anfangsbuchstaben kommaspariert eingeben: ");

            var x = Console.ReadLine().ToUpper();

            if (x == "")
            {
                InteressierendeKlassen.AddRange((from s in this select s.Klasse).Distinct().ToList());
                rechteSeite = "alle";
            }
            else
            {
                foreach (var klasse in (from s in this select s.Klasse).Distinct().ToList())
                {
                    foreach (var item in x.Trim().Split(','))
                    {
                        if (klasse.StartsWith(item))
                        {
                            if (!InteressierendeKlassen.Contains(klasse))
                            {
                                InteressierendeKlassen.Add(klasse);
                            }
                        }
                    }
                }
                if (InteressierendeKlassen.Count > 0)
                {
                    rechteSeite = string.Join(",", InteressierendeKlassen);
                }
            }

            InteressierendeSchuelers.AddRange((from t in this where InteressierendeKlassen.Contains(t.Klasse) select t).ToList());
        }
        catch (Exception ex)
        {
            Fehler = ex;
        }
        finally
        {
            Global.ZeileSchreiben(2, linkeSeite, rechteSeite, Fehler);
        }
        return InteressierendeSchuelers;
    }

    internal Datei SchuelerLernabschnittsdaten(Datei lernabschnittsdaten, Datei exportLessons, Datei studentgroupStudents, Datei absencePerStudent, Datei marksPerLesson, string[] kopfzeile, string v)
    {
        Datei schuelerLernabschnittsdaten = new Datei();
        schuelerLernabschnittsdaten.DateiPfad = dateiPfad;
        schuelerLernabschnittsdaten.Kopfzeile = kopfzeile;

        try
        {
            foreach (var schueler in this)
            {
                schueler.GetFehlstd(absencePerStudent, AktSj, Abschnitt);
                schueler.GetUnentFehlstd(absencePerStudent, AktSj, Abschnitt);

                schuelerLernabschnittsdaten.Zeilen.Add(new List<string>
                {
                    schueler.Nachname,                              // Plichtfeld
                    schueler.Vorname,                               // Plichtfeld
                    schueler.Geburtsdatum.ToString("dd.MM.yyyy"),   // Plichtfeld
                    AktSj[0].ToString(),                            // Plichtfeld
                    Abschnitt.ToString(),                           // Plichtfeld
                    "", // Jahrgang
                    "", // Klasse
                    "", // Schulgliederung
                    "", // OrgForm
                    "", // Klassenart
                    "", // Fachklasse
                    "", // Förderschwerpunkt
                    "", // Förderschwerpunkt2
                    "", // Schwerstbehinderung      // Plichtfeld
                    "", // Wertung                  // Plichtfeld
                    "", // Wiederholung             // Plichtfeld
                    "", // Klassenlehrer
                    "", // Versetzung
                    "", // Abschluss
                    "", // Schwerpunkt
                    "", // Konferenzdatum
                    "", // Zeugnisdatum
                    schueler.Fehlstd == 0 ? "" : schueler.Fehlstd.ToString(), // SummeFehlstd
                    schueler.UnentschFehlstd == 0 ? "" : schueler.UnentschFehlstd.ToString(), // SummeFehlstd_unentschuldigt
                    "", // allgBildenderAbschluss
                    "", // berufsbezAbschluss
                    "", // Zeugnisart
                    "", // FehlstundenGrenzwert
                    "", // DatumVon
                    "" // DatumBis
                });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            Console.ReadKey();
        }
        return schuelerLernabschnittsdaten;
    }
}