# API Endpoint Implementation Plan: Login Endpoint

## 1. Przegl�d punktu ko�cowego  
   Endpoint `/Account/Login` s�u�y do logowania u�ytkownik�w aplikacji MVC. U�ytkownik pobiera formularz logowania poprzez metod� GET, a nast�pnie wysy�a dane logowania (Email i Password) metod� POST. W przypadku poprawnego uwierzytelnienia, system generuje token JWT, kt�ry jest przekazywany wraz z widokiem strony g��wnej. 

## 2. Szczeg�y ��dania  
   - **Metody HTTP:**  
     - GET � s�u�y do renderowania formularza logowania (widok Razor)  
     - POST � s�u�y do przetwarzania danych logowania  
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
     - `LoginRequestDto` � zawiera w�a�ciwo�ci `Email` oraz `Password`  
     - `LoginResponseDto` � zawiera w�a�ciwo�� `Token` (JWT) oraz ewentualnie dodatkowe informacje o u�ytkowniku  
   - **Command Model:**  
     - `LoginCommand` � model przekazuj�cy dane logowania do warstwy serwisowej

## 4. Szczeg�y odpowiedzi  
   - **GET /Account/Login:**  
     - **Status:** 200 OK  
     - Widok renderowany przy u�yciu Razor (np. `Views/Account/Login.cshtml`) prezentuj�cy formularz logowania  
   - **POST /Account/Login (sukces):**  
     - **Status:** 200 OK  
     - **Response Body:**  
        {
     "Token": "jwtTokenString"
   }
        - Renderowany widok strony g��wnej zawieraj�cy token JWT  
   - **B��dy:**  
     - **401 Unauthorized:** Gdy dane logowania (b��dny Email lub Password) s� nieprawid�owe  
     - **400 Bad Request:** W przypadku niekompletnych lub b��dnych danych wej�ciowych  
     - **500 Internal Server Error:** Wyst�pienie b��du w warstwie serwisowej lub problem�w z generowaniem tokenu

## 5. Przep�yw danych  
   - **GET /Account/Login:**  
     1. U�ytkownik wysy�a ��danie GET, aby pobra� formularz logowania  
     2. Kontroler (np. `AccountController`) renderuje widok Razor z formularzem logowania  
   - **POST /Account/Login:**  
     1. U�ytkownik przesy�a dane logowania w formacie JSON  
     2. Kontroler mapuje dane na `LoginRequestDto` lub bezpo�rednio na `LoginCommand`  
     3. Dane s� przekazywane do `AccountService`, gdzie nast�puje:  
        - Walidacja danych wej�ciowych (w tym weryfikacja formatu email i obecno�ci has�a)  
        - Weryfikacja istnienia u�ytkownika w tabeli `Users` oraz por�wnanie zachowanego hasha has�a  
        - Generacja tokenu JWT przy u�yciu odpowiedniego algorytmu i ustawienie terminu wa�no�ci  
     4. Na podstawie wyniku:  
        - Przy sukcesie � u�ytkownik jest przekierowywany do strony g��wnej, a token JWT jest zawarty w odpowiedzi  
        - W przypadku b��d�w � zwracany jest odpowiedni kod b��du

## 6. Wzgl�dy bezpiecze�stwa  
   - Wym�g korzystania z HTTPS do zabezpieczenia transmisji danych  
   - Walidacja danych wej�ciowych z wykorzystaniem atrybut�w modeli (np. `[Required]`, `[EmailAddress]`)  
   - Implementacja mechanizm�w zabezpieczaj�cych przed atakami brute force (przyk�adowo lockout po okre�lonej liczbie nieudanych pr�b)  
   - Generacja tokenu JWT przy u�yciu silnego algorytmu (np. HS256 lub RS256) oraz okre�lonego czasu wyga�ni�cia  
   - Bezpieczne przechowywanie kluczy s�u��cych do generowania tokenu (np. przy u�yciu __UserSecrets__)

## 7. Obs�uga b��d�w  
   - **400 Bad Request:**  
     - Zwracany w przypadku braku wymaganych danych lub b��dnego formatu danych wej�ciowych  
   - **401 Unauthorized:**  
     - Zwracany, gdy dane logowania s� nieprawid�owe, z odpowiednim komunikatem wyja�niaj�cym problem autoryzacji  
   - **500 Internal Server Error:**  
     - Globalna obs�uga wyj�tk�w przy u�yciu middleware (np. __ExceptionFilter__) oraz logowanie b��d�w przy u�yciu mechanizmu ILogger  
   - Opcjonalnie: Rejestracja krytycznych b��d�w w dedykowanej tabeli b��d�w, je�li jest taka potrzeba

## 8. Rozwa�ania dotycz�ce wydajno�ci  
   - Optymalizacja zapyta� do bazy, w tym indeksowanie kolumny `Email` w tabeli `Users`  
   - Minimalizacja operacji w ramach logiki logowania w celu skr�cenia czasu odpowiedzi  
   - Potencjalne wykorzystanie mechanizmu cache dla statycznych konfiguracji tokena lub kluczy
       
## 9. Etapy wdro�enia  
   1. **Definicja DTO i Command Model:**  
      - Utworzenie klasy `LoginRequestDto` z w�a�ciwo�ciami `Email` oraz `Password`  
      - Utworzenie klasy `LoginResponseDto` zwracaj�cej token JWT  
      - Utworzenie klasy `LoginCommand` do przekazywania danych do warstwy serwisowej  
   2. **Implementacja logiki w AccountService:**  
      - Dodanie metody weryfikuj�cej dane u�ytkownika, por�wnuj�cej has�o (hash) oraz generuj�cej token JWT  
      - Zaimplementowanie zabezpiecze� przed atakami brute force, np. poprzez mechanizm lockout  
      - Integracja walidacji danych wej�ciowych i generacji tokenu zgodnie z ustalonymi regu�ami  
   3. **Modyfikacja kontrolera Account:**  
      - Utworzenie akcji GET, kt�ra renderuje formularz logowania przy u�yciu Razor View (`Views/Account/Login.cshtml`)  
      - Utworzenie akcji POST, kt�ra mapuje dane przes�ane przez u�ytkownika na `LoginCommand` i wywo�uje metod� w `AccountService`  
      - Implementacja mechanizmu obs�ugi b��d�w oraz zwracanie w�a�ciwych kod�w statusu (400, 401, 500)  
   4. **Implementacja widok�w:**  
      - Utworzenie lub modyfikacja widoku Razor do logowania (`Views/Account/Login.cshtml`) z formularzem logowania  
      - Aktualizacja widoku strony g��wnej (np. `Views/Home/Index.cshtml`), aby obs�ugiwa� przekazanie tokenu JWT po zalogowaniu  
   5. **Testowanie:**  
      - Przeprowadzenie test�w jednostkowych i integracyjnych dla logiki logowania, w tym zar�wno pozytywnych, jak i negatywnych scenariuszy  
      - Testowanie mechanizm�w bezpiecze�stwa, takich jak lockout, poprawna transmisja HTTPS oraz poprawno�� generacji tokenu JWT  
   6. **Code Review i wdro�enie:**  
      - Przeprowadzenie przegl�du kodu przez zesp�  
      - Wdro�enie zmian do �rodowiska testowego, a nast�pnie produkcyjnego  
      - Monitorowanie log�w i wydajno�ci endpointu po wdro�eniu
