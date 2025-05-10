# API Endpoint Implementation Plan: Wy�wietlanie trening�w

## 1. Przegl�d punktu ko�cowego
Endpoint umo�liwia wy�wietlenie pe�nych informacji o wybranym treningu. U�ytkownik przechodzi do szczeg��w treningu z listy (np. z historii trening�w), gdzie widzi wszystkie dane dotycz�ce treningu, w tym dat�, �wiczenia, serie, powt�rzenia oraz ewentualnie u�yty ci�ar.

## 2. Szczeg�y ��dania
- **Metoda HTTP:** GET  
- **Struktura URL:** /Workouts/{id}
- **Parametry zapytania:**  
  - `id` (int): id treningu do wy�wietlenia  
- **�adunek ��dania:** Brak (parametr `id` zawarty jest w �cie�ce URL)

## 3. Wykorzystywane typy
- **View Model:** Odpowiedni model zawieraj�cy pola:
  - `Id` (int)
  - `Date` (DateTime)
  - `Exercises` (kolekcja obiekt�w zawieraj�cych szczeg�owe informacje o �wiczeniach, np. ExerciseName, Sets, Reps, Weight)

## 4. Szczeg�y odpowiedzi
- **200 OK:** Widok zawieraj�cy wszystkie szczeg�y wybranego treningu, poprawnie zmapowany do widocznego na stronie formatu.
- **400 Bad Request:** W przypadku np. nieprawid�owego id lub b��d�w w przetwarzaniu danych.

## 5. Przep�yw danych
1. U�ytkownik wybiera konkretny trening z listy (np. widoku historii trening�w).
2. ��danie GET kierowane jest na `/Workouts/{id}`.
3. Kontroler (lub PageModel) odbiera parametr `id` i wywo�uje metod� w warstwie serwisowej, np. `WorkoutService.GetWorkoutDetails(id)`.
4. Serwis pobiera dane treningu z bazy danych przy u�yciu EF Core oraz mapuje je do odpowiedniego View Modelu.
5. Je�li dane zostan� poprawnie zmapowane, aplikacja zwraca widok z pe�nym opisem treningu.
6. W przypadku problem�w (np. b��dnego `id`), zwracany jest status 400 Bad Request wraz z komunikatem b��du.

## 6. Wzgl�dy bezpiecze�stwa
- Endpoint dost�pny tylko dla zalogowanych u�ytkownik�w.
- Sprawdzenie, czy trening nale�y do bie��cego u�ytkownika, aby zapobiec wy�wietlaniu informacji trening�w innych u�ytkownik�w.
- Walidacja poprawno�ci przekazanego parametru `id` w celu ochrony przed wstrzykni�ciem nieprawid�owych danych.

## 7. Obs�uga b��d�w
- **400 Bad Request:** Wy�wietlenie komunikatu o b��dzie w przypadku nieprawid�owego identyfikatora treningu lub b��du podczas pobierania danych.
- **401 Unauthorized:** W przypadku pr�by dost�pu bez autoryzacji.
- Dodatkowe logowanie b��d�w przy pomocy mechanizmu ILogger w celu monitorowania ewentualnych problem�w.

## 8. Etapy wdro�enia
1. Utworzenie View Modelu dla szczeg��w treningu (np. `WorkoutDetailsViewModel`).
2. Implementacja metody w serwisie (`WorkoutService.GetWorkoutDetails`) odpowiedzialnej za pobranie danych na podstawie id treningu.
3. Implementacja akcji GET w kontrolerze lub PageModel dla `/Workouts/{id}`.
4. Utworzenie widoku (Razor Page lub Partial View) prezentuj�cego szczeg�y treningu.
5. Testy jednostkowe oraz integracyjne weryfikuj�ce poprawno�� pobierania oraz wy�wietlania danych treningu.
6. Aktualizacja mechanizm�w autoryzacji w przypadku wykrycia b��d�w bezpiecze�stwa.