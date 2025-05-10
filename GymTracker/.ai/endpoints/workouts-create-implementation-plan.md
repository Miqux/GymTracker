# API Endpoint Implementation Plan: Dodawanie treningu

## 1. Przegl�d punktu ko�cowego
Endpoint umo�liwia dodanie nowego treningu. Aplikacja wy�wietla formularz (GET) do wprowadzenia daty oraz listy �wicze� (z informacjami o seriach, powt�rzeniach i opcjonalnie ci�arze) i przetwarza dane (POST) z zachowaniem walidacji, np. sprawdzaj�c, czy �wiczenie nie jest zablokowane. Przej�cie na formularz mo�liwe przez przycisk "Dodaj trening" na li�cie wszystki trening�w. U�ytkownik mo�e wybra� w�asne treningi kt�re nie s� zablokowane.

## 2. Szczeg�y ��dania
- **Metoda HTTP:**
  - GET � wy�wietlenie formularza dodawania treningu
  - POST � przes�anie danych formularza
- **Struktura URL:** /Workouts/Create
- **Parametry:**
  - GET: Brak
  - POST: 
    - Wymagane: `Date` (data treningu), `Exercises` (lista obiekt�w zawieraj�cych: ExerciseId, Sets, Reps, opcjonalnie Weight)
- **Request Body:** Dane przes�ane z formularza (jako form-data lub JSON)

## 3. Wykorzystywane typy
- **Command Model/DTO:** `WorkoutCreateCommand` � zawiera w�a�ciwo�ci:
  - `Date` (DateTime)
  - `Exercises` (kolekcja typu np. `WorkoutExerciseDTO` zawieraj�ca ExerciseId, Sets, Reps, Weight)

## 4. Szczeg�y odpowiedzi
- **200 OK:** Widok potwierdzaj�cy, �e trening zosta� dodany.
- **201 Created:** (alternatywnie, je�li przyjmujemy RESTowe podej�cie)
- **400 Bad Request:** Je�li wyst�pi b��d walidacji (np. nieprawid�owa data, brak �wicze�, lub �wiczenie zablokowane).
- **401 Unauthorized:** Gdy u�ytkownik nie jest zalogowany.
- **500 Internal Server Error:** Przy nieoczekiwanych b��dach serwera.

## 5. Przep�yw danych
1. U�ytkownik odwiedza `/Workouts/Create` (GET) � formularz jest renderowany.
2. U�ytkownik wprowadza dat� treningu oraz dodaje �wiczenia i zatwierdza formularz (POST).
3. Kontroler/PageModel odbiera dane i przekazuje je do warstwy serwisowej (np. `WorkoutService.CreateWorkout`).
4. Serwis sprawdza:
   - Czy data treningu jest poprawna.
   - Czy lista �wicze� nie jest pusta.
   - Czy ka�de �wiczenie nie jest zablokowane (sprawdzanie flagi `IsBlocked`).
5. Je�li walidacja przejdzie, dane s� zapisywane do bazy danych przez EF Core.
6. U�ytkownik otrzymuje widok potwierdzaj�cy dodanie treningu lub widok z komunikatem o b��dzie.

## 6. Wzgl�dy bezpiecze�stwa
- Formularz dodawania treningu dost�pny tylko dla u�ytkownik�w zalogowanych.
- Walidacja danych wej�ciowych po stronie serwera, aby zapobiec nieprawid�owym warto�ciom.
- Sprawdzenie, czy trening dotyczy bie��cego u�ytkownika (UserId potwierdzony przez sesj�).

## 7. Obs�uga b��d�w
- **400 Bad Request:** Wy�wietlanie b��d�w walidacji (np. brak �wicze�, data niezgodna z formatem).
- **401 Unauthorized:** Je�li operacja jest wywo�ana przez niezalogowanego u�ytkownika.
- **500 Internal Server Error:** Przy niespodziewanych sytuacjach � logowanie b��d�w przez ILogger.

## 8. Etapy wdro�enia
1. Utworzenie Command Modelu `WorkoutCreateCommand` oraz ewentualnego DTO `WorkoutExerciseDTO`.
2. Implementacja akcji GET i POST w kontrolerze lub PageModel dla `/Workouts/Create`.
3. Dodanie logiki w serwisie (np. `WorkoutService.CreateWorkout`) odpowiedzialnej za walidacj� i zapis treningu.
4. Implementacja widoku formularza dodawania treningu.
5. Testy jednostkowe i integracyjne � sprawdzenie poprawno�ci danych, walidacji oraz zabezpiecze�.
6. Aktualizacja _Layout.cshtml: Dodanie przycisku/linku do listy trening�w dla u�ytkownik�w zalogowanych.