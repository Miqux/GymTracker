# Stos technologiczny dla Gym Tracker

Aplikacja Gym Tracker zosta³a stworzona jako prosta aplikacja webowa przy wykorzystaniu nastêpuj¹cych technologii:

1. Platforma:  
   - .NET 8

2. Framework webowy:  
   - ASP.NET Core Razor Pages  
     Projekt wykorzystuje podejœcie oparte na Razor Pages, które umo¿liwia efektywne zarz¹dzanie stronami i cieszy siê prostot¹ implementacji, co jest idealne dla naszej aplikacji.
     **Uwaga:** W tym projekcie nie korzystamy z PageModeli. Zamiast tego preferujemy routing oparty o kontrolery i widoki (MVC), co u³atwia integracjê z istniej¹cymi komponentami oraz utrzymanie spójnoœci w kodzie.

3. Jêzyk programowania:  
   - C#

4. Uwierzytelnianie:  
   - Cookies  
     Aplikacja korzysta z mechanizmu Cookie Authentication do logowania i zarz¹dzania sesjami u¿ytkowników, dziêki czemu dostêp maj¹ wy³¹cznie autoryzowani u¿ytkownicy.

5. Dostêp do danych:  
   - Entity Framework Core (EF Core)  
     EF Core s³u¿y do obs³ugi operacji na bazie danych, umo¿liwiaj¹c ³atw¹ walidacjê danych oraz propagacjê zmian w historii rekordów.

6. Baza danych:  
   - Konfiguracja bazy danych (np. SQL Server, SQLite) jest elastyczna i mo¿e byæ dostosowana do potrzeb, wykorzystuj¹c mo¿liwoœci EF Core.

Ten zestaw technologiczny zapewnia solidn¹, bezpieczn¹ i ³atw¹ w utrzymaniu podstawê dla aplikacji Gym Tracker, umo¿liwiaj¹c jej skalowalnoœæ oraz szybki rozwój.