# API Endpoint Implementation Plan: Wy�wietlanie �wicze�

## 1. Przegl�d punktu ko�cowego
Wy�wietlanie listy �wicze� u�ytkownika umo�liwia przegl�danie wszystkich �wicze� (niezablokowanych i historycznych) powi�zanych z zalogowanym u�ytkownikiem. Strona ta jest punktem startowym, z kt�rego u�ytkownik mo�e przechodzi� do operacji dodawania, edycji lub blokowania �wicze�.

## 2. Szczeg�y ��dania
- **Metoda HTTP:** GET
- **Struktura URL:** `/Exercises`
- **Parametry:**
  - Wymagane: Brak
  - Opcjonalne: Filtry (opcjonalnie w przysz�o�ci)
- **Request Body:** Brak

## 3. Wykorzystywane typy
- Opcjonalnie: `ExerciseListDTO` � DTO zawieraj�ce list� �wicze� (np. Id, Name, MuscleGroup, DifficultyLevel, Description, IsBlocked)

## 4. Szczeg�y odpowiedzi
- **200 OK:** Widok z list� �wicze�
- **400 Bad Request:** W przypadku wyst�pienia problemu z operacj� (np. b��d pobierania danych)

## 5. Przep�yw danych
1. U�ytkownik wykonuje ��danie GET do `/Exercises`.
2. Strona Razor wywo�uje metod� w warstwie serwisu (np. `ExerciseService.GetUserExercises`) wykorzystuj�c� EF Core do pobrania �wicze� powi�zanych z zalogowanym u�ytkownikiem (sprawdzaj�c `User.Identity.IsAuthenticated` i Id u�ytkownika z tokena/cookie).
3. Wynik (lista �wicze�) przekazywany jest do widoku.
4. Widok renderuje list� �wicze� wraz z przyciskami do przej�cia do dodawania, edycji oraz blokowania.

## 6. Wzgl�dy bezpiecze�stwa
- Strona dost�pna tylko dla zalogowanych u�ytkownik�w.
- Obs�uga mechanizmu autoryzacji oparta na `User.Identity.IsAuthenticated`.
- Walidacja danych powinna by� wykonywana w serwisie, a dost�p do danych ograniczony do bie��cego u�ytkownika.

## 7. Obs�uga b��d�w
- **401 Unauthorized:** Je�li u�ytkownik nie jest zalogowany.
- **400 Bad Request:** Je�li nast�pi b��d pobierania �wicze�.
- **500 Internal Server Error:** Przy niespodziewanych wyj�tkach � dodatkowe logowanie b��d�w.

## 8. Rozwa�ania dotycz�ce wydajno�ci
- U�ycie AsNoTracking() przy operacjach odczytu w EF Core.
- Opcjonalne cache�owanie listy �wicze� dla zwi�kszenia wydajno�ci przy du�ej liczbie odczyt�w.
- Eager loading powi�zanych danych (je�li wymagane).

## 9. Etapy wdro�enia
1. **Implementacja metody serwisowej** `ExerciseService.GetUserExercises` z ograniczeniem wynik�w do bie��cego u�ytkownika.
2. **Utworzenie strony Razor** pod �cie�k� `/Exercises` wy�wietlaj�cej list� �wicze�.
3. **Dodanie przycisk�w akcji** (dodawanie, edycja, blokowanie) dla ka�dego �wiczenia.
4. **Modyfikacja pliku _Layout.cshtml:** Dodanie przycisku/linku do listy �wicze� widocznego tylko dla zalogowanych.
5. **Testy jednostkowe i integracyjne:** Sprawdzenie poprawno�ci danych oraz zabezpiecze�.

### Dodatkowa modyfikacja w _Layout.cshtml
Dodaj poni�szy fragment do sekcji menu lub po zalogowaniu:
@if (User.Identity.IsAuthenticated) { <li class="nav-item"> <a class="nav-link text-dark" href="/Exercises">Moje �wiczenia</a> </li> }