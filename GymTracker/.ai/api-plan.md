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
- **Opis:** Wyœwietlenie formularza rejestracji oraz przetwarzanie danych rejestracyjnych u¿ytkownika. Po poprawnej rejestracji wyœwietlany jest widok z potwierdzeniem lub komunikatem o b³êdzie walidacji.
- **£adunek ¿¹dania:**  
  Dane rejestracyjne (m.in. Email, Has³o)
- **£adunek odpowiedzi:** Widok strony potwierdzaj¹cej rejestracjê lub wyœwietlenie b³êdów walidacji.
- **Kody powodzenia:**  
  - 200 OK – formularz lub potwierdzenie rejestracji
  - 400 Bad Request – b³êdne dane wejœciowe

#### 2. Logowanie
- **Metoda HTTP:** GET (formularz logowania), POST (przyjmowanie danych)
- **Œcie¿ka URL:** /Account/Login
- **Opis:** Wyœwietlenie formularza logowania oraz przetwarzanie danych logowania. Po poprawnym logowaniu u¿ytkownik zostaje zalogowany przy u¿yciu mechanizmu Cookie Authentication i przekierowany do strony g³ównej.
- **£adunek ¿¹dania:**  
  Dane logowania (Email, Has³o)
- **£adunek odpowiedzi:** Widok strony g³ównej z komunikatem o pomyœlnym logowaniu lub komunikat o b³êdzie w przypadku niepoprawnych danych.
- **Kody powodzenia:**  
  - 200 OK – widok g³ówny po poprawnym logowaniu
  - 401 Unauthorized – niepoprawne dane logowania

### B. Æwiczenia (Exercises)
#### 1. Dodawanie æwiczenia
- **Metoda HTTP:** GET (formularz dodawania), POST (przyjmowanie danych)
- **Œcie¿ka URL:** /Exercises/Create
- **Opis:** Umo¿liwia dodanie nowego æwiczenia z walidacj¹ unikalnoœci w obrêbie u¿ytkownika oraz sprawdzeniem d³ugoœci nazwy (3-150 znaków). Po dodaniu wyœwietlany jest widok potwierdzaj¹cy.
- **£adunek ¿¹dania:**  
  Dane æwiczenia (nazwa, grupa miêœniowa, poziom trudnoœci, opis)
- **£adunek odpowiedzi:** Widok potwierdzaj¹cy dodanie æwiczenia lub wyœwietlenie komunikatu o b³êdzie walidacji.
- **Kody powodzenia:**  
  - 200 OK – æwiczenie dodane pomyœlnie
  - 400 Bad Request – b³¹d walidacji (np. nazwa zbyt krótka/d³uga, nieunikalna)

#### 2. Edycja æwiczenia
- **Metoda HTTP:** GET (formularz edycji), POST (przyjmowanie zaktualizowanych danych)
- **Œcie¿ka URL:** /Exercises/Edit/{id}
- **Opis:** Umo¿liwia edycjê danych æwiczenia. Wprowadzone zmiany s¹ propagowane do historycznych rekordów treningowych.
- **£adunek ¿¹dania:**  
  Zaktualizowane dane æwiczenia
- **£adunek odpowiedzi:** Widok potwierdzaj¹cy aktualizacjê lub komunikat o b³êdzie walidacji.
- **Kody powodzenia:**  
  - 200 OK – edycja zakoñczona sukcesem
  - 400 Bad Request – b³¹d walidacji

#### 3. Blokowanie æwiczenia
- **Metoda HTTP:** POST
- **Œcie¿ka URL:** /Exercises/Block/{id}
- **Opis:** Oznacza wybrane æwiczenie jako zablokowane, co uniemo¿liwia jego wykorzystanie w nowych treningach, lecz pozostawia jego widocznoœæ w historii.
- **£adunek ¿¹dania:** Minimalny identyfikator æwiczenia
- **£adunek odpowiedzi:** Widok lub przekierowanie z komunikatem o powodzeniu operacji.
- **Kody powodzenia:**  
  - 200 OK – æwiczenie zablokowane
  - 400 Bad Request – problem z operacj¹

#### 4. Wyœwietlanie æwiczeñ
- **Metoda HTTP:** GET
- **Œcie¿ka URL:** /Exercises
- **Opis:** Wyœwietlanie æwiczeñ u¿ytkownika w formie listy. Umo¿liwia przejœæ do dodawania æwiczenia, edycji lub blokowania. Z tej listy mo¿na przejœæ do edycji, blokowania lub dodawania æwiczenia.
- **£adunek ¿¹dania:** Brak
- **£adunek odpowiedzi:** Widok.
- **Kody powodzenia:**  
  - 200 OK – Lista æwiczeñ zwrócona pomyœlnie
  - 400 Bad Request – problem z operacj¹

### C. Treningi (Workouts)
#### 1. Dodawanie treningu
- **Metoda HTTP:** GET (formularz dodawania), POST (przyjmowanie danych)
- **Œcie¿ka URL:** /Workouts/Create
- **Opis:** Umo¿liwia dodanie nowego treningu z okreœlon¹ dat¹ oraz list¹ æwiczeñ (wraz z informacjami o seriach, powtórzeniach, a opcjonalnie ciê¿arze). Podczas tworzenia treningu sprawdzana jest walidacja danych, m.in. czy æwiczenie nie jest zablokowane.
- **£adunek ¿¹dania:**  
  Dane treningu (data, æwiczenia z detalami)
- **£adunek odpowiedzi:** Widok potwierdzaj¹cy zapis treningu lub komunikat o b³êdzie walidacji.
- **Kody powodzenia:**  
  - 200 OK – trening dodany pomyœlnie
  - 400 Bad Request – b³¹d danych wejœciowych

#### 2. Przegl¹danie historii treningów
- **Metoda HTTP:** GET
- **Œcie¿ka URL:** /Workouts/History
- **Opis:** Wyœwietla listê zapisanych treningów u¿ytkownika w trybie tylko do odczytu. Umo¿liwia filtrowanie oraz paginacjê rekordów.
- **Parametry zapytania:**  
  - page (int): numer strony  
  - pageSize (int): liczba rekordów na stronê  
  - dateFrom, dateTo (YYYY-MM-DD): zakres dat
- **£adunek odpowiedzi:**  
  Lista treningów u¿ytkownika
- **Kody powodzenia:**  
  - 200 OK – lista treningów zwrócona pomyœlnie

#### 3. Usuwanie treningów
- **Metoda HTTP:** DELETE
- **Œcie¿ka URL:** /Workouts/Delete/{id}
- **Opis:** Usuwa trening, dostêpne z listy treningów, dodatkowy przycisk "Usuñ" przy ka¿dym treningu.
- **Parametry zapytania:**  
  - id (int): id treningu do usuniêcia 
- **£adunek odpowiedzi:**  
  Komunikat o powodzeniu lub b³êdzie
- **Kody powodzenia:**  
  - 200 OK – trening usuniêty
  - 400 Bad Request – problem z operacj¹


 #### 4. Wyœwietlanie treningów
- **Metoda HTTP:** GET
- **Œcie¿ka URL:** /Workouts/{id}
- **Opis:** Wyœwietla pe³ne informacje na temat wybranego treningu.
- **Parametry zapytania:**  
  - id (int): id treningu do wyœwietlenia
- **£adunek odpowiedzi:**  
  widok z informacjami o treningu
- **Kody powodzenia:**  
  - 200 OK – widok treningu zwrócony pomyœlnie
  - 400 Bad Request – problem z operacj¹
	
## 3. Uwierzytelnianie i autoryzacja
- Mechanizm: Cookie Authentication (dla operacji zwracaj¹cych widoki)  
- **Opis:** Wszystkie operacje modyfikuj¹ce dane (tworzenie, edycja, blokowanie) wymagaj¹ autoryzacji. U¿ytkownik musi byæ zalogowany, a sesja uwierzytelniona za pomoc¹ cookies. W przypadku wywo³añ API, zamiast JWT wykorzystuje siê standardowe mechanizmy autoryzacji ASP.NET Core ([Authorize]).
- **Wymagania wejœciowe:**  
  - Ka¿de ¿¹danie modyfikuj¹ce zasoby musi pochodziæ od uwierzytelnionego u¿ytkownika.
  - W przypadku nieautoryzowanego dostêpu ¿¹danie powinno zwracaæ 401 Unauthorized.

## 4. Walidacja i logika biznesowa
- **Walidacja:**
  - Æwiczenia: Nazwa musi mieæ d³ugoœæ od 3 do 150 znaków oraz byæ unikalna w obrêbie u¿ytkownika (zgodnie z ograniczeniami bazy danych, np. CHECK CONSTRAINT i unikalnym indeksem).
  - Treningi: Daty musz¹ byæ w poprawnym formacie, a lista æwiczeñ nie mo¿e byæ pusta.
- **Logika biznesowa:**
  - Rejestracja i logowanie: Sprawdzanie poprawnoœci danych u¿ytkownika; w przypadku logowania, u¿ytkownik jest uwierzytelniany przy u¿yciu mechanizmu Cookie Authentication.
  - Edycja æwiczenia: Zmiany w danych æwiczenia s¹ propagowane do historycznych rekordów treningowych poprzez odpowiednie mechanizmy w logice biznesowej.
  - Blokowanie æwiczeñ: Æwiczenie oznaczone jako zablokowane nie jest dostêpne podczas tworzenia nowych treningów, jednak pozostaje widoczne w historii.
  - Dodawanie treningu: Podczas tworzenia treningu API weryfikuje, czy u¿yte æwiczenie nie jest zablokowane oraz stosuje walidacjê dotycz¹c¹ liczby serii, powtórzeñ i ewentualnego ciê¿aru.

## Za³o¿enia
- Projekt oparty jest o ASP.NET Core MVC, gdzie endpointy zwracaj¹ widoki. Operacje zmieniaj¹ce stan danych korzystaj¹ z metod POST.
- Obs³uga paginacji, filtrowania i sortowania jest realizowana na poziomie zapytañ do bazy danych (EF Core).
- Mechanizm uwierzytelniania opiera siê na standardowych rozwi¹zaniach ASP.NET Core (Cookie Authentication).