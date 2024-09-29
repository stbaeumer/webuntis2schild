//internal class SchuelerLernabschnittsdaten
//{
//    private string v;

//    public SchuelerLernabschnittsdaten(string kriterium)
//    {
//        string datei = Global.CheckFile(kriterium);

//        try
//        {
//            if (datei != null)
//            {
//                using (StreamReader reader = new StreamReader(datei))
//                {
//                    var überschrift = reader.ReadLine();
//                    int i = 1;

//                    while (true)
//                    {
//                        i++;
//                        var schuelerabschnittsdatum = new Schuelerleistungsdatum();

//                        string line = reader.ReadLine();

//                        if (line != null)
//                        {
//                            //var x = line.Split('|');
//                            //schuelerleistungsdatum.Nachname = x[0];
//                            //schuelerleistungsdatum.Vorname = x[1];
//                            //schuelerleistungsdatum.Geburtsdatum = x[2];
//                            //schuelerleistungsdatum.Jahr = Convert.ToInt32(x[3]);
//                            //schuelerleistungsdatum.Abschnitt = Convert.ToInt32(x[4]);
//                            //schuelerleistungsdatum.Jahrgang = x[5];
//                            //schuelerleistungsdatum.Klasse = x[6];
//                            //schuelerleistungsdatum.schuelerleistungsdatum.Schulgliederung = x[7];
//                            //schuelerleistungsdatum.OrgForm = x[8];
//                            //schuelerleistungsdatum.Klassenart = x[9];
//                            //schuelerleistungsdatum.Fachklasse = x[10];
//                            //schuelerleistungsdatum.Förderschwerpunkt = x[11];
//                            //schuelerleistungsdatum.Förderschwerpunkt2 = x[12];
//                            //schuelerleistungsdatum.Schwerstbehinderung = x[13];
//                            //schuelerleistungsdatum.Wertung = x[14];
//                            //schuelerleistungsdatum.Wiederholung = x[15];
//                            //schuelerleistungsdatum.Klassenlehrer = x[16];
//                            //schuelerleistungsdatum.Versetzung = x[17];
//                            //schuelerleistungsdatum.Abschluss = x[18];
//                            //schuelerleistungsdatum.Schwerpunkt = x[19];
//                            //schuelerleistungsdatum.Konferenzdatum = x[20];
//                            //schuelerleistungsdatum.Zeugnisdatum = x[21];
//                            //schuelerleistungsdatum.SummeFehlstd = x[22];
//                            //schuelerleistungsdatum.SummeFehlstd_unentschuldigt = x[23];
//                            //schuelerleistungsdatum.allgBildenderAbschluss = x[24];
//                            //schuelerleistungsdatum.berufsbezAbschluss = x[25];
//                            //schuelerleistungsdatum.Zeugnisart = x[26];
//                            //schuelerleistungsdatum.FehlstundenGrenzwert = x[27];
//                            //schuelerleistungsdatum.DatumVon = x[28];
//                            //schuelerleistungsdatum.DatumBis = x[29];
//                            //this.Add(schuelerleistungsdatum);
//                        }

//                        if (line == null)
//                        {
//                            break;
//                        }
//                    }
//                }
//            }
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine(ex.ToString());
//            Console.ReadKey();
//        }
//        finally
//        {
//           // Global.WriteLine(datei, this.Count);
//        }
//    }
//}