# Passerby

## todo:
- gr. 5 - 1 mala liter 1 znak specjalny 
- znalezc algorytm hashujacy do przechowywania haseł 
- admin & user
  - zmiana hasla
  - przy pierwszym logowaniu musi zmienic haslo
- admin
  - zmiana nazwy uzytkownika
  - dodawanie nowych uzytkownikow
  - usuwanie uzytkownikow
  - ustawianie waznosci hasla
  - blokowanie konta
  - wylaczanie/wlacznie kryterium hasel
## implementacja
- wykorzystac errory w api
- trzeba zrobic role
- wykorzystanie bootstrapa do ui
- endpointy
  - / strona glowna apki pozwala przejsc do zmiany hasla
  - /login
  - /users lista userow mozliwosc dodania usuniecia modyfikacji
  - /users/id modyfikacja usera
- baza
  - id
  - login
  - pass
  - role
  - blocked bool
  - resetDate date
  - criteriaPass bool

SETUP:
- docker compose up -d / docker compose down -v
- docker exec -it passerby-db-1
- /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P"Root_123" -i docker-entrypoint-initdb.d/init.sql
- /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P"Root_123"
