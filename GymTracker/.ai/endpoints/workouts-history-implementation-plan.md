# API Endpoint Implementation Plan: Przegl¹danie historii treningów

## 1. Przegl¹d punktu koñcowego
Celem tego endpointu jest wyœwietlenie listy treningów zalogowanego u¿ytkownika w trybie tylko do odczytu. Umo¿liwia on filtrowanie oraz paginacjê wyników, dziêki czemu u¿ytkownik mo¿e przeszukiwaæ historiê treningów.

## 2. Szczegó³y ¿¹dania
- **Metoda HTTP:** GET
- **Struktura URL:** /Workouts/History
- **Parametry:**
  - Opcjonalne: 
    - `page` (int): numer strony
    - `pageSize` (int): liczba rekordów na stronê
    - `dateFrom` i `dateTo` (YYYY-MM-DD): zakres dat
- **Request Body:** Brak

## 3. Wykorzystywane typy
- **DTO:** `WorkoutHistoryDTO` (opcjonalnie) – zawiera w³aœciwoœci: Id, Date, Notes oraz dodatkowe informacje do paginacji.

## 4. Szczegó³y odpowiedzi
- **200 OK:** Widok prezentuj¹cy listê treningów (punktacja oraz paginacja wyników).
- **400 Bad Request:** W przypadku b³êdnych parametrów zapytania.
- **401 Unauthorized:** Gdy u¿ytkownik nie jest zalogowany.
- **500 Internal Server Error:** Przy nieoczekiwanych b³êdach.

## 5. Przep³yw danych
1. U¿ytkownik zalogowany wysy³a ¿¹danie GET do `/Workouts/History` (ewentualnie z parametrami filtrowania/paginacji).
2. Kontroler lub PageModel sprawdza autoryzacjê (User.Identity.IsAuthenticated) oraz pobiera Id u¿ytkownika.
3. Warstwa serwisowa (np. `WorkoutService.GetUserWorkoutHistory`) wykonuje zapytanie do bazy danych wykorzystuj¹c EF Core, stosuj¹c filtry i paginacjê.
4. Wynik zwracany jest do widoku, który renderuje listê treningów.

## 6. Wzglêdy bezpieczeñstwa
- Endpoint dostêpny tylko dla zalogowanych u¿ytkowników.
- Dane s¹ pobierane z wykorzystaniem bie¿¹cego Id u¿ytkownika, co zapobiega nieautoryzowanemu dostêpowi do danych innych u¿ytkowników.
- Parametry filtrowania nale¿y walidowaæ pod k¹tem formatu dat oraz zakresu wartoœci.

## 7. Obs³uga b³êdów
- **401 Unauthorized:** Jeœli u¿ytkownik nie jest zalogowany.
- **400 Bad Request:** B³êdy zwi¹zane ze z³ymi parametrami (np. niepoprawny format dat).
- **500 Internal Server Error:** Przy niespodziewanych wyj¹tkach – b³êdy s¹ logowane przez ILogger i odpowiedni komunikat zwracany.

## 8. Etapy wdro¿enia
1. Implementacja metody serwisowej `WorkoutService.GetUserWorkoutHistory` wykorzystuj¹cej EF Core z paginacj¹ i filtrowaniem.
2. Utworzenie lub aktualizacja kontrolera/PageModel z akcj¹ GET dla `/Workouts/History`.
3. Stworzenie lub modyfikacja widoku prezentuj¹cego listê treningów.
4. Testy jednostkowe i integracyjne – weryfikacja poprawnoœci filtrowania, paginacji oraz autoryzacji.
5. Aktualizacja _Layout.cshtml: Dodanie przycisku/linku nawigacyjnego do listy treningów dla zalogowanych u¿ytkowników.