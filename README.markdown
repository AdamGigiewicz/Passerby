# Passerby

## todo:
- znalezc algorytm hashujacy do przechowywania haseł
- jakie my mielismy to zadanie w koncu
- admin & user
  - zmiana hasla
  - przy pierwszym logowaniu musi zmienic haslo
- admin
  - zmiana nazwy uzytkownika
  - dodawanie nowych uzytkownikow
  - usuwanie uzytkownikow
  - ustawianie waznosci hasla
  - blokowanie modyfikacji konta
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
  - name
  - login
  - pass
  - resetDate date
  - resetPass bool
  - criteriaPass bool
