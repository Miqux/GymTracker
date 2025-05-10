# API Endpoint Implementation Plan: Dodawanie æwiczenia

## 1. Przegl¹d punktu koñcowego
Funkcjonalnoœæ dodawania æwiczenia pozwala zalogowanemu u¿ytkownikowi na utworzenie nowego æwiczenia, przy czym walidacja obejmuje unikalnoœæ nazwy w obrêbie u¿ytkownika oraz ograniczenie d³ugoœci nazwy (3-150 znaków).

## 2. Szczegó³y ¿¹dania
- **Metoda HTTP:**
  - GET – wyœwietlenie formularza dodawania æwiczenia
  - POST – pobranie danych z formularza i zapis nowego æwiczenia
- **Struktura URL:** `/Exercises/Create`
- **Parametry:**
  - GET: brak
  - POST: 
    - Wymagane: `Name` (3-150 znaków, unikalna dla u¿ytkownika), `MuscleGroup`, `DifficultyLevel`
    - Opcjonalne: `Description`
- **Request Body:** Dane æwiczenia przes³ane przez formularz (np. jako form-data)

## 3. Wykorzystywane typy
- `ExerciseCreateCommand` – Command model zawieraj¹cy: `Name`, `MuscleGroup`, `DifficultyLevel`, `Description`
- Opcjonalnie: DTO potwierdzaj¹ce dodanie (np. komunikat sukcesu)

## 4. Szczegó³y odpowiedzi
- **200 OK:** Widok potwierdzaj¹cy dodanie æwiczenia
- **201 Created:** Alternatywnie, w przypadku RESTowe podejœcie
- **400 Bad Request:** Gdy walidacja danych zawiedzie (np. zbyt krótka/d³uga nazwa, duplikat)

## 5. Przep³yw danych
1. U¿ytkownik wysy³a ¿¹danie GET do `/Exercises/Create`, a formularz zostaje wyrenderowany.
2. U¿ytkownik wype³nia formularz i zatwierdza dane za pomoc¹ POST.
3. Formularz przesy³a dane do backendu.
4. Kontroler odbiera dane i przekazuje je do serwisu (np. `ExerciseService.CreateExercise`), który:
   - Sprawdza poprawnoœæ danych (d³ugoœæ nazwy, unikalnoœæ dla danego `UserId`).
   - Zapisuje nowe æwiczenie w bazie (INSERT do tabeli Exercises).
5. Po udanej operacji u¿ytkownik otrzymuje widok potwierdzaj¹cy lub komunikat sukcesu.

## 6. Wzglêdy bezpieczeñstwa
- Dostêp do formularza i operacji POST tylko dla zalogowanych u¿ytkowników.
- Walidacja danych po stronie serwera, aby zabezpieczyæ przed atakami.
- Sprawdzenie, czy `UserId` æwiczenia odpowiada bie¿¹cemu u¿ytkownikowi.

## 7. Obs³uga b³êdów
- **400 Bad Request:** Gdy walidacja danych (np. d³ugoœæ nazwy, unikalnoœæ) siê nie powiedzie.
- **401 Unauthorized:** Dostêp do operacji tylko dla zalogowanych.
- **500 Internal Server Error:** Przy nieoczekiwanych b³êdach – logowanie b³êdów z u¿yciem ILogger.

## 8. Rozwa¿ania dotycz¹ce wydajnoœci
- U¿ywanie AsNoTracking() podczas walidacji unikalnoœci.
- Efektywne zapytania EF Core przy wyszukiwaniu duplikatów.
- Kompilowane zapytania, jeœli operacja jest wykonywana czêsto.

## 9. Etapy wdro¿enia
1. **Utworzenie Command Modelu:** `ExerciseCreateCommand`.
2. **Implementacja strony Razor:** Utworzyæ widok formularza pod `/Exercises/Create`.
3. **Implementacja akcji GET i POST** w dedykowanym kontrolerze lub PageModel (w Razor Pages).
4. **Rozbudowa serwisu:** Dodanie metody `ExerciseService.CreateExercise` obs³uguj¹cej walidacjê i zapis.
5. **Walidacja danych:** Weryfikacja poprawnoœci pól oraz unikalnoœci na poziomie bazy.
6. **Testy:** Przygotowanie testów jednostkowych sprawdzaj¹cych walidacjê i zapis.
7. **Widok potwierdzaj¹cy:** Po udanym dodaniu wyœwietliæ komunikat lub przekierowaæ na listê æwiczeñ.