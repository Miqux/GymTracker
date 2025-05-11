# Kompleksowy Plan Testów dla Projektu GymTracker

## 1. Wprowadzenie i cele testowania

### 1.1. Wprowadzenie
Niniejszy dokument przedstawia kompleksowy plan testów dla aplikacji internetowej GymTracker. GymTracker jest aplikacj¹ umo¿liwiaj¹c¹ u¿ytkownikom œledzenie postêpów treningowych, rejestrowanie wyników oraz zarz¹dzanie planem treningowym. Projekt jest realizowany w technologii .NET 8, ASP.NET Core Razor Pages z wykorzystaniem C# i Entity Framework Core.

### 1.2. Cele testowania
G³ównym celem testów jest zapewnienie wysokiej jakoœci aplikacji GymTracker poprzez:
*   Weryfikacjê zgodnoœci zdefiniowanych wymagañ funkcjonalnych i niefunkcjonalnych.
*   Wykrycie i zaraportowanie defektów.
*   Ocena stabilnoœci, wydajnoœci i bezpieczeñstwa aplikacji.
*   Zapewnienie pozytywnego doœwiadczenia u¿ytkownika (UX).
*   Minimalizacja ryzyka wyst¹pienia b³êdów na œrodowisku produkcyjnym.

## 2. Zakres testów

### 2.1. Funkcjonalnoœci objête testami
Testom podlegaæ bêd¹ wszystkie kluczowe modu³y i funkcjonalnoœci aplikacji, w tym:
*   **Zarz¹dzanie Kontem U¿ytkownika:**
    *   Rejestracja nowych u¿ytkowników.
    *   Logowanie i wylogowywanie.
    *   Obs³uga b³êdnych danych logowania/rejestracji.
*   **Zarz¹dzanie Æwiczeniami:**
    *   Tworzenie nowych æwiczeñ z walidacj¹ (unikalnoœæ nazwy per u¿ytkownik, d³ugoœæ nazwy).
    *   Wyœwietlanie listy æwiczeñ u¿ytkownika (tylko niezablokowane).
    *   Edycja istniej¹cych æwiczeñ.
    *   Blokowanie æwiczeñ (zablokowane æwiczenia nie s¹ dostêpne do wyboru w nowych treningach, ale widoczne w historii).
*   **Zarz¹dzanie Treningami:**
    *   Tworzenie nowych treningów (wybór daty, dodawanie æwiczeñ, okreœlanie serii, powtórzeñ, wagi).
    *   Wyœwietlanie historii treningów z paginacj¹ i filtrowaniem po zakresie dat.
    *   Wyœwietlanie szczegó³ów pojedynczego treningu.
    *   Usuwanie treningów.
*   **Walidacja Danych:**
    *   Walidacja po stronie klienta (JavaScript) i serwera (DataAnnotations).
*   **Autoryzacja i Bezpieczeñstwo:**
    *   Dostêp do funkcjonalnoœci tylko dla zalogowanych u¿ytkowników (z wyj¹tkiem strony g³ównej, logowania, rejestracji).
    *   Zabezpieczenie przed CSRF.
    *   Ochrona danych u¿ytkownika.
*   **Interfejs U¿ytkownika (UI) i Doœwiadczenie U¿ytkownika (UX):**
    *   Poprawnoœæ wyœwietlania na ró¿nych przegl¹darkach (zgodnie z przyjêtymi standardami).
    *   Intuicyjnoœæ nawigacji i obs³ugi.
    *   Responsywnoœæ (jeœli jest w zakresie).

### 2.2. Funkcjonalnoœci wy³¹czone z testów (Out of Scope)
Na obecnym etapie rozwoju, nastêpuj¹ce funkcjonalnoœci s¹ wy³¹czone z zakresu testów:
*   Generowanie zaawansowanych raportów i statystyk (poza prostym wyœwietlaniem historii).
*   Import/eksport danych.
*   Funkcjonalnoœci administracyjne (jeœli nie zosta³y zdefiniowane).
*   Integracje z systemami zewnêtrznymi (jeœli nie dotyczy).
*   Testy wydajnoœciowe pod bardzo du¿ym obci¹¿eniem (chyba ¿e zdefiniowano inaczej).
*   Zaawansowane testy penetracyjne (poza podstawowymi testami bezpieczeñstwa).

## 3. Typy testów do przeprowadzenia

*   **Testy Jednostkowe (Unit Tests):**
    *   Cel: Weryfikacja poprawnoœci dzia³ania poszczególnych komponentów (metod w serwisach, logiki w kontrolerach/PageModels) w izolacji.
    *   Zakres: Logika biznesowa w `AccountService`, `ExerciseService`, `WorkoutService`. Metody pomocnicze.
*   **Testy Integracyjne (Integration Tests):**
    *   Cel: Weryfikacja interakcji pomiêdzy komponentami systemu, np. kontroler-serwis-baza danych.
    *   Zakres: Poprawnoœæ dzia³ania endpointów API/akcji kontrolerów, integracja z Entity Framework Core, operacje CRUD na bazie danych, dzia³anie middleware.
*   **Testy Funkcjonalne (E2E - End-to-End):**
    *   Cel: Weryfikacja dzia³ania aplikacji z perspektywy u¿ytkownika koñcowego, symulowanie rzeczywistych scenariuszy u¿ycia.
    *   Zakres: Pe³ne przep³ywy u¿ytkownika, np. rejestracja -> logowanie -> dodanie æwiczenia -> dodanie treningu -> przegl¹danie historii.
*   **Testy Interfejsu U¿ytkownika (UI Tests):**
    *   Cel: Weryfikacja poprawnoœci wyœwietlania i dzia³ania elementów UI, responsywnoœci, zgodnoœci z projektem graficznym.
    *   Zakres: Wygl¹d i dzia³anie formularzy, tabel, przycisków, nawigacji.
*   **Testy Walidacyjne:**
    *   Cel: Sprawdzenie poprawnoœci mechanizmów walidacji danych wejœciowych (po stronie klienta i serwera).
    *   Zakres: Walidacja formularzy rejestracji, logowania, tworzenia/edycji æwiczeñ i treningów.
*   **Testy Bezpieczeñstwa (Security Tests):**
    *   Cel: Identyfikacja potencjalnych luk bezpieczeñstwa.
    *   Zakres: Kontrola dostêpu (autoryzacja), ochrona przed CSRF, walidacja danych wejœciowych pod k¹tem XSS (choæ Razor Pages domyœlnie enkoduje), bezpieczeñstwo sesji/ciasteczek.
*   **Testy U¿ytecznoœci (Usability Tests):**
    *   Cel: Ocena ³atwoœci obs³ugi i intuicyjnoœci interfejsu.
    *   Zakres: Nawigacja, przejrzystoœæ komunikatów, ogólne doœwiadczenie u¿ytkownika.
*   **Testy Kompatybilnoœci (Compatibility Tests):**
    *   Cel: Sprawdzenie dzia³ania aplikacji na ró¿nych przegl¹darkach i urz¹dzeniach.
    *   Zakres: G³ówne przegl¹darki (Chrome, Firefox, Edge - najnowsze wersje).
*   **Testy Akceptacyjne U¿ytkownika (UAT):**
    *   Cel: Potwierdzenie przez klienta/product ownera, ¿e aplikacja spe³nia wymagania biznesowe.
    *   Zakres: Kluczowe scenariusze biznesowe.

## 4. Scenariusze testowe dla kluczowych funkcjonalnoœci

Poni¿ej znajduj¹ siê przyk³adowe scenariusze testowe. Pe³na lista bêdzie rozwijana w trakcie trwania projektu.

### 4.1. Zarz¹dzanie Kontem U¿ytkownika

| ID Scenariusza | Opis                                                                 | Kroki Testowe                                                                                                                               | Oczekiwany Rezultat                                                                                                                               | Priorytet |
| :------------- | :------------------------------------------------------------------- | :------------------------------------------------------------------------------------------------------------------------------------------ | :------------------------------------------------------------------------------------------------------------------------------------------------ | :-------- |
| TC_ACC_001     | Pomyœlna rejestracja nowego u¿ytkownika                               | 1. PrzejdŸ do /Account/Register. 2. Wype³nij formularz poprawnymi danymi (unikalny email, has³o spe³niaj¹ce kryteria). 3. Kliknij "Zarejestruj siê". | U¿ytkownik zostaje zarejestrowany, zalogowany i przekierowany na stronê g³ówn¹. Wyœwietlony komunikat o sukcesie.                                  | Krytyczny |
| TC_ACC_002     | Rejestracja z istniej¹cym adresem email                               | 1. PrzejdŸ do /Account/Register. 2. WprowadŸ email, który ju¿ istnieje w systemie. 3. WprowadŸ has³o. 4. Kliknij "Zarejestruj siê".         | Wyœwietlony b³¹d informuj¹cy o zajêtym adresie email. U¿ytkownik nie zostaje zarejestrowany.                                                        | Wysoki    |
| TC_ACC_003     | Rejestracja z niepoprawnym formatem emaila                            | 1. PrzejdŸ do /Account/Register. 2. WprowadŸ email w niepoprawnym formacie. 3. WprowadŸ has³o. 4. Kliknij "Zarejestruj siê".             | Wyœwietlony b³¹d walidacji formatu emaila. U¿ytkownik nie zostaje zarejestrowany.                                                                  | Wysoki    |
| TC_ACC_004     | Rejestracja z za krótkim has³em                                      | 1. PrzejdŸ do /Account/Register. 2. WprowadŸ poprawny email. 3. WprowadŸ has³o krótsze ni¿ 6 znaków. 4. Kliknij "Zarejestruj siê".          | Wyœwietlony b³¹d walidacji d³ugoœci has³a. U¿ytkownik nie zostaje zarejestrowany.                                                                  | Wysoki    |
| TC_ACC_005     | Pomyœlne logowanie                                                   | 1. PrzejdŸ do /Account/Login. 2. WprowadŸ email i has³o zarejestrowanego u¿ytkownika. 3. Kliknij "Zaloguj siê".                            | U¿ytkownik zostaje zalogowany i przekierowany na stronê g³ówn¹. Ciasteczko uwierzytelniaj¹ce jest ustawione.                                     | Krytyczny |
| TC_ACC_006     | Logowanie z b³êdnym has³em                                           | 1. PrzejdŸ do /Account/Login. 2. WprowadŸ poprawny email zarejestrowanego u¿ytkownika. 3. WprowadŸ b³êdne has³o. 4. Kliknij "Zaloguj siê". | Wyœwietlony komunikat o b³êdnych danych logowania. U¿ytkownik nie zostaje zalogowany.                                                              | Wysoki    |
| TC_ACC_007     | Logowanie z nieistniej¹cym emailem                                   | 1. PrzejdŸ do /Account/Login. 2. WprowadŸ email, który nie istnieje w systemie. 3. WprowadŸ dowolne has³o. 4. Kliknij "Zaloguj siê".   | Wyœwietlony komunikat o b³êdnych danych logowania. U¿ytkownik nie zostaje zalogowany.                                                              | Wysoki    |
| TC_ACC_008     | Pomyœlne wylogowanie                                                 | 1. Zaloguj siê do aplikacji. 2. Kliknij przycisk/link "Wyloguj".                                                                            | U¿ytkownik zostaje wylogowany i przekierowany na stronê g³ówn¹ lub logowania. Chronione zasoby nie s¹ dostêpne. Ciasteczko uwierzytelniaj¹ce usuniête. | Krytyczny |

### 4.2. Zarz¹dzanie Æwiczeniami

| ID Scenariusza | Opis                                                                  | Kroki Testowe                                                                                                                                  | Oczekiwany Rezultat                                                                                                                              | Priorytet |
| :------------- | :-------------------------------------------------------------------- | :--------------------------------------------------------------------------------------------------------------------------------------------- | :----------------------------------------------------------------------------------------------------------------------------------------------- | :-------- |
| TC_EX_001      | Dodanie nowego æwiczenia z poprawnymi danymi                           | 1. Zaloguj siê. 2. PrzejdŸ do /Exercises/Create. 3. Wype³nij formularz (Nazwa, Grupa miêœniowa, Poziom trudnoœci, Opis). 4. Kliknij "Dodaj æwiczenie". | Æwiczenie zostaje dodane do listy. Komunikat o sukcesie. Nazwa unikalna dla u¿ytkownika, d³ugoœæ 3-150.                                          | Krytyczny |
| TC_EX_002      | Próba dodania æwiczenia z nazw¹ krótsz¹ ni¿ 3 znaki                   | 1. Zaloguj siê. 2. PrzejdŸ do /Exercises/Create. 3. WprowadŸ nazwê "AB". 4. Wype³nij resztê pól. 5. Kliknij "Dodaj æwiczenie".                      | Wyœwietlony b³¹d walidacji d³ugoœci nazwy. Æwiczenie nie zostaje dodane.                                                                         | Wysoki    |
| TC_EX_003      | Próba dodania æwiczenia z nazw¹ d³u¿sz¹ ni¿ 150 znaków                 | 1. Zaloguj siê. 2. PrzejdŸ do /Exercises/Create. 3. WprowadŸ nazwê >150 znaków. 4. Wype³nij resztê pól. 5. Kliknij "Dodaj æwiczenie".             | Wyœwietlony b³¹d walidacji d³ugoœci nazwy. Æwiczenie nie zostaje dodane.                                                                         | Wysoki    |
| TC_EX_004      | Próba dodania æwiczenia o tej samej nazwie przez tego samego u¿ytkownika | 1. Zaloguj siê. 2. Dodaj æwiczenie "Wyciskanie". 3. PrzejdŸ do /Exercises/Create. 4. WprowadŸ nazwê "Wyciskanie". 5. Kliknij "Dodaj æwiczenie".  | Wyœwietlony b³¹d o duplikacji nazwy. Æwiczenie nie zostaje dodane.                                                                               | Wysoki    |
| TC_EX_005      | Edycja istniej¹cego æwiczenia                                          | 1. Zaloguj siê. 2. PrzejdŸ do listy æwiczeñ. 3. Wybierz æwiczenie i kliknij "Edytuj". 4. Zmieñ dane. 5. Kliknij "Zapisz zmiany".                   | Dane æwiczenia zostaj¹ zaktualizowane. Komunikat o sukcesie. Zmiany widoczne w historii treningów.                                              | Wysoki    |
| TC_EX_006      | Blokowanie æwiczenia                                                  | 1. Zaloguj siê. 2. PrzejdŸ do listy æwiczeñ. 3. Wybierz æwiczenie i kliknij "Blokuj".                                                           | Æwiczenie oznaczone jako zablokowane. Nie pojawia siê na liœcie wyboru przy tworzeniu nowego treningu. Komunikat o sukcesie.                       | Wysoki    |
| TC_EX_007      | Wyœwietlanie listy æwiczeñ                                            | 1. Zaloguj siê. 2. PrzejdŸ do /Exercises/Index.                                                                                                | Wyœwietlona lista niezablokowanych æwiczeñ nale¿¹cych do u¿ytkownika. Dostêpne opcje "Edytuj", "Blokuj".                                          | Krytyczny |

### 4.3. Zarz¹dzanie Treningami

| ID Scenariusza | Opis                                                                  | Kroki Testowe                                                                                                                                                                                          | Oczekiwany Rezultat                                                                                                                                                              | Priorytet |
| :------------- | :-------------------------------------------------------------------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | :-------- |
| TC_WO_001      | Dodanie nowego treningu z poprawnymi danymi                            | 1. Zaloguj siê. 2. PrzejdŸ do /Workouts/Create. 3. Wybierz datê. 4. Dodaj co najmniej jedno æwiczenie (wybrane z listy niezablokowanych), podaj Serie, Powtórzenia, opcjonalnie Wagê. 5. Kliknij "Zapisz trening". | Trening zostaje zapisany. Komunikat o sukcesie. U¿ytkownik przekierowany do historii treningów.                                                                                   | Krytyczny |
| TC_WO_002      | Próba dodania treningu bez æwiczeñ                                      | 1. Zaloguj siê. 2. PrzejdŸ do /Workouts/Create. 3. Wybierz datê. 4. Nie dodawaj ¿adnych æwiczeñ. 5. Kliknij "Zapisz trening".                                                                             | Wyœwietlony b³¹d walidacji informuj¹cy o koniecznoœci dodania æwiczeñ. Trening nie zostaje zapisany.                                                                               | Wysoki    |
| TC_WO_003      | Próba dodania zablokowanego æwiczenia do treningu                     | 1. Zaloguj siê. 2. Zablokuj jedno ze swoich æwiczeñ. 3. PrzejdŸ do /Workouts/Create. 4. SprawdŸ listê dostêpnych æwiczeñ.                                                                                 | Zablokowane æwiczenie nie jest widoczne na liœcie wyboru æwiczeñ.                                                                                                                  | Wysoki    |
| TC_WO_004      | Wyœwietlanie historii treningów (paginacja, filtrowanie)               | 1. Zaloguj siê. 2. PrzejdŸ do /Workouts/History. 3. SprawdŸ wyœwietlanie listy treningów. 4. Przetestuj paginacjê. 5. Przetestuj filtrowanie po zakresie dat.                                                | Lista treningów wyœwietlana poprawnie. Paginacja dzia³a. Filtrowanie po datach zwraca poprawne wyniki.                                                                               | Krytyczny |
| TC_WO_005      | Wyœwietlanie szczegó³ów treningu                                       | 1. Zaloguj siê. 2. PrzejdŸ do /Workouts/History. 3. Kliknij "Szczegó³y" przy wybranym treningu.                                                                                                         | Wyœwietlone szczegó³y treningu: data, notatki, lista æwiczeñ z seriami, powtórzeniami, wag¹. Dane tylko do odczytu.                                                              | Wysoki    |
| TC_WO_006      | Usuwanie treningu                                                     | 1. Zaloguj siê. 2. PrzejdŸ do /Workouts/History. 3. Wybierz trening i kliknij "Usuñ". PotwierdŸ usuniêcie.                                                                                               | Trening zostaje usuniêty z historii. Komunikat o sukcesie.                                                                                                                        | Wysoki    |
| TC_WO_007      | Propagacja edycji nazwy æwiczenia w historii treningów                 | 1. Zaloguj siê. 2. Utwórz trening z æwiczeniem X. 3. Edytuj æwiczenie X, zmieniaj¹c jego nazwê na Y. 4. PrzejdŸ do /Workouts/History i wyœwietl szczegó³y treningu z kroku 2.                            | W szczegó³ach treningu widoczna jest nowa nazwa æwiczenia Y.                                                                                                                      | Wysoki    |

## 5. Œrodowisko testowe

*   **Œrodowisko deweloperskie lokalne:**
    *   System operacyjny: Windows/Linux/macOS (zgodnie z konfiguracj¹ deweloperów).
    *   Serwer WWW: IIS Express / Kestrel.
    *   Baza danych: SQL Server Express LocalDB lub dedykowana instancja deweloperska SQL Server.
    *   Przegl¹darki: Najnowsze wersje Chrome, Firefox.
*   **Œrodowisko testowe (Staging/QA):**
    *   System operacyjny: Zbli¿ony do produkcyjnego (np. Windows Server, Linux).
    *   Serwer WWW: IIS / Kestrel (zale¿nie od konfiguracji produkcyjnej).
    *   Baza danych: Dedykowana instancja SQL Server dla œrodowiska QA, z danymi testowymi zbli¿onymi do produkcyjnych (zanonimizowane, jeœli konieczne).
    *   Konfiguracja: Zbli¿ona do produkcyjnej (np. ustawienia logowania, HSTS).
*   **Dane Testowe:**
    *   Przygotowany zestaw danych u¿ytkowników, æwiczeñ i treningów, pokrywaj¹cy ró¿ne scenariusze.
    *   Skrypty do generowania/przywracania danych testowych.

## 6. Narzêdzia do testowania

*   **Testy Jednostkowe:**
    *   Framework: MSTest, NUnit lub xUnit (zale¿nie od preferencji zespo³u, `.csproj` wskazuje na mo¿liwoœæ u¿ycia `dotnet test`, co jest standardem).
    *   Mockowanie: Moq, NSubstitute.
*   **Testy Integracyjne:**
    *   Framework: Ten sam co dla testów jednostkowych, z wykorzystaniem `WebApplicationFactory` dla testowania endpointów.
    *   Baza danych: EF Core InMemory Provider, SQLite in-memory, lub dedykowana baza testowa.
*   **Testy Funkcjonalne (E2E) i UI:**
    *   Narzêdzia: Selenium WebDriver (z C# bindings), Playwright, Cypress.
*   **Testy API (jeœli powstan¹ dedykowane API, na razie MVC):**
    *   Narzêdzia: Postman, RestSharp, HttpClient w testach integracyjnych.
*   **Zarz¹dzanie Testami i B³êdami:**
    *   Narzêdzia: Jira, Azure DevOps, TestRail, lub prostsze rozwi¹zania jak arkusze kalkulacyjne (zale¿nie od skali projektu i zespo³u).
*   **Kontrola Wersji:**
    *   Git (zgodnie z repozytorium).

## 7. Harmonogram testów

Harmonogram testów bêdzie powi¹zany z cyklem rozwoju aplikacji (np. sprintami w metodyce Agile).
*   **Testy jednostkowe i integracyjne:** Pisane na bie¿¹co przez deweloperów i QA w trakcie implementacji nowych funkcjonalnoœci.
*   **Testy funkcjonalne i UI:** Wykonywane po zakoñczeniu implementacji wiêkszych modu³ów lub przed wydaniem nowej wersji.
*   **Testy regresji:** Przed ka¿dym wydaniem, aby upewniæ siê, ¿e nowe zmiany nie zepsu³y istniej¹cych funkcjonalnoœci.
*   **Testy akceptacyjne (UAT):** Po zakoñczeniu fazy testów QA, przed wdro¿eniem na produkcjê.

Przyk³adowy podzia³ czasowy dla jednego cyklu/wydania:
*   Faza rozwoju (wraz z testami jednostkowymi i integracyjnymi): X dni/tygodni.
*   Faza testów systemowych (funkcjonalne, UI, bezpieczeñstwa, kompatybilnoœci): Y dni.
*   Faza testów UAT: Z dni.
*   Poprawki b³êdów i retesty: Wplecione w powy¿sze fazy lub jako oddzielna iteracja.

## 8. Kryteria akceptacji testów

### 8.1. Kryteria wejœcia (rozpoczêcia testów)
*   Dostêpna stabilna wersja aplikacji na œrodowisku testowym.
*   Ukoñczona dokumentacja wymagañ (PRD) i specyfikacja techniczna dla testowanych funkcjonalnoœci.
*   Przygotowane œrodowisko testowe i dane testowe.
*   Zakoñczone testy jednostkowe i integracyjne (lub osi¹gniêty zdefiniowany poziom pokrycia).

### 8.2. Kryteria wyjœcia (zakoñczenia testów)
*   Wykonane wszystkie zaplanowane scenariusze testowe dla danego cyklu.
*   Osi¹gniêty zdefiniowany poziom pokrycia testami (np. 95% krytycznych scenariuszy, 80% pozosta³ych).
*   Brak nierozwi¹zanych b³êdów krytycznych i wysokich.
*   Liczba b³êdów œrednich i niskich nie przekracza ustalonego progu.
*   Przygotowany i zaakceptowany raport z testów.
*   Akceptacja UAT (jeœli dotyczy).

## 9. Role i odpowiedzialnoœci w procesie testowania

*   **In¿ynier QA/Tester:**
    *   Tworzenie i aktualizacja planu testów.
    *   Projektowanie i wykonywanie scenariuszy testowych.
    *   Raportowanie i œledzenie b³êdów.
    *   Przygotowywanie raportów z testów.
    *   Automatyzacja testów (jeœli w zakresie).
    *   Wspó³praca z deweloperami i Product Ownerem.
*   **Deweloperzy:**
    *   Pisanie testów jednostkowych i integracyjnych.
    *   Poprawianie zg³oszonych b³êdów.
    *   Wsparcie dla zespo³u QA w diagnozowaniu problemów.
*   **Product Owner/Analityk Biznesowy:**
    *   Definiowanie wymagañ i kryteriów akceptacji.
    *   Udzia³ w testach UAT.
    *   Priorytetyzacja b³êdów.
*   **DevOps/Administrator Systemu (jeœli dotyczy):**
    *   Przygotowanie i utrzymanie œrodowisk testowych.
    *   Wsparcie w konfiguracji narzêdzi.

## 10. Procedury raportowania b³êdów

Ka¿dy wykryty b³¹d powinien byæ zaraportowany w systemie œledzenia b³êdów i zawieraæ nastêpuj¹ce informacje:
*   **ID B³êdu:** Unikalny identyfikator.
*   **Tytu³:** Krótki, zwiêz³y opis problemu.
*   **Œrodowisko:** Wersja aplikacji, przegl¹darka, system operacyjny, na którym wyst¹pi³ b³¹d.
*   **Kroki do odtworzenia:** Szczegó³owa lista kroków pozwalaj¹ca na jednoznaczne odtworzenie b³êdu.
*   **Obecny rezultat:** Opis tego, co siê sta³o.
*   **Oczekiwany rezultat:** Opis tego, co powinno siê staæ.
*   **Priorytet:** (np. Krytyczny, Wysoki, Œredni, Niski) - okreœlaj¹cy wp³yw b³êdu na dzia³anie aplikacji.
*   **Stopieñ trudnoœci (Severity):** (np. Blokuj¹cy, Powa¿ny, Drobny, Kosmetyczny) - okreœlaj¹cy techniczny wp³yw b³êdu.
*   **Za³¹czniki:** Zrzuty ekranu, logi, filmy ilustruj¹ce b³¹d.
*   **Osoba zg³aszaj¹ca:** Imiê i nazwisko osoby, która wykry³a b³¹d.
*   **Data zg³oszenia.**

Cykl ¿ycia b³êdu:
1.  **Nowy (New/Open):** B³¹d zosta³ zg³oszony.
2.  **W Analizie (In Analysis/Assigned):** B³¹d jest analizowany przez dewelopera.
3.  **Do Poprawy (To Be Fixed/Reopened):** B³¹d zosta³ zaakceptowany i czeka na poprawê lub zosta³ ponownie otwarty po nieudanej weryfikacji.
4.  **W Trakcie Poprawy (In Progress):** Deweloper pracuje nad poprawk¹.
5.  **Rozwi¹zany (Resolved/Fixed):** B³¹d zosta³ poprawiony i czeka na weryfikacjê przez QA.
6.  **W Weryfikacji (In Test/Verifying):** QA weryfikuje poprawkê.
7.  **Zamkniêty (Closed):** Poprawka zosta³a zweryfikowana pomyœlnie.
8.  **Odrzucony (Rejected/Won't Fix):** B³¹d nie zostanie poprawiony (np. nie jest b³êdem, duplikat, niski priorytet).