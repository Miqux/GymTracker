# API Endpoint Implementation Plan: Login Endpoint

## 1. Przegl¹d punktu koñcowego  
   Endpoint `/Account/Login` s³u¿y do logowania u¿ytkowników aplikacji MVC. U¿ytkownik pobiera formularz logowania poprzez metodê GET, a nastêpnie wysy³a dane logowania (Email i Password) metod¹ POST. W przypadku poprawnego uwierzytelnienia, system generuje token JWT, który jest przekazywany wraz z widokiem strony g³ównej. 

## 2. Szczegó³y ¿¹dania  
   - **Metody HTTP:**  
     - GET – s³u¿y do renderowania formularza logowania (widok Razor)  
     - POST – s³u¿y do przetwarzania danych logowania  
   - **Struktura URL:**  
     `/Account/Login`  
   - **Parametry:**  
     - **Wymagane (POST):**  
       - `Email` (string)  
       - `Password` (string)  
     - **Opcjonalne:** Brak  
   - **Request Body (dla POST):**  
 {
   "Email": "user@example.com",
   "Password": "UserPassword123!"
 }
 ## 3. Wykorzystywane typy  
   - **DTO:**  
     - `LoginRequestDto` – zawiera w³aœciwoœci `Email` oraz `Password`  
     - `LoginResponseDto` – zawiera w³aœciwoœæ `Token` (JWT) oraz ewentualnie dodatkowe informacje o u¿ytkowniku  
   - **Command Model:**  
     - `LoginCommand` – model przekazuj¹cy dane logowania do warstwy serwisowej

## 4. Szczegó³y odpowiedzi  
   - **GET /Account/Login:**  
     - **Status:** 200 OK  
     - Widok renderowany przy u¿yciu Razor (np. `Views/Account/Login.cshtml`) prezentuj¹cy formularz logowania  
   - **POST /Account/Login (sukces):**  
     - **Status:** 200 OK  
     - **Response Body:**  
        {
     "Token": "jwtTokenString"
   }
        - Renderowany widok strony g³ównej zawieraj¹cy token JWT  
   - **B³êdy:**  
     - **401 Unauthorized:** Gdy dane logowania (b³êdny Email lub Password) s¹ nieprawid³owe  
     - **400 Bad Request:** W przypadku niekompletnych lub b³êdnych danych wejœciowych  
     - **500 Internal Server Error:** Wyst¹pienie b³êdu w warstwie serwisowej lub problemów z generowaniem tokenu

## 5. Przep³yw danych  
   - **GET /Account/Login:**  
     1. U¿ytkownik wysy³a ¿¹danie GET, aby pobraæ formularz logowania  
     2. Kontroler (np. `AccountController`) renderuje widok Razor z formularzem logowania  
   - **POST /Account/Login:**  
     1. U¿ytkownik przesy³a dane logowania w formacie JSON  
     2. Kontroler mapuje dane na `LoginRequestDto` lub bezpoœrednio na `LoginCommand`  
     3. Dane s¹ przekazywane do `AccountService`, gdzie nastêpuje:  
        - Walidacja danych wejœciowych (w tym weryfikacja formatu email i obecnoœci has³a)  
        - Weryfikacja istnienia u¿ytkownika w tabeli `Users` oraz porównanie zachowanego hasha has³a  
        - Generacja tokenu JWT przy u¿yciu odpowiedniego algorytmu i ustawienie terminu wa¿noœci  
     4. Na podstawie wyniku:  
        - Przy sukcesie – u¿ytkownik jest przekierowywany do strony g³ównej, a token JWT jest zawarty w odpowiedzi  
        - W przypadku b³êdów – zwracany jest odpowiedni kod b³êdu

## 6. Wzglêdy bezpieczeñstwa  
   - Wymóg korzystania z HTTPS do zabezpieczenia transmisji danych  
   - Walidacja danych wejœciowych z wykorzystaniem atrybutów modeli (np. `[Required]`, `[EmailAddress]`)  
   - Implementacja mechanizmów zabezpieczaj¹cych przed atakami brute force (przyk³adowo lockout po okreœlonej liczbie nieudanych prób)  
   - Generacja tokenu JWT przy u¿yciu silnego algorytmu (np. HS256 lub RS256) oraz okreœlonego czasu wygaœniêcia  
   - Bezpieczne przechowywanie kluczy s³u¿¹cych do generowania tokenu (np. przy u¿yciu __UserSecrets__)

## 7. Obs³uga b³êdów  
   - **400 Bad Request:**  
     - Zwracany w przypadku braku wymaganych danych lub b³êdnego formatu danych wejœciowych  
   - **401 Unauthorized:**  
     - Zwracany, gdy dane logowania s¹ nieprawid³owe, z odpowiednim komunikatem wyjaœniaj¹cym problem autoryzacji  
   - **500 Internal Server Error:**  
     - Globalna obs³uga wyj¹tków przy u¿yciu middleware (np. __ExceptionFilter__) oraz logowanie b³êdów przy u¿yciu mechanizmu ILogger  
   - Opcjonalnie: Rejestracja krytycznych b³êdów w dedykowanej tabeli b³êdów, jeœli jest taka potrzeba

## 8. Rozwa¿ania dotycz¹ce wydajnoœci  
   - Optymalizacja zapytañ do bazy, w tym indeksowanie kolumny `Email` w tabeli `Users`  
   - Minimalizacja operacji w ramach logiki logowania w celu skrócenia czasu odpowiedzi  
   - Potencjalne wykorzystanie mechanizmu cache dla statycznych konfiguracji tokena lub kluczy
       
## 9. Etapy wdro¿enia  
   1. **Definicja DTO i Command Model:**  
      - Utworzenie klasy `LoginRequestDto` z w³aœciwoœciami `Email` oraz `Password`  
      - Utworzenie klasy `LoginResponseDto` zwracaj¹cej token JWT  
      - Utworzenie klasy `LoginCommand` do przekazywania danych do warstwy serwisowej  
   2. **Implementacja logiki w AccountService:**  
      - Dodanie metody weryfikuj¹cej dane u¿ytkownika, porównuj¹cej has³o (hash) oraz generuj¹cej token JWT  
      - Zaimplementowanie zabezpieczeñ przed atakami brute force, np. poprzez mechanizm lockout  
      - Integracja walidacji danych wejœciowych i generacji tokenu zgodnie z ustalonymi regu³ami  
   3. **Modyfikacja kontrolera Account:**  
      - Utworzenie akcji GET, która renderuje formularz logowania przy u¿yciu Razor View (`Views/Account/Login.cshtml`)  
      - Utworzenie akcji POST, która mapuje dane przes³ane przez u¿ytkownika na `LoginCommand` i wywo³uje metodê w `AccountService`  
      - Implementacja mechanizmu obs³ugi b³êdów oraz zwracanie w³aœciwych kodów statusu (400, 401, 500)  
   4. **Implementacja widoków:**  
      - Utworzenie lub modyfikacja widoku Razor do logowania (`Views/Account/Login.cshtml`) z formularzem logowania  
      - Aktualizacja widoku strony g³ównej (np. `Views/Home/Index.cshtml`), aby obs³ugiwa³ przekazanie tokenu JWT po zalogowaniu  
   5. **Testowanie:**  
      - Przeprowadzenie testów jednostkowych i integracyjnych dla logiki logowania, w tym zarówno pozytywnych, jak i negatywnych scenariuszy  
      - Testowanie mechanizmów bezpieczeñstwa, takich jak lockout, poprawna transmisja HTTPS oraz poprawnoœæ generacji tokenu JWT  
   6. **Code Review i wdro¿enie:**  
      - Przeprowadzenie przegl¹du kodu przez zespó³  
      - Wdro¿enie zmian do œrodowiska testowego, a nastêpnie produkcyjnego  
      - Monitorowanie logów i wydajnoœci endpointu po wdro¿eniu
