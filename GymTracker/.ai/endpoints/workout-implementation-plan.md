# API Endpoint Implementation Plan: Wyœwietlanie treningów

## 1. Przegl¹d punktu koñcowego
Endpoint umo¿liwia wyœwietlenie pe³nych informacji o wybranym treningu. U¿ytkownik przechodzi do szczegó³ów treningu z listy (np. z historii treningów), gdzie widzi wszystkie dane dotycz¹ce treningu, w tym datê, æwiczenia, serie, powtórzenia oraz ewentualnie u¿yty ciê¿ar.

## 2. Szczegó³y ¿¹dania
- **Metoda HTTP:** GET  
- **Struktura URL:** /Workouts/{id}
- **Parametry zapytania:**  
  - `id` (int): id treningu do wyœwietlenia  
- **£adunek ¿¹dania:** Brak (parametr `id` zawarty jest w œcie¿ce URL)

## 3. Wykorzystywane typy
- **View Model:** Odpowiedni model zawieraj¹cy pola:
  - `Id` (int)
  - `Date` (DateTime)
  - `Exercises` (kolekcja obiektów zawieraj¹cych szczegó³owe informacje o æwiczeniach, np. ExerciseName, Sets, Reps, Weight)

## 4. Szczegó³y odpowiedzi
- **200 OK:** Widok zawieraj¹cy wszystkie szczegó³y wybranego treningu, poprawnie zmapowany do widocznego na stronie formatu.
- **400 Bad Request:** W przypadku np. nieprawid³owego id lub b³êdów w przetwarzaniu danych.

## 5. Przep³yw danych
1. U¿ytkownik wybiera konkretny trening z listy (np. widoku historii treningów).
2. ¯¹danie GET kierowane jest na `/Workouts/{id}`.
3. Kontroler (lub PageModel) odbiera parametr `id` i wywo³uje metodê w warstwie serwisowej, np. `WorkoutService.GetWorkoutDetails(id)`.
4. Serwis pobiera dane treningu z bazy danych przy u¿yciu EF Core oraz mapuje je do odpowiedniego View Modelu.
5. Jeœli dane zostan¹ poprawnie zmapowane, aplikacja zwraca widok z pe³nym opisem treningu.
6. W przypadku problemów (np. b³êdnego `id`), zwracany jest status 400 Bad Request wraz z komunikatem b³êdu.

## 6. Wzglêdy bezpieczeñstwa
- Endpoint dostêpny tylko dla zalogowanych u¿ytkowników.
- Sprawdzenie, czy trening nale¿y do bie¿¹cego u¿ytkownika, aby zapobiec wyœwietlaniu informacji treningów innych u¿ytkowników.
- Walidacja poprawnoœci przekazanego parametru `id` w celu ochrony przed wstrzykniêciem nieprawid³owych danych.

## 7. Obs³uga b³êdów
- **400 Bad Request:** Wyœwietlenie komunikatu o b³êdzie w przypadku nieprawid³owego identyfikatora treningu lub b³êdu podczas pobierania danych.
- **401 Unauthorized:** W przypadku próby dostêpu bez autoryzacji.
- Dodatkowe logowanie b³êdów przy pomocy mechanizmu ILogger w celu monitorowania ewentualnych problemów.

## 8. Etapy wdro¿enia
1. Utworzenie View Modelu dla szczegó³ów treningu (np. `WorkoutDetailsViewModel`).
2. Implementacja metody w serwisie (`WorkoutService.GetWorkoutDetails`) odpowiedzialnej za pobranie danych na podstawie id treningu.
3. Implementacja akcji GET w kontrolerze lub PageModel dla `/Workouts/{id}`.
4. Utworzenie widoku (Razor Page lub Partial View) prezentuj¹cego szczegó³y treningu.
5. Testy jednostkowe oraz integracyjne weryfikuj¹ce poprawnoœæ pobierania oraz wyœwietlania danych treningu.
6. Aktualizacja mechanizmów autoryzacji w przypadku wykrycia b³êdów bezpieczeñstwa.