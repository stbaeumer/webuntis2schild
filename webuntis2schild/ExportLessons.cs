//using System.Globalization;
//using System.Text.RegularExpressions;

//public class ExportLessons : List<Exportlesson>
//{
//    private string sourceExportLessons;
//    private DateTime dateTime;

//    public ExportLessons()
//    {
//    }

//    public ExportLessons(string kriterium)
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
//                        var exportLesson = new Exportlesson();

//                        string line = reader.ReadLine();

//                        if (line != null)
//                        {
//                            var x = line.Split('\t');
                                                        
//                            exportLesson.LessonNumbers = new List<int>();
//                            exportLesson.Zeile = i;
//                            exportLesson.LessonId = Convert.ToInt32(x[0]);
//                            exportLesson.LessonNumbers.Add(Convert.ToInt32(x[1]) / 100);
//                            exportLesson.Fach = x[2];
//                            exportLesson.Lehrkraft = x[3];
//                            exportLesson.Klassen = x[4];
//                            exportLesson.Studentgroup = x[5];
//                            exportLesson.Periods = Convert.ToInt32(x[6]); // Wochenstunden
//                            exportLesson.Startdate = DateTime.ParseExact(x[7], "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
//                            exportLesson.Enddate = DateTime.ParseExact(x[8], "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
//                            this.Add(exportLesson);
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
//}