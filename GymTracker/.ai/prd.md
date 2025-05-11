# Dokument wymagañ produktu (PRD) - Gym Tracker

## 1. Przegl¹d produktu
Gym Tracker to aplikacja webowa maj¹ca na celu uproszczenie i usystematyzowanie rejestrowania postêpów w treningach. Aplikacja umo¿liwia u¿ytkownikom:
- Rejestracjê i logowanie.
- Dodawanie, edycjê oraz blokowanie æwiczeñ z informacjami o grupach miêœniowych i poziomie trudnoœci.
- Tworzenie treningów, które sk³adaj¹ siê z daty, opcjonalnych notatek oraz listy æwiczeñ, zawieraj¹cych informacje o liczbie serii, powtórzeñ i opcjonalnie ciê¿arze.
- Przegl¹danie historii treningowej z mo¿liwoœci¹ filtrowania i paginacji.
- Wyœwietlanie szczegó³owych informacji o treningach.
- Usuwanie treningów.

## 2. Problem u¿ytkownika
U¿ytkownicy obecnie musz¹ rêcznie zapisywaæ postêpy na si³owni, co jest czasoch³onne i czêsto nieprzejrzyste. Brak przejrzystoœci oraz z³o¿onoœæ manualnego zapisywania treningów powoduj¹, ¿e wielu u¿ytkowników rezygnuje z regularnego prowadzenia notatek treningowych. Dodatkowo brakuje mo¿liwoœci kategoryzacji æwiczeñ wed³ug grup miêœniowych i poziomu trudnoœci, co utrudnia planowanie efektywnych treningów.

## 3. Wymagania funkcjonalne
- U¿ytkownik musi mieæ mo¿liwoœæ rejestracji oraz logowania.
- Dodawanie æwiczeñ z wymagan¹ walidacj¹:
  - Nazwy æwiczeñ musz¹ byæ unikalne w obrêbie jednego u¿ytkownika.
  - Nazwa æwiczenia musi mieæ d³ugoœæ od 3 do 150 znaków.
  - Ka¿de æwiczenie musi mieæ przypisan¹ grupê miêœniow¹ (np. klatka piersiowa, nogi, plecy).
  - Ka¿de æwiczenie musi mieæ okreœlony poziom trudnoœci.
  - Opcjonalny opis æwiczenia.
- Edycja æwiczeñ:
  - Zmiany w æwiczeniu maj¹ byæ propagowane do wszystkich historycznych rekordów.
- Blokowanie æwiczeñ:
  - Mechanizm blokowania æwiczeñ uniemo¿liwia ich u¿ycie w nowych treningach, ale zachowuje widocznoœæ w historii treningów.
- Dodawanie treningów:
  - Trening sk³ada siê z daty, opcjonalnych notatek i listy æwiczeñ.
  - Ka¿de æwiczenie w treningu zawiera liczbê serii, powtórzeñ oraz opcjonalnie pole na ciê¿ar.
  - System sprawdza, czy wybrane æwiczenia nie s¹ zablokowane.
- Przegl¹danie historii treningów:
- Szczegó³y treningu:
  - Wyœwietlanie pe³nych informacji o treningu, w tym daty, notatek i wszystkich æwiczeñ z ich parametrami.
- Usuwanie treningów:
  - Mo¿liwoœæ usuniêcia ca³ego treningu wraz z powi¹zanymi æwiczeniami.

## 4. Granice produktu
- Import æwiczeñ z dokumentów w formatach PDF, DOCX itp. nie wchodzi w zakres MVP.
- Wspó³dzielenie æwiczeñ miêdzy u¿ytkownikami nie jest obs³ugiwane.
- Aplikacje mobilne nie s¹ uwzglêdnione na pocz¹tkowym etapie – tylko wersja webowa.
- Brak ustalonej strategii dotycz¹cej audytu lub wersjonowania danych przy propagowaniu zmian edycji æwiczeñ do historycznych rekordów zostanie rozstrzygniêty w póŸniejszym etapie.
- Generowanie statystyk i wykresów postêpu nie jest czêœci¹ pierwszej wersji produktu.
- Eksport danych do formatów zewnêtrznych (CSV, PDF, itp.) nie jest obs³ugiwany.

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
- U¿ytkownik wybiera grupê miêœniow¹ oraz poziom trudnoœci æwiczenia.
- U¿ytkownik opcjonalnie mo¿e dodaæ opis æwiczenia.
- Æwiczenie zostaje zapisane w systemie.
- System wyœwietla komunikat o powodzeniu operacji.

### US-004
ID: US-004  
Tytu³: Edycja æwiczenia  
Opis: Jako u¿ytkownik, chcê móc edytowaæ istniej¹ce æwiczenie, aby aktualizowaæ jego dane, a zmiany te maj¹ byæ widoczne równie¿ w historii treningów.  
Kryteria akceptacji:
- U¿ytkownik wybiera æwiczenie do edycji.
- U¿ytkownik mo¿e zmodyfikowaæ nazwê, grupê miêœniow¹, poziom trudnoœci lub opis.
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
Opis: Jako u¿ytkownik, chcê móc dodawaæ nowe treningi zawieraj¹ce datê, opcjonalne notatki oraz listê æwiczeñ, aby móc rejestrowaæ swoje postêpy.  
Kryteria akceptacji:
- U¿ytkownik wybiera datê treningu.
- U¿ytkownik mo¿e dodaæ opcjonalne notatki do treningu.
- U¿ytkownik dodaje æwiczenia do treningu wraz z liczb¹ serii, powtórzeñ i opcjonalnym ciê¿arem.
- System weryfikuje, czy wybrane æwiczenia nie s¹ zablokowane.
- Trening zostaje zapisany w systemie.
- System wyœwietla komunikat o powodzeniu operacji.

### US-007
ID: US-007  
Tytu³: Przegl¹danie historii treningów  
Opis: Jako u¿ytkownik, chcê móc przegl¹daæ historiê moich treningów, aby móc monitorowaæ swoje postêpy.  
Kryteria akceptacji:
- U¿ytkownik mo¿e zobaczyæ listê treningów.
- System wyœwietla datê i opcjonalne notatki dla ka¿dego treningu.

### US-008
ID: US-008  
Tytu³: Wyœwietlanie szczegó³ów treningu  
Opis: Jako u¿ytkownik, chcê mieæ mo¿liwoœæ wyœwietlenia pe³nych szczegó³ów konkretnego treningu, aby dok³adnie przeanalizowaæ jego zawartoœæ.  
Kryteria akceptacji:
- U¿ytkownik mo¿e wybraæ konkretny trening z listy historii.
- System wyœwietla datê, notatki oraz pe³n¹ listê æwiczeñ z ich parametrami (serie, powtórzenia, ciê¿ar).
- Informacje s¹ prezentowane w czytelny sposób.

### US-009
ID: US-009  
Tytu³: Usuwanie treningu  
Opis: Jako u¿ytkownik, chcê móc usun¹æ trening, który zosta³ dodany przez pomy³kê lub jest niepotrzebny.  
Kryteria akceptacji:
- U¿ytkownik mo¿e wybraæ opcjê usuniêcia treningu.
- System usuwa trening i wszystkie powi¹zane z nim dane æwiczeñ.
- System wyœwietla komunikat potwierdzaj¹cy usuniêcie.

## 6. Metryki sukcesu
- U¿ytkownik mo¿e dodaæ æwiczenie spe³niaj¹ce kryteria unikalnoœci, d³ugoœci nazwy oraz posiadaj¹ce wymagane pola (grupa miêœniowa, poziom trudnoœci).
- U¿ytkownik mo¿e z powodzeniem zarejestrowaæ siê i zalogowaæ.
- Proces dodawania, edycji i blokowania æwiczeñ odbywa siê bez b³êdów, a zmiany w edycji s¹ propagowane do historycznych rekordów.
- U¿ytkownik mo¿e utworzyæ trening, który poprawnie zapisuje datê, notatki oraz szczegó³y æwiczeñ.
- System poprawnie filtruje treningi wed³ug wybranego zakresu dat.
- Historia treningów jest wyœwietlana w sposób czytelny i z efektywn¹ paginacj¹.
- Szczegó³y treningu pokazuj¹ wszystkie niezbêdne informacje o æwiczeniach.
- System pozwala na usuniêcie treningu bez wp³ywu na inne dane u¿ytkownika.
- System rejestruje operacje u¿ytkownika i zapewnia bezpieczeñstwo danych poprzez mechanizmy uwierzytelniania i autoryzacji.