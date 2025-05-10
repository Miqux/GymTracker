# API Endpoint Implementation Plan: Usuwanie treningów

## 1. Przegl¹d punktu koñcowego
Endpoint s³u¿y do usuwania istniej¹cego treningu przez zalogowanego u¿ytkownika. Usuniêcie odbywa siê poprzez identyfikator treningu, a operacja powinna byæ dostêpna tylko dla u¿ytkownika, który jest w³aœcicielem danego treningu.

## 2. Szczegó³y ¿¹dania
- **Metoda HTTP:** DELETE (lub POST, jeœli ograniczenia przegl¹darki wymagaj¹ workaround)
- **Struktura URL:** /Workouts/Delete/{id}
- **Parametry:**
  - Wymagane: `id` (int) – identyfikator treningu, który ma zostaæ usuniêty.
- **Request Body:** Brak (opcjonalnie minimum payload jeœli metoda POST jest u¿ywana)

## 3. Wykorzystywane typy
- **Command Model:** `WorkoutDeleteCommand` (opcjonalnie) – zawieraj¹cy `Id` treningu.
- Opcjonalnie: DTO lub komunikat zwracany po operacji usuwania.

## 4. Szczegó³y odpowiedzi
- **200 OK:** Komunikat o pomyœlnym usuniêciu treningu (lub przekierowanie do listy treningów).
- **400 Bad Request:** W przypadku problemów z operacj¹ (np. trening nie nale¿y do u¿ytkownika).
- **401 Unauthorized:** Jeœli u¿ytkownik nie jest zalogowany.
- **404 Not Found:** Jeœli trening o podanym identyfikatorze nie istnieje.
- **500 Internal Server Error:** Przy b³êdach serwera.

## 5. Przep³yw danych
1. U¿ytkownik inicjuje ¿¹danie DELETE do `/Workouts/Delete/{id}` (np. poprzez przycisk "Usuñ" w widoku listy treningów).
2. Kontroler/PageModel sprawdza autoryzacjê i identyfikuje bie¿¹cego u¿ytkownika.
3. Warstwa serwisowa (np. `WorkoutService.DeleteWorkout`):
   - Weryfikuje, ¿e trening o danym `id` istnieje oraz nale¿y do bie¿¹cego u¿ytkownika.
   - Usuwa rekord treningu z bazy danych.
4. Po wykonaniu operacji u¿ytkownik otrzymuje komunikat o sukcesie lub odpowiedni b³¹d.

## 6. Wzglêdy bezpieczeñstwa
- Operacja dostêpna tylko dla autoryzowanych u¿ytkowników.
- Weryfikacja, czy trening nale¿y do bie¿¹cego u¿ytkownika, aby zapobiec usuwaniu cudzych rekordów.
- Walidacja przekazanego identyfikatora w URL.

## 7. Obs³uga b³êdów
- **400 Bad Request:** B³¹d walidacji lub brak zgodnoœci treningu z u¿ytkownikiem.
- **401 Unauthorized:** Jeœli operacja jest wywo³ywana przez niezalogowanego u¿ytkownika.
- **404 Not Found:** Jeœli trening o danym `id` nie istnieje.
- **500 Internal Server Error:** Niespodziewane wyj¹tki, logowanie b³êdów za pomoc¹ ILogger.

## 8. Etapy wdro¿enia
1. Utworzenie Command Modelu `WorkoutDeleteCommand` (opcjonalnie).
2. Implementacja akcji DELETE (lub POST, jeœli to konieczne) w kontrolerze lub PageModel dla `/Workouts/Delete/{id}`.
3. Rozbudowa serwisu – dodanie metody `WorkoutService.DeleteWorkout` ze wstêpn¹ weryfikacj¹ u¿ytkownika.
4. Aktualizacja widoku listy treningów:
   - Dodanie przycisku "Usuñ" przy ka¿dym treningu.
   - Potwierdzenie operacji usuwania (np. modal dialog).
5. Testy jednostkowe i integracyjne – weryfikacja, ¿e usuniêcie mo¿liwe jest tylko przez w³aœciciela oraz ¿e b³¹d jest odpowiednio obs³ugiwany.
6. Aktualizacja _Layout.cshtml: Upewnienie siê, ¿e przycisk/link do listy treningów jest widoczny tylko dla zalogowanych u¿ytkowników.