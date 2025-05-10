# Stos technologiczny dla Gym Tracker

Aplikacja Gym Tracker zosta�a stworzona jako prosta aplikacja webowa przy wykorzystaniu nast�puj�cych technologii:

1. Platforma:  
   - .NET 8

2. Framework webowy:  
   - ASP.NET Core Razor Pages  
     Projekt wykorzystuje podej�cie oparte na Razor Pages, kt�re umo�liwia efektywne zarz�dzanie stronami i cieszy si� prostot� implementacji, co jest idealne dla naszej aplikacji.
     **Uwaga:** W tym projekcie nie korzystamy z PageModeli. Zamiast tego preferujemy routing oparty o kontrolery i widoki (MVC), co u�atwia integracj� z istniej�cymi komponentami oraz utrzymanie sp�jno�ci w kodzie.

3. J�zyk programowania:  
   - C#

4. Uwierzytelnianie:  
   - Cookies  
     Aplikacja korzysta z mechanizmu Cookie Authentication do logowania i zarz�dzania sesjami u�ytkownik�w, dzi�ki czemu dost�p maj� wy��cznie autoryzowani u�ytkownicy.

5. Dost�p do danych:  
   - Entity Framework Core (EF Core)  
     EF Core s�u�y do obs�ugi operacji na bazie danych, umo�liwiaj�c �atw� walidacj� danych oraz propagacj� zmian w historii rekord�w.

6. Baza danych:  
   - Konfiguracja bazy danych (np. SQL Server, SQLite) jest elastyczna i mo�e by� dostosowana do potrzeb, wykorzystuj�c mo�liwo�ci EF Core.

Ten zestaw technologiczny zapewnia solidn�, bezpieczn� i �atw� w utrzymaniu podstaw� dla aplikacji Gym Tracker, umo�liwiaj�c jej skalowalno�� oraz szybki rozw�j.