
public class Dateien : List<Datei>
{
    public Dateien()
    {
    }

    public Exception Fehler { get; private set; }

    internal void DoppelteZeilenAusDerZweitenDateiEntfernen(Datei untisFaecher, Datei schildFaecher, string dateiPfad, string meldung)
    {
        Datei exportDatei = new()
        {
            Kopfzeile = schildFaecher.Kopfzeile,
            DateiPfad = dateiPfad
        };

        try
        {
            // Vergleiche schildfaecher.Zeilen zeile für zeile mit untisfaecher.Zeilen
            foreach (var untisZeile in untisFaecher.Zeilen)
            {
                bool found = false;
                string[] schildZeileNeu = new string[schildFaecher.Kopfzeile.Length];
                // Gesucht werden zeilen, die in untisfaecher existieren, aber nicht in schildfaecher
                foreach (var schildZeile in schildFaecher.Zeilen)
                {
                    bool match = true;

                    // Es werden nur diejenigen Spalten einbezogen, deren Spaltenkopf in der Kopfzeile beider Dateien existiert
                    for (int i = 0; i < untisFaecher.Kopfzeile.Length; i++)
                    {
                        string spaltenkopf = untisFaecher.Kopfzeile[i];
                        var index = Array.IndexOf(schildFaecher.Kopfzeile, spaltenkopf);

                        // Überprüfe, ob der Spaltenkopf in der Kopfzeile von schildfaecher existiert
                        if (index != -1)
                        {
                            // Vergleiche den Wert in der aktuellen Spalte
                            if (untisZeile[i] != schildZeile[index])
                            {
                                schildZeileNeu[index] = untisZeile[i];
                                match = false;
                                //break;
                            }
                        }
                    }

                    // Wenn eine Übereinstimmung gefunden wurde, setze found auf true und beende die innere Schleife
                    if (match)
                    {
                        found = true;
                        break;
                    }
                }

                // Wenn keine Übereinstimmung gefunden wurde, füge die Zeile zu fehlendeZeilen hinzu
                if (!found)
                {
                    exportDatei.Zeilen.Add(schildZeileNeu.ToList());
                }
            }
        }
        catch (Exception ex)
        {
            Fehler = ex;
        }
        finally 
        {
            Global.ZeileSchreiben(4, meldung, exportDatei.Zeilen.Count.ToString(), Fehler);
        }
        exportDatei.DatAusgabe();
    }
}