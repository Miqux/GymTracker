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
- **Opis:** Wy�wietlenie formularza rejestracji oraz przetwarzanie danych rejestracyjnych u�ytkownika. Po poprawnej rejestracji wy�wietlany jest widok z potwierdzeniem lub komunikatem o b��dzie walidacji.
- **�adunek ��dania:**  
  Dane rejestracyjne (m.in. Email, Has�o)
- **�adunek odpowiedzi:** Widok strony potwierdzaj�cej rejestracj� lub wy�wietlenie b��d�w walidacji.
- **Kody powodzenia:**  
  - 200 OK � formularz lub potwierdzenie rejestracji
  - 400 Bad Request � b��dne dane wej�ciowe

#### 2. Logowanie
- **Metoda HTTP:** GET (formularz logowania), POST (przyjmowanie danych)
- **�cie�ka URL:** /Account/Login
- **Opis:** Wy�wietlenie formularza logowania oraz przetwarzanie danych logowania. Po poprawnym logowaniu u�ytkownik zostaje zalogowany przy u�yciu mechanizmu Cookie Authentication i przekierowany do strony g��wnej.
- **�adunek ��dania:**  
  Dane logowania (Email, Has�o)
- **�adunek odpowiedzi:** Widok strony g��wnej z komunikatem o pomy�lnym logowaniu lub komunikat o b��dzie w przypadku niepoprawnych danych.
- **Kody powodzenia:**  
  - 200 OK � widok g��wny po poprawnym logowaniu
  - 401 Unauthorized � niepoprawne dane logowania

### B. �wiczenia (Exercises)
#### 1. Dodawanie �wiczenia
- **Metoda HTTP:** GET (formularz dodawania), POST (przyjmowanie danych)
- **�cie�ka URL:** /Exercises/Create
- **Opis:** Umo�liwia dodanie nowego �wiczenia z walidacj� unikalno�ci w obr�bie u�ytkownika oraz sprawdzeniem d�ugo�ci nazwy (3-150 znak�w). Po dodaniu wy�wietlany jest widok potwierdzaj�cy.
- **�adunek ��dania:**  
  Dane �wiczenia (nazwa, grupa mi�niowa, poziom trudno�ci, opis)
- **�adunek odpowiedzi:** Widok potwierdzaj�cy dodanie �wiczenia lub wy�wietlenie komunikatu o b��dzie walidacji.
- **Kody powodzenia:**  
  - 200 OK � �wiczenie dodane pomy�lnie
  - 400 Bad Request � b��d walidacji (np. nazwa zbyt kr�tka/d�uga, nieunikalna)

#### 2. Edycja �wiczenia
- **Metoda HTTP:** GET (formularz edycji), POST (przyjmowanie zaktualizowanych danych)
- **�cie�ka URL:** /Exercises/Edit/{id}
- **Opis:** Umo�liwia edycj� danych �wiczenia. Wprowadzone zmiany s� propagowane do historycznych rekord�w treningowych.
- **�adunek ��dania:**  
  Zaktualizowane dane �wiczenia
- **�adunek odpowiedzi:** Widok potwierdzaj�cy aktualizacj� lub komunikat o b��dzie walidacji.
- **Kody powodzenia:**  
  - 200 OK � edycja zako�czona sukcesem
  - 400 Bad Request � b��d walidacji

#### 3. Blokowanie �wiczenia
- **Metoda HTTP:** POST
- **�cie�ka URL:** /Exercises/Block/{id}
- **Opis:** Oznacza wybrane �wiczenie jako zablokowane, co uniemo�liwia jego wykorzystanie w nowych treningach, lecz pozostawia jego widoczno�� w historii.
- **�adunek ��dania:** Minimalny identyfikator �wiczenia
- **�adunek odpowiedzi:** Widok lub przekierowanie z komunikatem o powodzeniu operacji.
- **Kody powodzenia:**  
  - 200 OK � �wiczenie zablokowane
  - 400 Bad Request � problem z operacj�

#### 4. Wy�wietlanie �wicze�
- **Metoda HTTP:** GET
- **�cie�ka URL:** /Exercises
- **Opis:** Wy�wietlanie �wicze� u�ytkownika w formie listy. Umo�liwia przej�� do dodawania �wiczenia, edycji lub blokowania. Z tej listy mo�na przej�� do edycji, blokowania lub dodawania �wiczenia.
- **�adunek ��dania:** Brak
- **�adunek odpowiedzi:** Widok.
- **Kody powodzenia:**  
  - 200 OK � Lista �wicze� zwr�cona pomy�lnie
  - 400 Bad Request � problem z operacj�

### C. Treningi (Workouts)
#### 1. Dodawanie treningu
- **Metoda HTTP:** GET (formularz dodawania), POST (przyjmowanie danych)
- **�cie�ka URL:** /Workouts/Create
- **Opis:** Umo�liwia dodanie nowego treningu z okre�lon� dat� oraz list� �wicze� (wraz z informacjami o seriach, powt�rzeniach, a opcjonalnie ci�arze). Podczas tworzenia treningu sprawdzana jest walidacja danych, m.in. czy �wiczenie nie jest zablokowane.
- **�adunek ��dania:**  
  Dane treningu (data, �wiczenia z detalami)
- **�adunek odpowiedzi:** Widok potwierdzaj�cy zapis treningu lub komunikat o b��dzie walidacji.
- **Kody powodzenia:**  
  - 200 OK � trening dodany pomy�lnie
  - 400 Bad Request � b��d danych wej�ciowych

#### 2. Przegl�danie historii trening�w
- **Metoda HTTP:** GET
- **�cie�ka URL:** /Workouts/History
- **Opis:** Wy�wietla list� zapisanych trening�w u�ytkownika w trybie tylko do odczytu. Umo�liwia filtrowanie oraz paginacj� rekord�w.
- **Parametry zapytania:**  
  - page (int): numer strony  
  - pageSize (int): liczba rekord�w na stron�  
  - dateFrom, dateTo (YYYY-MM-DD): zakres dat
- **�adunek odpowiedzi:**  
  Lista trening�w u�ytkownika
- **Kody powodzenia:**  
  - 200 OK � lista trening�w zwr�cona pomy�lnie

#### 3. Usuwanie trening�w
- **Metoda HTTP:** DELETE
- **�cie�ka URL:** /Workouts/Delete/{id}
- **Opis:** Usuwa trening, dost�pne z listy trening�w, dodatkowy przycisk "Usu�" przy ka�dym treningu.
- **Parametry zapytania:**  
  - id (int): id treningu do usuni�cia 
- **�adunek odpowiedzi:**  
  Komunikat o powodzeniu lub b��dzie
- **Kody powodzenia:**  
  - 200 OK � trening usuni�ty
  - 400 Bad Request � problem z operacj�


 #### 4. Wy�wietlanie trening�w
- **Metoda HTTP:** GET
- **�cie�ka URL:** /Workouts/{id}
- **Opis:** Wy�wietla pe�ne informacje na temat wybranego treningu.
- **Parametry zapytania:**  
  - id (int): id treningu do wy�wietlenia
- **�adunek odpowiedzi:**  
  widok z informacjami o treningu
- **Kody powodzenia:**  
  - 200 OK � widok treningu zwr�cony pomy�lnie
  - 400 Bad Request � problem z operacj�
	
## 3. Uwierzytelnianie i autoryzacja
- Mechanizm: Cookie Authentication (dla operacji zwracaj�cych widoki)  
- **Opis:** Wszystkie operacje modyfikuj�ce dane (tworzenie, edycja, blokowanie) wymagaj� autoryzacji. U�ytkownik musi by� zalogowany, a sesja uwierzytelniona za pomoc� cookies. W przypadku wywo�a� API, zamiast JWT wykorzystuje si� standardowe mechanizmy autoryzacji ASP.NET Core ([Authorize]).
- **Wymagania wej�ciowe:**  
  - Ka�de ��danie modyfikuj�ce zasoby musi pochodzi� od uwierzytelnionego u�ytkownika.
  - W przypadku nieautoryzowanego dost�pu ��danie powinno zwraca� 401 Unauthorized.

## 4. Walidacja i logika biznesowa
- **Walidacja:**
  - �wiczenia: Nazwa musi mie� d�ugo�� od 3 do 150 znak�w oraz by� unikalna w obr�bie u�ytkownika (zgodnie z ograniczeniami bazy danych, np. CHECK CONSTRAINT i unikalnym indeksem).
  - Treningi: Daty musz� by� w poprawnym formacie, a lista �wicze� nie mo�e by� pusta.
- **Logika biznesowa:**
  - Rejestracja i logowanie: Sprawdzanie poprawno�ci danych u�ytkownika; w przypadku logowania, u�ytkownik jest uwierzytelniany przy u�yciu mechanizmu Cookie Authentication.
  - Edycja �wiczenia: Zmiany w danych �wiczenia s� propagowane do historycznych rekord�w treningowych poprzez odpowiednie mechanizmy w logice biznesowej.
  - Blokowanie �wicze�: �wiczenie oznaczone jako zablokowane nie jest dost�pne podczas tworzenia nowych trening�w, jednak pozostaje widoczne w historii.
  - Dodawanie treningu: Podczas tworzenia treningu API weryfikuje, czy u�yte �wiczenie nie jest zablokowane oraz stosuje walidacj� dotycz�c� liczby serii, powt�rze� i ewentualnego ci�aru.

## Za�o�enia
- Projekt oparty jest o ASP.NET Core MVC, gdzie endpointy zwracaj� widoki. Operacje zmieniaj�ce stan danych korzystaj� z metod POST.
- Obs�uga paginacji, filtrowania i sortowania jest realizowana na poziomie zapyta� do bazy danych (EF Core).
- Mechanizm uwierzytelniania opiera si� na standardowych rozwi�zaniach ASP.NET Core (Cookie Authentication).