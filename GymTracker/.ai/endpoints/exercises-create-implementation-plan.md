# API Endpoint Implementation Plan: Dodawanie �wiczenia

## 1. Przegl�d punktu ko�cowego
Funkcjonalno�� dodawania �wiczenia pozwala zalogowanemu u�ytkownikowi na utworzenie nowego �wiczenia, przy czym walidacja obejmuje unikalno�� nazwy w obr�bie u�ytkownika oraz ograniczenie d�ugo�ci nazwy (3-150 znak�w).

## 2. Szczeg�y ��dania
- **Metoda HTTP:**
  - GET � wy�wietlenie formularza dodawania �wiczenia
  - POST � pobranie danych z formularza i zapis nowego �wiczenia
- **Struktura URL:** `/Exercises/Create`
- **Parametry:**
  - GET: brak
  - POST: 
    - Wymagane: `Name` (3-150 znak�w, unikalna dla u�ytkownika), `MuscleGroup`, `DifficultyLevel`
    - Opcjonalne: `Description`
- **Request Body:** Dane �wiczenia przes�ane przez formularz (np. jako form-data)

## 3. Wykorzystywane typy
- `ExerciseCreateCommand` � Command model zawieraj�cy: `Name`, `MuscleGroup`, `DifficultyLevel`, `Description`
- Opcjonalnie: DTO potwierdzaj�ce dodanie (np. komunikat sukcesu)

## 4. Szczeg�y odpowiedzi
- **200 OK:** Widok potwierdzaj�cy dodanie �wiczenia
- **201 Created:** Alternatywnie, w przypadku RESTowe podej�cie
- **400 Bad Request:** Gdy walidacja danych zawiedzie (np. zbyt kr�tka/d�uga nazwa, duplikat)

## 5. Przep�yw danych
1. U�ytkownik wysy�a ��danie GET do `/Exercises/Create`, a formularz zostaje wyrenderowany.
2. U�ytkownik wype�nia formularz i zatwierdza dane za pomoc� POST.
3. Formularz przesy�a dane do backendu.
4. Kontroler odbiera dane i przekazuje je do serwisu (np. `ExerciseService.CreateExercise`), kt�ry:
   - Sprawdza poprawno�� danych (d�ugo�� nazwy, unikalno�� dla danego `UserId`).
   - Zapisuje nowe �wiczenie w bazie (INSERT do tabeli Exercises).
5. Po udanej operacji u�ytkownik otrzymuje widok potwierdzaj�cy lub komunikat sukcesu.

## 6. Wzgl�dy bezpiecze�stwa
- Dost�p do formularza i operacji POST tylko dla zalogowanych u�ytkownik�w.
- Walidacja danych po stronie serwera, aby zabezpieczy� przed atakami.
- Sprawdzenie, czy `UserId` �wiczenia odpowiada bie��cemu u�ytkownikowi.

## 7. Obs�uga b��d�w
- **400 Bad Request:** Gdy walidacja danych (np. d�ugo�� nazwy, unikalno��) si� nie powiedzie.
- **401 Unauthorized:** Dost�p do operacji tylko dla zalogowanych.
- **500 Internal Server Error:** Przy nieoczekiwanych b��dach � logowanie b��d�w z u�yciem ILogger.

## 8. Rozwa�ania dotycz�ce wydajno�ci
- U�ywanie AsNoTracking() podczas walidacji unikalno�ci.
- Efektywne zapytania EF Core przy wyszukiwaniu duplikat�w.
- Kompilowane zapytania, je�li operacja jest wykonywana cz�sto.

## 9. Etapy wdro�enia
1. **Utworzenie Command Modelu:** `ExerciseCreateCommand`.
2. **Implementacja strony Razor:** Utworzy� widok formularza pod `/Exercises/Create`.
3. **Implementacja akcji GET i POST** w dedykowanym kontrolerze lub PageModel (w Razor Pages).
4. **Rozbudowa serwisu:** Dodanie metody `ExerciseService.CreateExercise` obs�uguj�cej walidacj� i zapis.
5. **Walidacja danych:** Weryfikacja poprawno�ci p�l oraz unikalno�ci na poziomie bazy.
6. **Testy:** Przygotowanie test�w jednostkowych sprawdzaj�cych walidacj� i zapis.
7. **Widok potwierdzaj�cy:** Po udanym dodaniu wy�wietli� komunikat lub przekierowa� na list� �wicze�.