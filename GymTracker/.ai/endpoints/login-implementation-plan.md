
# API Endpoint Implementation Plan: Login Endpoint

## 1. Przegl¹d punktu koñcowego  
Endpoint `/Account/Login` s³u¿y do logowania u¿ytkowników aplikacji MVC. Formularz logowania jest pobierany poprzez metodê GET, a dane logowania (Email i Password) wysy³ane s¹ metod¹ POST. W przypadku poprawnego uwierzytelnienia, u¿ytkownik jest logowany przy u¿yciu mechanizmu Cookie Authentication i przekierowywany do strony g³ównej.

## 2. Szczegó³y ¿¹dania  
- **Metody HTTP:**  
  - GET – renderuje formularz logowania (widok Razor)  
  - POST – przetwarza dane logowania  
- **Struktura URL:**  
  `/Account/Login`  
- **Parametry:**  
  - **Wymagane (POST):**  
    - `Email` (string)  
    - `Password` (string)  
- **Request Body (dla POST):**
## 3. Wykorzystywane typy  
- **DTO:**  
  - `LoginRequestDto` – zawiera w³aœciwoœci `Email` oraz `Password`  
- **Command Model:**  
  - `LoginCommand` – model przekazuj¹cy dane logowania do warstwy serwisowej

## 4. Szczegó³y odpowiedzi  
- **GET /Account/Login:**  
  - **Status:** 200 OK  
  - Widok renderowany przy u¿yciu Razor (np. `Views/Account/Login.cshtml`) prezentuj¹cy formularz logowania  
- **POST /Account/Login (sukces):**  
  - **Status:** 302 Found  
  - U¿ytkownik zostaje przekierowany do strony g³ównej przy u¿yciu Cookie Authentication  
- **B³êdy:**  
  - **401 Unauthorized:** Gdy dane logowania (b³êdny Email lub Password) s¹ nieprawid³owe  
  - **400 Bad Request:** W przypadku niekompletnych lub b³êdnych danych wejœciowych  
  - **500 Internal Server Error:** Wyst¹pienie b³êdu w warstwie serwisowej

## 5. Przep³yw danych  
- **GET /Account/Login:**  
  1. U¿ytkownik wysy³a ¿¹danie GET, aby pobraæ formularz logowania.  
  2. Kontroler (`AccountController`) renderuje widok Razor z formularzem logowania.  
- **POST /Account/Login:**  
  1. U¿ytkownik przesy³a dane logowania w formacie JSON.  
  2. Kontroler mapuje dane na `LoginRequestDto` lub bezpoœrednio na `LoginCommand`.  
  3. Dane s¹ przekazywane do `AccountService`, gdzie nastêpuje:  
     - Walidacja danych wejœciowych (m.in. weryfikacja formatu email i obecnoœci has³a)  
     - Weryfikacja istnienia u¿ytkownika w tabeli `Users` oraz porównanie has³a (hash)  
     - Uwzglêdnienie mechanizmu lockout przy kolejnych nieudanych próbach  
  4. Na podstawie wyniku:  
     - Przy sukcesie – u¿ytkownik jest logowany poprzez Cookie Authentication i przekierowywany do strony g³ównej  
     - W przypadku b³êdów – zwracany jest odpowiedni kod b³êdu

## 6. Wzglêdy bezpieczeñstwa  
- Wymóg korzystania z HTTPS do zabezpieczenia transmisji danych  
- Walidacja danych wejœciowych przy u¿yciu atrybutów modeli (np. `[Required]`, `[EmailAddress]`)  
- Implementacja mechanizmów zabezpieczaj¹cych przed atakami brute force (m.in. lockout po okreœlonej liczbie nieudanych prób)  
- U¿ycie bezpiecznej metody przechowywania danych sesyjnych i ciasteczek

## 7. Obs³uga b³êdów  
- **400 Bad Request:**  
  - Zwracany w przypadku braku wymaganych danych lub b³êdnego formatu danych wejœciowych  
- **401 Unauthorized:**  
  - Zwracany, gdy dane logowania s¹ nieprawid³owe, wraz z odpowiednim komunikatem wyjaœniaj¹cym problem autoryzacji  
- **500 Internal Server Error:**  
  - Globalna obs³uga wyj¹tków przy u¿yciu middleware (np. `ExceptionFilter`) oraz logowanie b³êdów przy u¿yciu mechanizmu `ILogger`

## 8. Rozwa¿ania dotycz¹ce wydajnoœci  
- Optymalizacja zapytañ do bazy, w tym indeksowanie kolumny `Email` w tabeli `Users`  
- Minimalizacja operacji w ramach logiki logowania w celu skrócenia czasu odpowiedzi  
- Potencjalne wykorzystanie mechanizmu cache dla statycznych konfiguracji

## 9. Etapy wdro¿enia  
1. **Definicja DTO i Command Model:**  
   - Utworzenie klasy `LoginRequestDto` z w³aœciwoœciami `Email` oraz `Password`  
   - Utworzenie klasy `LoginCommand` przekazuj¹cej dane do warstwy serwisowej  
2. **Implementacja logiki w AccountService:**  
   - Sprawdzenie danych wejœciowych, weryfikacja u¿ytkownika i has³a  
   - Implementacja mechanizmów zabezpieczaj¹cych, takich jak lockout przy nieudanych próbach  
3. **Modyfikacja kontrolera Account:**  
   - Akcja GET renderuj¹ca formularz logowania przy u¿yciu Razor View (`Views/Account/Login.cshtml`)  
   - Akcja POST mapuj¹ca dane na `LoginCommand` i wywo³uj¹ca odpowiedni¹ metodê w `AccountService`  
   - Implementacja obs³ugi b³êdów i odpowiednie zwracanie kodów statusu (400, 401, 500)  
4. **Implementacja widoków:**  
   - Utworzenie lub modyfikacja widoku Razor do logowania (`Views/Account/Login.cshtml`)  
   - Aktualizacja widoku strony g³ównej (np. `Views/Home/Index.cshtml`) w celu prawid³owego przekierowania po logowaniu  
5. **Testowanie:**  
   - Przeprowadzenie testów jednostkowych i integracyjnych dla logiki logowania, obejmuj¹cych scenariusze pozytywne i negatywne  
   - Testowanie mechanizmów zabezpieczaj¹cych, takich jak lockout i bezpieczeñstwo transmisji  
6. **Code Review i wdro¿enie:**  
   - Przeprowadzenie przegl¹du kodu przez zespó³  
   - Wdro¿enie zmian do œrodowiska testowego, a nastêpnie produkcyjnego  
   - Monitorowanie logów i wydajnoœci endpointu po wdro¿eniu