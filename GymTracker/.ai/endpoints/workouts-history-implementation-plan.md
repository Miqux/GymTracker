# API Endpoint Implementation Plan: Przegl�danie historii trening�w

## 1. Przegl�d punktu ko�cowego
Celem tego endpointu jest wy�wietlenie listy trening�w zalogowanego u�ytkownika w trybie tylko do odczytu. Umo�liwia on filtrowanie oraz paginacj� wynik�w, dzi�ki czemu u�ytkownik mo�e przeszukiwa� histori� trening�w.

## 2. Szczeg�y ��dania
- **Metoda HTTP:** GET
- **Struktura URL:** /Workouts/History
- **Parametry:**
  - Opcjonalne: 
    - `page` (int): numer strony
    - `pageSize` (int): liczba rekord�w na stron�
    - `dateFrom` i `dateTo` (YYYY-MM-DD): zakres dat
- **Request Body:** Brak

## 3. Wykorzystywane typy
- **DTO:** `WorkoutHistoryDTO` (opcjonalnie) � zawiera w�a�ciwo�ci: Id, Date, Notes oraz dodatkowe informacje do paginacji.

## 4. Szczeg�y odpowiedzi
- **200 OK:** Widok prezentuj�cy list� trening�w (punktacja oraz paginacja wynik�w).
- **400 Bad Request:** W przypadku b��dnych parametr�w zapytania.
- **401 Unauthorized:** Gdy u�ytkownik nie jest zalogowany.
- **500 Internal Server Error:** Przy nieoczekiwanych b��dach.

## 5. Przep�yw danych
1. U�ytkownik zalogowany wysy�a ��danie GET do `/Workouts/History` (ewentualnie z parametrami filtrowania/paginacji).
2. Kontroler lub PageModel sprawdza autoryzacj� (User.Identity.IsAuthenticated) oraz pobiera Id u�ytkownika.
3. Warstwa serwisowa (np. `WorkoutService.GetUserWorkoutHistory`) wykonuje zapytanie do bazy danych wykorzystuj�c EF Core, stosuj�c filtry i paginacj�.
4. Wynik zwracany jest do widoku, kt�ry renderuje list� trening�w.

## 6. Wzgl�dy bezpiecze�stwa
- Endpoint dost�pny tylko dla zalogowanych u�ytkownik�w.
- Dane s� pobierane z wykorzystaniem bie��cego Id u�ytkownika, co zapobiega nieautoryzowanemu dost�powi do danych innych u�ytkownik�w.
- Parametry filtrowania nale�y walidowa� pod k�tem formatu dat oraz zakresu warto�ci.

## 7. Obs�uga b��d�w
- **401 Unauthorized:** Je�li u�ytkownik nie jest zalogowany.
- **400 Bad Request:** B��dy zwi�zane ze z�ymi parametrami (np. niepoprawny format dat).
- **500 Internal Server Error:** Przy niespodziewanych wyj�tkach � b��dy s� logowane przez ILogger i odpowiedni komunikat zwracany.

## 8. Etapy wdro�enia
1. Implementacja metody serwisowej `WorkoutService.GetUserWorkoutHistory` wykorzystuj�cej EF Core z paginacj� i filtrowaniem.
2. Utworzenie lub aktualizacja kontrolera/PageModel z akcj� GET dla `/Workouts/History`.
3. Stworzenie lub modyfikacja widoku prezentuj�cego list� trening�w.
4. Testy jednostkowe i integracyjne � weryfikacja poprawno�ci filtrowania, paginacji oraz autoryzacji.
5. Aktualizacja _Layout.cshtml: Dodanie przycisku/linku nawigacyjnego do listy trening�w dla zalogowanych u�ytkownik�w.