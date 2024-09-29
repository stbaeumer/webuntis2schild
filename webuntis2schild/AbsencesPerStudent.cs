//using System.Globalization;

//public class AbsencesPerStudent : List<AbsencePerStudent>
//{
//    public AbsencesPerStudent()
//    {
//    }

//    public AbsencesPerStudent(string kriterium, int dieLetztenBleibenUnberücksichtigt,int maxFehlstundenProTag)
//    {
//        string datei = Global.CheckFile(kriterium);

//        try
//        {
//            int zeile = 1;
           
//            using (var reader = new StreamReader(datei))
//            {
//                reader.ReadLine();

//                while (!reader.EndOfStream)
//                {
//                    AbsencePerStudent absencePerStudent = new AbsencePerStudent();
//                    string fehler = "";

//                    try
//                    {
//                        var line = reader.ReadLine();
//                        var values = line.Split('\t');

//                        if (values.Count() != 16)
//                        {
//                            fehler = values.Count().ToString();
//                        }
//                        absencePerStudent.Name = values[0];
//                        absencePerStudent.StudentId = Convert.ToInt32(Convert.ToString(values[1]));
//                        absencePerStudent.Klasse = (values[3] != null) ? Convert.ToString(values[3]) : "";
//                        absencePerStudent.Datum = DateTime.ParseExact(values[4], "dd.MM.yy", CultureInfo.InvariantCulture);
//                        absencePerStudent.Fehlstunden = (values[6] != null && values[6] != "") ? Convert.ToInt32(values[6]) : 0;
//                        absencePerStudent.Fehlminuten = (values[7] != null && values[7] != "") ? Convert.ToInt32(values[7]) : 0;
//                        absencePerStudent.GanzerFehlTag = (values[15] != null && values[15] != "") ? Convert.ToInt32(values[15]) : 0;
//                        absencePerStudent.Grund = (values[8] != null && values[8] != "") ? Convert.ToString(values[8]) : ""; // krank, krank
//                                                                                                                       //abwesenheit.Text = (values[9] != null && values[9] != "") ? Convert.ToString(values[9]) : "";
//                        absencePerStudent.Status = (values[14] != null && values[14] != "") ? Convert.ToString(values[14]) : ""; // entsch. 

//                        if (
//                                absencePerStudent.Status == "offen" ||
//                                absencePerStudent.Status == "nicht entsch.")
//                        {
//                            if (absencePerStudent.Fehlstunden > maxFehlstundenProTag) // maximal 8 Fehlstunden am Tag werden gezählt. Klassenfahrten würden sonst 24 Stunden zählen.
//                            {
//                                absencePerStudent.Fehlstunden = maxFehlstundenProTag;
//                            }

//                            // Fehlzeiten der letzten 7 Tage werden ignoriert
//                            if (absencePerStudent.Datum > DateTime.Now.Date.AddDays(-dieLetztenBleibenUnberücksichtigt))
//                            {
//                                this.Add(absencePerStudent);
//                            }
//                        }
//                        zeile++;
//                    }
//                    catch (Exception ex)
//                    {
//                        //Console.WriteLine(" Die Zeile " + zeile + " müsste 16 Spalten haben, hat aber " + fehler + ". Die Zeile wird ignoriert.");
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

//    public DateTime ÄltesteAbwesenheit { get; private set; }
//}