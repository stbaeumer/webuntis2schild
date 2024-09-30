using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class Datei
{
    public string[] Kopfzeile { get; internal set; }
    public List<List<string>> Zeilen { get; internal set; }
    public Exception Fehler { get; private set; }
    public string DateiPfad { get; set; }

    public Datei()
    {
        Zeilen = new List<List<string>>();
    }

    public Datei(string dateiPfad, string delimiter = "|", string kopfzeile = "")
    {
        try
        {
            dateiPfad = CheckFile(dateiPfad);
            Zeilen = [];
            DateiPfad = dateiPfad;
            Zeilen = new List<List<string>>();

            List<string> zeilenInEinemString = new List<string>();

            using (var reader = new StreamReader(dateiPfad))
            {
                if (kopfzeile == "")
                {
                    var line = reader.ReadLine();
                    Kopfzeile = line.Split(delimiter);
                }
                else
                {
                    Kopfzeile = kopfzeile.Split(delimiter);
                }

                while (!reader.EndOfStream)
                {
                    var line = "";

                    do
                    {
                        line = line + reader.ReadLine();
                    } while (line.Split(delimiter).Count() < Kopfzeile.Length);

                    Zeilen.Add((line.Replace("\"", "").Replace("'", "").Split(delimiter)).ToList());
                }
            }
        }
        catch (Exception ex)
        {
            Fehler = ex;
        }
        finally
        {
            Global.ZeileSchreiben(1, DateiPfad, Zeilen.Count.ToString(), Fehler);
        }
    }

    private string CheckFile(string dateiPfad)
    {
        var extensions = new List<string> { "*.csv", "*.dat", "*.TXT" };

        if (!Path.Exists(Path.GetDirectoryName(dateiPfad)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(dateiPfad));
        }

        var sourceFile = (from ext in extensions
                          from f in Directory.GetFiles(Path.GetDirectoryName(dateiPfad), ext, SearchOption.AllDirectories)
                          where Path.GetFileName(f).StartsWith(Path.GetFileName(dateiPfad))
                          orderby File.GetLastWriteTime(f)
                          select f).LastOrDefault();

        if (sourceFile == null)
        {
            Hinweis(dateiPfad);
        }

        return sourceFile;
    }

    private void Hinweis(string sourceFile)
    {
        if (sourceFile.Contains("Student_"))
        {
            Console.WriteLine("");
            Console.WriteLine("  Exportieren Sie die Datei " + Path.GetFileName(sourceFile) + " aus Webuntis, indem Sie als Administrator:");
            Console.WriteLine("   1. Stammdaten > Schülerinnen");
            Console.WriteLine("   2. \"Berichte\" auswählen");
            Console.WriteLine("   3. Bei \"Schüler\" auf CSV klicken");
            Console.WriteLine("   4. Die Datei speichern im Ordner: " + Directory.GetCurrentDirectory() + "\\" + Path.GetDirectoryName(sourceFile));
        }

        if (sourceFile.Contains("MarksPerLesson"))
        {
            Console.WriteLine("");
            Console.WriteLine("  Exportieren Sie die Datei " + Path.GetFileName(sourceFile) + " aus Webuntis, indem Sie als Administrator:");
            Console.WriteLine("   1. Klassenbuch > Berichte klicken");
            Console.WriteLine("   2. Alle Klassen auswählen und ggfs. den Zeitraum einschränken");
            Console.WriteLine("   3. Unter \"Noten\" die Prüfungsart (-Alle-) auswählen");
            Console.WriteLine("   4. Unter \"Noten\" den Haken bei Notennamen ausgeben _NICHT_ setzen");
            Console.WriteLine("   5. Hinter \"Noten pro Schüler\" auf CSV klicken");
            Console.WriteLine("   6. Die Datei speichern im Ordner: " + Directory.GetCurrentDirectory() + "\\" + Path.GetDirectoryName(sourceFile));
        }

        if (sourceFile.Contains("AbsencePerStudent"))
        {
            Console.WriteLine("");
            Console.WriteLine("  Exportieren Sie die Datei " + Path.GetFileName(sourceFile) + " aus Webuntis, indem Sie als Administrator:");
            Console.WriteLine("   1. Den Pafad gehen: Klassenbuch > Berichte klicken");
            Console.WriteLine("   2. Alle Klassen oder einzelne Klassen auswählen.");
            Console.WriteLine("   3. Unter \"Abwesenheiten\" Fehlzeiten pro Schüler*in auswählen");
            Console.WriteLine("   4. \"pro Tag\" ");
            Console.WriteLine("   5. Auf CSV klicken");
            Console.WriteLine("   6. Die Datei speichern im Ordner: " + Directory.GetCurrentDirectory() + "\\" + Path.GetDirectoryName(sourceFile));
        }

        if (sourceFile.Contains(".dat"))
        {
            Console.WriteLine("");
            Console.WriteLine("  Exportieren Sie " + Path.GetFileName(sourceFile) + " aus SchILD, indem Sie:");
            Console.WriteLine("   1. In SchILD den Pfad gehen: Datenaustausch > Schnittstelle > Export");
            Console.WriteLine("   2. Die Datei auswählen.");
            Console.WriteLine("   3. Die Datei speichern im Ordner: " + Directory.GetCurrentDirectory() + "\\" + Path.GetDirectoryName(sourceFile));
        }

        if (sourceFile.Contains("GPU"))
        {
            Console.WriteLine("");
            Console.WriteLine("  Exportieren Sie die *" + Path.GetFileName(sourceFile) + "* aus Untis, indem Sie:");
            Console.WriteLine("   1. Datei > Import/Export > Export TXT > Fächer klicken");
            Console.WriteLine("   2. Trennzeichen: Semikolon, Textbegrenzung \", Encoding UTF8");
            Console.WriteLine("   3. Die Datei speichern im Ordner: " + Directory.GetCurrentDirectory() + "\\" + Path.GetDirectoryName(sourceFile));
        }

        if (sourceFile.Contains("StudentgroupStudents"))
        {
            Console.WriteLine("");
            Console.WriteLine("  Exportieren Sie die Datei " + Path.GetFileName(sourceFile) + " frisch aus Webuntis, indem Sie als Administrator:");
            Console.WriteLine("   1. Administration > Export klicken");
            Console.WriteLine("   2. Zeitraum begrenzen, also die Woche der Zeugniskonferenz und vergange Abschnitte herauslassen");
            Console.WriteLine("   3. Das CSV-Icon hinter Schülergruppen klicken");
            Console.WriteLine("   4. Die Datei speichern im Ordner: " + Directory.GetCurrentDirectory() + "\\" + Path.GetDirectoryName(sourceFile));
        }

        if (sourceFile.Contains("ExportLessons"))
        {
            Console.WriteLine("");
            Console.WriteLine("  Exportieren Sie die Datei *" + Path.GetFileName(sourceFile) + "* aus Webuntis, indem Sie als Administrator:");
            Console.WriteLine("   1. Administration > Export klicken");
            Console.WriteLine("   3. Das CSV-Icon hinter Unterricht klicken");
            Console.WriteLine("   4. Die Datei speichern im Ordner: " + Directory.GetCurrentDirectory() + "\\" + Path.GetDirectoryName(sourceFile));
        }
        Console.ReadKey();
        Environment.Exit(0);
    }

    internal void DatAusgabe()
    {
        IEnumerable<System.Object> objektListe = Zeilen;
        Encoding encoding = Encoding.UTF8;
        char trennzeichen = ';';
        string kopfzeile = string.Join(";", Kopfzeile);

        int index = 0;
        int anzahl = 0;

        if (!Path.Exists(Path.GetDirectoryName(DateiPfad)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(DateiPfad));
        }

        try
        {
            while (File.Exists(DateiPfad))
            {
                DateTime lastWriteTime = File.GetLastWriteTime(DateiPfad);
                string timeStamp = lastWriteTime.ToString("yyyy-MM-dd_HH-mm-ss");
                try
                {
                    File.Move(DateiPfad, DateiPfad.Replace(".dat", timeStamp + ".dat"));   
                }
                catch (Exception ex)
                {
                    Fehler = ex;
                }
                finally 
                {
                    Global.ZeileSchreiben(5, "Datei " + DateiPfad + " existiert bereits und wird umbenannt", "ok", Fehler);
                }                
            }

            using (FileStream fs = new FileStream(DateiPfad, FileMode.CreateNew))
            {
                using (StreamWriter writer = new StreamWriter(fs, encoding))
                {
                    try
                    {
                        writer.WriteLine(string.Join("|", Kopfzeile));

                        foreach (var zeile in Zeilen)
                        {
                            writer.WriteLine(string.Join("|", zeile));
                        }
                    }
                    catch (Exception ex)
                    {
                        Fehler = ex;
                    }
                    finally 
                    {
                        Global.ZeileSchreiben(5, "Datei " + DateiPfad + " wird erstellt", "ok", Fehler);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            Console.ReadKey();
        }
    }

    public Datei Filtern(Datei interessierendeExportLessons)
    {
        Datei interessierendeDatei = new()
        {
            Kopfzeile = Kopfzeile,
            Zeilen = Zeilen,
            DateiPfad = DateiPfad
        };

        try
        {
            int indexOfInternKrz = Array.IndexOf(Kopfzeile, "InternKrz");
            int indexOfBezeichnung = Array.IndexOf(Kopfzeile, "Bezeichnung");
            int indexOfSubject = Array.IndexOf(interessierendeExportLessons.Kopfzeile, "subject");

            foreach (var zeile in interessierendeDatei.Zeilen.ToList())
            {
                if (indexOfInternKrz >= 0)
                {
                    if (!(from i in interessierendeExportLessons.Zeilen where zeile[indexOfInternKrz] == i[indexOfSubject] select i).Any())
                    {
                        Zeilen.Remove(zeile);
                    }
                }
                if (indexOfBezeichnung >= 0)
                {
                    if (!(from i in interessierendeExportLessons.Zeilen where zeile[indexOfBezeichnung] == i[indexOfSubject] select i).Any())
                    {
                        Zeilen.Remove(zeile);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Fehler = ex;
        }
        finally 
        {
            Global.ZeileSchreiben(3, "Interessierende " + Path.GetFileName(DateiPfad), Zeilen.Count.ToString(), Fehler);
        }
        return interessierendeDatei;
    }

    public Datei Filtern(Schülers schuelers, int dieLetztenBleibenUnberücksichtigt = 0, int maxFehlstundenProTag = 0)
    {
        Datei interessierendeDatei = new()
        {
            Kopfzeile = Kopfzeile,
            Zeilen = Zeilen,
            DateiPfad = DateiPfad
        };

        try
        {
            int indexOfStatus = Array.IndexOf(Kopfzeile, "Status");
            int indexOfFehlstunden = Array.IndexOf(Kopfzeile, "Fehlstd.");
            int indexOfDatum = Array.IndexOf(Kopfzeile, "Datum");
            int indexOfKlasse = Array.IndexOf(Kopfzeile, "Klasse");
            int indexOfKlassen = Array.IndexOf(Kopfzeile, "klassen");
            int indexOfstudentgroupName = Array.IndexOf(Kopfzeile, "studentgroup.name");
            int indexOfInternKrz = Array.IndexOf(Kopfzeile, "InternKrz");
            int indexOfBezeichnung = Array.IndexOf(Kopfzeile, "Bezeichnung");

            // Lernabschnittsdaten:
            int indexOfVorname = Array.IndexOf(Kopfzeile, "Vorname");
            int indexOfNachname = Array.IndexOf(Kopfzeile, "Nachname");
            int indexOfGeburtsdatum = Array.IndexOf(Kopfzeile, "Geburtsdatum");
            int indexOfJahr = Array.IndexOf(Kopfzeile, "Jahr");
            int indexOfAbschnitt = Array.IndexOf(Kopfzeile, "Abschnitt");

            foreach (var zeile in interessierendeDatei.Zeilen.ToList())
            {
                if (indexOfKlasse >= 0)
                {
                    if (!(from i in schuelers where i.Klasse == zeile[indexOfKlasse] select i).Any())
                    {
                        Zeilen.Remove(zeile);
                    }
                }
                if (indexOfKlassen >= 0)
                {
                    if (!(from i in schuelers where zeile[indexOfKlassen].Split('~').Contains(i.Klasse) select i).Any())
                    {
                        Zeilen.Remove(zeile);
                    }
                }
                if (indexOfstudentgroupName >= 0)
                {
                    if (!(from i in schuelers where zeile[indexOfstudentgroupName].Contains(i.Klasse) select i).Any())
                    {
                        Zeilen.Remove(zeile);
                    }
                }
                if (indexOfNachname >= 0 && indexOfVorname >= 0 && indexOfGeburtsdatum >= 0 && indexOfJahr >= 0 && indexOfAbschnitt >= 0)
                {
                    if (!(from i in schuelers 
                          where zeile[indexOfNachname] == i.Nachname
                          where zeile[indexOfVorname] == i.Vorname
                          where zeile[indexOfGeburtsdatum] == i.Geburtsdatum.ToString("yyyy.MM.dd")
                          where zeile[indexOfJahr] == i.Jahrgang.ToString()
                          where zeile[indexOfAbschnitt] == schuelers.Abschnitt.ToString()
                          select i).Any())
                    {
                        Zeilen.Remove(zeile);
                    }
                }
            }

            if (dieLetztenBleibenUnberücksichtigt + maxFehlstundenProTag > 0)
            {
                foreach (var zeile in interessierendeDatei.Zeilen.ToList())
                {
                    try
                    {
                        if (zeile[indexOfStatus] == "offen" || zeile[indexOfStatus] == "nicht entsch.")
                        {
                            if (Convert.ToInt32(zeile[indexOfFehlstunden]) > maxFehlstundenProTag)
                            {
                                zeile[indexOfFehlstunden] = maxFehlstundenProTag.ToString();
                            }

                            if (DateTime.ParseExact(zeile[indexOfDatum], "dd.MM.yy", CultureInfo.InvariantCulture) > DateTime.Now.Date.AddDays(-dieLetztenBleibenUnberücksichtigt))
                            {
                                Zeilen.Remove(zeile);
                            }
                        }
                    }
                    catch (Exception)
                    {
                       
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Fehler = ex;
        }
        finally
        {
            Global.ZeileSchreiben(3, "Interessierende " + Path.GetFileName(DateiPfad), Zeilen.Count.ToString(), Fehler);
        }

        return interessierendeDatei;
    }
}