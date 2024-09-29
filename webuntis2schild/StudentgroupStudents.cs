//public class StudentgroupStudents : List<StudentgroupStudent>
//{
//    public StudentgroupStudents(string kriterium)
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
//                        var studentgroupStudent = new StudentgroupStudent();

//                        string line = reader.ReadLine();

//                        try
//                        {
//                            if (line != null)
//                            {
//                                var x = line.Split('\t');

//                                studentgroupStudent = new StudentgroupStudent
//                                {
//                                    MarksPerLessonZeile = i,
//                                    StudentId = Convert.ToInt32(x[0]),
//                                    Nachname = x[1],
//                                    Vorname = x[2],
//                                    Kursname = x[3],
//                                    Fach = x[4]
//                                };
//                                try
//                                {
//                                    studentgroupStudent.Startdate = DateTime.ParseExact(x[5], "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
//                                }
//                                catch (Exception)
//                                {
//                                }

//                                try
//                                {
//                                    studentgroupStudent.Enddate = DateTime.ParseExact(x[6], "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
//                                }
//                                catch (Exception ex)
//                                {
//                                }

//                                this.Add(studentgroupStudent);
//                            }
//                        }
//                        catch (Exception ex)
//                        {
//                            Console.WriteLine(ex.ToString());
//                            Console.ReadKey();
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
//            Console.WriteLine(ex.ToString);
//            Console.ReadKey();
//        }
//        finally 
//        {
//            Global.WriteLine(datei, this.Count);
//        }
//    }
//}