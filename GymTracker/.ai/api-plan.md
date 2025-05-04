# REST API Plan

## 1. Zasoby
- **Users** (tabela Users)
- **Exercises** (tabela Exercises)
- **Workouts** (tabela Workouts)
- **WorkoutExercises** (tabela WorkoutExercises – relacja miêdzy Workouts a Exercises)

## 2. Punkty koñcowe

### A. Konto u¿ytkownika (Account)
#### 1. Rejestracja
- **Metoda HTTP:** GET (wyœwietlenie formularza), POST (przyjmowanie danych)
- **Œcie¿ka URL:** /Account/Register
- **Opis:** Wyœwietlenie formularza rejestracji oraz przetwarzanie danych rejestracyjnych.
- **£adunek ¿¹dania:**
- **£adunek odpowiedzi:** Widok strony potwierdzaj¹cej rejestracjê lub b³¹d walidacji.
- **Kody powodzenia:**  
  - 200 OK – formularz lub potwierdzenie rejestracji
  - 400 Bad Request – b³êdne dane wejœciowe

#### 2. Logowanie
- **Metoda HTTP:** GET (formularz logowania), POST (przyjmowanie danych)
- **Œcie¿ka URL:** /Account/Login
- **Opis:** Wyœwietlenie formularza logowania oraz przetwarzanie danych logowania. Po pomyœlnym logowaniu zwracany jest token JWT.
- **£adunek ¿¹dania:**
- **£adunek odpowiedzi:** Widok strony g³ównej lub komunikat o b³êdzie wraz z tokenem JWT po poprawnym logowaniu.
- **Kody powodzenia:**  
  - 200 OK – widok g³ówny z tokenem
  - 401 Unauthorized – niepoprawne dane logowania

### B. Æwiczenia (Exercises)
#### 1. Dodawanie æwiczenia
- **Metoda HTTP:** GET (formularz dodawania), POST (przyjmowanie danych)
- **Œcie¿ka URL:** /Exercises/Create
- **Opis:** Umo¿liwia dodanie nowego æwiczenia z walidacj¹ unikalnoœci w obrêbie u¿ytkownika i d³ugoœci nazwy (3-150 znaków).
- **£adunek ¿¹dania:**
- **£adunek odpowiedzi:** Widok potwierdzenia dodania lub komunikat o b³êdzie walidacji.
- **Kody powodzenia:**  
  - 200 OK – æwiczenie dodane pomyœlnie
  - 400 Bad Request – b³¹d walidacji (np. nazwa zbyt krótka/d³uga, nieunikalna)

#### 2. Edycja æwiczenia
- **Metoda HTTP:** GET (formularz edycji), POST (przyjmowanie zaktualizowanych danych)
- **Œcie¿ka URL:** /Exercises/Edit/{id}
- **Opis:** Umo¿liwia edycjê danych æwiczenia. Zmiany s¹ propagowane do historycznych rekordów treningowych.
- **£adunek ¿¹dania:**
- **£adunek odpowiedzi:** Widok potwierdzenia aktualizacji lub komunikat o b³êdzie.
- **Kody powodzenia:**  
  - 200 OK – edycja zakoñczona sukcesem
  - 400 Bad Request – b³¹d walidacji

#### 3. Blokowanie æwiczenia
- **Metoda HTTP:** POST
- **Œcie¿ka URL:** /Exercises/Block/{id}
- **Opis:** Oznacza wybrane æwiczenie jako zablokowane, uniemo¿liwiaj¹c jego u¿ycie w nowych treningach, ale pozostawiaj¹c widocznoœæ w historii.
- **£adunek ¿¹dania:** brak lub minimalny identyfikator
- **£adunek odpowiedzi:** Widok lub przekierowanie z komunikatem o powodzeniu operacji.
- **Kody powodzenia:**  
  - 200 OK – æwiczenie zablokowane
  - 400 Bad Request – problem z operacj¹

### C. Treningi (Workouts)
#### 1. Dodawanie treningu
- **Metoda HTTP:** GET (formularz dodawania), POST (przyjmowanie danych)
- **Œcie¿ka URL:** /Workouts/Create
- **Opis:** Umo¿liwia dodanie nowego treningu z dat¹ i list¹ æwiczeñ (z informacjami o seriach, powtórzeniach, opcjonalnie ciê¿arze).
- **£adunek ¿¹dania:**
- **£adunek odpowiedzi:** Widok potwierdzaj¹cy zapis treningu lub komunikat o b³êdzie.
- **Kody powodzenia:**  
  - 200 OK – trening dodany pomyœlnie
  - 400 Bad Request – b³¹d danych wejœciowych

#### 2. Przegl¹danie historii treningów
- **Metoda HTTP:** GET
- **Œcie¿ka URL:** /Workouts/History
- **Opis:** Wyœwietla listê zapisanych treningów u¿ytkownika w trybie tylko do odczytu. Mo¿liwe filtrowanie oraz paginacja.
- **Parametry zapytania:**  
  - page (int): numer strony
  - pageSize (int): liczba rekordów na stronê
  - dateFrom, dateTo (YYYY-MM-DD): zakres dat
- **£adunek odpowiedzi:**
- **Kody powodzenia:**  
  - 200 OK – lista treningów zwrócona pomyœlnie

## 3. Uwierzytelnianie i autoryzacja
- Mechanizm: JWT (JSON Web Tokens)
- **Opis:** Wszystkie operacje modyfikuj¹ce dane (tworzenie, edycja, blokowanie) wymagaj¹ autoryzacji. U¿ytkownik musi byæ zalogowany, a token JWT przesy³any jest w nag³ówku ¿¹dania. Endpointy zwracaj¹ce widoki (m.in. lista treningów, formularze) wykorzystuj¹ standardowe mechanizmy autoryzacji (np. [Authorize] w ASP.NET Core).
- **Wymagania wejœciowe:**  
  - Ka¿de ¿¹danie modyfikuj¹ce zasoby musi zawieraæ poprawny token w nag³ówku "Authorization: Bearer <token>".
  - ¯¹dania bez wa¿nego tokena powinny zwracaæ 401 Unauthorized.

## 4. Walidacja i logika biznesowa
- **Walidacja:**
  - Æwiczenia: Nazwa musi mieæ d³ugoœæ od 3 do 150 znaków oraz byæ unikalna w obrêbie u¿ytkownika (zgodnie z CHECK CONSTRAINT i unikalnym indeksem).
  - Treningi: Daty musz¹ byæ poprawnym formatem, a lista æwiczeñ nie mo¿e byæ pusta.
- **Logika biznesowa:**
  - Rejestracja i logowanie: Sprawdzanie poprawnoœci danych u¿ytkownika, generowanie tokenu JWT.
  - Edycja æwiczenia: Zmiany musz¹ byæ propagowane do historycznych rekordów – logika nak³adana na modyfikacjê rekordu oraz odczyt danych z tabeli WorkoutExercises.
  - Blokowanie æwiczeñ: Æwiczenie oznaczone jako zablokowane nie jest wyœwietlane podczas tworzenia nowych treningów, jednak pozostaje widoczne w historii.
  - Dodawanie treningu: Podczas tworzenia treningu, API weryfikuje czy u¿yte æwiczenie nie jest zablokowane (sprawdzanie w³aœciwoœci IsBlocked) oraz aplikuje walidacjê dotycz¹c¹ poprawnoœci liczby serii, powtórzeñ i opcjonalnego ciê¿aru.

## Za³o¿enia
- Projekt oparty jest o ASP.NET Core MVC, gdzie endpointy zwracaj¹ widoki. Operacje zmieniaj¹ce stan danych korzystaj¹ z metod POST.
- Obs³uga paginacji, filtrowania i sortowania jest realizowana na poziomie zapytañ do bazy danych (EF Core).
- Wykorzystanie JWT dla zabezpieczenia endpointów modyfikuj¹cych dane zapewnia, ¿e tylko autoryzowani u¿ytkownicy mog¹ wykonywaæ operacje CRUD.
