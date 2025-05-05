
# API Endpoint Implementation Plan: Login Endpoint

## 1. Przegl�d punktu ko�cowego  
Endpoint `/Account/Login` s�u�y do logowania u�ytkownik�w aplikacji MVC. Formularz logowania jest pobierany poprzez metod� GET, a dane logowania (Email i Password) wysy�ane s� metod� POST. W przypadku poprawnego uwierzytelnienia, u�ytkownik jest logowany przy u�yciu mechanizmu Cookie Authentication i przekierowywany do strony g��wnej.

## 2. Szczeg�y ��dania  
- **Metody HTTP:**  
  - GET � renderuje formularz logowania (widok Razor)  
  - POST � przetwarza dane logowania  
- **Struktura URL:**  
  `/Account/Login`  
- **Parametry:**  
  - **Wymagane (POST):**  
    - `Email` (string)  
    - `Password` (string)  
- **Request Body (dla POST):**
## 3. Wykorzystywane typy  
- **DTO:**  
  - `LoginRequestDto` � zawiera w�a�ciwo�ci `Email` oraz `Password`  
- **Command Model:**  
  - `LoginCommand` � model przekazuj�cy dane logowania do warstwy serwisowej

## 4. Szczeg�y odpowiedzi  
- **GET /Account/Login:**  
  - **Status:** 200 OK  
  - Widok renderowany przy u�yciu Razor (np. `Views/Account/Login.cshtml`) prezentuj�cy formularz logowania  
- **POST /Account/Login (sukces):**  
  - **Status:** 302 Found  
  - U�ytkownik zostaje przekierowany do strony g��wnej przy u�yciu Cookie Authentication  
- **B��dy:**  
  - **401 Unauthorized:** Gdy dane logowania (b��dny Email lub Password) s� nieprawid�owe  
  - **400 Bad Request:** W przypadku niekompletnych lub b��dnych danych wej�ciowych  
  - **500 Internal Server Error:** Wyst�pienie b��du w warstwie serwisowej

## 5. Przep�yw danych  
- **GET /Account/Login:**  
  1. U�ytkownik wysy�a ��danie GET, aby pobra� formularz logowania.  
  2. Kontroler (`AccountController`) renderuje widok Razor z formularzem logowania.  
- **POST /Account/Login:**  
  1. U�ytkownik przesy�a dane logowania w formacie JSON.  
  2. Kontroler mapuje dane na `LoginRequestDto` lub bezpo�rednio na `LoginCommand`.  
  3. Dane s� przekazywane do `AccountService`, gdzie nast�puje:  
     - Walidacja danych wej�ciowych (m.in. weryfikacja formatu email i obecno�ci has�a)  
     - Weryfikacja istnienia u�ytkownika w tabeli `Users` oraz por�wnanie has�a (hash)  
     - Uwzgl�dnienie mechanizmu lockout przy kolejnych nieudanych pr�bach  
  4. Na podstawie wyniku:  
     - Przy sukcesie � u�ytkownik jest logowany poprzez Cookie Authentication i przekierowywany do strony g��wnej  
     - W przypadku b��d�w � zwracany jest odpowiedni kod b��du

## 6. Wzgl�dy bezpiecze�stwa  
- Wym�g korzystania z HTTPS do zabezpieczenia transmisji danych  
- Walidacja danych wej�ciowych przy u�yciu atrybut�w modeli (np. `[Required]`, `[EmailAddress]`)  
- Implementacja mechanizm�w zabezpieczaj�cych przed atakami brute force (m.in. lockout po okre�lonej liczbie nieudanych pr�b)  
- U�ycie bezpiecznej metody przechowywania danych sesyjnych i ciasteczek

## 7. Obs�uga b��d�w  
- **400 Bad Request:**  
  - Zwracany w przypadku braku wymaganych danych lub b��dnego formatu danych wej�ciowych  
- **401 Unauthorized:**  
  - Zwracany, gdy dane logowania s� nieprawid�owe, wraz z odpowiednim komunikatem wyja�niaj�cym problem autoryzacji  
- **500 Internal Server Error:**  
  - Globalna obs�uga wyj�tk�w przy u�yciu middleware (np. `ExceptionFilter`) oraz logowanie b��d�w przy u�yciu mechanizmu `ILogger`

## 8. Rozwa�ania dotycz�ce wydajno�ci  
- Optymalizacja zapyta� do bazy, w tym indeksowanie kolumny `Email` w tabeli `Users`  
- Minimalizacja operacji w ramach logiki logowania w celu skr�cenia czasu odpowiedzi  
- Potencjalne wykorzystanie mechanizmu cache dla statycznych konfiguracji

## 9. Etapy wdro�enia  
1. **Definicja DTO i Command Model:**  
   - Utworzenie klasy `LoginRequestDto` z w�a�ciwo�ciami `Email` oraz `Password`  
   - Utworzenie klasy `LoginCommand` przekazuj�cej dane do warstwy serwisowej  
2. **Implementacja logiki w AccountService:**  
   - Sprawdzenie danych wej�ciowych, weryfikacja u�ytkownika i has�a  
   - Implementacja mechanizm�w zabezpieczaj�cych, takich jak lockout przy nieudanych pr�bach  
3. **Modyfikacja kontrolera Account:**  
   - Akcja GET renderuj�ca formularz logowania przy u�yciu Razor View (`Views/Account/Login.cshtml`)  
   - Akcja POST mapuj�ca dane na `LoginCommand` i wywo�uj�ca odpowiedni� metod� w `AccountService`  
   - Implementacja obs�ugi b��d�w i odpowiednie zwracanie kod�w statusu (400, 401, 500)  
4. **Implementacja widok�w:**  
   - Utworzenie lub modyfikacja widoku Razor do logowania (`Views/Account/Login.cshtml`)  
   - Aktualizacja widoku strony g��wnej (np. `Views/Home/Index.cshtml`) w celu prawid�owego przekierowania po logowaniu  
5. **Testowanie:**  
   - Przeprowadzenie test�w jednostkowych i integracyjnych dla logiki logowania, obejmuj�cych scenariusze pozytywne i negatywne  
   - Testowanie mechanizm�w zabezpieczaj�cych, takich jak lockout i bezpiecze�stwo transmisji  
6. **Code Review i wdro�enie:**  
   - Przeprowadzenie przegl�du kodu przez zesp�  
   - Wdro�enie zmian do �rodowiska testowego, a nast�pnie produkcyjnego  
   - Monitorowanie log�w i wydajno�ci endpointu po wdro�eniu