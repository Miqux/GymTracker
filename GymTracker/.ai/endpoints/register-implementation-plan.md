# API Endpoint Implementation Plan: Rejestracja konta u�ytkownika

## 1. Przegl�d punktu ko�cowego
Endpoint /Account/Register ma za zadanie umo�liwi� rejestracj� nowych u�ytkownik�w. U�ytkownik mo�e uzyska� formularz rejestracji metod� GET oraz przes�a� dane rejestracyjne metod� POST. Endpoint zapewnia walidacj� danych, zabezpieczenie przed duplikatami (na podstawie unikalnego pola Email) oraz odpowiedni� obs�ug� b��d�w.

## 2. Szczeg�y ��dania
- **Metoda HTTP:** GET (wy�wietlenie formularza), POST (przetwarzanie danych rejestracyjnych)
- **Struktura URL:** `/Account/Register`
- **Parametry:**
  - **GET:** Brak parametr�w � ��danie s�u�y wy��cznie do pobrania formularza.
  - **POST:**  
    - **Wymagane:**  
      - `Email` (string): Adres e-mail nowego u�ytkownika.
      - `Password` (string): Has�o w postaci zwyk�ego tekstu (do haszowania przed zapisaniem).
    - **Opcjonalne:** Inne dane, takie jak potwierdzenie has�a, mog� by� obs�u�one przez walidacj� po stronie clienta.

- **Request Body (POST):**    
{ "Email": "user@example.com", "Password": "StrongPassword123" }
## 3. Wykorzystywane typy
- **DTO:**  
  - `RegisterDto` � reprezentuje dane otrzymywane z formularza rejestracji.
  
- **Command Model:**  
  - `RegisterUserCommand` � model przetwarzania rejestracji, kt�ry zawiera logik� walidacyjn� oraz inicjuje proces tworzenia u�ytkownika.

## 4. Szczeg�y odpowiedzi
- **GET /Account/Register:**  
  - Status 200 OK z widokiem formularza rejestracji.
- **POST /Account/Register:**  
  - **200 OK:** Dane prawid�owe � potwierdzenie rejestracji lub przekierowanie do innego widoku.
  - **400 Bad Request:** B��dne dane wej�ciowe, np. niepoprawny format e-mail lub s�abe has�o.
  - **500 Internal Server Error:** Wewn�trzny b��d serwera w przypadku problem�w z przetwarzaniem.

## 5. Przep�yw danych
1. **GET Request:**  
   - U�ytkownik ��da formularza rejestracji.
   - Akcja kontrolera renderuje widok formularza.

2. **POST Request:**  
   - Dane rejestracyjne trafiaj� do kontrolera.
   - Kontroler mapuje dane z `RegisterDto` na `RegisterUserCommand`.
   - Us�uga (np. `AccountService`) wykonuje walidacj�:
     - Sprawdzenie unikalno�ci adresu Email przy u�yciu repozytorium Users.
     - Haszowanie has�a przed zapisaniem.
   - W przypadku powodzenia, tworzenie nowego rekordu w tabeli Users przy u�yciu wzorca Repository/Unit of Work.
   - Zwr�cenie odpowiedniego widoku lub komunikatu potwierdzaj�cego rejestracj�.

## 6. Wzgl�dy bezpiecze�stwa
- **Walidacja wej�cia:**  
  - Walidacja danych przez atrybuty modelu (np. [Required], [EmailAddress]) oraz dodatkowa walidacja w warstwie serwisowej.
- **Szyfrowanie:**  
  - Haszowanie hase� przy u�yciu bezpiecznej biblioteki (np. ASP.NET Core Identity lub innej biblioteki haszuj�cej).
- **Ochrona przed duplikatami:**  
  - Sprawdzenie unikalno�ci pola Email, wykorzystuj�c unikalny indeks w bazie danych.
- **Zabezpieczenie transmisji:**  
  - Wym�g u�ycia protoko�u HTTPS dla bezpiecznej transmisji danych.

## 7. Obs�uga b��d�w
- **400 Bad Request:**  
  - Zwracany w przypadku b��dnej walidacji danych wej�ciowych (np. pusty email, format has�a).
- **500 Internal Server Error:**  
  - Zwracany przy nieoczekiwanych wyj�tkach lub b��dach w warstwie serwisowej.
- **Rejestracja b��d�w:**  
  - Logowanie szczeg�owych informacji o b��dach za pomoc� istniej�cego mechanizmu logowania w aplikacji (np. ILogger), aby umo�liwi� diagnoz� problem�w.
  
## 8. Rozwa�ania dotycz�ce wydajno�ci
- **Optymalizacja zapyta�:**  
  - U�ywanie mechanizm�w minimalizuj�cych obci��enie bazy danych, jak AsNoTracking() dla odczyt�w oraz odpowiednie indeksowanie kolumn wykorzystywanych do wyszukiwania unikalnych u�ytkownik�w.
- **U�ycie cache:**  
  - Dla formularza GET mo�e zosta� rozwa�one buforowanie statycznych zasob�w.
- **Skalowalno��:**  
  - Wzorzec Repository/Unit of Work umo�liwi �atwe refaktoryzacje i optymalizacj� zapyta� w przysz�o�ci.

## 9. Etapy wdro�enia
1. **Definicja DTO i Command Model:**  
   - Utworzenie klasy `RegisterDto` w odpowiednim folderze (np. Models/Dto) z wymaganymi atrybutami walidacyjnymi.
   - Utworzenie klasy `RegisterUserCommand` w folderze Commands lub Services.

2. **Implementacja kontrolera:**  
   - Rozszerzenie kontrolera Account (np. `AccountController`) o akcje dla GET i POST.
   - W akcji POST zaimplementowa� walidacj� wej�cia, mapowanie danych, i wywo�anie metody us�ugi.

3. **Implementacja us�ugi konta:**  
   - Utworzenie serwisu `AccountService` lub modyfikacja istniej�cej us�ugi, aby zawiera�a logik� rejestracji:
     - Sprawdzanie unikalno�ci email.
     - Haszowanie has�a.
     - Zapis do bazy danych przy u�yciu wzorca Repository/Unit of Work.

4. **Walidacja i testy:**  
   - Zastosowanie atrybut�w walidacyjnych w DTO.
   - Implementacja dodatkowej walidacji w serwisie konta.
   - Pokrycie wdro�enia testami jednostkowymi i integracyjnymi.

5. **Obs�uga b��d�w i logowanie:**  
   - Dodanie mechanizm�w przechwytywania wyj�tk�w (ExceptionFilter lub middleware) dla konsystentnego zwracania b��d�w.
   - Integracja z istniej�cym systemem logowania b��d�w.

6. **Review i testy end-to-end:**  
   - Przeprowadzenie test�w r�cznych/formularzowych oraz automatycznych, aby upewni� si�, �e endpoint spe�nia wymagania bezpiecze�stwa i funkcjonalno�ci.

7. **Wdro�enie i monitoring:**  
   - Wdro�enie aktualizacji do �rodowiska testowego, a nast�pnie produkcyjnego.
   - Monitorowanie log�w i wydajno�ci po wdro�eniu.