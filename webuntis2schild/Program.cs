using System.Text;

Console.WriteLine("      webuntis2schild.exe | Published under the terms of GPLv3 | Stefan Bäumer " + DateTime.Now.Year + " | Version 20240929");
Console.WriteLine("=".PadRight(Console.WindowWidth, '='));

Dateien dateien = [];

Datei schuelerleistungsdaten = new(@"ExportAusSchild\SchuelerLeistungsdaten.dat");
Datei lernabschnittsdaten = new(@"ExportAusSchild\SchuelerLernabschnittsdaten.dat");
Datei exportLessons = new(@"ExportAusWebuntis\ExportLessons", "\t");
Datei studentgroupStudents = new(@"ExportAusWebuntis\StudentgroupStudents", "\t");
Datei marksPerLesson = new(@"ExportAusWebuntis\MarksPerLesson", "\t");
Datei absencePerStudent = new(@"ExportAusWebuntis\AbsencePerStudent", "\t");
Datei schildFaecher = new(@"ExportAusSchild\Faecher.dat");
Datei untisFaecher = new(@"ExportAusUntis\GPU006", ";",
    @"InternKrz;Bezeichnung;;;;;;;;;;;;;;;;;;;;;"); // die Anzahl der Semikolon muss stimmen,
                                                    // ansonsten nur interessierende Spalten nennen

Schülers schuelers = new(new(@"ExportAusWebuntis\Student_", "\t"));

do
{
    Schülers interessierendeSchuelers = [.. schuelers.GetIntessierendeKlasse()];
    
    Datei interessierendeExportLessons = exportLessons.Filtern(interessierendeSchuelers);
    Datei interessierendeUntisFaecher = untisFaecher.Filtern(interessierendeExportLessons);
    Datei interessierendeStudentgroupStudents = studentgroupStudents.Filtern(interessierendeSchuelers);
    Datei interessierendeMarksPerLesson = marksPerLesson.Filtern(interessierendeSchuelers);
    Datei interessierendeLernabschnittsdaten = lernabschnittsdaten.Filtern(interessierendeSchuelers);
    Datei interessierendeAbsencePerStudent = absencePerStudent.Filtern(interessierendeSchuelers,
        7,   // Die letzten 7 Tage bleiben unberücksichtigt, weil die Klassenleitung
             // Zeit für die Bearbeitung braucht.
        8);  // Maximal 8 Fehlstunden pro Tag werden gezählt.
             // Ansonsten würden bei ganztägigen Veranstaltungen 24 Fehlstunden entstehen.
        
    Datei neueLeistungsdaten = interessierendeSchuelers.Leistungsdaten(
        interessierendeExportLessons,
        interessierendeStudentgroupStudents, 
        interessierendeAbsencePerStudent, 
        interessierendeMarksPerLesson, 
        schuelerleistungsdaten.Kopfzeile,
        @"ImportFürSchild\Schülerleistungsdaten.dat");

    dateien.DoppelteZeilenAusDerZweitenDateiEntfernen(
        interessierendeUntisFaecher,              // An dieser Datei ...
        schildFaecher,                            // diese Datei wird an der anderen verglichen
        @"ImportFürSchild\Faecher.dat",           // Diese Datei kommt heraus
        "In SchILD fehlen Fächer oder Fächer-Eigenschaften weichen ab");   

    dateien.DoppelteZeilenAusDerZweitenDateiEntfernen(
        neueLeistungsdaten,                              // An dieser Datei ...
        schuelerleistungsdaten,                          // diese Datei wird an der anderen verglichen
        @"ImportFürSchild\Schuelerleistungsdateien.dat", // Diese Datei kommt heraus
        "In SchILD fehlen Leistungsdaten oder Leistungsdaten weichen ab");

    Datei neueLernabschnitsdaten = interessierendeSchuelers.SchuelerLernabschnittsdaten(
        lernabschnittsdaten,
        interessierendeExportLessons,
        interessierendeStudentgroupStudents,
        interessierendeAbsencePerStudent,
        interessierendeMarksPerLesson,
        schuelerleistungsdaten.Kopfzeile,
        @"ImportFürSchild\Lernabschnittsdaten.dat");

    dateien.DoppelteZeilenAusDerZweitenDateiEntfernen(
        neueLernabschnitsdaten,                     // An dieser Datei ...
        lernabschnittsdaten,                // diese Datei wird an der anderen verglichen
        @"ImportFürSchild\Lernabschnittsdaten.dat", // Diese Datei kommt heraus
        "In SchILD fehlen Lernabschnittsdaten oder Lernabschnittsdaten weichen ab");

} while (true);