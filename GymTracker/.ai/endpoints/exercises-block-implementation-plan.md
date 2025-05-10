# API Endpoint Implementation Plan: Blokowanie �wiczenia

## 1. Przegl�d punktu ko�cowego
Blokowanie �wiczenia umo�liwia oznaczenie wybranego �wiczenia jako zablokowanego, co uniemo�liwia jego u�ycie w nowych treningach, ale pozostawia widoczno�� w historii. Operacja ta dotyczy tylko �wicze� nale��cych do bie��cego u�ytkownika.

## 2. Szczeg�y ��dania
- **Metoda HTTP:** POST
- **Struktura URL:** `/Exercises/Block/{id}`
- **Parametry:**
  - Wymagane: `id` �wiczenia (przekazywane w adresie URL)
- **Request Body:** Minimalny � identyfikator �wiczenia jest cz�ci� URL

## 3. Wykorzystywane typy
- `ExerciseBlockCommand` � zawiera w�a�ciwo�� `Id`
- Opcjonalnie: DTO komunikatu sukcesu lub widok potwierdzaj�cy

## 4. Szczeg�y odpowiedzi
- **200 OK:** �wiczenie zosta�o pomy�lnie zablokowane; widok potwierdzaj�cy lub przekierowanie.
- **400 Bad Request:** Je�li wyst�pi problem z operacj� (np. �wiczenie nie istnieje lub nie nale�y do u�ytkownika).
- **401 Unauthorized:** Je�li u�ytkownik nie jest zalogowany.
- **404 Not Found:** Je�li �wiczenie o podanym identyfikatorze nie istnieje.

## 5. Przep�yw danych
1. U�ytkownik wysy�a ��danie POST do `/Exercises/Block/{id}`.
2. Metoda odbiera identyfikator �wiczenia.
3. Warstwa serwisowa (np. `ExerciseService.BlockExercise`) sprawdza, czy �wiczenie nale�y do bie��cego u�ytkownika.
4. Je�li walidacja przejdzie, ustawiana jest flaga `IsBlocked` na `true` w bazie danych.
5. Po operacji nast�puje przekierowanie lub wy�wietlenie widoku potwierdzaj�cego.

## 6. Wzgl�dy bezpiecze�stwa
- Operacja dost�pna tylko dla zalogowanych u�ytkownik�w.
- Weryfikacja, czy �wiczenie nale��ce do u�ytkownika � blokowanie mo�e by� wykonane tylko dla w�asnych �wicze�.
- Walidacja wej�cia w celu zapobiegania atakom typu injection.

## 7. Obs�uga b��d�w
- **400 Bad Request:** B��dy walidacji (�wiczenie nie nale�y do u�ytkownika, nie znalezione pole Id).
- **401 Unauthorized:** Dost�p do operacji tylko dla zalogowanych.
- **404 Not Found:** �wiczenie o podanym identyfikatorze nie istnieje.
- **500 Internal Server Error:** B��dy serwerowe � logowanie b��d�w.

## 8. Rozwa�ania dotycz�ce wydajno�ci
- Optymalizacja zapytania do bazy � u�ycie AsNoTracking() w operacjach odczytu, a nast�pnie zmiana stanu encji.
- Minimalizacja operacji na bazie danych poprzez jednorazowe wykonanie aktualizacji flagi.

## 9. Etapy wdro�enia
1. **Utworzenie Command Modelu:** `ExerciseBlockCommand` z w�a�ciwo�ci� `Id`.
2. **Implementacja akcji POST** w dedykowanym PageModel lub kontrolerze.
3. **Rozbudowa serwisu:** Dodanie metody `ExerciseService.BlockExercise` z walidacj� u�ytkownika.
4. **Aktualizacja bazy danych:** Zmiana flagi `IsBlocked` na `true` dla danego �wiczenia.
5. **Widok potwierdzaj�cy:** Wy�wietlenie komunikatu sukcesu lub przekierowanie na stron� listy �wicze�.
6. **Testy i walidacja:** Sprawdzenie scenariuszy b��d�w oraz w�a�ciwych odpowiedzi API.