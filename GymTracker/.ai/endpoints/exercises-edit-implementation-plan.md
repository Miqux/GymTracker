# API Endpoint Implementation Plan: Edycja æwiczenia

## 1. Przegl¹d punktu koñcowego
Edycja æwiczenia umo¿liwia modyfikacjê danych istniej¹cego æwiczenia przez zalogowanego u¿ytkownika. Zmiany te maj¹ byæ propagowane do historycznych rekordów treningowych. Operacja odbywa siê w dwóch etapach: pobranie formularza (GET) oraz zapis zmian (POST).

## 2. Szczegó³y ¿¹dania
- **Metoda HTTP:**
  - GET – wyœwietlenie formularza edycji æwiczenia
  - POST – przyjmowanie i zapis zaktualizowanych danych æwiczenia
- **Struktura URL:** `/Exercises/Edit/{id}`
- **Parametry:**
  - GET: `id` æwiczenia
  - POST: `id` przekazywane w URL, wymagane pola: `Name`, `MuscleGroup`, `DifficultyLevel`; opcjonalne pole: `Description`
- **Request Body:** Dane przes³ane metod¹ POST (formularz lub JSON)

## 3. Wykorzystywane typy
- `ExerciseEditCommand` – Command model zawieraj¹cy: `Id`, `Name`, `MuscleGroup`, `DifficultyLevel`, `Description`
- Opcjonalnie: DTO lub widok potwierdzaj¹cy aktualizacjê

## 4. Szczegó³y odpowiedzi
- **200 OK:** Widok potwierdzaj¹cy sukces edycji
- **400 Bad Request:** B³¹d walidacji, np. niepoprawne dane wejœciowe lub duplikat nazwy
- **401 Unauthorized:** Jeœli u¿ytkownik nie jest zalogowany
- **404 Not Found:** Jeœli æwiczenie o danym `id` nie istnieje
- **500 Internal Server Error:** W przypadku b³êdów serwera

## 5. Przep³yw danych
1. U¿ytkownik wysy³a ¿¹danie GET do `/Exercises/Edit/{id}` – formularz edycji jest wyœwietlany z aktualnymi danymi æwiczenia.
2. U¿ytkownik wprowadza zmiany i wysy³a ¿¹danie POST.
3. Kontroler/PageModel odbiera dane i przekazuje je do serwisu (np. `ExerciseService.EditExercise`).
4. Serwis waliduje dane:
   - Sprawdzenie zakresu d³ugoœci i unikalnoœci nazwy (dla bie¿¹cego u¿ytkownika).
   - Propagacja zmian do historycznych rekordów treningowych.
5. Zaktualizowane dane s¹ zapisywane w bazie danych.
6. U¿ytkownik otrzymuje widok potwierdzaj¹cy sukces aktualizacji.

## 6. Wzglêdy bezpieczeñstwa
- Operacja dostêpna wy³¹cznie dla zalogowanych u¿ytkowników.
- Weryfikacja, czy edytowane æwiczenie nale¿y do bie¿¹cego u¿ytkownika.
- Serwerowa walidacja wejœcia, aby zapobiec wstrzykniêciu nieprawid³owych danych.

## 7. Obs³uga b³êdów
- **400 Bad Request:** B³êdy walidacji danych (np. niepoprawna d³ugoœæ nazwy lub duplikat).
- **401 Unauthorized:** Dostêp tylko dla autoryzowanych u¿ytkowników.
- **404 Not Found:** Æwiczenie nie znalezione.
- **500 Internal Server Error:** B³êdy serwerowe – logowanie i obs³uga wyj¹tków.

## 8. Rozwa¿ania dotycz¹ce wydajnoœci
- U¿ywanie AsNoTracking() przy odczycie danych w celu optymalizacji.
- Kompilowanie zapytañ, jeœli edycja odbywa siê czêsto.
- U¿ycie strategii eager loading dla zwi¹zanych danych, jeœli wymagane.

## 9. Etapy wdro¿enia
1. **Utworzenie Command Modelu:** `ExerciseEditCommand` z w³aœciwoœciami: `Id`, `Name`, `MuscleGroup`, `DifficultyLevel`, `Description`.
2. **Implementacja strony Razor:** Utworzyæ widok dla `/Exercises/Edit/{id}` z prewype³nionym formularzem.
3. **Implementacja akcji GET i POST:** W dedykowanym PageModel lub kontrolerze.
4. **Rozbudowa serwisu:** Dodanie metody `ExerciseService.EditExercise` zawieraj¹cej walidacjê i mechanizm propagacji zmian.
5. **Testy:** Przygotowaæ testy jednostkowe oraz integracyjne dla walidacji i zapisu danych.
6. **Widok potwierdzaj¹cy:** Wyœwietlenie komunikatu sukcesu lub przekierowanie do listy æwiczeñ.
