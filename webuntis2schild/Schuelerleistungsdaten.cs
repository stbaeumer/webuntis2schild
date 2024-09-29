
public class Schuelerleistungsdaten : List<Schuelerleistungsdatum>
{
    private string sourceExportLessons;
    private DateTime dateTime;

    //public Schuelerleistungsdaten(string kriterium)
    //{
    //    string datei = Global.CheckFile(kriterium);

    //    try
    //    {
    //        if (datei != null)
    //        {
    //            using (StreamReader reader = new StreamReader(datei))
    //            {
    //                var überschrift = reader.ReadLine();
    //                int i = 1;

    //                while (true)
    //                {
    //                    i++;
    //                    var schuelerleistungsdatum = new Schuelerleistungsdatum();

    //                    string line = reader.ReadLine();

    //                    if (line != null)
    //                    {
    //                        var x = line.Split('|');
    //                        schuelerleistungsdatum.Nachname = x[0];
    //                        schuelerleistungsdatum.Vorname = x[1];
    //                        schuelerleistungsdatum.Geburtsdatum = x[2];
    //                        schuelerleistungsdatum.Jahr = Convert.ToInt32(x[3]);
    //                        schuelerleistungsdatum.Abschnitt = Convert.ToInt32(x[4]);
    //                        schuelerleistungsdatum.Fach = x[5];
    //                        schuelerleistungsdatum.Fachlehrer = x[6];
    //                        schuelerleistungsdatum.Kursart = x[7];
    //                        schuelerleistungsdatum.Kurs = x[8];
    //                        schuelerleistungsdatum.Note = x[9];
    //                        schuelerleistungsdatum.Abiturfach = x[10];
    //                        schuelerleistungsdatum.Wochenstd = Convert.ToInt32(x[11]);
    //                        schuelerleistungsdatum.ExterneSchulnr = x[12];
    //                        schuelerleistungsdatum.Zusatzkraft = x[13];
    //                        schuelerleistungsdatum.WochenstdZK = x[14];
    //                        schuelerleistungsdatum.Jahrgang = x[15];
    //                        schuelerleistungsdatum.Jahrgänge = x[16];
    //                        schuelerleistungsdatum.Fehlstd = Convert.ToInt32(x[17]);
    //                        schuelerleistungsdatum.UnentschFehlstd = Convert.ToInt32(x[18]);
    //                        this.Add(schuelerleistungsdatum);
    //                    }

    //                    if (line == null)
    //                    {
    //                        break;
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine(ex.ToString());
    //        Console.ReadKey();
    //    }
    //    finally
    //    {
    //        Global.WriteLine(datei, this.Count);
    
    internal IEnumerable<object> UnveränderteFiltern(IEnumerable<object> schuelerleistungsdatenDat)
    {
        IEnumerable<object> veränderteOderNeue = new List<object>();

        List<object> veränderteOderNeueListe = veränderteOderNeue.ToList();

        foreach (Schuelerleistungsdatum neueSchülerleistungsdatum in schuelerleistungsdatenDat)
        {
            var bestehendesLeistungsdatum = (from b in this
                                             where b.Nachname == neueSchülerleistungsdatum.Nachname
                                             where b.Vorname == neueSchülerleistungsdatum.Vorname
                                             where b.Geburtsdatum == neueSchülerleistungsdatum.Geburtsdatum
                                             where b.Jahr == neueSchülerleistungsdatum.Jahr
                                             where b.Abschnitt == neueSchülerleistungsdatum.Abschnitt
                                             where b.Fach == neueSchülerleistungsdatum.Fach
                                             where b.Fachlehrer == neueSchülerleistungsdatum.Fachlehrer
                                             where b.Kursart == neueSchülerleistungsdatum.Kursart
                                             where b.Kurs == neueSchülerleistungsdatum.Kurs
                                             where b.Note == neueSchülerleistungsdatum.Note
                                             where b.Abiturfach == neueSchülerleistungsdatum.Abiturfach
                                             where b.Wochenstd == neueSchülerleistungsdatum.Wochenstd
                                             where b.ExterneSchulnr == neueSchülerleistungsdatum.ExterneSchulnr
                                             where b.Zusatzkraft == neueSchülerleistungsdatum.Zusatzkraft
                                             where b.WochenstdZK == neueSchülerleistungsdatum.WochenstdZK
                                             where b.Jahrgang == neueSchülerleistungsdatum.Jahrgang
                                             where b.Jahrgänge == neueSchülerleistungsdatum.Jahrgänge
                                             where b.Fehlstd == neueSchülerleistungsdatum.Fehlstd
                                             where b.UnentschFehlstd == neueSchülerleistungsdatum.UnentschFehlstd
                                             select b).FirstOrDefault();

            if (bestehendesLeistungsdatum == null)
            {
                veränderteOderNeueListe.Add(neueSchülerleistungsdatum);
            }
        }

        return veränderteOderNeueListe;
    }
}