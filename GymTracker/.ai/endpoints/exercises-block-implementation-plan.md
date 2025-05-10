# API Endpoint Implementation Plan: Blokowanie æwiczenia

## 1. Przegl¹d punktu koñcowego
Blokowanie æwiczenia umo¿liwia oznaczenie wybranego æwiczenia jako zablokowanego, co uniemo¿liwia jego u¿ycie w nowych treningach, ale pozostawia widocznoœæ w historii. Operacja ta dotyczy tylko æwiczeñ nale¿¹cych do bie¿¹cego u¿ytkownika.

## 2. Szczegó³y ¿¹dania
- **Metoda HTTP:** POST
- **Struktura URL:** `/Exercises/Block/{id}`
- **Parametry:**
  - Wymagane: `id` æwiczenia (przekazywane w adresie URL)
- **Request Body:** Minimalny – identyfikator æwiczenia jest czêœci¹ URL

## 3. Wykorzystywane typy
- `ExerciseBlockCommand` – zawiera w³aœciwoœæ `Id`
- Opcjonalnie: DTO komunikatu sukcesu lub widok potwierdzaj¹cy

## 4. Szczegó³y odpowiedzi
- **200 OK:** Æwiczenie zosta³o pomyœlnie zablokowane; widok potwierdzaj¹cy lub przekierowanie.
- **400 Bad Request:** Jeœli wyst¹pi problem z operacj¹ (np. æwiczenie nie istnieje lub nie nale¿y do u¿ytkownika).
- **401 Unauthorized:** Jeœli u¿ytkownik nie jest zalogowany.
- **404 Not Found:** Jeœli æwiczenie o podanym identyfikatorze nie istnieje.

## 5. Przep³yw danych
1. U¿ytkownik wysy³a ¿¹danie POST do `/Exercises/Block/{id}`.
2. Metoda odbiera identyfikator æwiczenia.
3. Warstwa serwisowa (np. `ExerciseService.BlockExercise`) sprawdza, czy æwiczenie nale¿y do bie¿¹cego u¿ytkownika.
4. Jeœli walidacja przejdzie, ustawiana jest flaga `IsBlocked` na `true` w bazie danych.
5. Po operacji nastêpuje przekierowanie lub wyœwietlenie widoku potwierdzaj¹cego.

## 6. Wzglêdy bezpieczeñstwa
- Operacja dostêpna tylko dla zalogowanych u¿ytkowników.
- Weryfikacja, czy æwiczenie nale¿¹ce do u¿ytkownika – blokowanie mo¿e byæ wykonane tylko dla w³asnych æwiczeñ.
- Walidacja wejœcia w celu zapobiegania atakom typu injection.

## 7. Obs³uga b³êdów
- **400 Bad Request:** B³êdy walidacji (æwiczenie nie nale¿y do u¿ytkownika, nie znalezione pole Id).
- **401 Unauthorized:** Dostêp do operacji tylko dla zalogowanych.
- **404 Not Found:** Æwiczenie o podanym identyfikatorze nie istnieje.
- **500 Internal Server Error:** B³êdy serwerowe – logowanie b³êdów.

## 8. Rozwa¿ania dotycz¹ce wydajnoœci
- Optymalizacja zapytania do bazy – u¿ycie AsNoTracking() w operacjach odczytu, a nastêpnie zmiana stanu encji.
- Minimalizacja operacji na bazie danych poprzez jednorazowe wykonanie aktualizacji flagi.

## 9. Etapy wdro¿enia
1. **Utworzenie Command Modelu:** `ExerciseBlockCommand` z w³aœciwoœci¹ `Id`.
2. **Implementacja akcji POST** w dedykowanym PageModel lub kontrolerze.
3. **Rozbudowa serwisu:** Dodanie metody `ExerciseService.BlockExercise` z walidacj¹ u¿ytkownika.
4. **Aktualizacja bazy danych:** Zmiana flagi `IsBlocked` na `true` dla danego æwiczenia.
5. **Widok potwierdzaj¹cy:** Wyœwietlenie komunikatu sukcesu lub przekierowanie na stronê listy æwiczeñ.
6. **Testy i walidacja:** Sprawdzenie scenariuszy b³êdów oraz w³aœciwych odpowiedzi API.