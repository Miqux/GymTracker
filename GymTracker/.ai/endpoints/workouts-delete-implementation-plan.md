# API Endpoint Implementation Plan: Usuwanie trening�w

## 1. Przegl�d punktu ko�cowego
Endpoint s�u�y do usuwania istniej�cego treningu przez zalogowanego u�ytkownika. Usuni�cie odbywa si� poprzez identyfikator treningu, a operacja powinna by� dost�pna tylko dla u�ytkownika, kt�ry jest w�a�cicielem danego treningu.

## 2. Szczeg�y ��dania
- **Metoda HTTP:** DELETE (lub POST, je�li ograniczenia przegl�darki wymagaj� workaround)
- **Struktura URL:** /Workouts/Delete/{id}
- **Parametry:**
  - Wymagane: `id` (int) � identyfikator treningu, kt�ry ma zosta� usuni�ty.
- **Request Body:** Brak (opcjonalnie minimum payload je�li metoda POST jest u�ywana)

## 3. Wykorzystywane typy
- **Command Model:** `WorkoutDeleteCommand` (opcjonalnie) � zawieraj�cy `Id` treningu.
- Opcjonalnie: DTO lub komunikat zwracany po operacji usuwania.

## 4. Szczeg�y odpowiedzi
- **200 OK:** Komunikat o pomy�lnym usuni�ciu treningu (lub przekierowanie do listy trening�w).
- **400 Bad Request:** W przypadku problem�w z operacj� (np. trening nie nale�y do u�ytkownika).
- **401 Unauthorized:** Je�li u�ytkownik nie jest zalogowany.
- **404 Not Found:** Je�li trening o podanym identyfikatorze nie istnieje.
- **500 Internal Server Error:** Przy b��dach serwera.

## 5. Przep�yw danych
1. U�ytkownik inicjuje ��danie DELETE do `/Workouts/Delete/{id}` (np. poprzez przycisk "Usu�" w widoku listy trening�w).
2. Kontroler/PageModel sprawdza autoryzacj� i identyfikuje bie��cego u�ytkownika.
3. Warstwa serwisowa (np. `WorkoutService.DeleteWorkout`):
   - Weryfikuje, �e trening o danym `id` istnieje oraz nale�y do bie��cego u�ytkownika.
   - Usuwa rekord treningu z bazy danych.
4. Po wykonaniu operacji u�ytkownik otrzymuje komunikat o sukcesie lub odpowiedni b��d.

## 6. Wzgl�dy bezpiecze�stwa
- Operacja dost�pna tylko dla autoryzowanych u�ytkownik�w.
- Weryfikacja, czy trening nale�y do bie��cego u�ytkownika, aby zapobiec usuwaniu cudzych rekord�w.
- Walidacja przekazanego identyfikatora w URL.

## 7. Obs�uga b��d�w
- **400 Bad Request:** B��d walidacji lub brak zgodno�ci treningu z u�ytkownikiem.
- **401 Unauthorized:** Je�li operacja jest wywo�ywana przez niezalogowanego u�ytkownika.
- **404 Not Found:** Je�li trening o danym `id` nie istnieje.
- **500 Internal Server Error:** Niespodziewane wyj�tki, logowanie b��d�w za pomoc� ILogger.

## 8. Etapy wdro�enia
1. Utworzenie Command Modelu `WorkoutDeleteCommand` (opcjonalnie).
2. Implementacja akcji DELETE (lub POST, je�li to konieczne) w kontrolerze lub PageModel dla `/Workouts/Delete/{id}`.
3. Rozbudowa serwisu � dodanie metody `WorkoutService.DeleteWorkout` ze wst�pn� weryfikacj� u�ytkownika.
4. Aktualizacja widoku listy trening�w:
   - Dodanie przycisku "Usu�" przy ka�dym treningu.
   - Potwierdzenie operacji usuwania (np. modal dialog).
5. Testy jednostkowe i integracyjne � weryfikacja, �e usuni�cie mo�liwe jest tylko przez w�a�ciciela oraz �e b��d jest odpowiednio obs�ugiwany.
6. Aktualizacja _Layout.cshtml: Upewnienie si�, �e przycisk/link do listy trening�w jest widoczny tylko dla zalogowanych u�ytkownik�w.