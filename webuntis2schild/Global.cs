
using System.Diagnostics;
using System.Formats.Asn1;
using System.Globalization;
using System.Reflection;
using System.Text;

public static class Global
{
    public static string User = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToUpper().Split('\\')[1];

    public static string Pfad = @"c:\users\" + Global.User + @"\Downloads\";
    public static int SoAltDürfenImportDateienHöchstensSein = 6;

    public static object FehlendeFächer { get; internal set; }
    public static string DateiPfad { get; private set; }

    internal static void Write(int padLeft, string v)
    {
        Console.Write(("".PadLeft(padLeft) + v).PadRight(Console.WindowWidth - 6, '.'));
    }

    internal static void WriteLine(string v, int count)
    {
        Console.WriteLine((v + " ").PadRight(Console.WindowWidth - 6, '.') + (" " + count).ToString().PadLeft(6), '.');
    }

    internal static void WriteLine(string v)
    {
        for (int i = 0; i < v.Length; i += Console.WindowWidth - 6)
        {
            Console.WriteLine((i == 0 ? "" : " ") + v.Substring(i, Math.Min(Console.WindowWidth - 20, v.Length - i)));
        }
    }

    /// <summary>
    /// Wenn eine Anzahl >= 0 eigegeben wird, dann wird die Anzahl angezeigt. 
    /// Ansonsten wird "ok" angezeigt.
    /// </summary>
    /// <param name="anzahl"></param>
    internal static void WriteLineOkOderAnzahl(int anzahl = -1)
    {
        Console.WriteLine((anzahl >= 0 ? anzahl.ToString() : "ok").PadLeft(6));
    }

    internal static void ZeileSchreiben(int linkerAbstand, string linkeSeite, string rechteSeite, Exception? fehler)
    {
        var gesamtbreite = Console.WindowWidth;

        if (fehler != null)
        {
            rechteSeite = fehler.Message;
            Console.ForegroundColor = ConsoleColor.Red;
        }
        

        int punkte = gesamtbreite - linkerAbstand - linkeSeite.Length - rechteSeite.Length - 2;
        var mitte = " .".PadRight(Math.Max(3, punkte), '.') + " ";
        Console.WriteLine("".PadRight(linkerAbstand) + linkeSeite + mitte + rechteSeite);

        if (fehler != null)
        {
            Console.WriteLine("");            
            Console.WriteLine("");
            Console.WriteLine(fehler.ToString());
            Console.ReadKey();
        }
    }
}