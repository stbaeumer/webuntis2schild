public class Exportlesson
{    
    public List<int> LessonNumbers { get; internal set; }
    public int Zeile { get; internal set; }
    public int LessonId { get; internal set; }
    public string Fach { get; internal set; }
    public string Lehrkraft { get; internal set; }
    public string Klassen { get; internal set; }
    public string Studentgroup { get; internal set; }
    public int Periods { get; internal set; }
    public DateTime Startdate { get; internal set; }
    public DateTime Enddate { get; internal set; }

    private DateTime GetVon(int tag, int stunde, DateTime datumTagMontagDerKalenderwoche)
    {
        DateTime dt = datumTagMontagDerKalenderwoche.AddDays(tag - 1);

        TimeSpan ts = new TimeSpan();

        switch (stunde)
        {
            case 1:
                ts = new TimeSpan(7, 40, 0);
                break;
            case 2:
                ts = new TimeSpan(8, 25, 0);
                break;
            case 3:
                ts = new TimeSpan(9, 30, 0);
                break;
            case 4:
                ts = new TimeSpan(10, 15, 0);
                break;
            case 5:
                ts = new TimeSpan(11, 15, 0);
                break;
            case 6:
                ts = new TimeSpan(12, 0, 0);
                break;
            case 7:
                ts = new TimeSpan(13, 0, 0);
                break;
            case 8:
                ts = new TimeSpan(13, 45, 0);
                break;
            case 9:
                ts = new TimeSpan(14, 45, 0);
                break;
            case 10:
                ts = new TimeSpan(15, 30, 0);
                break;
            case 11:
                ts = new TimeSpan(16, 30, 0);
                break;
            case 12:
                ts = new TimeSpan(18, 0, 0);
                break;
            case 13:
                ts = new TimeSpan(19, 0, 0);
                break;
            case 14:
                ts = new TimeSpan(19, 55, 0);
                break;
        }

        return dt.Date + ts;
    }
}