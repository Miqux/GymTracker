# API Endpoint Implementation Plan: Wyœwietlanie æwiczeñ

## 1. Przegl¹d punktu koñcowego
Wyœwietlanie listy æwiczeñ u¿ytkownika umo¿liwia przegl¹danie wszystkich æwiczeñ (niezablokowanych i historycznych) powi¹zanych z zalogowanym u¿ytkownikiem. Strona ta jest punktem startowym, z którego u¿ytkownik mo¿e przechodziæ do operacji dodawania, edycji lub blokowania æwiczeñ.

## 2. Szczegó³y ¿¹dania
- **Metoda HTTP:** GET
- **Struktura URL:** `/Exercises`
- **Parametry:**
  - Wymagane: Brak
  - Opcjonalne: Filtry (opcjonalnie w przysz³oœci)
- **Request Body:** Brak

## 3. Wykorzystywane typy
- Opcjonalnie: `ExerciseListDTO` – DTO zawieraj¹ce listê æwiczeñ (np. Id, Name, MuscleGroup, DifficultyLevel, Description, IsBlocked)

## 4. Szczegó³y odpowiedzi
- **200 OK:** Widok z list¹ æwiczeñ
- **400 Bad Request:** W przypadku wyst¹pienia problemu z operacj¹ (np. b³¹d pobierania danych)

## 5. Przep³yw danych
1. U¿ytkownik wykonuje ¿¹danie GET do `/Exercises`.
2. Strona Razor wywo³uje metodê w warstwie serwisu (np. `ExerciseService.GetUserExercises`) wykorzystuj¹c¹ EF Core do pobrania æwiczeñ powi¹zanych z zalogowanym u¿ytkownikiem (sprawdzaj¹c `User.Identity.IsAuthenticated` i Id u¿ytkownika z tokena/cookie).
3. Wynik (lista æwiczeñ) przekazywany jest do widoku.
4. Widok renderuje listê æwiczeñ wraz z przyciskami do przejœcia do dodawania, edycji oraz blokowania.

## 6. Wzglêdy bezpieczeñstwa
- Strona dostêpna tylko dla zalogowanych u¿ytkowników.
- Obs³uga mechanizmu autoryzacji oparta na `User.Identity.IsAuthenticated`.
- Walidacja danych powinna byæ wykonywana w serwisie, a dostêp do danych ograniczony do bie¿¹cego u¿ytkownika.

## 7. Obs³uga b³êdów
- **401 Unauthorized:** Jeœli u¿ytkownik nie jest zalogowany.
- **400 Bad Request:** Jeœli nast¹pi b³¹d pobierania æwiczeñ.
- **500 Internal Server Error:** Przy niespodziewanych wyj¹tkach – dodatkowe logowanie b³êdów.

## 8. Rozwa¿ania dotycz¹ce wydajnoœci
- U¿ycie AsNoTracking() przy operacjach odczytu w EF Core.
- Opcjonalne cache’owanie listy æwiczeñ dla zwiêkszenia wydajnoœci przy du¿ej liczbie odczytów.
- Eager loading powi¹zanych danych (jeœli wymagane).

## 9. Etapy wdro¿enia
1. **Implementacja metody serwisowej** `ExerciseService.GetUserExercises` z ograniczeniem wyników do bie¿¹cego u¿ytkownika.
2. **Utworzenie strony Razor** pod œcie¿k¹ `/Exercises` wyœwietlaj¹cej listê æwiczeñ.
3. **Dodanie przycisków akcji** (dodawanie, edycja, blokowanie) dla ka¿dego æwiczenia.
4. **Modyfikacja pliku _Layout.cshtml:** Dodanie przycisku/linku do listy æwiczeñ widocznego tylko dla zalogowanych.
5. **Testy jednostkowe i integracyjne:** Sprawdzenie poprawnoœci danych oraz zabezpieczeñ.

### Dodatkowa modyfikacja w _Layout.cshtml
Dodaj poni¿szy fragment do sekcji menu lub po zalogowaniu:
@if (User.Identity.IsAuthenticated) { <li class="nav-item"> <a class="nav-link text-dark" href="/Exercises">Moje æwiczenia</a> </li> }