# Webuntis2SchILD

Mit **_Webuntis2SchILD_** können Daten von Untis und Webuntis nach SchILD übertragen werden. Insbesondere geht es darum, die Leistungsdaten (also die Kombination von Fach, Kursart, Kursbezeichnung, Lehrkraft, Wochenstunden, Note usw.) von Webuntis nach SchILD zu übertragen. Nach dem Import sehen die Leistungsdaten beipsielsweise für den Schüler Ahenstiel im aktuellen Halbjahr so aus:

![Schild](https://github.com/stbaeumer/webuntis2schild/blob/main/bilder/schild.png?raw=true)

Mit jedem Import werden neue Datensätze angelegt und bestehende Datensätze aktualisiert.

Der Übertrag der Leistungsdaten ist mindestens 3x im Jahr sinnvoll:

1. Haupterhebung, um die UVD direkt aus SchILD zu erstellen
2. Halbjahreszeugnis 
3. Jahreszeugnis

Vor den Zeugniskonferenzen kann **_Webuntis2SchILD_** wunderbar dafür eingesetzt werden, insbesondere Noten nach SchILD zu übertragen. Dazu müssen alle Lehrkräfte ihre Zeugnisnoten als Gesamtnoten in Webuntis eintragen. 
Abwesenheiten werden ebenfalls nach SchILD übertragen. Der Einsatz eines weiteren Programms zum Einsammeln der Noten ist nicht erforderlich.

![Gesamtnoten](https://github.com/stbaeumer/webuntis2schild/blob/main/bilder/gesamtnoten.png?raw=true)

## Vier Voraussetzungen

1. Administrativer Zugang zu Webuntis
2. Administrativer Zugang zu SchILD
3. Administrativer Zugang zu Untis
4. Kursnamen in Untis müssen den Namen der beteiligten Klasse(n) enthalten. Beispiel: 

![Kurse benennen](https://github.com/stbaeumer/webuntis2schild/blob/main/bilder/kurse.png?raw=true)

## Vorbereitungen

Stellen Sie diese Ordnerstruktur in Ihrem Download-Ordner her und laden Sie die geforderten Dateien aus den entsprechenden Programmen. Die Dateien im Ordner *ImportfürSchILD* werden durch das Programm **_Webuntis2SchILD_** erstellt:

```
Download-Ordner
+---ExportAusSchild
|       Faecher.dat
|       SchuelerLeistungsdaten.dat
|       SchuelerLernabschnittsdaten.dat
|       
+---ExportAusUntis
|       GPU006.TXT
|       
+---ExportAusWebuntis
|       AbsencePerStudent_20240918_1230.csv
|       ExportLessons_20240918_1022.csv
|       MarksPerLesson_20240922_1603.csv
|       StudentgroupStudents_20240918_1022.csv
|       Student_20240922_0952.csv
|       
\---ImportFürSchild
        Faecher.dat
        Schuelerleistungsdateien.dat
        SchuelerLernabschnittsdaten.dat
```

**So wird es gemacht:**

### Exportieren Sie die Datei `Student_<...>.csv` frisch aus Webuntis, indem Sie als Administrator:

- Stammdaten > Schülerinnen
- "Berichte" auswählen
- Bei "Schüler" auf CSV klicken
- Die Datei `Student_<...>.csv` im Ordner `ExportAusWebuntis` speichern");

![Stammdaten](https://github.com/stbaeumer/webuntis2schild/blob/main/bilder/stammdaten.png?raw=true)

### Exportieren Sie die Datei `MarksPerLesson_<...>.csv` frisch aus Webuntis, indem Sie als Administrator:

- Klassenbuch > Berichte klicken
- Alle Klassen auswählen und ggfs. den Zeitraum einschränken
- Unter "Noten" die Prüfungsart (-Alle-) auswählen
- Unter "Noten" den Haken bei Notennamen ausgeben _NICHT_ setzen
- Hinter "Noten pro Schüler" auf CSV klicken
- Die Datei `MarksPerLesson<...>.csv` im Ordner `ExportAusWebuntis` speichern"); 

![Noten](https://github.com/stbaeumer/webuntis2schild/blob/main/bilder/noten.png?raw=true)

### Exportieren Sie die Datei `AbsencePerStudent_<...>.csv` frisch aus Webuntis, indem Sie als Administrator:

- Klassenbuch > Berichte klicken
- Alle Klassen auswählen und als Zeitraum am besten die letzen vier Wochen wählen
- Unter "Abwesenheiten" Fehlzeiten pro Schüler*in auswählen
- pro Tag anhaken
- Auf CSV klicken
- Die Datei `AbsencePerStudent_<...>.csv` im Ordner `ExportAusWebuntis` speichern"); 

![Klassenbuch Berichte](https://github.com/stbaeumer/webuntis2schild/blob/main/bilder/abwesenheiten.png?raw=true)

### Exportieren Sie die Datei `GPU006.TXT` frisch aus Webuntis, indem Sie als Administrator:

- Datei > Import/Export > Export TXT > Fächer klicken
- Trennzeichen: Semikolon, Textbegrenzung: ", Encoding :UTF8
- Die Datei `GPU006.TXT` im Ordner `ExportAusUntis` speichern"); 
	
![Untis Export](https://github.com/stbaeumer/webuntis2schild/blob/main/bilder/gpu.png?raw=true)

### Exportieren Sie die Dateien `Faecher.dat`, `SchuelerLeistungsdaten.dat` und `SchuelerLernabschnittsdaten.dat` frisch aus Webuntis, indem Sie als Administrator:

- Datenaustausch > Schnittstelle > Export klicken.
- Alle Dateien abhaken und dann die drei Dateien `Faecher.dat`, `SchuelerLeistungsdaten.dat` und `SchuelerLernabschnittsdaten.dat` auswählen
- Den Export-Ordner auswählen
- Die Dateien im Ordner `ExportAusSchild` speichern"); 

## Herunterladen des Programms

Laden Sie alle Dateien aus dem *exe*-Ordner herunter. Also: *webuntis2schild.exe*, *webuntis2schild.dll* usw. Eine Installation ist nicht notwendig. 
**_Webuntis2SchILD_** kann mit Doppelklick gestartet werden:

![Programm starten](https://github.com/stbaeumer/webuntis2schild/blob/main/bilder/exe.png?raw=true)


## Programmstart

**_Webuntis2SchILD_** kann mit Doppelklick gestartet werden. Es öffnet sich ein Terminalfenster, in dem die Ausführung des Programms angezeigt wird.
Nacheinander werden alle o.g. Dateien eingelesen.  

Nach dem Einlesen haben Sie die Möglichkeit gewünschte Klassen anzugeben: 

![Programm starten](https://github.com/stbaeumer/webuntis2schild/blob/main/bilder/programmstart.png?raw=true)

Beispiele:

- `5a,5b,5c` sucht nach Klassen 5a, 5b und 5c
- `5` sucht nach allen Klassen, die mit 5 beginnen
- `5a` sucht nach Klasse 5a
- ENTER ohne weitere Angaben sucht nach allen Klassen

Im Anschluss werden die relevanten Dateien im Ordner `ExportFürSchild` abgelegt:

- `Faecher.dat` wird angelegt, sofern Fächer in Untis vorhanden sind, die in SchILD noch nicht existieren. Prüfen Sie am besten zuerst die Fächer. Vervollständigen Sie in SchILD die Fächer.
- `SchuelerLernabschnittsdaten.dat` wird angelegt, sofern die Lernabschnitte in SchILD noch nicht existieren. Grundsätzlich sollten die Lernabschnitte bereits in SchILD angelegt sein.
- `SchuelerLeistungsdaten.dat` wird angelegt, sofern Leistungsdaten sich verändert haben oder der Leistungsdatensatz in SchILD noch nicht existiert. Der Import dieser Datei setzt voraus, dass die Fächer und Lernabschnitte in SchILD existieren bzw. in den entsprechenden Dateien `Faecher.dat` und `SchuelerLernabschnittsdaten.dat` für den Import bereit sind.

![Import für Schild](https://github.com/stbaeumer/webuntis2schild/blob/main/bilder/importfuerschild.png?raw=true)

## Übertrag der Zeugnisnoten und Abwesenheiten

**_Webuntis2SchILD_** so konzipiert, dass Lehrerinnen und Lehrer alle Zeugnisnoten als Gesamtnoten in Webuntis eintragen können. 
Zur Vorbereitung auf die Zeugniskonferenz lässt die Zeugnisschreibung **_Webuntis2SchILD_** nach dem Fristende für die Noteneingabe laufen und importiert somit die Zeugnisnoten nach SchILD. Diese Lösung kommt also ohne händisches Eintippen und ohne weitere Software (SchILD-App usw.).

## Fragen und Antworten

### Ist es gefährlich Daten über *.dat Dateien nach SchILD zu importieren? 
Nein. Die Schnittstellen von SchILD sind sehr gut dokumentiert. SchILD meldet sich, wenn der Import aus irgendeinem Grund nicht klappt.

### Die Umlaute sehen kaputt aus.
Das darf eigentlich nicht passieren. Haben Sie eine der Dateien evtl. nachträglich mit Excel geöffnet und bearbeitet? Öffnen Sie Dateien bitte nicht mit Excel, sondern -sofern notwendig- mit einem Editor, wie z.B. Notepad++. In jedem Fall müssen die Dateien im UTF-8 Format vorliegen.

### Darf ich das Programm kostenlos nutzen?
Ja. **_Webuntis2SchILD_** steht unter der GPL-3.0 Lizenz kostenlos für jedermann zur Verfügung.

### Ist es nicht gefährlich, Programme aus dem Internet zu laden?
Ja. Es ist immer gefährlich, Programme aus dem Internet zu laden. **_Webuntis2SchILD_** ist jedoch quelloffen und kann von jedem eingesehen und geprüft werden.


### Wie lese ich die Dateien in SchILD ein?

Schritt1:
![Import für Schild](https://github.com/stbaeumer/webuntis2schild/blob/main/bilder/import.png?raw=true)

Schritt2:
![Import für Schild](https://github.com/stbaeumer/webuntis2schild/blob/main/bilder/import2.png?raw=true)
