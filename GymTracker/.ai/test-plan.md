# Kompleksowy Plan Test�w dla Projektu GymTracker

## 1. Wprowadzenie i cele testowania

### 1.1. Wprowadzenie
Niniejszy dokument przedstawia kompleksowy plan test�w dla aplikacji internetowej GymTracker. GymTracker jest aplikacj� umo�liwiaj�c� u�ytkownikom �ledzenie post�p�w treningowych, rejestrowanie wynik�w oraz zarz�dzanie planem treningowym. Projekt jest realizowany w technologii .NET 8, ASP.NET Core Razor Pages z wykorzystaniem C# i Entity Framework Core.

### 1.2. Cele testowania
G��wnym celem test�w jest zapewnienie wysokiej jako�ci aplikacji GymTracker poprzez:
*   Weryfikacj� zgodno�ci zdefiniowanych wymaga� funkcjonalnych i niefunkcjonalnych.
*   Wykrycie i zaraportowanie defekt�w.
*   Ocena stabilno�ci, wydajno�ci i bezpiecze�stwa aplikacji.
*   Zapewnienie pozytywnego do�wiadczenia u�ytkownika (UX).
*   Minimalizacja ryzyka wyst�pienia b��d�w na �rodowisku produkcyjnym.

## 2. Zakres test�w

### 2.1. Funkcjonalno�ci obj�te testami
Testom podlega� b�d� wszystkie kluczowe modu�y i funkcjonalno�ci aplikacji, w tym:
*   **Zarz�dzanie Kontem U�ytkownika:**
    *   Rejestracja nowych u�ytkownik�w.
    *   Logowanie i wylogowywanie.
    *   Obs�uga b��dnych danych logowania/rejestracji.
*   **Zarz�dzanie �wiczeniami:**
    *   Tworzenie nowych �wicze� z walidacj� (unikalno�� nazwy per u�ytkownik, d�ugo�� nazwy).
    *   Wy�wietlanie listy �wicze� u�ytkownika (tylko niezablokowane).
    *   Edycja istniej�cych �wicze�.
    *   Blokowanie �wicze� (zablokowane �wiczenia nie s� dost�pne do wyboru w nowych treningach, ale widoczne w historii).
*   **Zarz�dzanie Treningami:**
    *   Tworzenie nowych trening�w (wyb�r daty, dodawanie �wicze�, okre�lanie serii, powt�rze�, wagi).
    *   Wy�wietlanie historii trening�w z paginacj� i filtrowaniem po zakresie dat.
    *   Wy�wietlanie szczeg��w pojedynczego treningu.
    *   Usuwanie trening�w.
*   **Walidacja Danych:**
    *   Walidacja po stronie klienta (JavaScript) i serwera (DataAnnotations).
*   **Autoryzacja i Bezpiecze�stwo:**
    *   Dost�p do funkcjonalno�ci tylko dla zalogowanych u�ytkownik�w (z wyj�tkiem strony g��wnej, logowania, rejestracji).
    *   Zabezpieczenie przed CSRF.
    *   Ochrona danych u�ytkownika.
*   **Interfejs U�ytkownika (UI) i Do�wiadczenie U�ytkownika (UX):**
    *   Poprawno�� wy�wietlania na r�nych przegl�darkach (zgodnie z przyj�tymi standardami).
    *   Intuicyjno�� nawigacji i obs�ugi.
    *   Responsywno�� (je�li jest w zakresie).

### 2.2. Funkcjonalno�ci wy��czone z test�w (Out of Scope)
Na obecnym etapie rozwoju, nast�puj�ce funkcjonalno�ci s� wy��czone z zakresu test�w:
*   Generowanie zaawansowanych raport�w i statystyk (poza prostym wy�wietlaniem historii).
*   Import/eksport danych.
*   Funkcjonalno�ci administracyjne (je�li nie zosta�y zdefiniowane).
*   Integracje z systemami zewn�trznymi (je�li nie dotyczy).
*   Testy wydajno�ciowe pod bardzo du�ym obci��eniem (chyba �e zdefiniowano inaczej).
*   Zaawansowane testy penetracyjne (poza podstawowymi testami bezpiecze�stwa).

## 3. Typy test�w do przeprowadzenia

*   **Testy Jednostkowe (Unit Tests):**
    *   Cel: Weryfikacja poprawno�ci dzia�ania poszczeg�lnych komponent�w (metod w serwisach, logiki w kontrolerach/PageModels) w izolacji.
    *   Zakres: Logika biznesowa w `AccountService`, `ExerciseService`, `WorkoutService`. Metody pomocnicze.
*   **Testy Integracyjne (Integration Tests):**
    *   Cel: Weryfikacja interakcji pomi�dzy komponentami systemu, np. kontroler-serwis-baza danych.
    *   Zakres: Poprawno�� dzia�ania endpoint�w API/akcji kontroler�w, integracja z Entity Framework Core, operacje CRUD na bazie danych, dzia�anie middleware.
*   **Testy Funkcjonalne (E2E - End-to-End):**
    *   Cel: Weryfikacja dzia�ania aplikacji z perspektywy u�ytkownika ko�cowego, symulowanie rzeczywistych scenariuszy u�ycia.
    *   Zakres: Pe�ne przep�ywy u�ytkownika, np. rejestracja -> logowanie -> dodanie �wiczenia -> dodanie treningu -> przegl�danie historii.
*   **Testy Interfejsu U�ytkownika (UI Tests):**
    *   Cel: Weryfikacja poprawno�ci wy�wietlania i dzia�ania element�w UI, responsywno�ci, zgodno�ci z projektem graficznym.
    *   Zakres: Wygl�d i dzia�anie formularzy, tabel, przycisk�w, nawigacji.
*   **Testy Walidacyjne:**
    *   Cel: Sprawdzenie poprawno�ci mechanizm�w walidacji danych wej�ciowych (po stronie klienta i serwera).
    *   Zakres: Walidacja formularzy rejestracji, logowania, tworzenia/edycji �wicze� i trening�w.
*   **Testy Bezpiecze�stwa (Security Tests):**
    *   Cel: Identyfikacja potencjalnych luk bezpiecze�stwa.
    *   Zakres: Kontrola dost�pu (autoryzacja), ochrona przed CSRF, walidacja danych wej�ciowych pod k�tem XSS (cho� Razor Pages domy�lnie enkoduje), bezpiecze�stwo sesji/ciasteczek.
*   **Testy U�yteczno�ci (Usability Tests):**
    *   Cel: Ocena �atwo�ci obs�ugi i intuicyjno�ci interfejsu.
    *   Zakres: Nawigacja, przejrzysto�� komunikat�w, og�lne do�wiadczenie u�ytkownika.
*   **Testy Kompatybilno�ci (Compatibility Tests):**
    *   Cel: Sprawdzenie dzia�ania aplikacji na r�nych przegl�darkach i urz�dzeniach.
    *   Zakres: G��wne przegl�darki (Chrome, Firefox, Edge - najnowsze wersje).
*   **Testy Akceptacyjne U�ytkownika (UAT):**
    *   Cel: Potwierdzenie przez klienta/product ownera, �e aplikacja spe�nia wymagania biznesowe.
    *   Zakres: Kluczowe scenariusze biznesowe.

## 4. Scenariusze testowe dla kluczowych funkcjonalno�ci

Poni�ej znajduj� si� przyk�adowe scenariusze testowe. Pe�na lista b�dzie rozwijana w trakcie trwania projektu.

### 4.1. Zarz�dzanie Kontem U�ytkownika

| ID Scenariusza | Opis                                                                 | Kroki Testowe                                                                                                                               | Oczekiwany Rezultat                                                                                                                               | Priorytet |
| :------------- | :------------------------------------------------------------------- | :------------------------------------------------------------------------------------------------------------------------------------------ | :------------------------------------------------------------------------------------------------------------------------------------------------ | :-------- |
| TC_ACC_001     | Pomy�lna rejestracja nowego u�ytkownika                               | 1. Przejd� do /Account/Register. 2. Wype�nij formularz poprawnymi danymi (unikalny email, has�o spe�niaj�ce kryteria). 3. Kliknij "Zarejestruj si�". | U�ytkownik zostaje zarejestrowany, zalogowany i przekierowany na stron� g��wn�. Wy�wietlony komunikat o sukcesie.                                  | Krytyczny |
| TC_ACC_002     | Rejestracja z istniej�cym adresem email                               | 1. Przejd� do /Account/Register. 2. Wprowad� email, kt�ry ju� istnieje w systemie. 3. Wprowad� has�o. 4. Kliknij "Zarejestruj si�".         | Wy�wietlony b��d informuj�cy o zaj�tym adresie email. U�ytkownik nie zostaje zarejestrowany.                                                        | Wysoki    |
| TC_ACC_003     | Rejestracja z niepoprawnym formatem emaila                            | 1. Przejd� do /Account/Register. 2. Wprowad� email w niepoprawnym formacie. 3. Wprowad� has�o. 4. Kliknij "Zarejestruj si�".             | Wy�wietlony b��d walidacji formatu emaila. U�ytkownik nie zostaje zarejestrowany.                                                                  | Wysoki    |
| TC_ACC_004     | Rejestracja z za kr�tkim has�em                                      | 1. Przejd� do /Account/Register. 2. Wprowad� poprawny email. 3. Wprowad� has�o kr�tsze ni� 6 znak�w. 4. Kliknij "Zarejestruj si�".          | Wy�wietlony b��d walidacji d�ugo�ci has�a. U�ytkownik nie zostaje zarejestrowany.                                                                  | Wysoki    |
| TC_ACC_005     | Pomy�lne logowanie                                                   | 1. Przejd� do /Account/Login. 2. Wprowad� email i has�o zarejestrowanego u�ytkownika. 3. Kliknij "Zaloguj si�".                            | U�ytkownik zostaje zalogowany i przekierowany na stron� g��wn�. Ciasteczko uwierzytelniaj�ce jest ustawione.                                     | Krytyczny |
| TC_ACC_006     | Logowanie z b��dnym has�em                                           | 1. Przejd� do /Account/Login. 2. Wprowad� poprawny email zarejestrowanego u�ytkownika. 3. Wprowad� b��dne has�o. 4. Kliknij "Zaloguj si�". | Wy�wietlony komunikat o b��dnych danych logowania. U�ytkownik nie zostaje zalogowany.                                                              | Wysoki    |
| TC_ACC_007     | Logowanie z nieistniej�cym emailem                                   | 1. Przejd� do /Account/Login. 2. Wprowad� email, kt�ry nie istnieje w systemie. 3. Wprowad� dowolne has�o. 4. Kliknij "Zaloguj si�".   | Wy�wietlony komunikat o b��dnych danych logowania. U�ytkownik nie zostaje zalogowany.                                                              | Wysoki    |
| TC_ACC_008     | Pomy�lne wylogowanie                                                 | 1. Zaloguj si� do aplikacji. 2. Kliknij przycisk/link "Wyloguj".                                                                            | U�ytkownik zostaje wylogowany i przekierowany na stron� g��wn� lub logowania. Chronione zasoby nie s� dost�pne. Ciasteczko uwierzytelniaj�ce usuni�te. | Krytyczny |

### 4.2. Zarz�dzanie �wiczeniami

| ID Scenariusza | Opis                                                                  | Kroki Testowe                                                                                                                                  | Oczekiwany Rezultat                                                                                                                              | Priorytet |
| :------------- | :-------------------------------------------------------------------- | :--------------------------------------------------------------------------------------------------------------------------------------------- | :----------------------------------------------------------------------------------------------------------------------------------------------- | :-------- |
| TC_EX_001      | Dodanie nowego �wiczenia z poprawnymi danymi                           | 1. Zaloguj si�. 2. Przejd� do /Exercises/Create. 3. Wype�nij formularz (Nazwa, Grupa mi�niowa, Poziom trudno�ci, Opis). 4. Kliknij "Dodaj �wiczenie". | �wiczenie zostaje dodane do listy. Komunikat o sukcesie. Nazwa unikalna dla u�ytkownika, d�ugo�� 3-150.                                          | Krytyczny |
| TC_EX_002      | Pr�ba dodania �wiczenia z nazw� kr�tsz� ni� 3 znaki                   | 1. Zaloguj si�. 2. Przejd� do /Exercises/Create. 3. Wprowad� nazw� "AB". 4. Wype�nij reszt� p�l. 5. Kliknij "Dodaj �wiczenie".                      | Wy�wietlony b��d walidacji d�ugo�ci nazwy. �wiczenie nie zostaje dodane.                                                                         | Wysoki    |
| TC_EX_003      | Pr�ba dodania �wiczenia z nazw� d�u�sz� ni� 150 znak�w                 | 1. Zaloguj si�. 2. Przejd� do /Exercises/Create. 3. Wprowad� nazw� >150 znak�w. 4. Wype�nij reszt� p�l. 5. Kliknij "Dodaj �wiczenie".             | Wy�wietlony b��d walidacji d�ugo�ci nazwy. �wiczenie nie zostaje dodane.                                                                         | Wysoki    |
| TC_EX_004      | Pr�ba dodania �wiczenia o tej samej nazwie przez tego samego u�ytkownika | 1. Zaloguj si�. 2. Dodaj �wiczenie "Wyciskanie". 3. Przejd� do /Exercises/Create. 4. Wprowad� nazw� "Wyciskanie". 5. Kliknij "Dodaj �wiczenie".  | Wy�wietlony b��d o duplikacji nazwy. �wiczenie nie zostaje dodane.                                                                               | Wysoki    |
| TC_EX_005      | Edycja istniej�cego �wiczenia                                          | 1. Zaloguj si�. 2. Przejd� do listy �wicze�. 3. Wybierz �wiczenie i kliknij "Edytuj". 4. Zmie� dane. 5. Kliknij "Zapisz zmiany".                   | Dane �wiczenia zostaj� zaktualizowane. Komunikat o sukcesie. Zmiany widoczne w historii trening�w.                                              | Wysoki    |
| TC_EX_006      | Blokowanie �wiczenia                                                  | 1. Zaloguj si�. 2. Przejd� do listy �wicze�. 3. Wybierz �wiczenie i kliknij "Blokuj".                                                           | �wiczenie oznaczone jako zablokowane. Nie pojawia si� na li�cie wyboru przy tworzeniu nowego treningu. Komunikat o sukcesie.                       | Wysoki    |
| TC_EX_007      | Wy�wietlanie listy �wicze�                                            | 1. Zaloguj si�. 2. Przejd� do /Exercises/Index.                                                                                                | Wy�wietlona lista niezablokowanych �wicze� nale��cych do u�ytkownika. Dost�pne opcje "Edytuj", "Blokuj".                                          | Krytyczny |

### 4.3. Zarz�dzanie Treningami

| ID Scenariusza | Opis                                                                  | Kroki Testowe                                                                                                                                                                                          | Oczekiwany Rezultat                                                                                                                                                              | Priorytet |
| :------------- | :-------------------------------------------------------------------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | :-------- |
| TC_WO_001      | Dodanie nowego treningu z poprawnymi danymi                            | 1. Zaloguj si�. 2. Przejd� do /Workouts/Create. 3. Wybierz dat�. 4. Dodaj co najmniej jedno �wiczenie (wybrane z listy niezablokowanych), podaj Serie, Powt�rzenia, opcjonalnie Wag�. 5. Kliknij "Zapisz trening". | Trening zostaje zapisany. Komunikat o sukcesie. U�ytkownik przekierowany do historii trening�w.                                                                                   | Krytyczny |
| TC_WO_002      | Pr�ba dodania treningu bez �wicze�                                      | 1. Zaloguj si�. 2. Przejd� do /Workouts/Create. 3. Wybierz dat�. 4. Nie dodawaj �adnych �wicze�. 5. Kliknij "Zapisz trening".                                                                             | Wy�wietlony b��d walidacji informuj�cy o konieczno�ci dodania �wicze�. Trening nie zostaje zapisany.                                                                               | Wysoki    |
| TC_WO_003      | Pr�ba dodania zablokowanego �wiczenia do treningu                     | 1. Zaloguj si�. 2. Zablokuj jedno ze swoich �wicze�. 3. Przejd� do /Workouts/Create. 4. Sprawd� list� dost�pnych �wicze�.                                                                                 | Zablokowane �wiczenie nie jest widoczne na li�cie wyboru �wicze�.                                                                                                                  | Wysoki    |
| TC_WO_004      | Wy�wietlanie historii trening�w (paginacja, filtrowanie)               | 1. Zaloguj si�. 2. Przejd� do /Workouts/History. 3. Sprawd� wy�wietlanie listy trening�w. 4. Przetestuj paginacj�. 5. Przetestuj filtrowanie po zakresie dat.                                                | Lista trening�w wy�wietlana poprawnie. Paginacja dzia�a. Filtrowanie po datach zwraca poprawne wyniki.                                                                               | Krytyczny |
| TC_WO_005      | Wy�wietlanie szczeg��w treningu                                       | 1. Zaloguj si�. 2. Przejd� do /Workouts/History. 3. Kliknij "Szczeg�y" przy wybranym treningu.                                                                                                         | Wy�wietlone szczeg�y treningu: data, notatki, lista �wicze� z seriami, powt�rzeniami, wag�. Dane tylko do odczytu.                                                              | Wysoki    |
| TC_WO_006      | Usuwanie treningu                                                     | 1. Zaloguj si�. 2. Przejd� do /Workouts/History. 3. Wybierz trening i kliknij "Usu�". Potwierd� usuni�cie.                                                                                               | Trening zostaje usuni�ty z historii. Komunikat o sukcesie.                                                                                                                        | Wysoki    |
| TC_WO_007      | Propagacja edycji nazwy �wiczenia w historii trening�w                 | 1. Zaloguj si�. 2. Utw�rz trening z �wiczeniem X. 3. Edytuj �wiczenie X, zmieniaj�c jego nazw� na Y. 4. Przejd� do /Workouts/History i wy�wietl szczeg�y treningu z kroku 2.                            | W szczeg�ach treningu widoczna jest nowa nazwa �wiczenia Y.                                                                                                                      | Wysoki    |

## 5. �rodowisko testowe

*   **�rodowisko deweloperskie lokalne:**
    *   System operacyjny: Windows/Linux/macOS (zgodnie z konfiguracj� deweloper�w).
    *   Serwer WWW: IIS Express / Kestrel.
    *   Baza danych: SQL Server Express LocalDB lub dedykowana instancja deweloperska SQL Server.
    *   Przegl�darki: Najnowsze wersje Chrome, Firefox.
*   **�rodowisko testowe (Staging/QA):**
    *   System operacyjny: Zbli�ony do produkcyjnego (np. Windows Server, Linux).
    *   Serwer WWW: IIS / Kestrel (zale�nie od konfiguracji produkcyjnej).
    *   Baza danych: Dedykowana instancja SQL Server dla �rodowiska QA, z danymi testowymi zbli�onymi do produkcyjnych (zanonimizowane, je�li konieczne).
    *   Konfiguracja: Zbli�ona do produkcyjnej (np. ustawienia logowania, HSTS).
*   **Dane Testowe:**
    *   Przygotowany zestaw danych u�ytkownik�w, �wicze� i trening�w, pokrywaj�cy r�ne scenariusze.
    *   Skrypty do generowania/przywracania danych testowych.

## 6. Narz�dzia do testowania

*   **Testy Jednostkowe:**
    *   Framework: MSTest, NUnit lub xUnit (zale�nie od preferencji zespo�u, `.csproj` wskazuje na mo�liwo�� u�ycia `dotnet test`, co jest standardem).
    *   Mockowanie: Moq, NSubstitute.
*   **Testy Integracyjne:**
    *   Framework: Ten sam co dla test�w jednostkowych, z wykorzystaniem `WebApplicationFactory` dla testowania endpoint�w.
    *   Baza danych: EF Core InMemory Provider, SQLite in-memory, lub dedykowana baza testowa.
*   **Testy Funkcjonalne (E2E) i UI:**
    *   Narz�dzia: Selenium WebDriver (z C# bindings), Playwright, Cypress.
*   **Testy API (je�li powstan� dedykowane API, na razie MVC):**
    *   Narz�dzia: Postman, RestSharp, HttpClient w testach integracyjnych.
*   **Zarz�dzanie Testami i B��dami:**
    *   Narz�dzia: Jira, Azure DevOps, TestRail, lub prostsze rozwi�zania jak arkusze kalkulacyjne (zale�nie od skali projektu i zespo�u).
*   **Kontrola Wersji:**
    *   Git (zgodnie z repozytorium).

## 7. Harmonogram test�w

Harmonogram test�w b�dzie powi�zany z cyklem rozwoju aplikacji (np. sprintami w metodyce Agile).
*   **Testy jednostkowe i integracyjne:** Pisane na bie��co przez deweloper�w i QA w trakcie implementacji nowych funkcjonalno�ci.
*   **Testy funkcjonalne i UI:** Wykonywane po zako�czeniu implementacji wi�kszych modu��w lub przed wydaniem nowej wersji.
*   **Testy regresji:** Przed ka�dym wydaniem, aby upewni� si�, �e nowe zmiany nie zepsu�y istniej�cych funkcjonalno�ci.
*   **Testy akceptacyjne (UAT):** Po zako�czeniu fazy test�w QA, przed wdro�eniem na produkcj�.

Przyk�adowy podzia� czasowy dla jednego cyklu/wydania:
*   Faza rozwoju (wraz z testami jednostkowymi i integracyjnymi): X dni/tygodni.
*   Faza test�w systemowych (funkcjonalne, UI, bezpiecze�stwa, kompatybilno�ci): Y dni.
*   Faza test�w UAT: Z dni.
*   Poprawki b��d�w i retesty: Wplecione w powy�sze fazy lub jako oddzielna iteracja.

## 8. Kryteria akceptacji test�w

### 8.1. Kryteria wej�cia (rozpocz�cia test�w)
*   Dost�pna stabilna wersja aplikacji na �rodowisku testowym.
*   Uko�czona dokumentacja wymaga� (PRD) i specyfikacja techniczna dla testowanych funkcjonalno�ci.
*   Przygotowane �rodowisko testowe i dane testowe.
*   Zako�czone testy jednostkowe i integracyjne (lub osi�gni�ty zdefiniowany poziom pokrycia).

### 8.2. Kryteria wyj�cia (zako�czenia test�w)
*   Wykonane wszystkie zaplanowane scenariusze testowe dla danego cyklu.
*   Osi�gni�ty zdefiniowany poziom pokrycia testami (np. 95% krytycznych scenariuszy, 80% pozosta�ych).
*   Brak nierozwi�zanych b��d�w krytycznych i wysokich.
*   Liczba b��d�w �rednich i niskich nie przekracza ustalonego progu.
*   Przygotowany i zaakceptowany raport z test�w.
*   Akceptacja UAT (je�li dotyczy).

## 9. Role i odpowiedzialno�ci w procesie testowania

*   **In�ynier QA/Tester:**
    *   Tworzenie i aktualizacja planu test�w.
    *   Projektowanie i wykonywanie scenariuszy testowych.
    *   Raportowanie i �ledzenie b��d�w.
    *   Przygotowywanie raport�w z test�w.
    *   Automatyzacja test�w (je�li w zakresie).
    *   Wsp�praca z deweloperami i Product Ownerem.
*   **Deweloperzy:**
    *   Pisanie test�w jednostkowych i integracyjnych.
    *   Poprawianie zg�oszonych b��d�w.
    *   Wsparcie dla zespo�u QA w diagnozowaniu problem�w.
*   **Product Owner/Analityk Biznesowy:**
    *   Definiowanie wymaga� i kryteri�w akceptacji.
    *   Udzia� w testach UAT.
    *   Priorytetyzacja b��d�w.
*   **DevOps/Administrator Systemu (je�li dotyczy):**
    *   Przygotowanie i utrzymanie �rodowisk testowych.
    *   Wsparcie w konfiguracji narz�dzi.

## 10. Procedury raportowania b��d�w

Ka�dy wykryty b��d powinien by� zaraportowany w systemie �ledzenia b��d�w i zawiera� nast�puj�ce informacje:
*   **ID B��du:** Unikalny identyfikator.
*   **Tytu�:** Kr�tki, zwi�z�y opis problemu.
*   **�rodowisko:** Wersja aplikacji, przegl�darka, system operacyjny, na kt�rym wyst�pi� b��d.
*   **Kroki do odtworzenia:** Szczeg�owa lista krok�w pozwalaj�ca na jednoznaczne odtworzenie b��du.
*   **Obecny rezultat:** Opis tego, co si� sta�o.
*   **Oczekiwany rezultat:** Opis tego, co powinno si� sta�.
*   **Priorytet:** (np. Krytyczny, Wysoki, �redni, Niski) - okre�laj�cy wp�yw b��du na dzia�anie aplikacji.
*   **Stopie� trudno�ci (Severity):** (np. Blokuj�cy, Powa�ny, Drobny, Kosmetyczny) - okre�laj�cy techniczny wp�yw b��du.
*   **Za��czniki:** Zrzuty ekranu, logi, filmy ilustruj�ce b��d.
*   **Osoba zg�aszaj�ca:** Imi� i nazwisko osoby, kt�ra wykry�a b��d.
*   **Data zg�oszenia.**

Cykl �ycia b��du:
1.  **Nowy (New/Open):** B��d zosta� zg�oszony.
2.  **W Analizie (In Analysis/Assigned):** B��d jest analizowany przez dewelopera.
3.  **Do Poprawy (To Be Fixed/Reopened):** B��d zosta� zaakceptowany i czeka na popraw� lub zosta� ponownie otwarty po nieudanej weryfikacji.
4.  **W Trakcie Poprawy (In Progress):** Deweloper pracuje nad poprawk�.
5.  **Rozwi�zany (Resolved/Fixed):** B��d zosta� poprawiony i czeka na weryfikacj� przez QA.
6.  **W Weryfikacji (In Test/Verifying):** QA weryfikuje poprawk�.
7.  **Zamkni�ty (Closed):** Poprawka zosta�a zweryfikowana pomy�lnie.
8.  **Odrzucony (Rejected/Won't Fix):** B��d nie zostanie poprawiony (np. nie jest b��dem, duplikat, niski priorytet).