# webuntis2schild

Mit webuntis2schild können Daten von Untis und Webuntis nach SchILD übertragen werden.
Insbesondere geht es darum, die Leistungsdaten von Webuntis nach SchILD zu übertragen. Der Übertrag der Leistungsdaten ist 3x im Jahr sinnvoll:

- Statistik
- Halbjahreszeugnis, 
- Jahreszeugnis

Vor den Zeugniskonferenzen kann `webuntis2schild` wunderbar dafür eingesetzt werden, insbesondere Noten nach SchILD zu übertragen. Dazu müssen alle Lehrkräfte ihre Zeugnisnoten als Gesamtnoten in Webuntis eintragen. 
Abwesenheiten werden ebenfalls nach SchILD übertragen. Der Einsatz eines weiteren Programms zum Einsammeln der Noten ist nicht mehr erforderlich.

## Voraussetzungen

- Administrativer Zugang zu Webuntis
- Administrativer Zugang zu SchILD
- Administrativer Zugang zu Untis
- Kurse in Untis müssen nach Vorgabe benannt sein, also den Namen der beteiligten Klasse(n) enthalten. Beispiel: `5a_Mathe`

## Vorbereitungen

Stellen Sie diese Ordnerstruktur in Ihrem Download-Ordner her:

![Ordnerstruktur](https://github.com/stbaeumer/webuntis2schild/blob/main/bilder/ordnerstruktur.png?raw=true)

So wird es gemacht:

Exportieren Sie die Datei `Student_<...>.csv` frisch aus Webuntis, indem Sie als Administrator:

- Stammdaten > Schülerinnen
- "Berichte" auswählen
- Bei "Schüler" auf CSV klicken
- Die Datei `Student_<...>.csv` im Ordner `ExportAusWebuntis` speichern");

Exportieren Sie die Datei `MarksPerLesson_<...>.csv` frisch aus Webuntis, indem Sie als Administrator:

- Klassenbuch > Berichte klicken
- Alle Klassen auswählen und ggfs. den Zeitraum einschränken
- Unter "Noten" die Prüfungsart (-Alle-) auswählen
- Unter "Noten" den Haken bei Notennamen ausgeben _NICHT_ setzen
- Hinter "Noten pro Schüler" auf CSV klicken
- Die Datei `MarksPerLesson<...>.csv` im Ordner `ExportAusWebuntis` speichern"); 

Exportieren Sie die Datei `AbsencePerStudent_<...>.csv` frisch aus Webuntis, indem Sie als Administrator:

- Klassenbuch > Berichte klicken
- Alle Klassen auswählen und als Zeitraum am besten die letzen vier Wochen wählen
- Unter "Abwesenheiten" Fehlzeiten pro Schüler*in auswählen
- pro Tag anhaken
- Auf CSV klicken
- Die Datei `AbsencePerStudent_<...>.csv` im Ordner `ExportAusWebuntis` speichern"); 

Exportieren Sie die Datei `GPU006.TXT` frisch aus Webuntis, indem Sie als Administrator:

- Datei > Import/Export > Export TXT > Fächer klicken
- Trennzeichen: Semikolon, Textbegrenzung: ", Encoding :UTF8
- Die Datei `GPU006.TXT` im Ordner `ExportAusUntis` speichern"); 
	
Exportieren Sie die Dateien `Faecher.dat`, `SchuelerLeistungsdaten.dat` und `SchuelerLernabschnittsdaten.dat` frisch aus Webuntis, indem Sie als Administrator:

- Datenaustausch > Schnittstelle > Export klicken.
- Alle Dateien abhaken und dann die drei Dateien `Faecher.dat`, `SchuelerLeistungsdaten.dat` und `SchuelerLernabschnittsdaten.dat` auswählen
- Den Export-Ordner auswählen
- Die Dateien im Ordner `ExportAusSchild` speichern"); 

## Installation

Das Programm kann in github heruntergeladen werden. Eine Installation ist nicht notwendig. 
Das Prgramm kann mit Doppelklick gestartet werden.

## Programmstart

Das Programm kann mit Doppelklick gestartet werden. Es öffnet sich ein Terminalfenster, in dem die Ausführung des Programms angezeigt wird.
Nacheinander werden alle o.g. Dateien eingelesen.  

Nach dem Einlesen haben Sie die Möglichkeit gewünschte Klassen anzugeben. Beispiele:

- `5a,5b,5c` sucht nach Klassen 5a, 5b und 5c
- `5` sucht nach allen Klassen, die mit 5 beginnen
- `5a` sucht nach Klasse 5a

Im Anschluss werden die relevanten Dateien im Ordner `ExportFürSchild` abgelegt:

- `Faecher.dat` wird angelegt, sofern Fächer in Untis vorhanden sind, die in SchILD noch nicht existieren. Prüfen Sie am besten zuerst die Fächer. Vervollständigen Sie in SchILD die Fächer.
- `SchuelerLernabschnittsdaten.dat` wird angelegt, sofern die Lernabschnitte in SchILD noch nicht existieren. Grundsätzlich sollten die Lernabschnitte bereits in SchILD angelegt sein.
- `SchuelerLeistungsdaten.dat` wird angelegt, sofern Leistungsdaten sich verändert haben oder der Leistungsdatensatz in SchILD noch nicht existiert. Der Import dieser Datei setzt voraus, dass die Fächer und Lernabschnitte in SchILD existieren bzw. in den entsprechenden Dateien `Faecher.dat` und `SchuelerLernabschnittsdaten.dat` für den Import bereit sind.

## Übertrag der Zeugnisnoten und Abwesenheiten

Das Programm ist so konzipiert, dass Lehrerinnen und Lehrer alle Zeugnisnoten als Gesamtnoten in Webuntis eintragen können. 
Zur Vorbereitung auf die Zeugniskonferenz lässt die Zeugnisschreibung das Programm nach dem Fristende für die Noteneingabe laufen und importiert somit die Zeugnisnoten nach SchILD. Diese Lösung kommt also ohne händisches Eintippen und ohne weitere Software (SchILD-App usw.).

## Fragen und Antworten

### Ist es gefährlich Daten über *.dat Dateien nach SchILD zu importieren? 
Nein. Die Schnittstellen von SchILD sind sehr gut dokumentiert. SchILD meldet sich, wenn der Import aus irgendeinem Grund nicht klappt.

### Die Umlaute sehen kaputt aus.
Das darf eigentlich nicht passieren. Haben Sie eine der Dateien evtl. mit Excel geöffnet und bearbeitet? Öffnen Sie Dateien bitte nicht mit Excel, sondern nur mit einem Editor, wie z.B. Notepad++.

### Darf ich das Programm kostenlos nutzen?
Ja. `webuntis2schild` steht unter der GPL3.0 Lizenz kostenlos für jedermann zur Verfügung.
