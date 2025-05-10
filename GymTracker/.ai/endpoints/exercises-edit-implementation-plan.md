# API Endpoint Implementation Plan: Edycja �wiczenia

## 1. Przegl�d punktu ko�cowego
Edycja �wiczenia umo�liwia modyfikacj� danych istniej�cego �wiczenia przez zalogowanego u�ytkownika. Zmiany te maj� by� propagowane do historycznych rekord�w treningowych. Operacja odbywa si� w dw�ch etapach: pobranie formularza (GET) oraz zapis zmian (POST).

## 2. Szczeg�y ��dania
- **Metoda HTTP:**
  - GET � wy�wietlenie formularza edycji �wiczenia
  - POST � przyjmowanie i zapis zaktualizowanych danych �wiczenia
- **Struktura URL:** `/Exercises/Edit/{id}`
- **Parametry:**
  - GET: `id` �wiczenia
  - POST: `id` przekazywane w URL, wymagane pola: `Name`, `MuscleGroup`, `DifficultyLevel`; opcjonalne pole: `Description`
- **Request Body:** Dane przes�ane metod� POST (formularz lub JSON)

## 3. Wykorzystywane typy
- `ExerciseEditCommand` � Command model zawieraj�cy: `Id`, `Name`, `MuscleGroup`, `DifficultyLevel`, `Description`
- Opcjonalnie: DTO lub widok potwierdzaj�cy aktualizacj�

## 4. Szczeg�y odpowiedzi
- **200 OK:** Widok potwierdzaj�cy sukces edycji
- **400 Bad Request:** B��d walidacji, np. niepoprawne dane wej�ciowe lub duplikat nazwy
- **401 Unauthorized:** Je�li u�ytkownik nie jest zalogowany
- **404 Not Found:** Je�li �wiczenie o danym `id` nie istnieje
- **500 Internal Server Error:** W przypadku b��d�w serwera

## 5. Przep�yw danych
1. U�ytkownik wysy�a ��danie GET do `/Exercises/Edit/{id}` � formularz edycji jest wy�wietlany z aktualnymi danymi �wiczenia.
2. U�ytkownik wprowadza zmiany i wysy�a ��danie POST.
3. Kontroler/PageModel odbiera dane i przekazuje je do serwisu (np. `ExerciseService.EditExercise`).
4. Serwis waliduje dane:
   - Sprawdzenie zakresu d�ugo�ci i unikalno�ci nazwy (dla bie��cego u�ytkownika).
   - Propagacja zmian do historycznych rekord�w treningowych.
5. Zaktualizowane dane s� zapisywane w bazie danych.
6. U�ytkownik otrzymuje widok potwierdzaj�cy sukces aktualizacji.

## 6. Wzgl�dy bezpiecze�stwa
- Operacja dost�pna wy��cznie dla zalogowanych u�ytkownik�w.
- Weryfikacja, czy edytowane �wiczenie nale�y do bie��cego u�ytkownika.
- Serwerowa walidacja wej�cia, aby zapobiec wstrzykni�ciu nieprawid�owych danych.

## 7. Obs�uga b��d�w
- **400 Bad Request:** B��dy walidacji danych (np. niepoprawna d�ugo�� nazwy lub duplikat).
- **401 Unauthorized:** Dost�p tylko dla autoryzowanych u�ytkownik�w.
- **404 Not Found:** �wiczenie nie znalezione.
- **500 Internal Server Error:** B��dy serwerowe � logowanie i obs�uga wyj�tk�w.

## 8. Rozwa�ania dotycz�ce wydajno�ci
- U�ywanie AsNoTracking() przy odczycie danych w celu optymalizacji.
- Kompilowanie zapyta�, je�li edycja odbywa si� cz�sto.
- U�ycie strategii eager loading dla zwi�zanych danych, je�li wymagane.

## 9. Etapy wdro�enia
1. **Utworzenie Command Modelu:** `ExerciseEditCommand` z w�a�ciwo�ciami: `Id`, `Name`, `MuscleGroup`, `DifficultyLevel`, `Description`.
2. **Implementacja strony Razor:** Utworzy� widok dla `/Exercises/Edit/{id}` z prewype�nionym formularzem.
3. **Implementacja akcji GET i POST:** W dedykowanym PageModel lub kontrolerze.
4. **Rozbudowa serwisu:** Dodanie metody `ExerciseService.EditExercise` zawieraj�cej walidacj� i mechanizm propagacji zmian.
5. **Testy:** Przygotowa� testy jednostkowe oraz integracyjne dla walidacji i zapisu danych.
6. **Widok potwierdzaj�cy:** Wy�wietlenie komunikatu sukcesu lub przekierowanie do listy �wicze�.
