# GymTracker

## Opis projektu
GymTracker to aplikacja internetowa umo¿liwiaj¹ca u¿ytkownikom œledzenie postêpów treningowych, rejestrowanie wyników oraz zarz¹dzanie planem treningowym. Projekt powsta³ z myœl¹ o entuzjastach sportu, którzy chc¹ monitorowaæ swoj¹ kondycjê oraz rozwój si³y.

## Stos technologiczny
- .NET 8
- ASP.NET Core Razor Pages
- C#
- (Dodatkowe technologie wed³ug potrzeb projektu)

## Testy
Projekt zawiera dwa g³ówne rodzaje testów:
1. **Testy jednostkowe (Unit Tests):**  
   Testy jednostkowe weryfikuj¹ poprawnoœæ dzia³ania poszczególnych komponentów aplikacji, takich jak logika biznesowa w serwisach (`AccountService`, `ExerciseService`, `WorkoutService`). Do ich implementacji wykorzystano frameworki takie jak MSTest, NUnit lub xUnit oraz narzêdzia do mockowania, np. Moq.

2. **Testy funkcjonalne (E2E - End-to-End):**  
   Testy E2E symuluj¹ rzeczywiste scenariusze u¿ycia aplikacji z perspektywy u¿ytkownika koñcowego. Przyk³adowe scenariusze obejmuj¹ rejestracjê, logowanie, dodawanie æwiczeñ i treningów oraz przegl¹danie historii. Do ich realizacji mo¿na u¿ywaæ narzêdzi takich jak Selenium WebDriver lub Playwright.

## Jak rozpocz¹æ pracê lokalnie
1. **Pobranie repozytorium**  
   Sklonuj projekt za pomoc¹:
2. **Otwórz projekt w Visual Studio**  
   Wybierz plik rozwi¹zania (.sln) i otwórz go w Visual Studio.
3. **Uruchomienie aplikacji**  
   U¿yj wbudowanej funkcji debugowania lub uruchom aplikacjê poprzez terminal:
## Dostêpne skrypty
- **dotnet build**: Kompilacja projektu.
- **dotnet run**: Uruchomienie aplikacji.
- **dotnet test**: Uruchomienie testów jednostkowych.
- (W razie potrzeby dodaj inne skrypty)

## Zakres projektu
- Œledzenie postêpów treningowych u¿ytkowników.
- Rejestracja treningów i wyników.
- Zarz¹dzanie planami treningowymi.
- Generowanie raportów i statystyk.

## Status projektu
Projekt jest w fazie aktywnego rozwoju. Nowe funkcje oraz optymalizacje s¹ regularnie wdra¿ane. Wszelkie uwagi oraz propozycje zmian s¹ mile widziane!

## Licencja

---
Dodatkowa dokumentacja oraz informacje projektowe mog¹ byæ dostêpne w przysz³ych aktualizacjach tego pliku.