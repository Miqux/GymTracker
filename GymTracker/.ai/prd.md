# Dokument wymaga� produktu (PRD) - Gym Tracker

## 1. Przegl�d produktu
Gym Tracker to aplikacja webowa maj�ca na celu uproszczenie i usystematyzowanie rejestrowania post�p�w w treningach. Aplikacja umo�liwia u�ytkownikom:
- Rejestracj� i logowanie.
- Dodawanie, edycj� oraz blokowanie �wicze� z informacjami o grupach mi�niowych i poziomie trudno�ci.
- Tworzenie trening�w, kt�re sk�adaj� si� z daty, opcjonalnych notatek oraz listy �wicze�, zawieraj�cych informacje o liczbie serii, powt�rze� i opcjonalnie ci�arze.
- Przegl�danie historii treningowej z mo�liwo�ci� filtrowania i paginacji.
- Wy�wietlanie szczeg�owych informacji o treningach.
- Usuwanie trening�w.

## 2. Problem u�ytkownika
U�ytkownicy obecnie musz� r�cznie zapisywa� post�py na si�owni, co jest czasoch�onne i cz�sto nieprzejrzyste. Brak przejrzysto�ci oraz z�o�ono�� manualnego zapisywania trening�w powoduj�, �e wielu u�ytkownik�w rezygnuje z regularnego prowadzenia notatek treningowych. Dodatkowo brakuje mo�liwo�ci kategoryzacji �wicze� wed�ug grup mi�niowych i poziomu trudno�ci, co utrudnia planowanie efektywnych trening�w.

## 3. Wymagania funkcjonalne
- U�ytkownik musi mie� mo�liwo�� rejestracji oraz logowania.
- Dodawanie �wicze� z wymagan� walidacj�:
  - Nazwy �wicze� musz� by� unikalne w obr�bie jednego u�ytkownika.
  - Nazwa �wiczenia musi mie� d�ugo�� od 3 do 150 znak�w.
  - Ka�de �wiczenie musi mie� przypisan� grup� mi�niow� (np. klatka piersiowa, nogi, plecy).
  - Ka�de �wiczenie musi mie� okre�lony poziom trudno�ci.
  - Opcjonalny opis �wiczenia.
- Edycja �wicze�:
  - Zmiany w �wiczeniu maj� by� propagowane do wszystkich historycznych rekord�w.
- Blokowanie �wicze�:
  - Mechanizm blokowania �wicze� uniemo�liwia ich u�ycie w nowych treningach, ale zachowuje widoczno�� w historii trening�w.
- Dodawanie trening�w:
  - Trening sk�ada si� z daty, opcjonalnych notatek i listy �wicze�.
  - Ka�de �wiczenie w treningu zawiera liczb� serii, powt�rze� oraz opcjonalnie pole na ci�ar.
  - System sprawdza, czy wybrane �wiczenia nie s� zablokowane.
- Przegl�danie historii trening�w:
- Szczeg�y treningu:
  - Wy�wietlanie pe�nych informacji o treningu, w tym daty, notatek i wszystkich �wicze� z ich parametrami.
- Usuwanie trening�w:
  - Mo�liwo�� usuni�cia ca�ego treningu wraz z powi�zanymi �wiczeniami.

## 4. Granice produktu
- Import �wicze� z dokument�w w formatach PDF, DOCX itp. nie wchodzi w zakres MVP.
- Wsp�dzielenie �wicze� mi�dzy u�ytkownikami nie jest obs�ugiwane.
- Aplikacje mobilne nie s� uwzgl�dnione na pocz�tkowym etapie � tylko wersja webowa.
- Brak ustalonej strategii dotycz�cej audytu lub wersjonowania danych przy propagowaniu zmian edycji �wicze� do historycznych rekord�w zostanie rozstrzygni�ty w p�niejszym etapie.
- Generowanie statystyk i wykres�w post�pu nie jest cz�ci� pierwszej wersji produktu.
- Eksport danych do format�w zewn�trznych (CSV, PDF, itp.) nie jest obs�ugiwany.

## 5. Historyjki u�ytkownik�w

### US-001
ID: US-001  
Tytu�: Rejestracja u�ytkownika  
Opis: Jako nowy u�ytkownik, chc� m�c si� zarejestrowa�, aby uzyska� dost�p do aplikacji Gym Tracker.  
Kryteria akceptacji:
- U�ytkownik wprowadza wymagane dane (email, has�o).
- System weryfikuje poprawno�� danych.
- Po pomy�lnej rejestracji u�ytkownik zostaje uwierzytelniony.

### US-002
ID: US-002  
Tytu�: Logowanie u�ytkownika  
Opis: Jako zarejestrowany u�ytkownik, chc� m�c si� zalogowa�, aby m�c korzysta� z funkcjonalno�ci aplikacji.  
Kryteria akceptacji:
- U�ytkownik wprowadza poprawne dane logowania.
- System potwierdza autentyczno�� danych.
- Po pomy�lnym logowaniu u�ytkownik zostaje uwierzytelniony.

### US-003
ID: US-003  
Tytu�: Dodawanie �wiczenia  
Opis: Jako u�ytkownik, chc� m�c doda� nowe �wiczenie, aby m�c je wykorzysta� podczas tworzenia trening�w.  
Kryteria akceptacji:
- U�ytkownik wprowadza nazw� �wiczenia, kt�ra jest unikalna i zawiera od 3 do 150 znak�w.
- U�ytkownik wybiera grup� mi�niow� oraz poziom trudno�ci �wiczenia.
- U�ytkownik opcjonalnie mo�e doda� opis �wiczenia.
- �wiczenie zostaje zapisane w systemie.
- System wy�wietla komunikat o powodzeniu operacji.

### US-004
ID: US-004  
Tytu�: Edycja �wiczenia  
Opis: Jako u�ytkownik, chc� m�c edytowa� istniej�ce �wiczenie, aby aktualizowa� jego dane, a zmiany te maj� by� widoczne r�wnie� w historii trening�w.  
Kryteria akceptacji:
- U�ytkownik wybiera �wiczenie do edycji.
- U�ytkownik mo�e zmodyfikowa� nazw�, grup� mi�niow�, poziom trudno�ci lub opis.
- Wprowadzone zmiany s� zapisywane i propagowane do wszystkich historycznych rekord�w.
- System wy�wietla komunikat o powodzeniu operacji.

### US-005
ID: US-005  
Tytu�: Blokowanie �wiczenia  
Opis: Jako u�ytkownik, chc� mie� mo�liwo�� zablokowania �wiczenia, aby uniemo�liwi� jego u�ycie w nowych treningach, przy zachowaniu widoczno�ci dotychczasowych trening�w.  
Kryteria akceptacji:
- U�ytkownik wybiera �wiczenie do blokady.
- System oznacza �wiczenie jako zablokowane.
- �wiczenie nie jest dost�pne przy tworzeniu nowych trening�w, ale pojawia si� w historii treningowej.

### US-006
ID: US-006  
Tytu�: Dodawanie treningu  
Opis: Jako u�ytkownik, chc� m�c dodawa� nowe treningi zawieraj�ce dat�, opcjonalne notatki oraz list� �wicze�, aby m�c rejestrowa� swoje post�py.  
Kryteria akceptacji:
- U�ytkownik wybiera dat� treningu.
- U�ytkownik mo�e doda� opcjonalne notatki do treningu.
- U�ytkownik dodaje �wiczenia do treningu wraz z liczb� serii, powt�rze� i opcjonalnym ci�arem.
- System weryfikuje, czy wybrane �wiczenia nie s� zablokowane.
- Trening zostaje zapisany w systemie.
- System wy�wietla komunikat o powodzeniu operacji.

### US-007
ID: US-007  
Tytu�: Przegl�danie historii trening�w  
Opis: Jako u�ytkownik, chc� m�c przegl�da� histori� moich trening�w, aby m�c monitorowa� swoje post�py.  
Kryteria akceptacji:
- U�ytkownik mo�e zobaczy� list� trening�w.
- System wy�wietla dat� i opcjonalne notatki dla ka�dego treningu.

### US-008
ID: US-008  
Tytu�: Wy�wietlanie szczeg��w treningu  
Opis: Jako u�ytkownik, chc� mie� mo�liwo�� wy�wietlenia pe�nych szczeg��w konkretnego treningu, aby dok�adnie przeanalizowa� jego zawarto��.  
Kryteria akceptacji:
- U�ytkownik mo�e wybra� konkretny trening z listy historii.
- System wy�wietla dat�, notatki oraz pe�n� list� �wicze� z ich parametrami (serie, powt�rzenia, ci�ar).
- Informacje s� prezentowane w czytelny spos�b.

### US-009
ID: US-009  
Tytu�: Usuwanie treningu  
Opis: Jako u�ytkownik, chc� m�c usun�� trening, kt�ry zosta� dodany przez pomy�k� lub jest niepotrzebny.  
Kryteria akceptacji:
- U�ytkownik mo�e wybra� opcj� usuni�cia treningu.
- System usuwa trening i wszystkie powi�zane z nim dane �wicze�.
- System wy�wietla komunikat potwierdzaj�cy usuni�cie.

## 6. Metryki sukcesu
- U�ytkownik mo�e doda� �wiczenie spe�niaj�ce kryteria unikalno�ci, d�ugo�ci nazwy oraz posiadaj�ce wymagane pola (grupa mi�niowa, poziom trudno�ci).
- U�ytkownik mo�e z powodzeniem zarejestrowa� si� i zalogowa�.
- Proces dodawania, edycji i blokowania �wicze� odbywa si� bez b��d�w, a zmiany w edycji s� propagowane do historycznych rekord�w.
- U�ytkownik mo�e utworzy� trening, kt�ry poprawnie zapisuje dat�, notatki oraz szczeg�y �wicze�.
- System poprawnie filtruje treningi wed�ug wybranego zakresu dat.
- Historia trening�w jest wy�wietlana w spos�b czytelny i z efektywn� paginacj�.
- Szczeg�y treningu pokazuj� wszystkie niezb�dne informacje o �wiczeniach.
- System pozwala na usuni�cie treningu bez wp�ywu na inne dane u�ytkownika.
- System rejestruje operacje u�ytkownika i zapewnia bezpiecze�stwo danych poprzez mechanizmy uwierzytelniania i autoryzacji.