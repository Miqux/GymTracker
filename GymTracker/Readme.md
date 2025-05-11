# GymTracker

## Opis projektu
GymTracker to aplikacja internetowa umo�liwiaj�ca u�ytkownikom �ledzenie post�p�w treningowych, rejestrowanie wynik�w oraz zarz�dzanie planem treningowym. Projekt powsta� z my�l� o entuzjastach sportu, kt�rzy chc� monitorowa� swoj� kondycj� oraz rozw�j si�y.

## Stos technologiczny
- .NET 8
- ASP.NET Core Razor Pages
- C#
- (Dodatkowe technologie wed�ug potrzeb projektu)

## Testy
Projekt zawiera dwa g��wne rodzaje test�w:
1. **Testy jednostkowe (Unit Tests):**  
   Testy jednostkowe weryfikuj� poprawno�� dzia�ania poszczeg�lnych komponent�w aplikacji, takich jak logika biznesowa w serwisach (`AccountService`, `ExerciseService`, `WorkoutService`). Do ich implementacji wykorzystano frameworki takie jak MSTest, NUnit lub xUnit oraz narz�dzia do mockowania, np. Moq.

2. **Testy funkcjonalne (E2E - End-to-End):**  
   Testy E2E symuluj� rzeczywiste scenariusze u�ycia aplikacji z perspektywy u�ytkownika ko�cowego. Przyk�adowe scenariusze obejmuj� rejestracj�, logowanie, dodawanie �wicze� i trening�w oraz przegl�danie historii. Do ich realizacji mo�na u�ywa� narz�dzi takich jak Selenium WebDriver lub Playwright.

## Jak rozpocz�� prac� lokalnie
1. **Pobranie repozytorium**  
   Sklonuj projekt za pomoc�:
2. **Otw�rz projekt w Visual Studio**  
   Wybierz plik rozwi�zania (.sln) i otw�rz go w Visual Studio.
3. **Uruchomienie aplikacji**  
   U�yj wbudowanej funkcji debugowania lub uruchom aplikacj� poprzez terminal:
## Dost�pne skrypty
- **dotnet build**: Kompilacja projektu.
- **dotnet run**: Uruchomienie aplikacji.
- **dotnet test**: Uruchomienie test�w jednostkowych.
- (W razie potrzeby dodaj inne skrypty)

## Zakres projektu
- �ledzenie post�p�w treningowych u�ytkownik�w.
- Rejestracja trening�w i wynik�w.
- Zarz�dzanie planami treningowymi.
- Generowanie raport�w i statystyk.

## Status projektu
Projekt jest w fazie aktywnego rozwoju. Nowe funkcje oraz optymalizacje s� regularnie wdra�ane. Wszelkie uwagi oraz propozycje zmian s� mile widziane!

## Licencja

---
Dodatkowa dokumentacja oraz informacje projektowe mog� by� dost�pne w przysz�ych aktualizacjach tego pliku.