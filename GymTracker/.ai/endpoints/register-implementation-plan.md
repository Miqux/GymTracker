# API Endpoint Implementation Plan: Rejestracja konta u¿ytkownika

## 1. Przegl¹d punktu koñcowego
Endpoint /Account/Register ma za zadanie umo¿liwiæ rejestracjê nowych u¿ytkowników. U¿ytkownik mo¿e uzyskaæ formularz rejestracji metod¹ GET oraz przes³aæ dane rejestracyjne metod¹ POST. Endpoint zapewnia walidacjê danych, zabezpieczenie przed duplikatami (na podstawie unikalnego pola Email) oraz odpowiedni¹ obs³ugê b³êdów.

## 2. Szczegó³y ¿¹dania
- **Metoda HTTP:** GET (wyœwietlenie formularza), POST (przetwarzanie danych rejestracyjnych)
- **Struktura URL:** `/Account/Register`
- **Parametry:**
  - **GET:** Brak parametrów – ¿¹danie s³u¿y wy³¹cznie do pobrania formularza.
  - **POST:**  
    - **Wymagane:**  
      - `Email` (string): Adres e-mail nowego u¿ytkownika.
      - `Password` (string): Has³o w postaci zwyk³ego tekstu (do haszowania przed zapisaniem).
    - **Opcjonalne:** Inne dane, takie jak potwierdzenie has³a, mog¹ byæ obs³u¿one przez walidacjê po stronie clienta.

- **Request Body (POST):**    
{ "Email": "user@example.com", "Password": "StrongPassword123" }
## 3. Wykorzystywane typy
- **DTO:**  
  - `RegisterDto` – reprezentuje dane otrzymywane z formularza rejestracji.
  
- **Command Model:**  
  - `RegisterUserCommand` – model przetwarzania rejestracji, który zawiera logikê walidacyjn¹ oraz inicjuje proces tworzenia u¿ytkownika.

## 4. Szczegó³y odpowiedzi
- **GET /Account/Register:**  
  - Status 200 OK z widokiem formularza rejestracji.
- **POST /Account/Register:**  
  - **200 OK:** Dane prawid³owe – potwierdzenie rejestracji lub przekierowanie do innego widoku.
  - **400 Bad Request:** B³êdne dane wejœciowe, np. niepoprawny format e-mail lub s³abe has³o.
  - **500 Internal Server Error:** Wewnêtrzny b³¹d serwera w przypadku problemów z przetwarzaniem.

## 5. Przep³yw danych
1. **GET Request:**  
   - U¿ytkownik ¿¹da formularza rejestracji.
   - Akcja kontrolera renderuje widok formularza.

2. **POST Request:**  
   - Dane rejestracyjne trafiaj¹ do kontrolera.
   - Kontroler mapuje dane z `RegisterDto` na `RegisterUserCommand`.
   - Us³uga (np. `AccountService`) wykonuje walidacjê:
     - Sprawdzenie unikalnoœci adresu Email przy u¿yciu repozytorium Users.
     - Haszowanie has³a przed zapisaniem.
   - W przypadku powodzenia, tworzenie nowego rekordu w tabeli Users przy u¿yciu wzorca Repository/Unit of Work.
   - Zwrócenie odpowiedniego widoku lub komunikatu potwierdzaj¹cego rejestracjê.

## 6. Wzglêdy bezpieczeñstwa
- **Walidacja wejœcia:**  
  - Walidacja danych przez atrybuty modelu (np. [Required], [EmailAddress]) oraz dodatkowa walidacja w warstwie serwisowej.
- **Szyfrowanie:**  
  - Haszowanie hase³ przy u¿yciu bezpiecznej biblioteki (np. ASP.NET Core Identity lub innej biblioteki haszuj¹cej).
- **Ochrona przed duplikatami:**  
  - Sprawdzenie unikalnoœci pola Email, wykorzystuj¹c unikalny indeks w bazie danych.
- **Zabezpieczenie transmisji:**  
  - Wymóg u¿ycia protoko³u HTTPS dla bezpiecznej transmisji danych.

## 7. Obs³uga b³êdów
- **400 Bad Request:**  
  - Zwracany w przypadku b³êdnej walidacji danych wejœciowych (np. pusty email, format has³a).
- **500 Internal Server Error:**  
  - Zwracany przy nieoczekiwanych wyj¹tkach lub b³êdach w warstwie serwisowej.
- **Rejestracja b³êdów:**  
  - Logowanie szczegó³owych informacji o b³êdach za pomoc¹ istniej¹cego mechanizmu logowania w aplikacji (np. ILogger), aby umo¿liwiæ diagnozê problemów.
  
## 8. Rozwa¿ania dotycz¹ce wydajnoœci
- **Optymalizacja zapytañ:**  
  - U¿ywanie mechanizmów minimalizuj¹cych obci¹¿enie bazy danych, jak AsNoTracking() dla odczytów oraz odpowiednie indeksowanie kolumn wykorzystywanych do wyszukiwania unikalnych u¿ytkowników.
- **U¿ycie cache:**  
  - Dla formularza GET mo¿e zostaæ rozwa¿one buforowanie statycznych zasobów.
- **Skalowalnoœæ:**  
  - Wzorzec Repository/Unit of Work umo¿liwi ³atwe refaktoryzacje i optymalizacjê zapytañ w przysz³oœci.

## 9. Etapy wdro¿enia
1. **Definicja DTO i Command Model:**  
   - Utworzenie klasy `RegisterDto` w odpowiednim folderze (np. Models/Dto) z wymaganymi atrybutami walidacyjnymi.
   - Utworzenie klasy `RegisterUserCommand` w folderze Commands lub Services.

2. **Implementacja kontrolera:**  
   - Rozszerzenie kontrolera Account (np. `AccountController`) o akcje dla GET i POST.
   - W akcji POST zaimplementowaæ walidacjê wejœcia, mapowanie danych, i wywo³anie metody us³ugi.

3. **Implementacja us³ugi konta:**  
   - Utworzenie serwisu `AccountService` lub modyfikacja istniej¹cej us³ugi, aby zawiera³a logikê rejestracji:
     - Sprawdzanie unikalnoœci email.
     - Haszowanie has³a.
     - Zapis do bazy danych przy u¿yciu wzorca Repository/Unit of Work.

4. **Walidacja i testy:**  
   - Zastosowanie atrybutów walidacyjnych w DTO.
   - Implementacja dodatkowej walidacji w serwisie konta.
   - Pokrycie wdro¿enia testami jednostkowymi i integracyjnymi.

5. **Obs³uga b³êdów i logowanie:**  
   - Dodanie mechanizmów przechwytywania wyj¹tków (ExceptionFilter lub middleware) dla konsystentnego zwracania b³êdów.
   - Integracja z istniej¹cym systemem logowania b³êdów.

6. **Review i testy end-to-end:**  
   - Przeprowadzenie testów rêcznych/formularzowych oraz automatycznych, aby upewniæ siê, ¿e endpoint spe³nia wymagania bezpieczeñstwa i funkcjonalnoœci.

7. **Wdro¿enie i monitoring:**  
   - Wdro¿enie aktualizacji do œrodowiska testowego, a nastêpnie produkcyjnego.
   - Monitorowanie logów i wydajnoœci po wdro¿eniu.