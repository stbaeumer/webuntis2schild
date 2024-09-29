//using System.Reflection;
//using System;

//internal class Fachs : List<Fach>
//{
//    public Fachs()
//    {
//    }

//    public Fachs(string datei, bool kopfzeile, char delimiter)
//    {
//        datei = Global.CheckFile(datei);

//        try
//        {
//            if (datei != null)
//            {
//                using (StreamReader reader = new StreamReader(datei))
//                {
//                    if (kopfzeile)
//                    {
//                        var überschrift = reader.ReadLine();
//                    }

//                    int i = 1;

//                    while (true)
//                    {
//                        i++;
//                        var fach = new Fach();

//                        string line = reader.ReadLine();

//                        if (line != null)
//                        {
//                            var x = line.Split(delimiter);

//                            fach.InternKrz = x[0].Replace("\"", "");
//                            fach.StatistikKrz = x[0].Replace("\"", "");
//                            fach.Bezeichnung = x[1].Replace("\"", "");
//                            fach.BezeichnungZeugnis = x[1].Replace("\"", "");
//                            fach.BezeichnungÜZeugnis = x[1].Replace("\"", "");
//                            this.Add(fach);
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
//            Global.WriteLine(datei, this.Count);
//        }
//    }

//    internal void FalscheBezeichnungen(Array schildFachs)
//    {
//        var abweichendeFächer = new List<string>();

//        foreach (var untisFach in this)
//        {
//            foreach (var zeile in schildFachs)
//            {
//                string a = "";



//            }
            
//                //Console.WriteLine(schildFachs[0][i])

//                Console.WriteLine(); // Leerzeile für bessere Lesbarkeit
            




//            //var schildFach = (from s in schildFachs where s.InternKrz == untisFach.InternKrz select s).FirstOrDefault();

//            //if (schildFach != null)
//            //{
//            //    if (schildFach.Bezeichnung != untisFach.Bezeichnung)
//            //    {
//            //        abweichendeFächer.Add(schildFach.InternKrz);
//            //    }
//            //}
//        }
//        if (abweichendeFächer.Count > 0) 
//        {
//            Global.WriteLine("Bei folgenden Fächern weicht die Bezeichnung in SchILD von Untis ab: " + string.Join(',', abweichendeFächer));
//        }
//    }
//}