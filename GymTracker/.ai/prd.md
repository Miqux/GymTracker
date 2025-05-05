# Dokument wymaga� produktu (PRD) - Gym Tracker

## 1. Przegl�d produktu
Gym Tracker to aplikacja webowa maj�ca na celu uproszczenie i usystematyzowanie rejestrowania post�p�w w treningach. Aplikacja umo�liwia u�ytkownikom:
- Rejestracj� i logowanie.
- Dodawanie, edycj� oraz blokowanie �wicze�.
- Tworzenie trening�w, kt�re sk�adaj� si� z daty oraz listy �wicze�, zawieraj�cych informacje o liczbie serii, powt�rze� i opcjonalnie ci�arze.
- Przegl�danie historii treningowej w trybie tylko do odczytu.

## 2. Problem u�ytkownika
U�ytkownicy obecnie musz� r�cznie zapisywa� post�py na si�owni, co jest czasoch�onne i cz�sto nieprzejrzyste. Brak przejrzysto�ci oraz z�o�ono�� manualnego zapisywania trening�w powoduj�, �e wielu u�ytkownik�w rezygnuje z regularnego prowadzenia notatek treningowych.

## 3. Wymagania funkcjonalne
- U�ytkownik musi mie� mo�liwo�� rejestracji oraz logowania.
- Dodawanie �wicze� z wymagan� walidacj�:
  - Nazwy �wicze� musz� by� unikalne w obr�bie jednego u�ytkownika.
  - Nazwa �wiczenia musi mie� d�ugo�� od 3 do 150 znak�w.
- Edycja �wicze�:
  - Zmiany w �wiczeniu maj� by� propagowane do wszystkich historycznych rekord�w.
- Blokowanie �wicze�:
  - Mechanizm blokowania �wicze� uniemo�liwia ich u�ycie w nowych treningach, ale zachowuje widoczno�� w historii trening�w.
- Dodawanie trening�w:
  - Trening sk�ada si� z daty i listy �wicze�.
  - Ka�de �wiczenie w treningu zawiera liczb� serii, powt�rze� oraz opcjonalnie pole na ci�ar.
- Przegl�danie historii trening�w w trybie tylko do odczytu.

## 4. Granice produktu
- Import �wicze� z dokument�w w formatach PDF, DOCX itp. nie wchodzi w zakres MVP.
- Wsp�dzielenie �wicze� mi�dzy u�ytkownikami nie jest obs�ugiwane.
- Aplikacje mobilne nie s� uwzgl�dnione na pocz�tkowym etapie � tylko wersja webowa.
- Brak ustalonej strategii dotycz�cej audytu lub wersjonowania danych przy propagowaniu zmian edycji �wicze� do historycznych rekord�w zostanie rozstrzygni�ty w p�niejszym etapie.

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
- �wiczenie zostaje zapisane w systemie.
- System wy�wietla komunikat o powodzeniu operacji.

### US-004
ID: US-004  
Tytu�: Edycja �wiczenia  
Opis: Jako u�ytkownik, chc� m�c edytowa� istniej�ce �wiczenie, aby aktualizowa� jego dane, a zmiany te maj� by� widoczne r�wnie� w historii trening�w.  
Kryteria akceptacji:
- U�ytkownik wybiera �wiczenie do edycji.
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
Opis: Jako u�ytkownik, chc� m�c dodawa� nowe treningi zawieraj�ce dat� oraz list� �wicze�, aby m�c rejestrowa� swoje post�py.  
Kryteria akceptacji:
- U�ytkownik wybiera dat� treningu.
- U�ytkownik dodaje �wiczenia do treningu wraz z liczb� serii, powt�rze� i opcjonalnym ci�arem.
- Trening zostaje zapisany w systemie.
- System wy�wietla komunikat o powodzeniu operacji.

### US-007
ID: US-007  
Tytu�: Przegl�danie historii trening�w  
Opis: Jako u�ytkownik, chc� m�c przegl�da� histori� moich trening�w, aby m�c monitorowa� swoje post�py.  
Kryteria akceptacji:
- U�ytkownik wy�wietla list� zapisanych trening�w.
- Ka�dy trening zawiera dat� oraz szczeg�ow� list� �wicze� z odpowiednimi danymi.
- Historia jest widoczna tylko do odczytu.

## 6. Metryki sukcesu
- U�ytkownik mo�e doda� �wiczenie spe�niaj�ce kryteria unikalno�ci i d�ugo�ci nazwy.
- U�ytkownik mo�e z powodzeniem zarejestrowa� si� i zalogowa�.
- Proces dodawania, edycji i blokowania �wicze� odbywa si� bez b��d�w, a zmiany w edycji s� propagowane do historycznych rekord�w.
- U�ytkownik mo�e utworzy� trening, kt�ry poprawnie zapisuje dat� oraz szczeg�y �wicze�.
- Historia trening�w jest wy�wietlana w spos�b czytelny i tylko do odczytu.
- System rejestruje operacje u�ytkownika i zapewnia bezpiecze�stwo danych poprzez mechanizmy uwierzytelniania i autoryzacji.