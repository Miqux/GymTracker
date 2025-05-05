# Dokument wymagañ produktu (PRD) - Gym Tracker

## 1. Przegl¹d produktu
Gym Tracker to aplikacja webowa maj¹ca na celu uproszczenie i usystematyzowanie rejestrowania postêpów w treningach. Aplikacja umo¿liwia u¿ytkownikom:
- Rejestracjê i logowanie.
- Dodawanie, edycjê oraz blokowanie æwiczeñ.
- Tworzenie treningów, które sk³adaj¹ siê z daty oraz listy æwiczeñ, zawieraj¹cych informacje o liczbie serii, powtórzeñ i opcjonalnie ciê¿arze.
- Przegl¹danie historii treningowej w trybie tylko do odczytu.

## 2. Problem u¿ytkownika
U¿ytkownicy obecnie musz¹ rêcznie zapisywaæ postêpy na si³owni, co jest czasoch³onne i czêsto nieprzejrzyste. Brak przejrzystoœci oraz z³o¿onoœæ manualnego zapisywania treningów powoduj¹, ¿e wielu u¿ytkowników rezygnuje z regularnego prowadzenia notatek treningowych.

## 3. Wymagania funkcjonalne
- U¿ytkownik musi mieæ mo¿liwoœæ rejestracji oraz logowania.
- Dodawanie æwiczeñ z wymagan¹ walidacj¹:
  - Nazwy æwiczeñ musz¹ byæ unikalne w obrêbie jednego u¿ytkownika.
  - Nazwa æwiczenia musi mieæ d³ugoœæ od 3 do 150 znaków.
- Edycja æwiczeñ:
  - Zmiany w æwiczeniu maj¹ byæ propagowane do wszystkich historycznych rekordów.
- Blokowanie æwiczeñ:
  - Mechanizm blokowania æwiczeñ uniemo¿liwia ich u¿ycie w nowych treningach, ale zachowuje widocznoœæ w historii treningów.
- Dodawanie treningów:
  - Trening sk³ada siê z daty i listy æwiczeñ.
  - Ka¿de æwiczenie w treningu zawiera liczbê serii, powtórzeñ oraz opcjonalnie pole na ciê¿ar.
- Przegl¹danie historii treningów w trybie tylko do odczytu.

## 4. Granice produktu
- Import æwiczeñ z dokumentów w formatach PDF, DOCX itp. nie wchodzi w zakres MVP.
- Wspó³dzielenie æwiczeñ miêdzy u¿ytkownikami nie jest obs³ugiwane.
- Aplikacje mobilne nie s¹ uwzglêdnione na pocz¹tkowym etapie – tylko wersja webowa.
- Brak ustalonej strategii dotycz¹cej audytu lub wersjonowania danych przy propagowaniu zmian edycji æwiczeñ do historycznych rekordów zostanie rozstrzygniêty w póŸniejszym etapie.

## 5. Historyjki u¿ytkowników

### US-001
ID: US-001  
Tytu³: Rejestracja u¿ytkownika  
Opis: Jako nowy u¿ytkownik, chcê móc siê zarejestrowaæ, aby uzyskaæ dostêp do aplikacji Gym Tracker.  
Kryteria akceptacji:
- U¿ytkownik wprowadza wymagane dane (email, has³o).
- System weryfikuje poprawnoœæ danych.
- Po pomyœlnej rejestracji u¿ytkownik zostaje uwierzytelniony.

### US-002
ID: US-002  
Tytu³: Logowanie u¿ytkownika  
Opis: Jako zarejestrowany u¿ytkownik, chcê móc siê zalogowaæ, aby móc korzystaæ z funkcjonalnoœci aplikacji.  
Kryteria akceptacji:
- U¿ytkownik wprowadza poprawne dane logowania.
- System potwierdza autentycznoœæ danych.
- Po pomyœlnym logowaniu u¿ytkownik zostaje uwierzytelniony.

### US-003
ID: US-003  
Tytu³: Dodawanie æwiczenia  
Opis: Jako u¿ytkownik, chcê móc dodaæ nowe æwiczenie, aby móc je wykorzystaæ podczas tworzenia treningów.  
Kryteria akceptacji:
- U¿ytkownik wprowadza nazwê æwiczenia, która jest unikalna i zawiera od 3 do 150 znaków.
- Æwiczenie zostaje zapisane w systemie.
- System wyœwietla komunikat o powodzeniu operacji.

### US-004
ID: US-004  
Tytu³: Edycja æwiczenia  
Opis: Jako u¿ytkownik, chcê móc edytowaæ istniej¹ce æwiczenie, aby aktualizowaæ jego dane, a zmiany te maj¹ byæ widoczne równie¿ w historii treningów.  
Kryteria akceptacji:
- U¿ytkownik wybiera æwiczenie do edycji.
- Wprowadzone zmiany s¹ zapisywane i propagowane do wszystkich historycznych rekordów.
- System wyœwietla komunikat o powodzeniu operacji.

### US-005
ID: US-005  
Tytu³: Blokowanie æwiczenia  
Opis: Jako u¿ytkownik, chcê mieæ mo¿liwoœæ zablokowania æwiczenia, aby uniemo¿liwiæ jego u¿ycie w nowych treningach, przy zachowaniu widocznoœci dotychczasowych treningów.  
Kryteria akceptacji:
- U¿ytkownik wybiera æwiczenie do blokady.
- System oznacza æwiczenie jako zablokowane.
- Æwiczenie nie jest dostêpne przy tworzeniu nowych treningów, ale pojawia siê w historii treningowej.

### US-006
ID: US-006  
Tytu³: Dodawanie treningu  
Opis: Jako u¿ytkownik, chcê móc dodawaæ nowe treningi zawieraj¹ce datê oraz listê æwiczeñ, aby móc rejestrowaæ swoje postêpy.  
Kryteria akceptacji:
- U¿ytkownik wybiera datê treningu.
- U¿ytkownik dodaje æwiczenia do treningu wraz z liczb¹ serii, powtórzeñ i opcjonalnym ciê¿arem.
- Trening zostaje zapisany w systemie.
- System wyœwietla komunikat o powodzeniu operacji.

### US-007
ID: US-007  
Tytu³: Przegl¹danie historii treningów  
Opis: Jako u¿ytkownik, chcê móc przegl¹daæ historiê moich treningów, aby móc monitorowaæ swoje postêpy.  
Kryteria akceptacji:
- U¿ytkownik wyœwietla listê zapisanych treningów.
- Ka¿dy trening zawiera datê oraz szczegó³ow¹ listê æwiczeñ z odpowiednimi danymi.
- Historia jest widoczna tylko do odczytu.

## 6. Metryki sukcesu
- U¿ytkownik mo¿e dodaæ æwiczenie spe³niaj¹ce kryteria unikalnoœci i d³ugoœci nazwy.
- U¿ytkownik mo¿e z powodzeniem zarejestrowaæ siê i zalogowaæ.
- Proces dodawania, edycji i blokowania æwiczeñ odbywa siê bez b³êdów, a zmiany w edycji s¹ propagowane do historycznych rekordów.
- U¿ytkownik mo¿e utworzyæ trening, który poprawnie zapisuje datê oraz szczegó³y æwiczeñ.
- Historia treningów jest wyœwietlana w sposób czytelny i tylko do odczytu.
- System rejestruje operacje u¿ytkownika i zapewnia bezpieczeñstwo danych poprzez mechanizmy uwierzytelniania i autoryzacji.