# REST API Plan

## 1. Zasoby
- **Users** (tabela Users)
- **Exercises** (tabela Exercises)
- **Workouts** (tabela Workouts)
- **WorkoutExercises** (tabela WorkoutExercises � relacja mi�dzy Workouts a Exercises)

## 2. Punkty ko�cowe

### A. Konto u�ytkownika (Account)
#### 1. Rejestracja
- **Metoda HTTP:** GET (wy�wietlenie formularza), POST (przyjmowanie danych)
- **�cie�ka URL:** /Account/Register
- **Opis:** Wy�wietlenie formularza rejestracji oraz przetwarzanie danych rejestracyjnych.
- **�adunek ��dania:**
- **�adunek odpowiedzi:** Widok strony potwierdzaj�cej rejestracj� lub b��d walidacji.
- **Kody powodzenia:**  
  - 200 OK � formularz lub potwierdzenie rejestracji
  - 400 Bad Request � b��dne dane wej�ciowe

#### 2. Logowanie
- **Metoda HTTP:** GET (formularz logowania), POST (przyjmowanie danych)
- **�cie�ka URL:** /Account/Login
- **Opis:** Wy�wietlenie formularza logowania oraz przetwarzanie danych logowania. Po pomy�lnym logowaniu zwracany jest token JWT.
- **�adunek ��dania:**
- **�adunek odpowiedzi:** Widok strony g��wnej lub komunikat o b��dzie wraz z tokenem JWT po poprawnym logowaniu.
- **Kody powodzenia:**  
  - 200 OK � widok g��wny z tokenem
  - 401 Unauthorized � niepoprawne dane logowania

### B. �wiczenia (Exercises)
#### 1. Dodawanie �wiczenia
- **Metoda HTTP:** GET (formularz dodawania), POST (przyjmowanie danych)
- **�cie�ka URL:** /Exercises/Create
- **Opis:** Umo�liwia dodanie nowego �wiczenia z walidacj� unikalno�ci w obr�bie u�ytkownika i d�ugo�ci nazwy (3-150 znak�w).
- **�adunek ��dania:**
- **�adunek odpowiedzi:** Widok potwierdzenia dodania lub komunikat o b��dzie walidacji.
- **Kody powodzenia:**  
  - 200 OK � �wiczenie dodane pomy�lnie
  - 400 Bad Request � b��d walidacji (np. nazwa zbyt kr�tka/d�uga, nieunikalna)

#### 2. Edycja �wiczenia
- **Metoda HTTP:** GET (formularz edycji), POST (przyjmowanie zaktualizowanych danych)
- **�cie�ka URL:** /Exercises/Edit/{id}
- **Opis:** Umo�liwia edycj� danych �wiczenia. Zmiany s� propagowane do historycznych rekord�w treningowych.
- **�adunek ��dania:**
- **�adunek odpowiedzi:** Widok potwierdzenia aktualizacji lub komunikat o b��dzie.
- **Kody powodzenia:**  
  - 200 OK � edycja zako�czona sukcesem
  - 400 Bad Request � b��d walidacji

#### 3. Blokowanie �wiczenia
- **Metoda HTTP:** POST
- **�cie�ka URL:** /Exercises/Block/{id}
- **Opis:** Oznacza wybrane �wiczenie jako zablokowane, uniemo�liwiaj�c jego u�ycie w nowych treningach, ale pozostawiaj�c widoczno�� w historii.
- **�adunek ��dania:** brak lub minimalny identyfikator
- **�adunek odpowiedzi:** Widok lub przekierowanie z komunikatem o powodzeniu operacji.
- **Kody powodzenia:**  
  - 200 OK � �wiczenie zablokowane
  - 400 Bad Request � problem z operacj�

### C. Treningi (Workouts)
#### 1. Dodawanie treningu
- **Metoda HTTP:** GET (formularz dodawania), POST (przyjmowanie danych)
- **�cie�ka URL:** /Workouts/Create
- **Opis:** Umo�liwia dodanie nowego treningu z dat� i list� �wicze� (z informacjami o seriach, powt�rzeniach, opcjonalnie ci�arze).
- **�adunek ��dania:**
- **�adunek odpowiedzi:** Widok potwierdzaj�cy zapis treningu lub komunikat o b��dzie.
- **Kody powodzenia:**  
  - 200 OK � trening dodany pomy�lnie
  - 400 Bad Request � b��d danych wej�ciowych

#### 2. Przegl�danie historii trening�w
- **Metoda HTTP:** GET
- **�cie�ka URL:** /Workouts/History
- **Opis:** Wy�wietla list� zapisanych trening�w u�ytkownika w trybie tylko do odczytu. Mo�liwe filtrowanie oraz paginacja.
- **Parametry zapytania:**  
  - page (int): numer strony
  - pageSize (int): liczba rekord�w na stron�
  - dateFrom, dateTo (YYYY-MM-DD): zakres dat
- **�adunek odpowiedzi:**
- **Kody powodzenia:**  
  - 200 OK � lista trening�w zwr�cona pomy�lnie

## 3. Uwierzytelnianie i autoryzacja
- Mechanizm: JWT (JSON Web Tokens)
- **Opis:** Wszystkie operacje modyfikuj�ce dane (tworzenie, edycja, blokowanie) wymagaj� autoryzacji. U�ytkownik musi by� zalogowany, a token JWT przesy�any jest w nag��wku ��dania. Endpointy zwracaj�ce widoki (m.in. lista trening�w, formularze) wykorzystuj� standardowe mechanizmy autoryzacji (np. [Authorize] w ASP.NET Core).
- **Wymagania wej�ciowe:**  
  - Ka�de ��danie modyfikuj�ce zasoby musi zawiera� poprawny token w nag��wku "Authorization: Bearer <token>".
  - ��dania bez wa�nego tokena powinny zwraca� 401 Unauthorized.

## 4. Walidacja i logika biznesowa
- **Walidacja:**
  - �wiczenia: Nazwa musi mie� d�ugo�� od 3 do 150 znak�w oraz by� unikalna w obr�bie u�ytkownika (zgodnie z CHECK CONSTRAINT i unikalnym indeksem).
  - Treningi: Daty musz� by� poprawnym formatem, a lista �wicze� nie mo�e by� pusta.
- **Logika biznesowa:**
  - Rejestracja i logowanie: Sprawdzanie poprawno�ci danych u�ytkownika, generowanie tokenu JWT.
  - Edycja �wiczenia: Zmiany musz� by� propagowane do historycznych rekord�w � logika nak�adana na modyfikacj� rekordu oraz odczyt danych z tabeli WorkoutExercises.
  - Blokowanie �wicze�: �wiczenie oznaczone jako zablokowane nie jest wy�wietlane podczas tworzenia nowych trening�w, jednak pozostaje widoczne w historii.
  - Dodawanie treningu: Podczas tworzenia treningu, API weryfikuje czy u�yte �wiczenie nie jest zablokowane (sprawdzanie w�a�ciwo�ci IsBlocked) oraz aplikuje walidacj� dotycz�c� poprawno�ci liczby serii, powt�rze� i opcjonalnego ci�aru.

## Za�o�enia
- Projekt oparty jest o ASP.NET Core MVC, gdzie endpointy zwracaj� widoki. Operacje zmieniaj�ce stan danych korzystaj� z metod POST.
- Obs�uga paginacji, filtrowania i sortowania jest realizowana na poziomie zapyta� do bazy danych (EF Core).
- Wykorzystanie JWT dla zabezpieczenia endpoint�w modyfikuj�cych dane zapewnia, �e tylko autoryzowani u�ytkownicy mog� wykonywa� operacje CRUD.
