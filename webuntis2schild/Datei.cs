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

        if (!Path.Exists(Global.Pfad + Path.GetDirectoryName(dateiPfad)))
        {
            Directory.CreateDirectory(Global.Pfad + Path.GetDirectoryName(dateiPfad));
        }

        var sourceFile = (from ext in extensions
                          from f in Directory.GetFiles(Global.Pfad + Path.GetDirectoryName(dateiPfad), ext, SearchOption.AllDirectories)
                          where Path.GetFileName(f).StartsWith(Path.GetFileName(dateiPfad))
                          orderby File.GetLastWriteTime(f)
                          select f).LastOrDefault();

        if (sourceFile == null)
        {
            Hinweis(Global.Pfad + dateiPfad);
        }
        else
        {   
            if (new FileInfo(sourceFile).LastWriteTime.Date < DateTime.Now.Date.AddDays(-(Global.SoAltDürfenImportDateienHöchstensSein)))
            {
                Console.WriteLine("Die Datei " + sourceFile + "ist älter als " + Global.SoAltDürfenImportDateienHöchstensSein + " Tage. ");
                Hinweis(Global.Pfad + dateiPfad + ".csv");
            }
        }

        return sourceFile;
    }

    private void Hinweis(string sourceFile)
    {
        if (sourceFile.Contains("ermine"))
        {
            Console.WriteLine("");
            Console.WriteLine("  1. Ansicht in Outlook auf Liste ändern.");
            Console.WriteLine("  2. *Beginn* muss in der ersten Spalte stehen.");
            Console.WriteLine("  3. Alle Listeneinträge markieren");
            Console.WriteLine("  4. Zwischenablage in " + sourceFile + " fallenlassen.");
        }

        if (sourceFile.Contains("Student_"))
        {
            Console.WriteLine("");
            Console.WriteLine("  Exportieren Sie die Datei " + Path.GetFileName(sourceFile) + " frisch aus Webuntis, indem Sie als Administrator:");
            Console.WriteLine("   1. Stammdaten > Schülerinnen");
            Console.WriteLine("   2. \"Berichte\" auswählen");
            Console.WriteLine("   3. Bei \"Schüler\" auf CSV klicken");
            Console.WriteLine("   4. Die Datei \"Student_<...>.CSV\" im Ordner " + Path.GetDirectoryName(sourceFile) + " zu speichern");
        }

        if (sourceFile.Contains("MarksPerLesson"))
        {
            Console.WriteLine("");
            Console.WriteLine("   1. Klassenbuch > Berichte klicken");
            Console.WriteLine("   2. Alle Klassen auswählen und ggfs. den Zeitraum einschränken");
            Console.WriteLine("   3. Unter \"Noten\" die Prüfungsart (-Alle-) auswählen");
            Console.WriteLine("   4. Unter \"Noten\" den Haken bei Notennamen ausgeben _NICHT_ setzen");
            Console.WriteLine("   5. Hinter \"Noten pro Schüler\" auf CSV klicken");
            Console.WriteLine("   6. Die Datei \"MarksPerLesson<...>.CSV\" im Ordner " + Path.GetDirectoryName(sourceFile) + " zu speichern");
        }

        if (sourceFile.Contains("AbsencePerStudent"))
        {
            Console.WriteLine("");
            Console.WriteLine("  Exportieren Sie die Datei " + Path.GetFileName(sourceFile) + " frisch aus Webuntis, indem Sie als Administrator:");
            Console.WriteLine("   1. Klassenbuch > Berichte klicken");
            Console.WriteLine("   2. Alle Klassen auswählen und als Zeitraum am besten die letzen vier Wochen wählen.");
            Console.WriteLine("   3. Unter \"Abwesenheiten\" Fehlzeiten pro Schüler*in auswählen");
            Console.WriteLine("   4. \"pro Tag\" ");
            Console.WriteLine("   5. Auf CSV klicken");
        }

        if (sourceFile.Contains(".dat"))
        {
            Console.WriteLine("");
            Console.WriteLine("  Exportieren Sie " + Path.GetFileName(sourceFile) + " frisch aus SchILD, indem Sie:");
            Console.WriteLine("   1. Datenaustausch > Schnittstelle > Export");
            Console.WriteLine("   2. Unterrichtsfächer auswählen.");
            Console.WriteLine("   3. Den Export-Ordner auswählen: " + Path.GetFileName(sourceFile));
        }

        if (sourceFile.Contains("GPU"))
        {
            Console.WriteLine("");
            Console.WriteLine("  Exportieren Sie die " + Path.GetFileName(sourceFile) + " frisch aus Untis, indem Sie:");
            Console.WriteLine("   1. Datei > Import/Export > Export TXT > Fächer klicken");
            Console.WriteLine("   2. Trennzeichen: Semikolon, Textbegrenzung \", Encoding UTF8");
            Console.WriteLine("   3. In den Ordner speichern: " + sourceFile + @"ExportAusUntis\");
        }

        if (sourceFile.Contains("AbsenceTimesTotal"))
        {
            Console.WriteLine("");
            Console.WriteLine("  Exportieren Sie die Datei " + Path.GetFileName(sourceFile) + " frisch aus Webuntis, indem Sie als Administrator:");
            Console.WriteLine("   1. Administration > Export klicken");
            Console.WriteLine("   2. Zeitraum begrenzen, also die Woche der Zeugniskonferenz und vergange Abschnitte herauslassen");
            Console.WriteLine("   3. Das CSV-Icon hinter Gesamtfehlzeiten klicken");
            Console.WriteLine("   4. Die Gesamtfehlzeiten (\"AbsenceTimesTotal<...>.CSV\") im Ordner " + Path.GetDirectoryName(sourceFile) + " zu speichern");
            Console.WriteLine("WICHTIG: Es kann Sinn machen nur Abwesenheiten bis zur letzten Woche in Webuntis auszuwählen.");
        }

        if (sourceFile.Contains("StudentgroupStudents"))
        {
            Console.WriteLine("");
            Console.WriteLine("  Exportieren Sie die Datei " + Path.GetFileName(sourceFile) + " frisch aus Webuntis, indem Sie als Administrator:");
            Console.WriteLine("   1. Administration > Export klicken");
            Console.WriteLine("   2. Zeitraum begrenzen, also die Woche der Zeugniskonferenz und vergange Abschnitte herauslassen");
            Console.WriteLine("   3. Das CSV-Icon hinter Schülergruppen klicken");
            Console.WriteLine("   4. Die Schülergruppen  (\"StudentgroupStudents<...>.CSV\") im Ordner " + Path.GetDirectoryName(sourceFile) + " zu speichern");
        }

        if (sourceFile.Contains("ExportLessons"))
        {
            Console.WriteLine("");
            Console.WriteLine("  Exportieren Sie die Datei " + Path.GetFileName(sourceFile) + " frisch aus Webuntis, indem Sie als Administrator:");
            Console.WriteLine("   1. Administration > Export klicken");
            Console.WriteLine("   2. Zeitraum begrenzen, also die Woche der Zeugniskonferenz und vergange Abschnitte herauslassen");
            Console.WriteLine("   3. Das CSV-Icon hinter Unterricht klicken");
            Console.WriteLine("   4. Die Unterrichte (\"ExportLessons<...>.CSV\") im Ordner " + Path.GetDirectoryName(sourceFile) + " zu speichern");
        }
        Console.ReadKey();
        Environment.Exit(0);
    }

    internal void DatAusgabe()
    {
        string name = DateiPfad;
        IEnumerable<System.Object> objektListe = Zeilen;
        Encoding encoding = Encoding.UTF8;
        char trennzeichen = ';';
        string kopfzeile = string.Join(";", Kopfzeile);

        int index = 0;
        int anzahl = 0;

        if (!Path.Exists(Global.Pfad + Path.GetDirectoryName(name)))
        {
            Directory.CreateDirectory(Global.Pfad + Path.GetDirectoryName(name));
        }

        var pfadUndDateiname = Global.Pfad + name;

        try
        {
            while (File.Exists(pfadUndDateiname))
            {
                DateTime lastWriteTime = File.GetLastWriteTime(pfadUndDateiname);
                string timeStamp = lastWriteTime.ToString("yyyy-MM-dd_HH-mm-ss");
                try
                {
                    File.Move(pfadUndDateiname, pfadUndDateiname.Replace(".dat", timeStamp + ".dat"));   
                }
                catch (Exception ex)
                {
                    Fehler = ex;
                }
                finally 
                {
                    Global.ZeileSchreiben(5, "Datei " + pfadUndDateiname + " existiert bereits und wird umbenannt", "ok", Fehler);
                }                
            }

            using (FileStream fs = new FileStream(pfadUndDateiname, FileMode.CreateNew))
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
                        Global.ZeileSchreiben(5, "Datei " + pfadUndDateiname + " wird erstellt", "ok", Fehler);
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