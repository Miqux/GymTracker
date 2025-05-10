# API Endpoint Implementation Plan: Dodawanie treningu

## 1. Przegl¹d punktu koñcowego
Endpoint umo¿liwia dodanie nowego treningu. Aplikacja wyœwietla formularz (GET) do wprowadzenia daty oraz listy æwiczeñ (z informacjami o seriach, powtórzeniach i opcjonalnie ciê¿arze) i przetwarza dane (POST) z zachowaniem walidacji, np. sprawdzaj¹c, czy æwiczenie nie jest zablokowane. Przejœcie na formularz mo¿liwe przez przycisk "Dodaj trening" na liœcie wszystki treningów. U¿ytkownik mo¿e wybraæ w³asne treningi które nie s¹ zablokowane.

## 2. Szczegó³y ¿¹dania
- **Metoda HTTP:**
  - GET – wyœwietlenie formularza dodawania treningu
  - POST – przes³anie danych formularza
- **Struktura URL:** /Workouts/Create
- **Parametry:**
  - GET: Brak
  - POST: 
    - Wymagane: `Date` (data treningu), `Exercises` (lista obiektów zawieraj¹cych: ExerciseId, Sets, Reps, opcjonalnie Weight)
- **Request Body:** Dane przes³ane z formularza (jako form-data lub JSON)

## 3. Wykorzystywane typy
- **Command Model/DTO:** `WorkoutCreateCommand` – zawiera w³aœciwoœci:
  - `Date` (DateTime)
  - `Exercises` (kolekcja typu np. `WorkoutExerciseDTO` zawieraj¹ca ExerciseId, Sets, Reps, Weight)

## 4. Szczegó³y odpowiedzi
- **200 OK:** Widok potwierdzaj¹cy, ¿e trening zosta³ dodany.
- **201 Created:** (alternatywnie, jeœli przyjmujemy RESTowe podejœcie)
- **400 Bad Request:** Jeœli wyst¹pi b³¹d walidacji (np. nieprawid³owa data, brak æwiczeñ, lub æwiczenie zablokowane).
- **401 Unauthorized:** Gdy u¿ytkownik nie jest zalogowany.
- **500 Internal Server Error:** Przy nieoczekiwanych b³êdach serwera.

## 5. Przep³yw danych
1. U¿ytkownik odwiedza `/Workouts/Create` (GET) – formularz jest renderowany.
2. U¿ytkownik wprowadza datê treningu oraz dodaje æwiczenia i zatwierdza formularz (POST).
3. Kontroler/PageModel odbiera dane i przekazuje je do warstwy serwisowej (np. `WorkoutService.CreateWorkout`).
4. Serwis sprawdza:
   - Czy data treningu jest poprawna.
   - Czy lista æwiczeñ nie jest pusta.
   - Czy ka¿de æwiczenie nie jest zablokowane (sprawdzanie flagi `IsBlocked`).
5. Jeœli walidacja przejdzie, dane s¹ zapisywane do bazy danych przez EF Core.
6. U¿ytkownik otrzymuje widok potwierdzaj¹cy dodanie treningu lub widok z komunikatem o b³êdzie.

## 6. Wzglêdy bezpieczeñstwa
- Formularz dodawania treningu dostêpny tylko dla u¿ytkowników zalogowanych.
- Walidacja danych wejœciowych po stronie serwera, aby zapobiec nieprawid³owym wartoœciom.
- Sprawdzenie, czy trening dotyczy bie¿¹cego u¿ytkownika (UserId potwierdzony przez sesjê).

## 7. Obs³uga b³êdów
- **400 Bad Request:** Wyœwietlanie b³êdów walidacji (np. brak æwiczeñ, data niezgodna z formatem).
- **401 Unauthorized:** Jeœli operacja jest wywo³ana przez niezalogowanego u¿ytkownika.
- **500 Internal Server Error:** Przy niespodziewanych sytuacjach – logowanie b³êdów przez ILogger.

## 8. Etapy wdro¿enia
1. Utworzenie Command Modelu `WorkoutCreateCommand` oraz ewentualnego DTO `WorkoutExerciseDTO`.
2. Implementacja akcji GET i POST w kontrolerze lub PageModel dla `/Workouts/Create`.
3. Dodanie logiki w serwisie (np. `WorkoutService.CreateWorkout`) odpowiedzialnej za walidacjê i zapis treningu.
4. Implementacja widoku formularza dodawania treningu.
5. Testy jednostkowe i integracyjne – sprawdzenie poprawnoœci danych, walidacji oraz zabezpieczeñ.
6. Aktualizacja _Layout.cshtml: Dodanie przycisku/linku do listy treningów dla u¿ytkowników zalogowanych.