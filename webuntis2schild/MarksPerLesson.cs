//using System.Globalization;

//internal class MarksPerLessons : List<MarksPerLesson>
//{
//    public MarksPerLessons(string kriterium)
//    {
//        string datei = Global.CheckFile(kriterium);

//        try
//        {
//            int zeile = 1;

//            using var reader = new StreamReader(datei);
//            reader.ReadLine();

//            while (!reader.EndOfStream)
//            {
//                MarksPerLesson marksPerLesson = new();

//                try
//                {
//                    string[] x = reader.ReadLine().Split('\t');

//                    marksPerLesson.Zeile = zeile;
//                    marksPerLesson.Datum = DateTime.ParseExact(x[0], "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
//                    marksPerLesson.Name = x[1];
//                    marksPerLesson.Klasse = x[2];
//                    marksPerLesson.Fach = x[3];
//                    marksPerLesson.Prüfungsart = x[4];
//                    marksPerLesson.Note = Gesamtpunkte2Gesamtnote(x[5]); // Für die Blauen Briefe
//                    marksPerLesson.Punkte = x[5].Length > 0 ? x[5].Substring(0, 1) : ""; // Für die blauen Briefe
//                    marksPerLesson.Gesamtpunkte = x[9].Split('.')[0] == "" ? "" : x[9].Split('.')[0];
//                    marksPerLesson.Gesamtnote = Gesamtpunkte2Gesamtnote(marksPerLesson.Gesamtpunkte);
//                    marksPerLesson.Tendenz = Gesamtpunkte2Tendenz(marksPerLesson.Gesamtpunkte);
//                    marksPerLesson.Bemerkung = x[6];
//                    this.Add(marksPerLesson);

//                    zeile++;
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine(ex.ToString());
//                    Console.ReadKey();
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
//            Global.WriteLine(datei, this.Count);
//        }
//    }

//    private string Gesamtpunkte2Tendenz(string gesamtpunkte)
//    {
//        string tendenz = "";

//        if (gesamtpunkte == "1")
//        {
//            tendenz = "-";
//        }
//        if (gesamtpunkte == "3")
//        {
//            tendenz = "+";
//        }
//        if (gesamtpunkte == "4")
//        {
//            tendenz = "-";
//        }
//        if (gesamtpunkte == "6")
//        {
//            tendenz = "+";
//        }
//        if (gesamtpunkte == "7")
//        {
//            tendenz = "-";
//        }
//        if (gesamtpunkte == "9")
//        {
//            tendenz = "+";
//        }
//        if (gesamtpunkte == "10")
//        {
//            tendenz = "-";
//        }
//        if (gesamtpunkte == "12")
//        {
//            tendenz = "+";
//        }
//        if (gesamtpunkte == "13")
//        {
//            tendenz = "-";
//        }
//        if (gesamtpunkte == "15")
//        {
//            tendenz = "+";
//        }

//        if (tendenz == "")
//        {
//            return "";
//        }
//        return tendenz;
//    }

//    public static string Gesamtpunkte2Gesamtnote(string gesamtpunkte)
//    {
//        if (gesamtpunkte == "0")
//        {
//            return "6";
//        }
//        if (gesamtpunkte == "0.0")
//        {
//            return "6";
//        }
//        if (gesamtpunkte == "1")
//        {
//            return "5";
//        }
//        if (gesamtpunkte == "2")
//        {
//            return "5";
//        }
//        if (gesamtpunkte == "2.0")
//        {
//            return "5";
//        }
//        if (gesamtpunkte == "3")
//        {
//            return "5";
//        }
//        if (gesamtpunkte == "4")
//        {
//            return "4";
//        }
//        if (gesamtpunkte == "5")
//        {
//            return "4";
//        }
//        if (gesamtpunkte == "6")
//        {
//            return "4";
//        }
//        if (gesamtpunkte == "7")
//        {
//            return "3";
//        }
//        if (gesamtpunkte == "8")
//        {
//            return "3";
//        }
//        if (gesamtpunkte == "9")
//        {
//            return "3";
//        }
//        if (gesamtpunkte == "10")
//        {
//            return "2";
//        }
//        if (gesamtpunkte == "11")
//        {
//            return "2";
//        }
//        if (gesamtpunkte == "12")
//        {
//            return "2";
//        }
//        if (gesamtpunkte == "13")
//        {
//            return "1";
//        }
//        if (gesamtpunkte == "14")
//        {
//            return "1";
//        }
//        if (gesamtpunkte == "15")
//        {
//            return "1";
//        }
//        if (gesamtpunkte == "84")
//        {
//            return "A";
//        }
//        if (gesamtpunkte == "99")
//        {
//            return "-";
//        }
//        return "";
//    }

//    internal string GetNote(Schueler schueler, string klassen, string lehrkraft, string fach)
//    {
        
//    }
//}